/*
 * EL CONTROLADOR SE ENCARGA DE HACER LA CONEXION ENTRE EL MODELO, PETICION (POST,GET,etc) Y LA BASE DE DATOS.
 */
using FairyBE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Dapper; //framework que nos permite hacer las consultas SQL
using Npgsql;//framework que nos permite conectarnos al POSTGRESQL
using Microsoft.Extensions.Configuration;
using System.Web.Http.Cors;
using System.Net.Mail;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;

namespace FairyBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : Controller
    {
        private NpgsqlConnection connection;
        private readonly IConfiguration _configuration;
        //CREAMOS UN CONSTRUCTOR DE LA CLASE PARA INICIALIZAR LA CONEXION A LA BD
        public AccountsController()
        {
<<<<<<< Updated upstream
            string connectionString = "Host=127.0.0.1;Port=5432;Database=proyectoHadaMadrina;Username=postgres;Password=postgres;";
=======
            _configuration = configuration;

            string connectionString = "Host=babar.db.elephantsql.com;Port=5432;Database=hdzoacnc;Username=hdzoacnc;Password=qBHkfmkyZf0a-KZ9G_i2GPS4ULBGtHIB;";
>>>>>>> Stashed changes

            //aqui se crea la conexion a la bd
            connection = new NpgsqlConnection(connectionString);

        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private string GenerateRandomCode()
        {
            const string caracteresPermitidos = "0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(caracteresPermitidos, 7)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void SendEmail(string recipientEmail, string emailSubject, string emailMessage)
        {
<<<<<<< Updated upstream
            var emailSettings = _configuration.GetSection("EmailSettings");
            var smtpServer = emailSettings["SmtpServer"];
            var smtpPort = int.Parse(emailSettings["SmtpPort"]);
            var smtpUsername = emailSettings["SmtpUsername"];
            var smtpPassword = emailSettings["SmtpPassword"];
            var senderEmail = emailSettings["SenderEmail"];
=======
            // Console.WriteLine("_configuration = " + _configuration);
            /*  var emailSettings = _configuration.GetSection("EmailSettings");
              var smtpServer = emailSettings["SmtpServer"];
              var smtpPort = int.Parse(emailSettings["SmtpPort"]);
              var smtpUsername = emailSettings["SmtpUsername"];
              var smtpPassword = emailSettings["SmtpPassword"];
              var senderEmail = emailSettings["SenderEmail"];
>>>>>>> Stashed changes

              var smtpClient = new SmtpClient(smtpServer)
              {
                  Port = smtpPort,
                  Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                  EnableSsl = true,
              };

              var mailMessage = new MailMessage(senderEmail, recipientEmail, emailSubject, emailMessage);

              await smtpClient.SendMailAsync(mailMessage);*/
            var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("091141768341f1", "ef884dc1011a0e"),
                EnableSsl = true
            };
            client.Send("from@example.com", "to@example.com", "Hello world", "testbody");

        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //AQUI CONFIGURAMOS UN ENDPOINT PARA REGISTRAR CUENTAS DE USUARIOS

            [HttpPost("RegisterAccounts")]
            public async Task<IActionResult> RegisterAccountsAsync([FromBody] Accounts accounts)
            {
                int result = -1;
                string insertQuery = "INSERT INTO accounts_user (password, last_login, is_superuser, email, is_staff, is_active, date_joined, last_updated) VALUES (@password, now(), @is_superuser, @email, @is_staff, @is_active, now(), now()) RETURNING Id";

                var queryArguments = new
                {
                    password = accounts.password,
                    last_login = accounts.last_login,
                    is_superuser = accounts.is_superuser,
                    email = accounts.email,
                    is_staff = accounts.is_staff,
                    is_active = accounts.is_active,
                    date_joined = accounts.date_joined,
                    last_updated = accounts.last_updated
                };

                try
                {
                    await connection.OpenAsync();
                    result = await connection.ExecuteAsync(insertQuery, queryArguments);
                    await connection.CloseAsync();

                    // Generate authentication code
                    string authenticationCode = GenerateRandomCode();

                    // Send email with authentication code
                    string recipientEmail = accounts.email;
                    string emailSubject = "Authentication Code";
                    string emailMessage = $"Su codigo de autenticación es: {authenticationCode}";

                    SendEmail(recipientEmail, emailSubject, emailMessage);
                // Devuelve un mensaje específico en caso de éxito
                return Ok(new { Result = result, Message = "Se envió el correo con éxito." });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Result = -1, Message = "Error al enviar el correo electronico." });

                }

        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

      //AQUI CONFIGURAMOS UN ENDPOINT PARA LISTAR LAS CUENTAS DE USUARIOS
       [HttpGet("ListAllAccounts")]
        public async Task<IActionResult> ListAllAccounts()
        {

            try
            {
                string commandText = "SELECT * FROM   accounts_user";
                connection.Open();
                var users = await connection.QueryAsync<Accounts>(commandText);
                connection.Close();
                return Ok(users);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw ex;

            }
        }
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //ENDPOINT PARA EDITAR LAS CUENTAS DE USUARIOS

        [HttpPost("UpdateAccounts")]
        public async Task<IActionResult> UpdateAccounts([FromBody] Accounts accounts)
        {

            int result = -1;
            string insertQuery = "UPDATE accounts_user  SET password = @password,last_login=@last_login,is_superuser=@is_superuser,email=@email,is_staff=@is_staff,is_active=@is_active,last_updated=now() WHERE id = @id";
            var queryArguments = new
            {
                password = accounts.password,
                last_login = accounts.last_login,
                is_superuser = accounts.is_superuser,
                email = accounts.email,
                is_staff = accounts.is_staff,
                is_active = accounts.is_active,
                last_updated = accounts.last_updated
            };
            try
            {
                connection.Open();
                result = await connection.ExecuteAsync(insertQuery, queryArguments);
                connection.Close();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw ex;
            }
        }
 //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        [HttpPost("DeleteAuthGroup")]
        public async Task<IActionResult> DeleteAuthGroup([FromBody] Accounts accounts)
        {

            int result = -1;
            string insertQuery = "DELETE FROM accounts_user WHERE id = @id";
            var queryArguments = new
            {
                id = accounts.id
            };
            try
            {
                connection.Open();
                result = await connection.ExecuteAsync(insertQuery, queryArguments);
                connection.Close();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw ex;
            }
        }
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
