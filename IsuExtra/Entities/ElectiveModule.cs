using System.Collections.Generic;

namespace IsuExtra.Entities
{
    public class ElectiveModule
    {
        private readonly List<Stream> _streams;

        public ElectiveModule(string name)
        {
            _streams = new List<Stream>();
            Name = name;
        }

        public IReadOnlyCollection<Stream> Streams => _streams;
        public string Name { get; }

        public void AddStream(Stream stream)
        {
            _streams.Add(stream);
        }
    }
}