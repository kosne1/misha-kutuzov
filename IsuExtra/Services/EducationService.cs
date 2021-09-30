using System;
using System.Collections.Generic;
using Isu.Entities;
using IsuExtra.Entities;

namespace IsuExtra.Services
{
    public class EducationService
    {
        private int _teachersId = 1;

        public EducationService()
        {
            Teachers = new List<Teacher>();
            Lessons = new List<Lesson>();
        }

        public List<Teacher> Teachers { get; }
        public List<Lesson> Lessons { get; }

        public ElectiveModule ElectiveModule { get; private set; }

        public Teacher AddTeacher(string name)
        {
            var teacher = new Teacher(name, _teachersId++);
            Teachers.Add(teacher);
            return teacher;
        }

        public Lesson AddLesson(string name, Teacher teacher, string address, DateTime beginTime)
        {
            var lesson = new Lesson(name, teacher, address, beginTime);
            Lessons.Add(lesson);
            return lesson;
        }

        public ElectiveModule AddElectiveModule(string name)
        {
            var electiveModule = new ElectiveModule(name);
            ElectiveModule = electiveModule;
            return electiveModule;
        }
    }
}