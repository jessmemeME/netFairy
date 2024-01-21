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

/* origenCarpeta = nombreProyecto.Controllers*/
namespace FairyBE.Controllers
{
   //especificaciones del api
    [ApiController]//con esto se especifica que va ser un controlador de API xq hay distintos tipos de controladores
    [Route("[controller]")]//la ruta que va utilizar sera el nombre del controlador

    //Definición del CONTROLADOR DEL API AuthController
    public class AuthController : Controller
    {
       
       
        private NpgsqlConnection connection;//Atributo para conectar con Postgresql
        private readonly IConfiguration _configuration;//Para leer la configuracion inicial

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //CREAMOS UN CONSTRUCTOR DE LA CLASE PARA INICIALIZAR LA CONEXION A LA BD
        public AuthController()
        {
            // string connectionString = _configuration.GetConnectionString("DefaultConnection");
            string connectionString = "Host=127.0.0.1;Port=5432;Database=proyectoHadaMadrina;Username=postgres;Password=postgres;";

            //aqui se crea la conexion a la bd
            connection = new NpgsqlConnection(connectionString);
           
        }

        //AQUI CONFIGURAMOS UN ENDPOINT (la ultima palabra de la URL que define la funcion a la que va llamar)
        [HttpPost("RegisterAuthGroup")]
        public async Task<IActionResult> RegisterAuthGroupAsync([FromBody] Auth auth_Group)
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

//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        [HttpPost("UpdateAuthGroup")]
        public async Task<IActionResult> UpdateAuthGroup([FromBody] Auth auth_Group)
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

//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        [HttpPost("DeleteAuthGroup")]
        public async Task<IActionResult> DeleteAuthGroup([FromBody] Auth auth_Group)
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

//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------       
        [HttpGet("ListAllAuthGroups")]
        public async Task<IActionResult> ListAllAuthGroups()
        {
            
            try {
                string commandText = "SELECT * FROM   auth_group";
                connection.Open();
                var groups = connection.Query<Auth_Group>(commandText);
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
