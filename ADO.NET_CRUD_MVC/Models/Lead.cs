namespace ADO.NET_CRUD_MVC.Models;

public class Lead
{
    public int Id { get; set; }
    public DateTime LeadDate { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string LeadSource { get; set; }
    public string LeadStatus { get; set; }
    public DateTime NextFollowUpDate { get; set; }
}
