using System;
using System.Collections.Generic;
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
            var blockedDomainSet = new HashSet<string>(blockedDomains.Select(domain => domain.ToLower()));

            // Exclude URLs that contain any of the blocked domains
            var filteredUrls = urlList
                .Where(url => !blockedDomainSet.Any(blockedDomain => url.ToLower().StartsWith(blockedDomain)))
                .ToList();

            return filteredUrls;
        }


        public async Task<List<string>> FetchUrlList()
        {
            return [
                "google.com/default",
                "microsoft.com",
                "bbc.co.uk/news/articles/xyz",
                "amazon.com"
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
