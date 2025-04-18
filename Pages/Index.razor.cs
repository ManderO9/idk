namespace IDK.Pages;

public partial class Index
{
    public string ChatBotResponse { get; set; } = string.Empty;
    public string ChatBotQuery { get; set; } = string.Empty;

    public bool SendingRequest { get; set; } = false;

    public async Task SendRequestAsync()
    {
        if(SendingRequest)
            return;

        SendingRequest = true;
        ChatBotResponse = await aiService.SendRequestAsync(ChatBotQuery);
        SendingRequest = false;
    }
}