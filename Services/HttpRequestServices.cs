using System.Diagnostics;
using BlazorPostman.Data;

namespace BlazorPostman.Services
{
    public class HttpRequestService
    {
        private readonly HttpClient _httpClient;
        public HttpRequestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ResponseModel> SendRequestAsync(RequestModel request)
        {
            var response = new ResponseModel();
            try
            {
                var httpRequest = new HttpRequestMessage
                {
                    Method = new HttpMethod(request.Method),
                    RequestUri = new Uri(request.Url)
                };
                foreach(var header in request.Headers)
                {
                    if(!string.IsNullOrWhiteSpace(header.Key))
                        httpRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
                if(!string.IsNullOrWhiteSpace(request.Body) && request.Method != "GET")
                {
                    httpRequest.Content = new StringContent(
                        request.Body,
                        System.Text.Encoding.UTF8,
                        "application/json"
                    );
                }
                var stopwatch = Stopwatch.StartNew();
                var httpResponse = await _httpClient.SendAsync(httpRequest);
                stopwatch.Stop();
                response.StatusCode = (int)httpResponse.StatusCode;
                response.IsSuccess = httpResponse.IsSuccessStatusCode;
                response.ResponseTimeMs = stopwatch.ElapsedMilliseconds;
                response.Body = await httpResponse.Content.ReadAsStringAsync();
                foreach(var header in httpResponse.Headers)
                    response.Headers[header.Key] = string.Join(", ", header.Value.ToArray());

                foreach(var header in httpResponse.Content.Headers)
                    response.Headers[header.Key] = string.Join(", ", header.Value.ToArray());
            }
            catch(Exception ex)
            {
                response.StatusCode =0;
                response.IsSuccess = false;
                response.Body = $"Error: {ex.Message}";
            }
            return response;
        }
    }
    
}