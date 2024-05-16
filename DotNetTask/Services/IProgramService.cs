using DotNetTask.Models;

namespace DotNetTask.Services
{
    public interface IProgramService
    {
        Task<string> CreateQuestionAsync(QuestionModel questionModel);
        Task UpdateQuestionAsync(string id, QuestionModel questionDto);
        Task<List<QuestionModel>> GetQuestionsByTypeAsync(int questionType);
    }
}
