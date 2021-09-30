using System.Collections.Generic;
using Isu.Entities;

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

        public Stream AddStream(Lesson lesson)
        {
            var stream = new Stream(lesson);
            _streams.Add(stream);
            return stream;
        }
    }
}