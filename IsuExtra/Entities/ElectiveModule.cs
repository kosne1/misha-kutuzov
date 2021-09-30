using System.Collections.Generic;
using Isu.Entities;

namespace IsuExtra.Entities
{
    public class ElectiveModule
    {
        public ElectiveModule(string name)
        {
            Streams = new List<Stream>();
            Name = name;
        }

        public List<Stream> Streams { get; }
        public string Name { get; }

        public Stream AddStream(Lesson lesson)
        {
            var stream = new Stream(lesson);
            Streams.Add(stream);
            return stream;
        }
    }
}