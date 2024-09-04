using System;
using System.Collections.Generic;

namespace CompanyManager.Database;

public partial class EmployeeRole
{
    public int IdEmployeeRole { get; set; }

    public int? FkRole { get; set; }

    public int? FkEmployee { get; set; }

    public virtual Role? FkRole1 { get; set; }

    public virtual Employee? FkRoleNavigation { get; set; }
}
