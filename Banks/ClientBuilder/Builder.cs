namespace Banks.ClientBuilder
{
    public class Builder : IBuilder
    {
        private int _clientsCounter;
        private Client _client;

        public Builder()
        {
            Reset();
        }

        public void Reset()
        {
            _client = new Client(_clientsCounter++);
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

        public Client GetClient()
        {
            Client result = _client;

            Reset();

            return result;
        }
    }
}