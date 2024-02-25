namespace FairyBE.Models
{
    public class Login
    {
        public required string email { get; set; }
        public required string password { get; set; }
    }
    public class ResponseLogin {
        public required string auth_login { get; set; }
    }

    public class ReturnLogin {
        public required string  mensaje { get; set; }
        public required string respuesta { get; set; }
    }
}
