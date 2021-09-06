namespace Isu.Entities
{
    public class Student
    {
        public Student(string name, int id, string groupName)
        {
            Name = name;
            Id = id;
            GroupName = groupName;
        }

        public string Name { get; private set; }
        public int Id { get; set; }
        public string GroupName { get; set; }
    }
}