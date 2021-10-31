namespace Banks.Entities
{
    public class Client
    {
        public Client(string name, string address = null, string passport = null)
        {
            Name = name;
            Address = address;
            Passport = passport;
        }

        public string Name { get; }
        public string Address { get; }
        public string Passport { get; }

        public bool IsSuspicious()
        {
            return string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(Passport);
        }
    }
}