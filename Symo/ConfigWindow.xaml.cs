using Symo.Entities;
using Symo.Library.Extensibility.Attributes;
using Symo.Library.Extensibility.Interfaces;
using Symo.Library.Modules;
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

namespace Symo
{
    /// <summary>
    /// Interaction logic for ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {
        public delegate void ConfiguredEventHandler(object sender, ConfiguredEventArgs e);

        IEnumerable<Type> _monitorsTypes;

        public event ConfiguredEventHandler Configured;

        private IConfigUserControl _configControl;
        private Type _monitorType;

        public ConfigWindow(IEnumerable<Type> monitorTypes)
        {
            InitializeComponent();
            ConfigFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;

            _monitorsTypes = monitorTypes;

            TypeList.SelectionChanged += TypeList_SelectionChanged;

            foreach (Type monitor in _monitorsTypes)
            {
                TypeList.Items.Add(GetListItem(monitor));
            }
        }

        private void TypeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (TypeListItem)e.AddedItems[0];
            _configControl = item.ConfigControl;
            _monitorType = item.Type;

            ConfigFrame.Content = _configControl;
        }

        private TypeListItem GetListItem(Type monitor)
        {
            var attr = (MonitorControlsAttribute)monitor.GetCustomAttributes(typeof(MonitorControlsAttribute), true).FirstOrDefault();
            return new TypeListItem
            {
                Name = attr.Name,
                Type = monitor,
                ConfigControl = (IConfigUserControl)Activator.CreateInstance(attr.ConfigControl)
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_configControl != null)
            {
                var args = new ConfiguredEventArgs
                {
                    Server = _configControl.Server,
                    Configuration = _configControl.Configuration,
                    MonitorType = _monitorType
                };
                Configured?.Invoke(this, args);
            }
            Close();
        }
    }
}
