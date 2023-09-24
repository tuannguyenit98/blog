namespace DTOs.Blog.Login
{
    public class TokenResultDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string TokenType { get; set; }

        public string AccessToken { get; set; }
    }
}
