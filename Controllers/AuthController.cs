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
                var groups =   connection.Query<Auth>(commandText);
                connection.Close();
                return Ok(groups);

            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
                throw ex;
                
            }
        }




        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //************************* AuthGroupPermissions *************************

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //ENDPOINT PARA CREAR UN NUEVO REGISTRO

        [HttpPost("RegisterAuthGroupPermissions")]
        public async Task<IActionResult> RegisterAuthGroupPermissionsAsync([FromBody] AuthGroupPermissions auth_group_permissions)
        {
            int result = -1;
            string insertQuery = "INSERT INTO auth_group_permissions (id, group_id, permission_id) VALUES (@id, @group_id, @permission_id) RETURNING Id";
            var queryArguments = new
            {
                id = auth_group_permissions.id,
                group_id = auth_group_permissions.group_id,
                permission_id = auth_group_permissions.permission_id
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

        [HttpPost("UpdateAuthGroupPermissions")]
        public async Task<IActionResult> UpdateAuthGroupPermissions([FromBody] AuthGroupPermissions auth_group_permissions)
        {

            int result = -1;
            string insertQuery = "UPDATE auth_group_permissions  SET  id=@, group_id=@, permission_id=@id=@id, group_id=@group_id, permission_id=@permission_id WHERE id = @id"; var queryArguments = new
            {
                id = auth_group_permissions.id,
                group_id = auth_group_permissions.group_id,
                permission_id = auth_group_permissions.permission_id
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

        [HttpPost("DeleteAuthGroupPermissions")]
        public async Task<IActionResult> DeleteAuthGroupPermissions([FromBody] AuthGroupPermissions auth_group_permissions)
        {

            int result = -1;
            string insertQuery = "DELETE FROM auth_group_permissions WHERE id = @id";
            var queryArguments = new
            {
                id = auth_group_permissions.id,
                group_id = auth_group_permissions.group_id,
                permission_id = auth_group_permissions.permission_id
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

        [HttpGet("ListAllAuthGroupPermissionss")]
        public async Task<IActionResult> ListAllAuthGroupPermissionss()
        {

            try
            {
                string commandText = "SELECT * FROM   auth_group_permissions";
                connection.Open();
                var groups = await connection.QueryAsync<AuthGroupPermissions>(commandText);
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
        //************************* AuthPermissions *************************

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //ENDPOINT PARA CREAR UN NUEVO REGISTRO

            [HttpPost("RegisterAuthPermissions")]
            public async Task<IActionResult> RegisterAuthPermissionsAsync([FromBody] AuthPermissions auth_permission)
            {
                int result = -1;
                string insertQuery = "INSERT INTO auth_permission (id, name, content_type_id, codename) VALUES (@id, @name, @content_type_id, @codename) RETURNING Id";
                var queryArguments = new
                {
                    id = auth_permission.id,
                    name = auth_permission.name,
                    content_type_id = auth_permission.content_type_id,
                    codename = auth_permission.codename
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

            [HttpPost("UpdateAuthPermissions")]
            public async Task<IActionResult> UpdateAuthPermissions([FromBody] AuthPermissions auth_permission)
            {

                int result = -1;
                string insertQuery = "UPDATE auth_permission  SET  id=@id, name=@name, content_type_id=@content_type_id, codename=@codename WHERE id = @id"; var queryArguments = new
                {
                    id = auth_permission.id,
                    name = auth_permission.name,
                    content_type_id = auth_permission.content_type_id,
                    codename = auth_permission.codename
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

            [HttpPost("DeleteAuthPermissions")]
            public async Task<IActionResult> DeleteAuthPermissions([FromBody] AuthPermissions auth_permission)
            {

                int result = -1;
                string insertQuery = "DELETE FROM auth_permission WHERE id = @id";
                var queryArguments = new
                {
                    id = auth_permission.id,
                    name = auth_permission.name,
                    content_type_id = auth_permission.content_type_id,
                    codename = auth_permission.codename
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

            [HttpGet("ListAllAuthPermissionss")]
            public async Task<IActionResult> ListAllAuthPermissionss()
            {

                try
                {
                    string commandText = "SELECT * FROM   auth_permission";
                    connection.Open();
                    var groups = await connection.QueryAsync<AuthPermissions>(commandText);
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
        //************************* AuthContentType *************************

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //ENDPOINT PARA CREAR UN NUEVO REGISTRO

        [HttpPost("RegisterAuthContentType")]
        public async Task<IActionResult> RegisterAuthContentTypeAsync([FromBody] AuthContentType auth_content_type)
        {
            int result = -1;
            string insertQuery = "INSERT INTO auth_content_type (id, app_label, model) VALUES (@id,@app_label,@model) RETURNING Id";
            var queryArguments = new
            {
                id = auth_content_type.id,
                app_label = auth_content_type.app_label,
                model = auth_content_type.model
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

        [HttpPost("UpdateAuthContentType")]
        public async Task<IActionResult> UpdateAuthContentType([FromBody] AuthContentType auth_content_type)
        {

            int result = -1;
            string insertQuery = "UPDATE auth_content_type  SET  app_label=@app_label, model=@model WHERE id = @id"; var queryArguments = new
            {
                id = auth_content_type.id,
                app_label = auth_content_type.app_label,
                model = auth_content_type.model
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

        [HttpPost("DeleteAuthContentType")]
        public async Task<IActionResult> DeleteAuthContentType([FromBody] AuthContentType auth_content_type)
        {

            int result = -1;
            string insertQuery = "DELETE FROM auth_content_type WHERE id = @id";
            var queryArguments = new
            {
                id = auth_content_type.id,
                app_label = auth_content_type.app_label,
                model = auth_content_type.model
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

        [HttpGet("ListAllAuthContentTypes")]
        public async Task<IActionResult> ListAllAuthContentTypes()
        {

            try
            {
                string commandText = "SELECT * FROM   auth_content_type";
                connection.Open();
                var groups = await connection.QueryAsync<AuthContentType>(commandText);
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
