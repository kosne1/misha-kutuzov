using System.Collections.Generic;

namespace Isu.Entities
{
    public class EducationalProgram
    {
        public EducationalProgram(string name)
        {
            Groups = new List<Group>();
            Name = name;
        }

        public List<Group> Groups { get; }
        public string Name { get; }
    }
}