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
            string connectionString = "Host=127.0.0.1;Port=5432;Database=proyectoHadaMadrina;Username=postgres;Password=postgres;";

            //aqui se crea la conexion a la bd
            connection = new NpgsqlConnection(connectionString);

        }
 //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //AQUI CONFIGURAMOS UN ENDPOINT PARA REGISTRAR CUENTAS DE USUARIOS

        [HttpPost("RegisterAccounts")]
        public async Task<IActionResult> RegisterAccountsAsync([FromBody] Accounts accounts)
        {

            int result = -1;
            string insertQuery = "INSERT INTO accounts_user (password, last_login, is_superuser, email, is_staff, is_active, date_joined, last_updated) VALUES (@password,now(),@is_superuser,@email,@is_staff,@is_active,now(),now()) RETURNING Id";
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
            string insertQuery = "UPDATE accounts_user  SET " +
                "password = @password," +
                "last_login=@last_login, " +
                "is_superuser=@is_superuser, " +
                "email=@email," +
                " is_staff=@is_staff, " +
                "is_active=@is_active, " +
                "last_updated=now() " +
                "WHERE id = @id";
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
