using Project.Bussiness.DataTransferObjects.DepartmentDtos;
using Project.Bussiness.Factories;
using Project.Bussiness.Services.Interfaces;
using Project.DataAccess.Repositories.Interfaces;

namespace Project.Bussiness.Services.Classes
{
    public class DepartmentService(IUnitOfWork _unitOfWork) : IDepartmentService
    {
        //Get All Departments
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return departments.Select(d => d.ToDepartmentDto());
        }

        //Get Department By Id
        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            //AutoMapper can be used for mapping

            //Manual mapping
            //return department is null ? null : new DepartmentDetailsDto
            //{
            //    Id = department.Id,
            //    Code = department.Code,
            //    Description = department.Description,
            //    Name = department.Name,
            //    CreatedBy = department.CreatedBy,
            //    CreatedOn = DateOnly.FromDateTime(department.CreatedOn),
            //    LastModifiedBy = department.LastModifiedBy,
            //    LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn) ,
            //    IsDeleted = department.IsDeleted
            //};

            //Constructor mapping
            //return department is null ? null : new DepartmentDetailsDto(department);

            //Extension method mapping
            return department == null ? null : department.ToDepartmentDetailsDto();
        }

        //Create new Department
        public int CreateDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = departmentDto.ToEntity();
            _unitOfWork.DepartmentRepository.Add(department);
            return _unitOfWork.saveChanges(); //Save to database

        }

        //Update Department
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            _unitOfWork.DepartmentRepository.Update(departmentDto.ToEntity());
            return _unitOfWork.saveChanges(); //Save to database

        }

        //Delete Department
        public bool DeleteDepartment(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department == null) return false;
            else
            {
                _unitOfWork.DepartmentRepository.Delete(department);
                int result = _unitOfWork.saveChanges(); //Save to database
                return result > 0 ? true : false;
            }
        }

    }
}
