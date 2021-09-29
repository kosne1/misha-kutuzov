using System;
using Isu.Entities;
using Isu.Lessons;
using Isu.Models;

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