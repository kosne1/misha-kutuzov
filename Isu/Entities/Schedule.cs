using System.Collections.Generic;

namespace Isu.Entities
{
    public class Schedule
    {
        public Schedule()
        {
            Lessons = new List<Lesson>();
        }

        public List<Lesson> Lessons { get; }

        public void AddLesson(Lesson lesson)
        {
            Lessons.Add(lesson);
        }
    }
}