using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    public class ConfigModel
    {
        public string ServerBase { get; set; }
        public int PortBase { get; set; }
        public string UserNameBase { get; set; }
        public string PasswordBase { get; set; }
        public string NameBase { get; set; }
    }
}
