namespace HttpRequestExample.ServiceContracts;

public interface IFinnhubService {
  public Task<Dictionary<string,object>?> GetMarketStatus();
}