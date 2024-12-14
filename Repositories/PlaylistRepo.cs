using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PlaylistRepo
    {
        private readonly string _connectionString;
        public PlaylistRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int AddPlayList(string playlistName)
        {
            string playlistPath = @"F:\UIT-K18\workspace\PlaylistManager\" + playlistName;
            int playlistID = 0 ;
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Playlists(playlistName, playlistPath)
                                OUTPUT INSERTED.playlistID
                                VALUES (@PlaylistName, @PlaylistPath)
                                ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlaylistName", playlistName);
                    cmd.Parameters.AddWithValue("@PlayListPath", playlistPath);
                    conn.Open();
                    playlistID = (int)cmd.ExecuteScalar();
                }
            }
            return playlistID;
        }

        public void RemovePlaylist(int playlistID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"DELETE FROM Playlists
                                WHERE playlistID = @PlaylistID
                                ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlaylistID", playlistID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<PlaylistModel> GetAllPlaylist()
        {
            var allPlaylists = new List<PlaylistModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT playlistID, playlistName, playlistPath FROM Playlists";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allPlaylists.Add(new PlaylistModel
                            {
                                playlistID = reader.GetInt32(0),
                                playlistName = reader.GetString(1),
                                playlistPath = reader.GetString(2) 
                            });
                        }
                    }
                }
            }
            return allPlaylists;
        }

    }
}
