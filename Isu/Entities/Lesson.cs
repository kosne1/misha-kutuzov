using System;

namespace Isu.Entities
{
    public class Lesson
    {
        public Lesson(string name, Teacher teacher, string address, DateTime beginTime)
        {
            Name = name;
            Teacher = teacher;
            Address = address;
            BeginTime = beginTime;

            EndTime = beginTime;
            EndTime.AddHours(1);
            EndTime.AddMinutes(30);
        }

        public string Name { get; }
        public Teacher Teacher { get; }
        public string Address { get; }
        public DateTime BeginTime { get; }
        public DateTime EndTime { get; }
    }
}