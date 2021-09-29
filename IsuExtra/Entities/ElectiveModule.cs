using System.Collections.Generic;

namespace IsuExtra.Entities
{
    public class ElectiveModule
    {
        public ElectiveModule(string name)
        {
            Name = name;
        }

        public List<Stream> Streams { get; }
        public string Name { get; }
    }
}