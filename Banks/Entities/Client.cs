namespace Banks.Entities
{
    public class Client
    {
        public Client(string name, string address, string passport)
        {
            Name = name;
            if (!IsInfoValid(address)) Suspicious = true;
            Address = address;
            if (!IsInfoValid(passport)) Suspicious = true;
            Passport = passport;
        }

        public string Name { get; }
        public string Address { get; }
        public string Passport { get; }
        public bool Suspicious { get; } = false;

        private bool IsInfoValid(string info)
        {
            return !string.IsNullOrEmpty(info);
        }
    }
}