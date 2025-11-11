namespace API.Exceptions.Account
{
    public class AccountWasNotCreatedException : Exception
    {
        public AccountWasNotCreatedException()
        {
        }
        public AccountWasNotCreatedException(string message)
            : base(message)
        {
        }
    }
}
