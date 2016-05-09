using Symo.Library.Extensibility.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symo.Library.Extensibility.Common
{
    public class Server : IServer, INotifyPropertyChanged
    {
        private string _name = string.Empty;
        private string _ipAddress = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Address
        {
            get
            {
                return _ipAddress;
            }
            set
            {
                _ipAddress = value;
                OnPropertyChanged("Address");
            }
        }
    }
}
