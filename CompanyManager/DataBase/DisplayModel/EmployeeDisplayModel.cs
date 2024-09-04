﻿namespace CompanyManager.DataBase.DisplayModel
{
    public class EmployeeDisplayModel
    {
        public int IdEmployee { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Phone { get; set; }
        public string? Passport { get; set; }
        public string? Address { get; set; }
        public string? RoleName { get; set; }
    }
}
