namespace DotNetTask.Models
{
    public class QuestionModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
    }

    public enum QuestionType
    {
        Paragraph,
        YesNo,
        Dropdown,
        MultipleChoice,
        Date,
        Number
    }
}
