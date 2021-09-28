using System;
using IsuExtra.Entities;
using IsuExtra.Models;

namespace IsuExtra.Lessons
{
    public class LaboratoryLesson : Lesson
    {
        public LaboratoryLesson(string name, Teacher teacher, Address address, DateTime beginTime)
            : base(name, teacher, address, beginTime)
        {
            LessonType = "Laboratory";
        }
    }
}