using Symo.Library.Extensibility.Attributes;
using Symo.Library.Extensibility.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Symo.Library.Persistence
{
    public class MonitorPersistence
    {
        public KeyValueGroup Store(IMonitor monitor)
        {
            var group = new KeyValueGroup
            {
                KeyValue = new List<KeyValue>()
            };

            var configuration = monitor.Configuration;
            var server = monitor.Server;

            var configValues = GetKeyValue(configuration);
            var serverValues = GetKeyValue(server);

            group.KeyValue.AddRange(configValues);
            group.KeyValue.AddRange(serverValues);
        }

        private IEnumerable<KeyValue> GetKeyValue(IConfiguration configuration)
        {
            var type = configuration.GetType();

            var properties = GetPersistProperties(type);
            var values = GetKeyValue(properties, configuration);
            
            throw new NotImplementedException();
        }

        private IEnumerable<KeyValue> GetKeyValue(IEnumerable<PropertyInfo> properties, object obj)
        {
            List<KeyValue> values = new List<KeyValue>();

            foreach(var property in properties)
            {
                var name = property.Name;
                var value = property.GetValue(obj);

                values.Add(new KeyValue
                {
                    Key = name,
                    Value = value
                });
            }

            return values;
        }

        private IEnumerable<PropertyInfo> GetPersistProperties(Type type)
        {
            var properties = type.GetProperties();
            var persistProperties = properties.Where(p => p.GetCustomAttribute<PersistAttribute>() != null);

            return persistProperties;
        }

        private IEnumerable<KeyValue> GetKeyValue(IServer server)
        {
            var serverType = server.GetType();

            throw new NotImplementedException();
        }
    }
}
