using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Symo.Module.HttpMonitor.UserControls.ViewModels
{
    public class HttpViewModel : INotifyPropertyChanged
    {
        private string _name = "Unknown";
        private string _url = "Unknown";
        private decimal _lossRate = 0;
        private Brush _status = Brushes.Gray;

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
        public string URL
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
                OnPropertyChanged("URL");
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

        public uint TotalRequests { get; internal set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
