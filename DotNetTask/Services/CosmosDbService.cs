using DotNetTask.Models;
using Microsoft.Azure.Cosmos;

namespace DotNetTask.Services
{
    public class CosmosDbService : ICosmosDbServices
    {
        private readonly Container _employeesContainer;
        private readonly Container _questionsContainer;

        public CosmosDbService(Container employeeClient, Container questionClient)
        {
            this._employeesContainer = employeeClient;
            this._questionsContainer = questionClient;
        }

        public async Task<string> CreateEmployeeAsync(EmployeeModel employeeModel)
        {
            string Id = Guid.NewGuid().ToString();
            var newEmployee = new
            {
                id = Id,
                firstName = employeeModel.FirstName,
                lastName = employeeModel.LastName,
                email = employeeModel.Email,
                phone = employeeModel.Phone,
                nationality = employeeModel.Nationality,
                residence = employeeModel.Residence,
                employeeId = employeeModel.EmployeeId,
                birthday = employeeModel.Birthday,
                gender = employeeModel.Gender,

            };
            await _employeesContainer.CreateItemAsync(newEmployee, new PartitionKey(newEmployee.employeeId));
            return Id;
        }

        public async Task<string> CreateQuestionAsync(QuestionModel questionModel)
        {
            string questionId = Guid.NewGuid().ToString();
            var newItem = new
            {
                id = questionId,
                text = questionModel.Text,
                type = questionModel.Type
            };
            await _questionsContainer.CreateItemAsync(newItem, new PartitionKey(newItem.id));
            return questionId;
        }

        public async Task UpdateQuestionAsync(QuestionModel questionModel)
        {
            ItemResponse<QuestionModel> response = await _questionsContainer.ReadItemAsync<QuestionModel>(questionModel.Id, new PartitionKey(questionModel.Id));
            var updatedItem = new
            {
                id = questionModel.Id,
                text = questionModel.Text,
                type = questionModel.Type
            };
            await _questionsContainer.ReplaceItemAsync(updatedItem, updatedItem.id, new PartitionKey(updatedItem.id));
        }

        public async Task<List<QuestionModel>> GetQuestionsAsync()
        {
            try
            {
                var query = new QueryDefinition("SELECT * FROM c");
                var questions = new List<QuestionModel>();

                var iterator = _questionsContainer.GetItemQueryIterator<QuestionModel>(query);
                while (iterator.HasMoreResults)
                {
                    var response = await iterator.ReadNextAsync();
                    questions.AddRange(response);
                }
                return questions;
            }

            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

    }
}
