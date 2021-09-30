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

        public void AddStudent(Group group, Student student)
        {
            var groupLessons = group.Schedule.Lessons.ToList();
            var maxDiff = new TimeSpan(1, 30, 0);
            foreach (Lesson lesson in from lesson in groupLessons
                let interval = Lesson.BeginTime - lesson.BeginTime
                where interval < maxDiff
                select lesson)
            {
                throw new IsuException(
                    $"There is Intersection of {Lesson.Name} that starts at {Lesson.BeginTime} and {lesson.Name} that starts at {lesson.BeginTime}");
            }

            Group.AddStudent(student);
        }
    }
}