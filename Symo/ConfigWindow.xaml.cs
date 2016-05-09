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
            ConfigureComponents();
            AssignFields(monitorTypes);
        }

        private void ConfigureComponents()
        {
            ContentFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;

            TypeList.SelectionChanged += SelectionChanged;

            foreach (Type monitor in _monitorsTypes)
            {
                TypeList.Items.Add(GetListItem(monitor));
            }
        }

        private void AssignFields(IEnumerable<Type> monitorTypes)
        {
            _monitorsTypes = monitorTypes;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (TypeListItem)e.AddedItems[0];
            SetTypeSelection(item);
        }

        private void SetTypeSelection(TypeListItem selection)
        {
            _monitorType = selection.Type;
            _configControl = selection.ConfigControl;
            ContentFrame.Content = selection.ConfigControl;
        }

        private TypeListItem GetListItem(Type monitor)
        {
            MonitorControlsAttribute attribute = GetTypeAttribute(monitor);
            return new TypeListItem
            {
                Name = attribute.Name,
                Type = monitor,
                ConfigControl = (IConfigUserControl)Activator.CreateInstance(attribute.ConfigControl)
            };
        }

        private static MonitorControlsAttribute GetTypeAttribute(Type monitor)
        {
            var attributes = monitor.GetCustomAttributes(typeof(MonitorControlsAttribute), true);
            return (MonitorControlsAttribute)attributes.FirstOrDefault();
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
