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
    
    /* This can be used to use a local database file instead of connecting to one
     * SqlConnectionStringBuilder builder = new()
     * {
     *     DataSource = @"(localdb)\MSSQLLocalDB",
     *     AttachDBFilename = @"|DataDirectory|\NCRS-DB.mdf",
     *     IntegratedSecurity = true,
     *     TrustServerCertificate = true
     * };
     */

    public static async Task<string> InsertNewComplaintAsync(Complaint complaint)
    {
        try
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

            return (await SQLDatabaseManager.ExecuteScalarAsync(cmd)).ToString();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static async Task<int> UpdateComplaintAsync(Complaint complaintToUpdate)
    {
        try
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
        catch (Exception)
        {
            throw;
        }
    }

    private static async Task<List<Complaint>> RetrieveComplaintsAsync(SqlCommand procedure)
    {
        try
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
                    Issuer = await RetrieveTenantAsync(rd.GetGuid(1).ToString()),
                    ComplaintLocation = await RetrieveApartmentAsync(rd.GetInt32(2)),
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
        catch (Exception)
        {
            throw;
        }
    }

    public static async Task<List<Complaint>> RetrieveComplaintsAsync()
    {
        try
        {
            return await RetrieveComplaintsAsync(new("RETRIEVE_Complaints"));
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static async Task<Complaint> RetrieveComplaintAsync(string Id)
    {
        try
        {
            SQLDatabaseManager.SetConnectionString(builder);
            SqlCommand cmd = new("RETRIEVE_Complaint")
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("Id", Guid.Parse(Id));

            return (await RetrieveComplaintsAsync(cmd)).First();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static async Task<List<Complaint>> RetrieveComplaintsByNameAsync(Tenant issuer)
    {
        try
        {
            SqlCommand command = new("RETRIEVE_ComplaintsByName");
            command.Parameters.AddWithValue("FirstName", issuer.FirstName);
            command.Parameters.AddWithValue("LastName", issuer.LastName);

            return await RetrieveComplaintsAsync(command);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static async Task<List<Complaint>> RetrieveComplaintsByDateAsync(DateTime creationDate)
    {
        try
        {
            SqlCommand command = new("RETRIEVE_ComplaintsByDate");
            command.Parameters.AddWithValue("Date", creationDate);

            return await RetrieveComplaintsAsync(command);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static async Task<Tenant> RetrieveTenantAsync(string Id)
    {
        try
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
                Residence = await RetrieveApartmentAsync(rd.GetInt32(3))
            };

            await rd.CloseAsync();
            procedure.Connection.Close();
            return tenant;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static async Task<Apartment> RetrieveApartmentAsync(int apartmentNr)
    {
        try
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
                Building = await RetrieveBuildingAsync(apartmentNr)
            };

            await rd.CloseAsync();
            procedure.Connection.Close();
            return apartment;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static async Task<Building> RetrieveBuildingAsync(int buildingNr)
    {
        try
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
        catch (Exception)
        {
            throw;
        }
    }
}
