A quick and dirty web crawler.

# Getting Started

1. Install DotNet Core 

    https://www.microsoft.com/net/core

2. Clone this repository
3. Open a command line terminal and change directory to the path of the cloned repo and enter the following commands:
```
    dotnet restore
    dotnet run
```

In your console you should see a list of URLs being crawled. 

This is a traditional crawler. It does will not crawl client generated links rendered by JavaScript in the browser. 

After waisting time trying to get XDocument to parse real world HTML (it doesn't like a lot of charecters that are common in HTML) and then trying to get HTMLAgilityPack working with DotNet Core (not quite ready), I settled on using Regex for parsing. 

This simple parser only parses simple achor tags, ie: ```<a href="http://....">Title</a>```. This means that parsing of: link, script, img, title -- is not supported. That said adding support for these would be trivial. 

The regexp parsers would be moved to its own classs or classes. It might make sence to use the Visitor Pattern to be able to easily add new parsers without opening closed classes. 

# What else is missing

1. Does not handle subdomains
2. Does not handle relative paths
3. Should capture the title of pages (would neeed a parser like mentioned above)
4. Should capture the link text of anchor tags, alt of img tags
5. Only handles 200 responses, should handle local redirects (301 and 302)
6. Unit Tests are missing
7. Does not handle ```?_escaped_fragment_=``` prerendered JavaScript pages
8. 
