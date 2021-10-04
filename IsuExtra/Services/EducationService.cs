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
        private readonly List<Schedule> _schedules;
        private int _teachersId = 1;

        public EducationService()
        {
            _teachers = new List<Teacher>();
            _lessons = new List<Lesson>();
            _schedules = new List<Schedule>();
        }

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

        public ElectiveModule AddElectiveModule(MegaFaculty megaFaculty, string name)
        {
            var electiveModule = new ElectiveModule(name);
            megaFaculty.ElectiveModule = electiveModule;
            return electiveModule;
        }

        public EducationalProgram AddEducationalProgram(MegaFaculty megaFaculty, string name)
        {
            var educationalProgram = new EducationalProgram(name);
            megaFaculty.AddEducationalProgram(educationalProgram);
            return educationalProgram;
        }

        public Stream AddStream(ElectiveModule electiveModule, Lesson lesson)
        {
            var stream = new Stream(lesson);
            electiveModule.AddStream(stream);
            return stream;
        }

        public void AddStudentToStream(Schedule schedule, Stream stream, Student student)
        {
            stream.AddStudent(schedule, student);
        }

        public Schedule AddSchedule()
        {
            var schedule = new Schedule();
            _schedules.Add(schedule);
            return schedule;
        }
    }
}