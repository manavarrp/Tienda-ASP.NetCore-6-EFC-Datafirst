namespace SALESSYSTEM.BLL.Implementation
{
    public class IgnoreSSL
    {
        private readonly HttpClient _httpClient;

        public IgnoreSSL(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("IgnoreSSL");
        }

        public async Task<string> GetHtmlFromUrl(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }

}
