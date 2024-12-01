using ADO.NET_CRUD_MVC.Models;

namespace ADO.NET_CRUD_MVC.Data;

public interface ILeadRepository
{
    List<Lead> GetLeads();

    Lead GetLead(int id);

    bool AddLead(Lead lead);

    bool UpdateLead(int id, Lead lead);

    bool DeleteLead(int id);
}