using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Song
    {
        string songName { get; set; }
        string songPath { get; set; }
        DateTime lastAccessDate { get; set; }
        string songSize { get; set; }
    }
}
