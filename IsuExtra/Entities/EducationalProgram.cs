using System.Collections.Generic;
using Isu.Entities;

namespace IsuExtra.Entities
{
    public class EducationalProgram
    {
        private readonly List<Group> _groups;

        public EducationalProgram(string name)
        {
            _groups = new List<Group>();
            Name = name;
        }

        public IReadOnlyCollection<Group> Groups => _groups;
        public string Name { get; }

        public void AddGroup(Group group)
        {
            _groups.Add(group);
        }
    }
}