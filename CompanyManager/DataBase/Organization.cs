namespace CompanyManager.Database;

public partial class Organization
{
    public int IdOrganization { get; set; }

    public string? NameOrganization { get; set; }

    public string? AddressOrganization { get; set; }

    public string? PhoneOrganization { get; set; }

    public string? MailOrganization { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
