using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace TSLibrary
{

    public class Message
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Imei { get; set; }
        public string GetMessage { get; set; }
    }

    public class Module
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Imei { get; set; }
    }

    public class MessageContext : DbContext
    {
        public MessageContext():base("TestContext")
        {

        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Module> Modules { get; set; }
    }
}
