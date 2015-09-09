using System;

namespace Acme.Helpers.Website
{
    public class AppSettings
    {
        public string Repository { get; set; }
        public string Version { get; set; }
        public int PagerPageSize { get; set; }
        public int InfinitePageSize { get; set; }

        public string GithubSource { get { return $"http://github.com/{Repository}"; } }
        public string GithubDownload { get { return $"http://github.com/{Repository}/zipball/master"; } }
    }
}
