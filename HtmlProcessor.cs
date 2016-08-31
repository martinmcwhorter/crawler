using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MartinMcwhorter.Crawler
{
    public class HtmlProcessor : IDisposable
    {
        private HttpClient _httpClient;

        private List<string> _processedPages = new List<string>();
        private List<string> _unProcessedPages = new List<string>();

        private const string BASE_URL = "http://wiprodigital.com";
 
        public HtmlProcessor() 
        {
            _httpClient = new HttpClient();
            _unProcessedPages.Add(BASE_URL);
        }

        public async Task<List<string>> Process() 
        {
            while(_unProcessedPages.Count > 0)
            {
                var path = _unProcessedPages.Last();
                _unProcessedPages.Remove(path);
                _processedPages.Add(path);

                Console.WriteLine("Page: {0}", path);

                var document = await GetPage(path);
                var links = ParseLinks(document);
                AddLinksToUnProcessed(links);
            }

            return _processedPages;
        }

        private void AddLinksToUnProcessed(List<string> links)
        {
            links.ForEach(x => 
            {
                if (!_unProcessedPages.Contains(x) && 
                        !_processedPages.Contains(x) &&
                        x.StartsWith(BASE_URL))
                    _unProcessedPages.Add(x);
            });
        }

        private async Task<string> GetPage(string path)
        {
            var page = await _httpClient.GetStringAsync(path);
            return page;
        }

        private List<string> ParseLinks(string page)
        {
            var links = new List<string>();

            var matches = Regex.Matches(page, @"(<a.*?>.*?</a>)", RegexOptions.Singleline);

            foreach (Match match in matches)
            {
                var anchor = match.Groups[1].Value;

                Match href = Regex.Match(anchor, @"href=\""(.*?)\""", RegexOptions.Singleline);
                
                if (href.Success)
                {
                    links.Add(href.Groups[1].Value);
                }
            }  

            return links;
        }

        public void Dispose()
        {
            ((IDisposable)_httpClient).Dispose();
        }
    }
}
