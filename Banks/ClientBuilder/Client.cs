namespace Banks.ClientBuilder
{
    // This is an account in bank, not a real person!!!
    public class Client
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Passport { get; set; }

        public bool IsSuspicious()
        {
            return string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(Passport);
        }
    }
}