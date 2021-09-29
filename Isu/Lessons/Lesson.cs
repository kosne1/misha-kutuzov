using System;
using Isu.Entities;
using Isu.Models;

namespace Isu.Lessons
{
    public abstract class Lesson
    {
        protected Lesson(string name, Teacher teacher, Address address, DateTime beginTime)
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
        public string LessonType { get; set; }
        public Teacher Teacher { get; }
        public Address Address { get; }
        public DateTime BeginTime { get; }
        public DateTime EndTime { get; }
    }
}