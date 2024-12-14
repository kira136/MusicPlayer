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
        public DateTime lastAccessDate { get; set; }
        public int songSize { get; set; }
        public TimeSpan songDuration { get; set; }

        public SongModel(string _songName = null, string _songPath = null, DateTime _lastAccessDate = default , int _songSize = 0, TimeSpan _songDuration = default)
        {
            songName = _songName;
            songPath = _songPath;
            lastAccessDate = _lastAccessDate;
            songSize = _songSize;
            songDuration = _songDuration;
        }

        //public SongModel(string _songName, string _songPath)
        //{
        //    songName = _songName;
        //    songPath = _songPath;
        //}

        public override string ToString()
        {
            return $"{songName}   {songDuration:mm\\:ss}";
        }
    }
}
