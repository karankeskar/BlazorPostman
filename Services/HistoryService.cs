using BlazorPostman.Data;
using BlazorPostman.Extensions;
using Microsoft.Extensions.Caching.Distributed;

namespace BlazorPostman.Services
{
    public class HistoryService
    {
        private readonly IDistributedCache _cache;
        private const string HistoryKey = "request_history";
        public HistoryService(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task SaveAsync(RequestModel request)
        {
            var history = await GetAllAsync();
            history.Insert(0, request);
            if(history.Count > 50)
                history = history.Take(50).ToList();
            await _cache.SetRecordAsync(HistoryKey, history, TimeSpan.FromDays(7));
        }
        public async Task<List<RequestModel>> GetAllAsync()
        {
            return await _cache.GetRecordAsync<List<RequestModel>>(HistoryKey) ?? new();
        }
    }
    
}