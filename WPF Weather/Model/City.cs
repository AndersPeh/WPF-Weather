using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Weather.Model
{
    public class City
    {
        public int Version { get; set; }
        public string Key { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Rank { get; set; }
        public string LocalizedName { get; set; } = string.Empty;
        public Area Country { get; set; }
        public Area AdministrativeArea { get; set; }
    }
}
