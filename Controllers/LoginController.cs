using Dapper;
using FairyBE.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;

namespace FairyBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        
        #region Variables
        private NpgsqlConnection connection;//Atributo para conectar con Postgresql
        public IConfiguration Configuration { get; }//Se inicializa la interfaz de configuracion
        #endregion
        #region Constructor

        //CREAMOS UN CONSTRUCTOR DE LA CLASE PARA INICIALIZAR LA CONEXION A LA BD
        public LoginController(IConfiguration config)
        {

            //Se asigna la interfaz de configuraciion a la configuracion local
            Configuration = config;
            // Se obtiene la cadena de conexion alojada en el json de configuracion
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            //aqui se crea la conexion a la bd
            connection = new NpgsqlConnection(connectionString);
        }
        #endregion

        [HttpPost("Login")]
        public async Task<IActionResult> Login(Login login) {
            string query = "select auth_login from auth_login(@email, @pass)";
            var queryArguments = new
            {
                email = login.email,
                pass = login.password
            };
            try {
                connection.Open();
                var queryResult = await connection.QueryFirstAsync<ResponseLogin>(query, queryArguments);
                ReturnLogin resultObject = JsonConvert.DeserializeObject<ReturnLogin>(queryResult.auth_login);
                resultObject.token = "toke254654646546" +
                    "" +
                    "";
                return Ok(resultObject);
            } 
            catch(Exception ex) { 
                return BadRequest(ex.Message);
            } 
            finally { 
                connection.Close();
            }
        }

        [HttpPost("VerifyCode")]
        public async Task<IActionResult> VerifyCode(Email email ) {
            string query = @"select auth_code from accounts_user au where au.email like @email";
            var queryArguments = new
            {
                email = email.email
            };
            try {
                connection.Open();
                var queryResult = await connection.QueryFirstAsync<Code>(query, queryArguments);
                return Ok(queryResult);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            } finally { 
                connection.Close();
            }
        }

        [HttpPost("ResetCode")]
        public async Task<IActionResult> ResetCode(String email) {
            string authenticationCode = GenerateRandomCode();
            string query = @"update accounts_user set auth_code = @aut_code where email like @email";
            var queryArguments = new
            {
                email = email,
                aut_code = authenticationCode
            };
            try {
                connection.Open();
                var queryResult = await connection.ExecuteAsync(query, queryArguments);
                string recipientEmail = email;
                string emailSubject = "Authentication Code";
                string emailMessage = $"Su codigo de autenticación es: {authenticationCode}";

                SendEmail(recipientEmail, emailSubject, emailMessage);
                return Ok(authenticationCode);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally { connection.Close(); }
        }

        [HttpPost("Validated")]
        public async Task<IActionResult> Validated(Email email)
        {
            string authenticationCode = GenerateRandomCode();
            string query = @"update accounts_user set is_verified=true where email like @email";
            var queryArguments = new
            {
                email = email.email,
            };
            try
            {
                connection.Open();
                var queryResult = await connection.ExecuteAsync(query, queryArguments);
                return Ok(queryResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally { connection.Close(); }
        }

        private string GenerateRandomCode()
        {
            const string caracteresPermitidos = "0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(caracteresPermitidos, 7)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void   SendEmail(string recipientEmail, string emailSubject, string emailMessage)
        {

            var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("091141768341f1", "ef884dc1011a0e"),
                EnableSsl = true
            };
            client.Send("from@example.com", "to@example.com", emailSubject, emailMessage);

        }

    }
}
