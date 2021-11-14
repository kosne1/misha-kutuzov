namespace Banks.ClientBuilder
{
    public class Director
    {
        private IBuilder _builder;

        public IBuilder Builder
        {
            set => _builder = value;
        }

        public void BuildMinimalViableClient(string name)
        {
            _builder.SetName(name);
        }

        public void BuildFullFeaturedClient(string name, string address, string passport)
        {
            _builder.SetName(name);
            _builder.SetAddress(address);
            _builder.SetPassport(passport);
        }
    }
}