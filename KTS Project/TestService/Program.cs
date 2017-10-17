using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSLibrary;
using System.Data.Entity;

namespace TestService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MainLogic _main = new MainLogic();
                _main.ServerListener();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("{0} - {1}", ex.TargetSite, ex.Message));
            }
            Console.Read();
        }
    }
}
