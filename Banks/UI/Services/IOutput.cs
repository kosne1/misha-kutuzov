namespace Banks.UI.Services
{
    public interface IOutput
    {
        public void Attach();
        public void Detach();
        public void Notify();
        public void ClientReact(string name);
    }
}