namespace CompanyManager.Database;

public partial class EmployeeRole
{
    public int IdEmployeeRole { get; set; }

    public int? FkRole { get; set; }

    public int? FkEmployee { get; set; }

    public virtual Employee? FkEmployeeNavigation { get; set; }

    public virtual Role? FkRoleNavigation { get; set; }
}
