using System;
using System.Collections.Generic;
using Isu.Entities;
using IsuExtra.Entities;

namespace IsuExtra.Services
{
    public class EducationService
    {
        private readonly List<Teacher> _teachers;
        private readonly List<Lesson> _lessons;
        private int _teachersId = 1;

        public EducationService()
        {
            _teachers = new List<Teacher>();
            _lessons = new List<Lesson>();
        }

        public IReadOnlyCollection<Teacher> Teachers => _teachers;
        public IReadOnlyCollection<Lesson> Lessons => _lessons;

        public ElectiveModule ElectiveModule { get; private set; }

        public Teacher AddTeacher(string name)
        {
            var teacher = new Teacher(name, _teachersId++);
            _teachers.Add(teacher);
            return teacher;
        }

        public Lesson AddLesson(string name, Teacher teacher, string address, DateTime beginTime)
        {
            var lesson = new Lesson(name, teacher, address, beginTime);
            _lessons.Add(lesson);
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