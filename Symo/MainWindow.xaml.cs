using Symo.Common;
using Symo.Entities;
using Symo.Library.Extensibility.Attributes;
using Symo.Library.Extensibility.Common;
using Symo.Library.Extensibility.Interfaces;
using Symo.Library.Modules;
using Symo.Library.Monitoring;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

namespace Symo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<IServer> _servers = new List<IServer>();

        public MainWindow()
        {
            InitializeComponent();
            AssignEvents();
        }

        private void AssignEvents()
        {
            MonitorEngine.Instance.Monitors.CollectionChanged += Monitors_Changed;
        }

        private void Monitors_Changed(object sender, NotifyCollectionChangedEventArgs e)
        {
            var items = (IEnumerable<IMonitor>)e.NewItems;
            HandleMonitorsChanged(items);
        }

        private void HandleMonitorsChanged(IEnumerable<IMonitor> items)
        {
            foreach (IMonitor monitor in items)
            {
                var control = CreateMonitorControl(monitor);

                if (control != null)
                {
                    AssignMonitorEvent(monitor, control);
                    AddControl(control);
                }
            }
        }

        private static void AssignMonitorEvent(IMonitor monitor, IMonitorUserControl control)
        {
            monitor.Triggered += control.Monitor_Triggered;
        }

        private void AddControl(IMonitorUserControl control)
        {
            Dispatcher.Invoke(() =>
            {
                BasePanel.Children.Add((UserControl)control);
            });
        }

        private IMonitorUserControl CreateMonitorControl(IMonitor monitor)
        {
            Type monitorType = monitor.GetType();
            var attribute = AttributeLoader.GetMonitorControlsAttribute(monitorType);
            var control = UserControlFactory.GetMonitorControl(attribute);

            return control;
        }

        private void AddMonitor<T>(IServer server, IConfiguration configuration)
        {
            AddMonitor(typeof(T), server, configuration);
        }

        private void AddMonitor(Type type, IServer server, IConfiguration configuration)
        {
            var monitor = (IMonitor)Activator.CreateInstance(type);
            monitor.Server = server;
            monitor.Configuration = configuration;

            MonitorEngine.Instance.Monitors.Add(monitor);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var modules = ModuleLoader.LoadModules();
            var configWindow = new ConfigWindow(modules);
            configWindow.Configured += Configured;
            configWindow.Show();
        }

        private void Configured(object sender, ConfiguredEventArgs e)
        {
            AddMonitor(e.MonitorType, e.Server, e.Configuration);
        }
    }
}
