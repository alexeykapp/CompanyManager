﻿using System;
using System.Collections.Generic;

namespace CompanyManager.Database;

public partial class Employee
{
    public int IdEmployee { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Phone { get; set; }

    public string? Passport { get; set; }

    public string? Address { get; set; }

    public int? FkOrganization { get; set; }

    public virtual ICollection<EmployeeRole> EmployeeRoles { get; set; } = new List<EmployeeRole>();

    public virtual Organization? FkOrganizationNavigation { get; set; }

    public virtual ICollection<PhotoEmployee> PhotoEmployees { get; set; } = new List<PhotoEmployee>();
}
