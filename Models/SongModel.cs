using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SongModel
    {
        private string songID { get; }
        //private string songThumbnail { get; set; }
        public string songName { get; set; }
        public string songPath { get; set; }
        private DateTime lastAccessDate { get; set; }
        private int songSize { get; set; }
        private TimeSpan songDuration { get; set; }

        public SongModel(string _songName, string _songPath, DateTime _lastAccessDate, int _songSize, TimeSpan _songDuration)
        {
            songName = _songName;
            songPath = _songPath;
            lastAccessDate = _lastAccessDate;
            songSize = _songSize;
            songDuration = _songDuration;
        }

        public SongModel(string _songName, string _songPath)
        {
            songName = _songName;
            songPath = _songPath;
        }

        public override string ToString()
        {
            return $"{songName}   {songDuration:mm\\:ss}";
        }
    }
}
