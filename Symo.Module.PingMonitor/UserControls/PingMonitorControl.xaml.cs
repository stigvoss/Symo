using Symo.Library.Extensibility.Common;
using Symo.Library.Extensibility.Interfaces;
using Symo.Module.PingMonitor.UserControls.ViewModels;
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

namespace Symo.Module.PingMonitor.UserControls
{
    /// <summary>
    /// Interaction logic for PingMonitorControl.xaml
    /// </summary>
    public partial class PingMonitorControl : UserControl, IMonitorUserControl
    {
        private PingViewModel _model = new PingViewModel();

        public PingMonitorControl()
        {
            InitializeComponent();
            DataContext = _model;
        }

        public void Monitor_Triggered(object sender)
        {
            if (sender is PingMonitor)
            {
                SetModel((PingMonitor)sender);
            }
        }

        private void SetModel(PingMonitor monitor)
        {
            Dispatcher.Invoke(() =>
            {
                _model.Name = monitor.Server.Name;
                _model.IPAddress = monitor.Server.Address;
                _model.LossRate = monitor.PackageLossRate;
                _model.TotalPackages = monitor.TotalPackageSent;

                switch (monitor.Status)
                {
                    case ConnectionState.HEALTHY:
                        _model.Status = Brushes.LightGreen;
                        break;
                    case ConnectionState.UNHEALTY:
                        _model.Status = Brushes.Yellow;
                        break;
                    case ConnectionState.FAILED:
                        _model.Status = Brushes.Red;
                        break;
                }
            });
        }
    }
}
