using System;
using System.Linq;
using Isu.Entities;
using Isu.Tools;

namespace IsuExtra.Entities
{
    public class Stream
    {
        public Stream(Lesson lesson)
        {
            Lesson = lesson;
            Group = new Group(lesson.Name);
        }

        public Lesson Lesson { get; }
        public Group Group { get; }

        public void AddStudent(Schedule schedule, Student student)
        {
            var groupLessons = schedule.Lessons.ToList();
            var maxDiff = new TimeSpan(1, 30, 0);
            foreach (Lesson lesson in groupLessons.Where(lesson => lesson.BeginTime - Lesson.BeginTime < maxDiff))
            {
                throw new IsuException(
                    $"There is an intersection of {Lesson.Name} that starts at {Lesson.BeginTime} and {lesson} that starts at {lesson.BeginTime}");
            }

            Group.AddStudent(student);
        }
    }
}