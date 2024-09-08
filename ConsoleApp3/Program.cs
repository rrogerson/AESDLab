using AESDLab;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IUrlFilterService, UrlFilterService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var urlFilterService = serviceProvider.GetRequiredService<IUrlFilterService>();

            var filteredUrls = await urlFilterService.FetchFilteredUrlList();

        }

       
    }
}
