using ADO.NET_CRUD_MVC.Factory;
using ADO.NET_CRUD_MVC.Models;
using System.Data;
using System.Data.SqlClient;

namespace ADO.NET_CRUD_MVC.Data;

public class LeadRepository(SqlConnectionFactory sqlConnectionFactory) : ILeadRepository
{
    public List<Lead> GetLeads()
    {
        var connection = sqlConnectionFactory.Create();

        var command = new SqlCommand("GetAllLeads", connection);

        command.CommandType = CommandType.StoredProcedure;

        var dataAdapter = new SqlDataAdapter(command);

        var leads = new DataTable();

        dataAdapter.Fill(leads);

        var leadsList = (from DataRow row in leads.Rows
                         select new Lead
                         {
                             Id = Convert.ToInt32(row["Id"]),
                             Name = row["Name"].ToString(),
                             Email = row["Email"].ToString(),
                             Phone = row["Phone"].ToString(),
                             LeadStatus = row["LeadStatus"].ToString(),
                             LeadSource = row["LeadSource"].ToString(),
                             LeadDate = Convert.ToDateTime(row["LeadDate"]),
                             NextFollowUpDate = Convert.ToDateTime(row["NextFollowUpDate"])
                         }).ToList();

        return leadsList;
    }

    public Lead GetLead(int id)
    {
        var connection = sqlConnectionFactory.Create();
        var command = new SqlCommand("GetLeadById", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@Id", id);

        var dataAdapter = new SqlDataAdapter(command);
        var dataTable = new DataTable();
        dataAdapter.Fill(dataTable);

        var lead = (from DataRow row in dataTable.Rows
                    select new Lead
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Name = row["Name"].ToString(),
                        Email = row["Email"].ToString(),
                        Phone = row["Phone"].ToString(),
                        LeadStatus = row["LeadStatus"].ToString(),
                        LeadSource = row["LeadSource"].ToString(),
                        LeadDate = Convert.ToDateTime(row["LeadDate"]),
                        NextFollowUpDate = Convert.ToDateTime(row["NextFollowUpDate"])
                    }).SingleOrDefault();

        return lead;
    }


    public bool AddLead(Lead lead)
    {
        using var connection = sqlConnectionFactory.Create();

        connection.Open();

        using var command = new SqlCommand("AddLead", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@LeadDate", lead.LeadDate);
        command.Parameters.AddWithValue("@Name", lead.Name);
        command.Parameters.AddWithValue("@Email", lead.Email);
        command.Parameters.AddWithValue("@Phone", lead.Phone);
        command.Parameters.AddWithValue("@LeadStatus", lead.LeadStatus);
        command.Parameters.AddWithValue("@LeadSource", lead.LeadSource);
        command.Parameters.AddWithValue("@NextFollowUpDate", lead.NextFollowUpDate);

        var check = command.ExecuteNonQuery();

        connection.Close();

        return check > 0;
    }

    public bool UpdateLead(int id, Lead lead)
    {
        using var connection = sqlConnectionFactory.Create();

        connection.Open();

        using var command = new SqlCommand("UpdateLead", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@Id", id);
        command.Parameters.AddWithValue("@LeadDate", lead.LeadDate);
        command.Parameters.AddWithValue("@Name", lead.Name);
        command.Parameters.AddWithValue("@Email", lead.Email);
        command.Parameters.AddWithValue("@Phone", lead.Phone);
        command.Parameters.AddWithValue("@LeadStatus", lead.LeadStatus);
        command.Parameters.AddWithValue("@LeadSource", lead.LeadSource);
        command.Parameters.AddWithValue("@NextFollowUpDate", lead.NextFollowUpDate);

        var check = command.ExecuteNonQuery();

        connection.Close();

        return check > 0;
    }

    public bool DeleteLead(int id)
    {
        var connection = sqlConnectionFactory.Create();
        var command = new SqlCommand("DeleteLead", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@Id", id);

        connection.Open();

        var check = command.ExecuteNonQuery();

        connection.Close();

        return check > 0;
    }
}