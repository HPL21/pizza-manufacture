namespace API.Exceptions.Account
{
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException()
        {
        }
        public AccountNotFoundException(string message)
            : base(message)
        {
        }
    }
}
