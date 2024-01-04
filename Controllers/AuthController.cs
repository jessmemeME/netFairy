using FairyBE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Dapper;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System.Web.Http.Cors;

namespace FairyBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
       
       
        private NpgsqlConnection connection;
        private readonly IConfiguration _configuration;
        public AuthController()
        {
            // string connectionString = _configuration.GetConnectionString("DefaultConnection");
            string connectionString = "Host = 127.0.0.1; Port = 5433; Database = desarrollo; Username = postgres; Password = 1234;";
            
            connection = new NpgsqlConnection(connectionString);
           
        }

        [HttpPost("RegisterAuthGroup")]
        public async Task<IActionResult> RegisterAuthGroupAsync([FromBody] Auth_Group auth_Group)
        {

            int result = -1;
            string insertQuery = "INSERT INTO auth_group (name) VALUES (@name) RETURNING Id";
            var queryArguments = new
            {
                name = auth_Group.name
            };
            try {
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

        [HttpPost("UpdateAuthGroup")]
        public async Task<IActionResult> UpdateAuthGroup([FromBody] Auth_Group auth_Group)
        {

            int result = -1;
            string insertQuery = "UPDATE auth_group  SET name = @name WHERE id = @id";
            var queryArguments = new
            {
                name = auth_Group.name,
                id= auth_Group.id
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


        [HttpPost("DeleteAuthGroup")]
        public async Task<IActionResult> DeleteAuthGroup([FromBody] Auth_Group auth_Group)
        {

            int result = -1;
            string insertQuery = "DELETE FROM auth_group WHERE id = @id";
            var queryArguments = new
            {
                id = auth_Group.id
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
       
        [HttpGet("ListAllAuthGroups")]
        public async Task<IActionResult> ListAllAuthGroups()
        {
            
            try {
                string commandText = "SELECT * FROM   auth_group";
                connection.Open();
                var groups =  await connection.QueryAsync<Auth_Group>(commandText);
                connection.Close();
                return Ok(groups);

            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
                throw ex;
                
            }
        }    
    }
}
