using System.Threading.Tasks;

namespace MartinMcwhorter.Crawler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RunAsync().Wait();
        }
        static async Task RunAsync()
        {
            using (var htmlProcessor = new HtmlProcessor())
            {
                await htmlProcessor.Process();
            }
        }
    }
}
