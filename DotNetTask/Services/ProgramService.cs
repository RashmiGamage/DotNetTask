using DotNetTask.Models;

namespace DotNetTask.Services
{
    public class ProgramService : IProgramService
    {
        private readonly ICosmosDbServices _cosmosDbService;

        public ProgramService(ICosmosDbServices cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        public async Task<string> CreateQuestionAsync(QuestionModel questionDto)
        {
            var questionModel = new QuestionModel
            {
                Text = questionDto.Text,
                Type = questionDto.Type,
            };
            return await _cosmosDbService.CreateQuestionAsync(questionModel);
        }

        public async Task UpdateQuestionAsync(string id, QuestionModel questionDto)
        {
            var questionModel = new QuestionModel
            {
                Id = id,
                Text = questionDto.Text,
                Type = questionDto.Type
            };
            await _cosmosDbService.UpdateQuestionAsync(questionModel);
        }
        public async Task<List<QuestionModel>> GetQuestionsByTypeAsync(int questionType)
        {
            List<QuestionModel> questions = await _cosmosDbService.GetQuestionsAsync();
            List<QuestionModel> filteredQuestions = questions.Where(q => (int)q.Type == questionType).ToList();
            return filteredQuestions;
        }
    }
}
