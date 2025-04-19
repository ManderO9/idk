using System.Text.Json;

namespace IDK.Pages;

public partial class Trivia
{

    public QuestionDetails Question { get; set; } = new QuestionDetails();
    public bool SendingRequest { get; set; } = false;
    public bool OptionSelected { get; set; } = false;


    public async Task NewQuestionAsync()
    {
        if(SendingRequest)
            return;

        SendingRequest = true;
        OptionSelected = false;

        var prompt = $"""
            You are a trivia game master. Your task is to create a trivia question with 4 options and one correct answer.
            
            The question should be engaging and interesting, they should be about general culture that anyone who is 20 years old or older should know. 
            The question should be for a global audience, so avoid questions that only United States citizens would know.
            Don't make the question too easy or too hard, it should be a good challenge for the player.
            The options should be plausible but only one of them should be correct.
            Please provide the question, the options, and the correct answer in a structured format.


            Do not include any additional text or explanation in your answer, only include the structured question data.
            The format of the data is the following json:
            {JsonSerializer.Serialize(new QuestionDetails())}
            """;

        try
        {
            var response = await aiService.SendRequestAsync(prompt);
            Question = JsonSerializer.Deserialize<QuestionDetails>(response.TrimStart("```json").TrimEnd("```").Trim())!;
        }
        catch(Exception ex)
        {
            Question.Question = "[Error response] " + ex.Message;
        }

        SendingRequest = false;
    }

    protected override async Task OnInitializedAsync()
    {
        await NewQuestionAsync();
    }

    public class QuestionDetails
    {
        public string Question { get; set; } = string.Empty;
        public List<string> Options { get; set; } = [];
        public string CorrectAnswer { get; set; } = string.Empty;
    }

}