using Isu.Services;

namespace IsuExtra.Entities
{
    public class EducationalProgram
    {
        public EducationalProgram(string name)
        {
            GroupService = new GroupService();
            Name = name;
        }

        public GroupService GroupService { get; }
        public string Name { get; }
    }
}