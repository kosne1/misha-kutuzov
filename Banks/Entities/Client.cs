namespace Banks.Entities
{
    public class Client
    {
        public Client(int id, string name, string address = null, string passport = null)
        {
            Id = id;
            Name = name;
            Address = address;
            Passport = passport;
        }

        public int Id { get; }
        public string Name { get; }
        public string Address { get; }
        public string Passport { get; }

        public bool IsSuspicious()
        {
            return string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(Passport);
        }
    }
}