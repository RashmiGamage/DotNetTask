using DotNetTask.Models;

namespace DotNetTask.Services
{
    public interface ICosmosDbServices
    {
        Task<string> CreateEmployeeAsync(EmployeeModel employeeModel);
    }
}
