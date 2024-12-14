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
        public string playlistPath { get; set; }

        public PlaylistModel()
        {
            
        }
        public PlaylistModel(int _playlistID, string _playlistName)
        {
            playlistID = _playlistID;
            playlistName = _playlistName;
            playlistPath = @"F:\UIT-K18\workspace\PlaylistManager" + playlistName;
        }
    }
}
