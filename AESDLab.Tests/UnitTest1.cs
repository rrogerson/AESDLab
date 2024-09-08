using Moq;


namespace AESDLab.Tests
{
    [TestClass]
    public class UnitTest1
    {
   
        [TestMethod]
        public async Task FetchUrlListReturnsFilteredUrls()
        {
            // Arrange
            var mockUrlList = new List<string>
            {
                "google.com/default",
                "microsoft.com",
                "bbc.co.uk/news/articles/xyz",
                "amazon.com"
            };

            var mockBlockedDomains = new List<string>
            {
                "bbc.co.uk",
                "linkedin.com",
                "amazon.com"
            };

            var mockService = new Mock<IUrlFilterService>();
            mockService.Setup(service => service.FetchUrlList()).ReturnsAsync(mockUrlList);
            mockService.Setup(service => service.FetchBlockedDomainsList()).ReturnsAsync(mockBlockedDomains);

            var urlFilterService = mockService.Object;

            // Act
            var result = await urlFilterService.FetchFilteredUrlList();

            // Assert
            var expectedUrls = new List<string>
            {
                "google.com/default",
                "microsoft.com"
            };

            CollectionAssert.AreEqual(expectedUrls, result.ToList());
        }
        
    }
}