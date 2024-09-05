namespace CompanyManager.Database;

public partial class PhotoEmployee
{
    public int IdPhotoEmployee { get; set; }

    public byte[]? PhotoEmployee1 { get; set; }

    public int? FkEmployee { get; set; }

    public virtual Employee? FkEmployeeNavigation { get; set; }
}
