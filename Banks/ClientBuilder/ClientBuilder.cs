namespace Banks.ClientBuilder
{
    public class ClientBuilder : IBuilder
    {
        private Client _client = new ();

        public ClientBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _client = new Client();
        }

        public void SetName(string name)
        {
            _client.Name = name;
        }

        public void SetPassport(string passport)
        {
            _client.Passport = passport;
        }

        public void SetAddress(string address)
        {
            _client.Address = address;
        }

        public Client GetProduct()
        {
            Client result = _client;

            Reset();

            return result;
        }
    }
}