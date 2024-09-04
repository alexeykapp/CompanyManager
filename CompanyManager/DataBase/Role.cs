using System;
using System.Collections.Generic;

namespace CompanyManager.Database;

public partial class Role
{
    public int IdRole { get; set; }

    public string? NameRole { get; set; }

    public virtual ICollection<EmployeeRole> EmployeeRoles { get; set; } = new List<EmployeeRole>();
}
