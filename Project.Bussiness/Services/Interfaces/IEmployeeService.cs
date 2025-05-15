using Project.Bussiness.DataTransferObjects.EmployeeDtos;

namespace Project.Bussiness.Services.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName);
        EmployeeDeatilsDto? GetEmployeeById(int id);
        int AddEmployee(CreatedEmployeeDto employeeDto);
        int UpdateEmployee(UpdatedEmployeeDto employeeDto);
        bool DeleteEmployee(int id);

    }
}
