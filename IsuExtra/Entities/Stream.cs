using Isu.Entities;

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
    }
}