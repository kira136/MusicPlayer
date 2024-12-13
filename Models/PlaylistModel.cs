using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models
{
    public class PlaylistModel
    {
        public int playlistID { get; set; }
        public string playlistName { get; set; }
        

        public PlaylistModel()
        {
            
        }
        public PlaylistModel(int _playlistID, string _playlistName)
        {
            playlistID = _playlistID;
            playlistName = _playlistName;
        }

        public void AddSong(SongModel song)
        {
            //if(song != null && !songs.Contains(song))
            //{
            //    songs.Add(song);
            //}
        }
    }
}
