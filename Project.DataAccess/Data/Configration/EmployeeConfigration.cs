
using Project.DataAccess.Models.Shared.Enums;

namespace Project.DataAccess.Data.Configration
{
    public class EmployeeConfigration : BaseEntityConfigration<Employee>, IEntityTypeConfiguration<Employee>
    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)");
            builder.Property(E => E.Address).HasColumnType("varchar(150)");
            builder.Property(E => E.Salary).HasColumnType("decimal(10,2)");
            builder.Property(E => E.Gender).HasConversion((EmpGender) => EmpGender.ToString(),
                                                          (_gender) => (Gender)Enum.Parse(typeof(Gender), _gender));
            builder.Property(E => E.EmployeeType).HasConversion((EmpType) => EmpType.ToString(),
                                                          (_empType) => (EmployeeType)Enum.Parse(typeof(EmployeeType), _empType));
            base.Configure(builder);

        }
    }
}
