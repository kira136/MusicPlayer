using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SongDTO
    {
        private string songThumbnail { get; set; }
        private string songName { get; set; }
        private string songPath { get; set; }
        private DateTime lastAccessDate { get; set; }
        private int songSize { get; set; }
        private TimeSpan songDuration { get; set; }

        public SongDTO(string _songName, string _songPath, DateTime _lastAccessDate, int _songSize, TimeSpan _songDuration)
        {
            songName = _songName;
            songPath = _songPath;
            lastAccessDate = _lastAccessDate;
            songSize = _songSize;
            songDuration = _songDuration;
        }

        public override string ToString()
        {
            return $"{songName}   {songDuration:mm\\:ss}";
        }
    }
}
