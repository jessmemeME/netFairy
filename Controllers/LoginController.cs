using Dapper;
using FairyBE.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Newtonsoft.Json;

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
                return Ok(resultObject);
            } 
            catch(Exception ex) { 
                return BadRequest(ex.Message);
            } 
            finally { 
                connection.Close();
            }
        }

       
    }
}
