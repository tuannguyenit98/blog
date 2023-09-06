namespace Common.Helpers
{
    public class ApiResponse<T>
    {
        public static ApiResponseFailed<T> Fail(string error_code, string message)
        {
            return new ApiResponseFailed<T> { status = "failed", error_code = error_code, message = message };
        }

        public static ApiResponseFailed<T> Fail(string message)
        {
            return new ApiResponseFailed<T> { status = "failed", message = message };
        }

        public static ApiResponseSucess<T> Success(T data)
        {
            return new ApiResponseSucess<T> { status = "success", data = data };
        }

        public static ApiResponseSucess<T> Success()
        {
            return new ApiResponseSucess<T> { status = "success"};
        }
    }

    public class ApiResponseSucess<T>
    {
        public string status { get; set; }
        public T data { get; set; }
    }

    public class ApiResponseFailed<T>
    {
        public string error_code { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }
}