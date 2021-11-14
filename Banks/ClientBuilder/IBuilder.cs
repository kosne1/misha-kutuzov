namespace Banks.ClientBuilder
{
    public interface IBuilder
    {
        void SetName(string name);
        void SetPassport(string passport);
        void SetAddress(string address);
    }
}