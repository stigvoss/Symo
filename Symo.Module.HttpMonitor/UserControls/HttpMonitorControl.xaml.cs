using Symo.Library.Extensibility.Common;
using Symo.Library.Extensibility.Interfaces;
using Symo.Module.HttpMonitor.UserControls.ViewModels;
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
    /// Interaction logic for HttpMonitorUserControl.xaml
    /// </summary>
    public partial class HttpMonitorControl : UserControl, IMonitorUserControl
    {
        private HttpViewModel _model = new HttpViewModel();

        public HttpMonitorControl()
        {
            InitializeComponent();
            DataContext = _model;
        }

        public void Monitor_Triggered(object sender)
        {
            if (sender is HttpMonitor)
            {
                SetModel((HttpMonitor)sender);
            }
        }

        private void SetModel(HttpMonitor monitor)
        {
            Dispatcher.Invoke(() =>
            {
                _model.Name = monitor.Server.Name;
                _model.URL = monitor.Server.Address;
                _model.LossRate = monitor.RequestFailureRate;
                _model.TotalRequests = monitor.TotalRequestsSent;

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
