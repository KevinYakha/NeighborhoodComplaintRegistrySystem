using System.Data.SqlClient;

using DatabaseHelper;

using NCRS_API.Data;

namespace NCRS_API;

public static class NCRS_DB
{
    private static readonly SqlConnectionStringBuilder builder = new()
    {
        DataSource = @"WIN-5EEDLGC0U7J",
        InitialCatalog = "NCRS-DB",
        IntegratedSecurity = true,
        Pooling = false,
        ConnectTimeout = 10,
        Encrypt = true,
        TrustServerCertificate = true
    };

    public static async Task<int> InsertNewComplaint(Complaint complaint)
    {
        SQLDatabaseManager.SetConnectionString(builder);
        SqlCommand cmd = new("INSERT_NewComplaint")
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };

        SqlParameter[] parameters =
        [
            new("Issuer", complaint.Issuer.Id),
            new("Location", complaint.ComplaintLocation.ApartmentNr),
            new("Description", complaint.Description),
            new("Status", (int)complaint.Status),
            new("Category", (int)complaint.Category)
        ];
        cmd.Parameters.AddRange(parameters);

        return await SQLDatabaseManager.ExecuteNonQueryAsync(cmd);
    }

    public static async Task<int> UpdateComplaint(Complaint complaintToUpdate)
    {
        SQLDatabaseManager.SetConnectionString(builder);
        SqlCommand cmd = new("UPDATE_Complaint")
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };

        cmd.Parameters.AddWithValue("Id", complaintToUpdate.Id);
        cmd.Parameters.AddWithValue("Description", complaintToUpdate.Description);
        cmd.Parameters.AddWithValue("Status", (int)complaintToUpdate.Status);

        return await SQLDatabaseManager.ExecuteNonQueryAsync(cmd);
    }

    public static async Task<List<Complaint>> RetrieveComplaints()
    {
        return await RetrieveComplaints(new("RETRIEVE_Complaints"));
    }

    public static async Task<Complaint> RetrieveComplaint(string Id)
    {
        SQLDatabaseManager.SetConnectionString(builder);
        SqlCommand cmd = new("RETRIEVE_Complaint")
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };

        cmd.Parameters.AddWithValue("Id", Guid.Parse(Id));

        return (await RetrieveComplaints(cmd)).First();
    }

    private static async Task<List<Complaint>> RetrieveComplaints(SqlCommand procedure)
    {
        SQLDatabaseManager.SetConnectionString(builder);
        procedure.CommandType = System.Data.CommandType.StoredProcedure;

        using SqlDataReader rd = await SQLDatabaseManager.ExecuteReaderAsync(procedure);

        if (rd == null || !rd.HasRows)
        {
            return null;
        }

        List<Complaint> results = [];

        while (await rd.ReadAsync())
        {
            Complaint singleResult = new()
            {
                Id = rd.GetGuid(0).ToString(),
                Issuer = await RetrieveTenant(rd.GetGuid(1).ToString()),
                ComplaintLocation = await RetrieveApartment(rd.GetInt32(2)),
                CreationDate = rd.GetDateTime(3),
                Description = rd.GetString(4),
                Status = (Complaint.ComplaintStatus)rd.GetInt32(5),
                Category = (Complaint.ComplaintCategory)rd.GetInt32(6),
            };

            results.Add(singleResult);
        }

        procedure.Connection.Close();
        return results;
    }

    public static async Task<List<Complaint>> RetrieveComplaintsByName(Tenant issuer)
    {
        SqlCommand command = new("RETRIEVE_ComplaintsByName");
        command.Parameters.AddWithValue("FirstName", issuer.FirstName);
        command.Parameters.AddWithValue("LastName", issuer.LastName);

        return await RetrieveComplaints(command);
    }

    public static async Task<List<Complaint>> RetrieveComplaintsByDate(DateTime creationDate)
    {
        SqlCommand command = new("RETRIEVE_ComplaintsByDate");
        command.Parameters.AddWithValue("Date", creationDate);

        return await RetrieveComplaints(command);
    }

    public static async Task<Tenant> RetrieveTenant(string Id)
    {
        SQLDatabaseManager.SetConnectionString(builder);
        SqlCommand procedure = new("RETRIEVE_Tenant")
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };

        procedure.Parameters.AddWithValue("Id", Id);

        using SqlDataReader rd = await SQLDatabaseManager.ExecuteReaderAsync(procedure);

        if (rd == null || !rd.HasRows)
        {
            return new();
        }

        rd.ReadAsync();

        Tenant tenant = new()
        {
            Id = rd.GetValue(0).ToString(),
            FirstName = rd.GetString(1),
            LastName = rd.GetString(2),
            Residence = await RetrieveApartment(rd.GetInt32(3))
        };

        await rd.CloseAsync();
        procedure.Connection.Close();
        return tenant;
    }

    public static async Task<Apartment> RetrieveApartment(int apartmentNr)
    {
        SQLDatabaseManager.SetConnectionString(builder);
        SqlCommand procedure = new("RETRIEVE_Apartment")
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };

        procedure.Parameters.AddWithValue("Nr", apartmentNr);

        using SqlDataReader rd = await SQLDatabaseManager.ExecuteReaderAsync(procedure);

        if (rd == null || !rd.HasRows)
        {
            return new();
        }

        rd.ReadAsync();

        Apartment apartment = new()
        {
            ApartmentNr = rd.GetInt32(0),
            Building = await RetrieveBuilding(apartmentNr)
        };

        await rd.CloseAsync();
        procedure.Connection.Close();
        return apartment;
    }

    public static async Task<Building> RetrieveBuilding(int buildingNr)
    {
        SQLDatabaseManager.SetConnectionString(builder);
        SqlCommand procedure = new("RETRIEVE_Building")
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };

        procedure.Parameters.AddWithValue("Nr", buildingNr);

        using SqlDataReader rd = await SQLDatabaseManager.ExecuteReaderAsync(procedure);

        if (rd == null || !rd.HasRows)
        {
            return new();
        }

        rd.ReadAsync();

        Building building = new()
        {
            BuildingNr = rd.GetInt32(0),
            Address = rd.GetString(1)
        };

        await rd.CloseAsync();
        procedure.Connection.Close();
        return building;
    }
}
