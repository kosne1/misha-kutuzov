using System;
using IsuExtra.Entities;
using IsuExtra.Models;

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