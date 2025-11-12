namespace API.Exceptions.Account
{
    public class AccountNotCreatedException : Exception
    {
        public AccountNotCreatedException()
        {
        }
        public AccountNotCreatedException(string message)
            : base(message)
        {
        }
    }
}
