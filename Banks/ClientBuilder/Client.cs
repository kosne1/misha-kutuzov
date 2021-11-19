using Banks.Notification;
using Banks.UI.Services;

namespace Banks.ClientBuilder
{
    // This is an account in bank, not a real person!!!
    public class Client : IObserver
    {
        private IOutput _output;

        public Client(int id)
        {
            Id = id;
        }

        public int Id { get; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Passport { get; set; }

        public bool IsSuspicious()
        {
            return string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(Passport);
        }

        public void SetOutput(IOutput output)
        {
            _output = output;
        }

        public void Update(ISubject subject)
        {
            _output.ClientReact(Name);
        }
    }
}