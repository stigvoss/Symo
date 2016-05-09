using Symo.Library.Extensibility.Common;
using Symo.Library.Extensibility.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Symo.Module.HttpMonitor.UserControls
{
    /// <summary>
    /// Interaction logic for HttpMonitorConfigControl.xaml
    /// </summary>
    public partial class HttpMonitorConfigControl : UserControl, IConfigUserControl
    {
        public IServer Server { get; private set; }

        public IConfiguration Configuration { get; private set; }

        public HttpMonitorConfigControl()
        {
            InitializeComponent();
            Server = new Server();
            Configuration = new DefaultConfiguration();
            DataContext = Server;
        }
    }
}
