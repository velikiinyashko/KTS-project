using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceProcess;

namespace Server
{
    [RunInstaller(true)]
    public partial class TSServerInstaller : System.Configuration.Install.Installer
    {

        ServiceInstaller serviceInstaller;
        ServiceProcessInstaller processInstaller;

        public TSServerInstaller()
        {
            InitializeComponent();
            serviceInstaller = new ServiceInstaller();
            processInstaller = new ServiceProcessInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;
            processInstaller.Username = "TSAdmin";
            processInstaller.Password = "TSAdmin";
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.ServiceName = "TSServer";
            serviceInstaller.DisplayName = "Tracker System Server";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
