namespace ContactsApp.Models
{
    public class AuthResult
    {
        public String Token { get; set; }
        public Boolean Result { get; set; }
        public List<String> Errors { get; set; }
    }
}
