namespace Symo.Library.Persistence
{
    public class KeyValue
    {
        public int ID { get; set; }

        public string Key { get; set; }

        public object Value { get; set; }

        public KeyValueGroup Configuration { get; set; }
    }
}