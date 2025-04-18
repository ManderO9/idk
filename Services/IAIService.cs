
namespace IDK;

public interface IAIService
{
    string ApiKey { get; set; }

    Task<string> SendRequestAsync(string query);
}