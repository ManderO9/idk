using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace IDK;

public class AIService : IAIService
{
    #region Private Members

    private const string _url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent";
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _httpClient;

    #endregion

    #region Public Properties

    public string ApiKey
    {
        get => field;
        set
        {
            field = value;
            _localStorage.SetItem(nameof(ApiKey), field);
        }
    }

    #endregion

    #region Constructor

    public AIService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        ApiKey = _localStorage.GetItem<string>(nameof(ApiKey)) ?? string.Empty;
    }

    #endregion

    public async Task<string> SendRequestAsync(string query)
    {
        var request = new ApiRequest()
        {
            Contents = [
                new()
                {
                    Parts = [
                        new()
                        {
                            Text = query
                        }
                    ]
                }
            ]
        };

        var response = await _httpClient.PostAsJsonAsync(_url + $"?key={ApiKey}", request);

        var body = await response.Content.ReadFromJsonAsync<ApiResponse>();
        
        return body?.Candidates.FirstOrDefault()?.Content.Parts.FirstOrDefault()?.Text ?? string.Empty;
    }
}

public class ApiRequest
{
    public List<Content> Contents { get; set; } = [];
    public class Content
    {
        public List<Part> Parts { get; set; } = [];
        public class Part
        {
            public string Text { get; set; } = string.Empty;
        }
    }
}

public class ApiResponse
{
    public List<Candidate> Candidates { get; set; } = [];
    public class Candidate
    {
        public ContentT Content { get; set; } = new();
        public class ContentT
        {
            public List<Part> Parts { get; set; } = [];
            public class Part
            {
                public string Text { get; set; } = string.Empty;
            }
        }
    }
}