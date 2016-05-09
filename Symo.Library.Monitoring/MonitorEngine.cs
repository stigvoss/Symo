using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Symo.Library.Extensibility.Interfaces;

namespace Symo.Library.Monitoring
{
    public class MonitorEngine
    {
        private static MonitorEngine _instance;

        public static MonitorEngine Instance { get { return _instance ?? (_instance = new MonitorEngine()); } }

        private ObservableCollection<IMonitor> _monitors;
        private ObservableCollection<MonitorInfo> _monitorInfo;

        public ObservableCollection<IMonitor> Monitors { get { return _monitors; } }

        public ObservableCollection<MonitorInfo> MonitorInfo { get { return _monitorInfo; } }

        public MonitorEngine()
        {
            _monitors = new ObservableCollection<IMonitor>();
            _monitorInfo = new ObservableCollection<MonitorInfo>();
            _monitors.CollectionChanged += Monitors_Changed;
        }

        private void Monitors_Changed(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    OnMonitorsAdded(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    OnMonitorsRemoved(e.OldItems);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    OnMonitorsReplaced(e.OldItems, e.NewItems);
                    break;
            }
        }

        private void OnMonitorsReplaced(IList removedItems, IList addedItems)
        {
            foreach (IMonitor monitor in removedItems)
            {
                RemoveMonitor(monitor);
            }

            foreach (IMonitor monitor in addedItems)
            {
                AddMonitor(monitor);
            }
        }

        private void OnMonitorsAdded(IList items)
        {
            foreach (IMonitor monitor in items)
            {
                AddMonitor(monitor);
            }
        }
        
        private void OnMonitorsRemoved(IList items)
        {
            foreach (IMonitor monitor in items)
            {
                RemoveMonitor(monitor);
            }
        }

        private void AddMonitor(IMonitor monitor)
        {
            if (!monitor.IsRunning)
            {
                monitor.Start();
            }
        }

        private void RemoveMonitor(IMonitor monitor)
        {
            if (!monitor.IsRunning)
            {
                monitor.Stop();
            }
        }
    }
}
