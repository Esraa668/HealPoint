using AutoMapper;
using Project.Bussiness.DataTransferObjects.EmployeeDtos;
using Project.Bussiness.Factories;
using Project.Bussiness.Services.AttachmentService;
using Project.Bussiness.Services.Interfaces;
using Project.DataAccess.Models.EmployeesModel;
using Project.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bussiness.Services.Classes
{
    public class EmployeeService(IUnitOfWork _unitOfWork
        , IMapper _mapper
        , IAttachmentService _attachmentService) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName)
        {

            //var employees = _employeeRepository.GetAll();
            //var employeesDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
            //return employeesDto;

            if (string.IsNullOrWhiteSpace(EmployeeSearchName))
            {
                var employees = _unitOfWork.EmployeeRepository.GetAll().Where(E => E.IsDeleted != true);
                return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
            }
            else
            {
                var employees = _unitOfWork.EmployeeRepository.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
                return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
            }

            //var employeesDto = _employeeRepository.GetAll(E => new EmployeeDto
            //{
            //    Id = E.Id,
            //    Name = E.Name,
            //    Salary = E.Salary,
            //    Age = E.Age,
            //}).Where(E => E.Age > 25);

            //return employees.Select(e => e.ToEmployeeDto());
        }
        public EmployeeDeatilsDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);

            return employee == null ? null : _mapper.Map<Employee, EmployeeDeatilsDto>(employee);
        }
        public int AddEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            if (employeeDto.ImageName is not null)
            {
                employee.ImageName = _attachmentService.Upload(employeeDto.ImageName, "Images");
            }
            _unitOfWork.EmployeeRepository.Add(employee); //Add locally
            return _unitOfWork.saveChanges(); //Save to database
        }
        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            _unitOfWork.EmployeeRepository.Update(_mapper.Map<UpdatedEmployeeDto, Employee>(employeeDto));
            var employee = _unitOfWork.EmployeeRepository.GetById(employeeDto.Id);
            return _unitOfWork.saveChanges(); // Save to database
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee == null)
                return false;
            else
            {
                employee.IsDeleted = true;
                _unitOfWork.EmployeeRepository.Update(employee);
                return _unitOfWork.saveChanges() > 0 ? true : false;
            }
        }

    }
}
