namespace Common.Unknown
{
    public enum LoginResultType : byte
    {
        Success = 1,

        InvalidUserNameOrPassword,

        UserIsNotActive,

        UserLockout,

        InvalidSystem
    }
}
