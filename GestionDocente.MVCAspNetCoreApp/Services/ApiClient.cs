namespace GestionDocente.MVCAspNetCoreApp.Services
{
    public class ApiClient : IApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ApiClient> _logger;

    public ApiClient(HttpClient httpClient, ILogger<ApiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public Task DeleteAsync(string endpoint)
    {
        throw new NotImplementedException();
    }

    public async Task<TResponse> GetAsync<TResponse>(string endpoint)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<TResponse>(endpoint);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling GET {Endpoint}", endpoint);
            throw;
        }
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling POST {Endpoint}", endpoint);
            throw;
        }
    }

    public Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, TRequest data)
    {
        throw new NotImplementedException();
    }

    // Implementar otros métodos...
}

}

