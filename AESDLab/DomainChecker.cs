using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AESDLab
{
    public class UrlFilterService : IUrlFilterService
    {
        public async Task<List<string>> FetchFilteredUrlList()
        {
            var urlList = await FetchUrlList();
            var blockedDomains = await FetchBlockedDomainsList();

            // Convert blocked domains to lowercase for case-insensitive comparison
            var blockedDomainSet = new HashSet<string>(blockedDomains.Select(domain => domain));

            List<string> filteredUrlList = FilterUrlsByDomains(urlList, blockedDomainSet);

            return filteredUrlList;
        }

        private List<string> FilterUrlsByDomains(List<string> urlList, HashSet<string> blockedDomainSet)
        {
            return urlList.Where(url =>
            {
                string domain = GetDomainFromUrl(url);
                return !blockedDomainSet.Any(d=> domain.Equals(d, StringComparison.OrdinalIgnoreCase));
            }).ToList();
        }
        
        private string GetDomainFromUrl(string url)
        {
            try
            {
                var uri = new Uri(url);
                string host = uri.Host;
                string[] domainParts = host.Split('.');
                if(domainParts.Length >= 2)
                {
                    return string.Join(".", domainParts.Skip(1));
                }
                return host;
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<List<string>> FetchUrlList()
        {
            return [
                "https://www.google.com/default",
                "https://www.microsoft.com",
                "https://www.bbc.co.uk/news/articles/xyz",
                "https://www.amazon.com"
            ];

        }
        public async Task<List<string>> FetchBlockedDomainsList()
        {

            return [
                "bbc.co.uk", "linkedin.com", "zon.com"
            ];

        }

    }

    public interface IUrlFilterService
    {
        Task<List<string>> FetchFilteredUrlList();
        Task<List<string>> FetchUrlList();

        Task<List<string>> FetchBlockedDomainsList();
    }
}
