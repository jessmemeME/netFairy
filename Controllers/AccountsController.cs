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
        public AccountsController(IConfiguration configuration)
        {
            _configuration = configuration;

            string connectionString = "Host=127.0.0.1;Port=5432;Database=proyectoHadaMadrina;Username=postgres;Password=postgres;";

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
        private async Task SendEmail(string recipientEmail, string emailSubject, string emailMessage)
        {
            Console.WriteLine("_configuration = " + _configuration);
            var emailSettings = _configuration.GetSection("EmailSettings");
            var smtpServer = emailSettings["SmtpServer"];
            var smtpPort = int.Parse(emailSettings["SmtpPort"]);
            var smtpUsername = emailSettings["SmtpUsername"];
            var smtpPassword = emailSettings["SmtpPassword"];
            var senderEmail = emailSettings["SenderEmail"];

            var smtpClient = new SmtpClient(smtpServer)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage(senderEmail, recipientEmail, emailSubject, emailMessage);

            await smtpClient.SendMailAsync(mailMessage);
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

                    await SendEmail(recipientEmail, emailSubject, emailMessage);
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
        [HttpPost("DeleteAccounts")]
        public async Task<IActionResult> DeleteAccounts([FromBody] Accounts accounts)
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
        //************************* Profile *************************

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //ENDPOINT PARA CREAR UN NUEVO REGISTRO

        [HttpPost("RegisterProfile")]
        public async Task<IActionResult> RegisterProfileAsync([FromBody] Profile accounts_profile)
        {
            int result = -1;
            string insertQuery = "INSERT INTO accounts_profile (id, auth_token, is_verified, created_date, updated_date, created_user_id, updated_user_id, user_id, is_active) VALUES (@id,@auth_token,@is_verified,@created_date,@updated_date,@created_user_id,@updated_user_id,@user_id,@is_active) RETURNING Id";
            var queryArguments = new
            {
                id = accounts_profile.id,
                auth_token = accounts_profile.auth_token,
                is_verified = accounts_profile.is_verified,
                created_date = accounts_profile.created_date,
                updated_date = accounts_profile.updated_date,
                created_user_id = accounts_profile.created_user_id,
                updated_user_id = accounts_profile.updated_user_id,
                user_id = accounts_profile.user_id,
                is_active = accounts_profile.is_active
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
        //ENDPOINT PARA EDITAR UN  REGISTRO

        [HttpPost("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] Profile accounts_profile)
        {

            int result = -1;
            string insertQuery = "UPDATE accounts_profile  SET id=@id, auth_token=@auth_token, is_verified=@is_verified, created_date=@created_date, updated_date=@updated_date, created_user_id=@created_user_id, updated_user_id=@updated_user_id, user_id=@user_id, is_active=@is_active WHERE id = @id"; var queryArguments = new
            {
                id = accounts_profile.id,
                auth_token = accounts_profile.auth_token,
                is_verified = accounts_profile.is_verified,
                created_date = accounts_profile.created_date,
                updated_date = accounts_profile.updated_date,
                created_user_id = accounts_profile.created_user_id,
                updated_user_id = accounts_profile.updated_user_id,
                user_id = accounts_profile.user_id,
                is_active = accounts_profile.is_active
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
        //ENDPOINT PARA ELIMINAR UN  REGISTRO

        [HttpPost("DeleteProfile")]
        public async Task<IActionResult> DeleteProfile([FromBody] Profile accounts_profile)
        {

            int result = -1;
            string insertQuery = "DELETE FROM accounts_profile WHERE id = @id";
            var queryArguments = new
            {
                id = accounts_profile.id,
                auth_token = accounts_profile.auth_token,
                is_verified = accounts_profile.is_verified,
                created_date = accounts_profile.created_date,
                updated_date = accounts_profile.updated_date,
                created_user_id = accounts_profile.created_user_id,
                updated_user_id = accounts_profile.updated_user_id,
                user_id = accounts_profile.user_id,
                is_active = accounts_profile.is_active
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
        //ENDPOINT PARA LISTAR TODOS LOS REGISTROS DE LA TABLA

        [HttpGet("ListAllProfiles")]
        public async Task<IActionResult> ListAllProfiles()
        {

            try
            {
                string commandText = "SELECT * FROM   accounts_profile";
                connection.Open();
                var groups = await connection.QueryAsync<Profile>(commandText);
                connection.Close();
                return Ok(groups);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw ex;

            }
        }





        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------





        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //************************* UserGroup *************************

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //ENDPOINT PARA CREAR UN NUEVO REGISTRO

        [HttpPost("RegisterUserGroup")]
        public async Task<IActionResult> RegisterUserGroupAsync([FromBody] UserGroup accounts_user_groups)
        {
            int result = -1;
            string insertQuery = "INSERT INTO accounts_user_groups (id, user_id, group_id) VALUES (@id, @user_id, @group_id) RETURNING Id";
            var queryArguments = new
            {
                id = accounts_user_groups.id,
                user_id = accounts_user_groups.user_id,
                group_id = accounts_user_groups.group_id
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
        //ENDPOINT PARA EDITAR UN  REGISTRO

        [HttpPost("UpdateUserGroup")]
        public async Task<IActionResult> UpdateUserGroup([FromBody] UserGroup accounts_user_groups)
        {

            int result = -1;
            string insertQuery = "UPDATE accounts_user_groups  SET  id=@, user_id=@, group_id=@id=@id, user_id=@user_id, group_id=@group_id WHERE id = @id"; var queryArguments = new
            {
                id = accounts_user_groups.id,
                user_id = accounts_user_groups.user_id,
                group_id = accounts_user_groups.group_id
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
        //ENDPOINT PARA ELIMINAR UN  REGISTRO

        [HttpPost("DeleteUserGroup")]
        public async Task<IActionResult> DeleteUserGroup([FromBody] UserGroup accounts_user_groups)
        {

            int result = -1;
            string insertQuery = "DELETE FROM accounts_user_groups WHERE id = @id";
            var queryArguments = new
            {
                id = accounts_user_groups.id,
                user_id = accounts_user_groups.user_id,
                group_id = accounts_user_groups.group_id
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
        //ENDPOINT PARA LISTAR TODOS LOS REGISTROS DE LA TABLA

        [HttpGet("ListAllUserGroups")]
        public async Task<IActionResult> ListAllUserGroups()
        {

            try
            {
                string commandText = "SELECT * FROM   accounts_user_groups";
                connection.Open();
                var groups = await connection.QueryAsync<UserGroup>(commandText);
                connection.Close();
                return Ok(groups);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw ex;

            }
        }



        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------





        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //************************* UserPermissions *************************

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //ENDPOINT PARA CREAR UN NUEVO REGISTRO

        [HttpPost("RegisterUserPermissions")]
        public async Task<IActionResult> RegisterUserPermissionsAsync([FromBody] UserPermissions accounts_user_permissions)
        {
            int result = -1;
            string insertQuery = "INSERT INTO accounts_user_permissions (id, user_id, permission_id) VALUES (@id, @user_id, @permission_id) RETURNING Id";
            var queryArguments = new
            {
                id = accounts_user_permissions.id,
                user_id = accounts_user_permissions.user_id,
                permission_id = accounts_user_permissions.permission_id
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
        //ENDPOINT PARA EDITAR UN  REGISTRO

        [HttpPost("UpdateUserPermissions")]
        public async Task<IActionResult> UpdateUserPermissions([FromBody] UserPermissions accounts_user_permissions)
        {

            int result = -1;
            string insertQuery = "UPDATE accounts_user_permissions  SET  id=@, user_id=@, permission_id=@id=@id, user_id=@user_id, permission_id=@permission_id WHERE id = @id"; var queryArguments = new
            {
                id = accounts_user_permissions.id,
                user_id = accounts_user_permissions.user_id,
                permission_id = accounts_user_permissions.permission_id
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
        //ENDPOINT PARA ELIMINAR UN  REGISTRO

        [HttpPost("DeleteUserPermissions")]
        public async Task<IActionResult> DeleteUserPermissions([FromBody] UserPermissions accounts_user_permissions)
        {

            int result = -1;
            string insertQuery = "DELETE FROM accounts_user_permissions WHERE id = @id";
            var queryArguments = new
            {
                id = accounts_user_permissions.id,
                user_id = accounts_user_permissions.user_id,
                permission_id = accounts_user_permissions.permission_id
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
        //ENDPOINT PARA LISTAR TODOS LOS REGISTROS DE LA TABLA

        [HttpGet("ListAllUserPermissionss")]
        public async Task<IActionResult> ListAllUserPermissionss()
        {

            try
            {
                string commandText = "SELECT * FROM   accounts_user_permissions";
                connection.Open();
                var groups = await connection.QueryAsync<UserPermissions>(commandText);
                connection.Close();
                return Ok(groups);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw ex;

            }
        }




        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------




    }
}
