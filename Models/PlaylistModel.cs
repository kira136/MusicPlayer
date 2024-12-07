using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models
{
    public class PlaylistModel
    {
        private int playlistID { get; set; }
        private string playlistName { get; set; }
        private List<SongModel> songs { get; set; }

        public PlaylistModel()
        {
            songs = new List<SongModel>();
        }
        public PlaylistModel(int _playlistID, string _playlistName, List<SongModel> _songs)
        {
            playlistID = _playlistID;
            playlistName = _playlistName;
            songs = _songs ?? new List<SongModel>();
        }

        public void AddSong(SongModel song)
        {
            if(song != null && !songs.Contains(song))
            {
                songs.Add(song);
            }
        }
    }
}
