using System;
using Isu.Entities;
using Isu.Lessons;
using Isu.Models;

namespace IsuExtra.Lessons
{
    public class LectureLesson : Lesson
    {
        public LectureLesson(string name, Teacher teacher, Address address, DateTime beginTime)
            : base(name, teacher, address, beginTime)
        {
            LessonType = "Lecture";
        }
    }
}