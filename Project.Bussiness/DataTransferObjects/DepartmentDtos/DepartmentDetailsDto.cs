using Project.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bussiness.DataTransferObjects.DepartmentDtos
{
    public class DepartmentDetailsDto
    {
        //public DepartmentDetailsDto(Department department)
        //{
        //    Id = department.Id;
        //    CreatedBy = department.CreatedBy;
        //    CreatedOn = DateOnly.FromDateTime(department.CreatedOn);
        //    LastModifiedBy = department.LastModifiedBy;
        //    LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn);
        //    IsDeleted = department.IsDeleted;
        //    Name = department.Name;
        //    Code = department.Code;
        //    Description = department.Description;
        //}
        public int Id { get; set; } //PK
        public int CreatedBy { get; set; } //User Id
        public DateOnly CreatedOn { get; set; }
        public int LastModifiedBy { get; set; } //User Id
        public DateOnly LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; } //Soft Delete
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }

    }
}
