using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Symo.Module.PingMonitor.UserControls.ViewModels
{
    public class PingViewModel : INotifyPropertyChanged
    {
        private string _name = "Unknown";
        private string _ipAddress = "Unknown";
        private decimal _lossRate = 0;
        private Brush _status = Brushes.Gray;
        private uint _totalPackages = 0;

        public string Name {
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
        public string IPAddress
        {
            get
            {
                return _ipAddress;
            }
            set
            {
                _ipAddress = value;
                OnPropertyChanged("IPAddress");
            }
        }
        public decimal LossRate
        {
            get
            {
                return _lossRate;
            }
            set
            {
                _lossRate = value;
                OnPropertyChanged("LossRate");
            }
        }
        public Brush Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        public uint TotalPackages
        {
            get
            {
                return _totalPackages;
            }
            set
            {
                _totalPackages = value;
                OnPropertyChanged("TotalPackages");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
