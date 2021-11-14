namespace Banks.Notification
{
    public interface IObserver
    {
        void Update(ISubject subject);
    }
}