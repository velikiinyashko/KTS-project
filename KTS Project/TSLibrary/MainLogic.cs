using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TSLibrary
{
    public class MainLogic
    {
        private ServerLogic _server;
        private Thread _listenThread;
        private LogEventLogic _log = new LogEventLogic("V:\\Main.log");

        public void ServerListener()
        {
            try
            {
                _server = new ServerLogic("0.0.0.0", 4000);
                _listenThread = new Thread(new ThreadStart(_server.Start));
                _listenThread.Start();
            }
            catch(Exception ex)
            {
                _server.Stop();
                _log.Write(ex.Message);
            }
        }
    }
}
