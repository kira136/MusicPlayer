using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using System.Data.SqlClient;
using Models;
using System.Data;

namespace Repositories
{
    public class FolderRepo
    {
        private readonly string _connectionString;
        public FolderRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int AddFolder(string folderName, string folderPath)
        {
            int folderID = 0 ;
            if(string.IsNullOrEmpty(folderName) || string.IsNullOrWhiteSpace(folderPath))
            {
                throw new ArgumentException("folderName, folderPath");
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    //Console.WriteLine("Database connection successful.");

                    string query = @"INSERT INTO Folders (folderPath, folderName) 
                         OUTPUT INSERTED.folderID
                         VALUES (@FolderPath, @FolderName)";

                    Console.WriteLine($"Query: {query}");
                    Console.WriteLine($"Parameters: FolderPath={folderPath}, FolderName={folderName}");

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FolderPath", folderPath);
                        cmd.Parameters.AddWithValue("@FolderName", folderName);

                        folderID = (int)cmd.ExecuteScalar();
                        Console.WriteLine($"Inserted FolderID: {folderID}");
                        return folderID;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                foreach (SqlError error in ex.Errors)
                {
                    Console.WriteLine($"Error Code: {error.Number}, Message: {error.Message}");
                }
                throw new ApplicationException("An error occurred while adding the folder to the database.", ex);
            }

        }

        public List<FolderItem> GetAllFolders()
        {
            List<FolderItem> folders = new List<FolderItem>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT folderName, folderPath FROM Folders";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        folders.Add(new FolderItem
                        {
                            FolderName = reader["folderName"].ToString(),
                            FolderPath = reader["folderPath"].ToString()
                        });
                    }
                }
            }
            return folders;
        }

        public void RemoveFolder(FolderItem selectedFolder)
        {
            var selectedPath = selectedFolder.FolderPath;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                
                string query = @"DELETE FROM Folders
                               WHERE folderPath = @FolderPath
                                ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("FolderPath", selectedPath);
                conn.Open();
                int row = cmd.ExecuteNonQuery();
            }
        }

    }
}
