using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class SongRepo
    {
        private readonly string _connectionString;
        public SongRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void SaveSongToDatabase(SongModel song)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var checkingQuery = "SELECT COUNT(*) FROM Songs WHERE songPath = @songPath";
                using (var checkCommand = new SqlCommand(checkingQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@songPath", song.songPath);
                    int count = (int)checkCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        return;
                    }
                }
                var query = "INSERT INTO Songs (songName, songPath, songSize, lastAccessDate, songDuration) VALUES (@songName, @songPath, @songSize, @lastAccessDate, @songDuration)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@songName", song.songName);
                    command.Parameters.AddWithValue("@songPath", song.songPath);
                    command.Parameters.AddWithValue("@songSize", song.songSize);
                    command.Parameters.AddWithValue("@lastAccessDate", song.lastAccessDate);
                    command.Parameters.AddWithValue("@songDuration", song.songDuration);

                    command.ExecuteNonQuery();
                }
            }
        }
        public List<SongModel> GetRecentSongs()
        {
            List<SongModel> recentSongs = new List<SongModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"
                    SELECT TOP 5 songName, songPath, songSize, lastAccessDate, songDuration
                    FROM Songs
                    ORDER BY lastAccessDate DESC";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            recentSongs.Add(new SongModel
                            {
                                songName = reader["songName"].ToString(),
                                songPath = reader["songPath"].ToString(),
                                songSize = Convert.ToInt32(reader["songSize"]),
                                lastAccessDate = Convert.ToDateTime(reader["lastAccessDate"]),
                                songDuration = TimeSpan.Parse(reader["songDuration"].ToString())
                            });
                        }
                    }
                }
            }

            return recentSongs;

        }

    }
}
