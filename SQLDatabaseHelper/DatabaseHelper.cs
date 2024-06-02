using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;

namespace DatabaseHelper;

public static class SQLDatabaseManager
{
    private static SqlConnectionStringBuilder _builder = new()
    {
        DataSource = @"localhost",
        InitialCatalog = "ProductCatalogExample",
        UserID = "ProductCatalogExampleUser",
        Password = "test123",
        IntegratedSecurity = true,
        TrustServerCertificate = true,
        ConnectTimeout = 10
    };

    public static void SetConnectionString(SqlConnectionStringBuilder builder)
    {
        _builder = builder;
    }

    public static void SetConnectionString(string connectionString)
    {
        _builder.ConnectionString = connectionString;
    }
    
    public async static Task<object> ExecuteScalarAsync(SqlCommand command)
    {
        try
        {
            using SqlConnection connection = new(_builder.ConnectionString);
            command.Connection = connection;
            await command.Connection.OpenAsync();
            return await command.ExecuteScalarAsync();
        }
        // These catch statements are listed this way on learn.microsoft.com
        // I don't have the exact link at the moment
        catch (SqlException ex)
        {
            string errorMessages = "";
            for (int i = 0; i < ex.Errors.Count; i++)
            {
                errorMessages +=
                    "Index #" + i + "\n" +
                    "Message: " + ex.Errors[i].Message + "\n" +
                    "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                    "Source: " + ex.Errors[i].Source + "\n" +
                    "Procedure: " + ex.Errors[i].Procedure + "\n";
            }
            Console.WriteLine(errorMessages.ToString());
            return ex;
        }
    }

    public async static Task<int> ExecuteNonQueryAsync(SqlCommand command)
    {
        try
        {
            using SqlConnection connection = new(_builder.ConnectionString);
            command.Connection = connection;
            await command.Connection.OpenAsync();
            return await command.ExecuteNonQueryAsync();
        }
        catch (SqlException ex)
        {
            string errorMessages = "";
            for (int i = 0; i < ex.Errors.Count; i++)
            {
                errorMessages +=
                    "Index #" + i + "\n" +
                    "Message: " + ex.Errors[i].Message + "\n" +
                    "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                    "Source: " + ex.Errors[i].Source + "\n" +
                    "Procedure: " + ex.Errors[i].Procedure + "\n";
            }
            Console.WriteLine(errorMessages.ToString());
            return -1;
        }
    }

    public async static Task<SqlDataReader> ExecuteReaderAsync(SqlCommand command)
    {
        // I don't like the way I did this by passing the reader
        // I am leaving the connection dangling
        // and have it be closed by the caller
        //
        // I may want to change the function to retrieve the complete table
        // and then return a DataSet instead
        try
        {
            SqlConnection connection = new(_builder.ConnectionString);
            command.Connection = connection;
            await command.Connection.OpenAsync();
            return await command.ExecuteReaderAsync();
        }
        catch (SqlException ex)
        {
            string errorMessages = "";
            for (int i = 0; i < ex.Errors.Count; i++)
            {
                errorMessages +=
                    "Index #" + i + "\n" +
                    "Message: " + ex.Errors[i].Message + "\n" +
                    "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                    "Source: " + ex.Errors[i].Source + "\n" +
                    "Procedure: " + ex.Errors[i].Procedure + "\n";
            }
            Console.WriteLine(errorMessages.ToString());
            await command.Connection.CloseAsync();
            return null;
        }
    }

    public async static Task ExportTableToCSV(SqlCommand retrieveCommand, string directoryPath)
    {
        try
        {
            using SqlDataReader reader = await ExecuteReaderAsync(retrieveCommand);

            if (reader == null || !reader.HasRows)
            {
                return;
            }

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using StreamWriter writer = new(directoryPath + $"\\{DateTime.Today:yy-mm-dd}.csv");
            using CsvHelper.CsvWriter csvWriter = new(writer, CultureInfo.InvariantCulture);

            ReadOnlyCollection<DbColumn> dbColumns = await reader.GetColumnSchemaAsync();
            foreach (DbColumn dbColumn in dbColumns)
            {
                csvWriter.WriteField(dbColumn.ColumnName);
            }
            await csvWriter.NextRecordAsync();

            while (await reader.ReadAsync())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    csvWriter.WriteField(reader.GetValue(i));
                }
                await csvWriter.NextRecordAsync();
            }
        }
        catch (SqlException ex)
        {
            string errorMessages = "";
            for (int i = 0; i < ex.Errors.Count; i++)
            {
                errorMessages +=
                    "Index #" + i + "\n" +
                    "Message: " + ex.Errors[i].Message + "\n" +
                    "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                    "Source: " + ex.Errors[i].Source + "\n" +
                    "Procedure: " + ex.Errors[i].Procedure + "\n";
            }
            Console.WriteLine(errorMessages.ToString());
            await retrieveCommand.Connection.CloseAsync();
        }
    }
}
