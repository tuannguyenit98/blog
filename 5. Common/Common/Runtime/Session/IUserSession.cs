namespace Common.Runtime.Session
{
    public interface IUserSession
    {
        int? GetUserEmail();

        string UserName { get; }

        string UserId { get; }
    }
}
