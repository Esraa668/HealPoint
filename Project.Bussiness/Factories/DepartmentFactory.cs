using Project.Bussiness.DataTransferObjects.DepartmentDtos;
using Project.DataAccess.Models.DepartmentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bussiness.Factories
{
    static class DepartmentFactory
    {

        public static DepartmentDto ToDepartmentDto(this Department department)
        {
            return new DepartmentDto
            {
                DeptId = department.Id,
                Code = department.Code,
                Description = department.Description ?? "",
                Name = department.Name,
                DateOfCreation = DateOnly.FromDateTime(department.CreatedOn)
            };
        }
        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto
            {
                Id = department.Id,
                CreatedBy = department.CreatedBy,
                CreatedOn = DateOnly.FromDateTime(department.CreatedOn),
                LastModifiedBy = department.LastModifiedBy,
                LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn),
                IsDeleted = department.IsDeleted,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description
            };
        }
        public static Department ToEntity(this CreatedDepartmentDto departmentDto)
        {
            return new Department
            {
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly()),
            };
        }
        public static Department ToEntity(this UpdatedDepartmentDto departmentDto) => new Department
        {
            Id = departmentDto.Id,
            Name = departmentDto.Name,
            Code = departmentDto.Code,
            Description = departmentDto.Description,
            CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly()),
        };

    }
}
