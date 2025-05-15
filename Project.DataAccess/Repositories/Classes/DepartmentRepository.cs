using Project.DataAccess.Data.Contexts;
using Project.DataAccess.Models.DepartmentModel;
using Project.DataAccess.Repositories.Interfaces;

namespace Project.DataAccess.Repositories.Classes
{
    public class DepartmentRepository(ApplicationDbContext dbContext) :GenericRepository<Department>(dbContext) ,IDepartmentRepository
    {

    }
}
