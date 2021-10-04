namespace Isu.Models
{
    public abstract class Person
    {
        protected Person(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; }
        public int Id { get; }
    }
}