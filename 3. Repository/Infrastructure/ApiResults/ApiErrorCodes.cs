namespace Infrastructure.ApiResults
{
    public enum ApiErrorCodes
    {
        Failed,
        ObjectNotFound,
        InvalidParamters,
        UserLoginInvalidUserNameOrPassword,
        UserLoginIsNotActive,
        ResetPasswordActiveCodeIsIncorrect,
        InvalidSystem
    }
}
