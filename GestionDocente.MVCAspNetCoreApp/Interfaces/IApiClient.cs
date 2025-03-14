public interface IApiClient
{
    Task<TResponse> GetAsync<TResponse>(string endpoint);
    Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data);
    Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, TRequest data);
    Task DeleteAsync(string endpoint);
}
