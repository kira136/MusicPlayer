using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DTO
{
    public class PlaylistDTO
    {
        private int playlistID { get; set; }
        private string playlistName { get; set; }
        private List<SongDTO> songs { get; set; }

        public PlaylistDTO()
        {
            songs = new List<SongDTO>();
        }
        public PlaylistDTO(int _playlistID, string _playlistName, List<SongDTO> _songs)
        {
            playlistID = _playlistID;
            playlistName = _playlistName;
            songs = _songs ?? new List<SongDTO>();
        }

        public void AddSong(SongDTO song)
        {
            if(song != null && !songs.Contains(song))
            {
                songs.Add(song);
            }
        }
    }
}
