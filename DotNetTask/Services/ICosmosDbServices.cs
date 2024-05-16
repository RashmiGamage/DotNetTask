using DotNetTask.Models;

namespace DotNetTask.Services
{
    public interface ICosmosDbServices
    {
        Task<string> CreateEmployeeAsync(EmployeeModel employeeModel);
        Task<string> CreateQuestionAsync(QuestionModel questionModel);
        Task UpdateQuestionAsync(QuestionModel questionModel);
        Task<List<QuestionModel>> GetQuestionsAsync();
    }
}
