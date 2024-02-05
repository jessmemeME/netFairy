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
        #region VARIABLES INTERNAS
        private NpgsqlConnection connection;//Atributo para conectar con Postgresql
        public IConfiguration Configuration { get; }//Se inicializa la interfaz de configuracion
        #endregion
        #region CONSTRUCTOR
        public AuthController(IConfiguration config)
        {
            //Se asigna la interfaz de configuraciion a la configuracion local
            Configuration = config;
            // Se obtiene la cadena de conexion alojada en el json de configuracion
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            //aqui se crea la conexion a la bd
            connection = new NpgsqlConnection(connectionString);
        }
        #endregion
        #region AUTH GROUP
        #region Register Auth Group
        [HttpPost("RegisterAuthGroup")]
        public async Task<IActionResult> RegisterAuthGroupAsync([FromBody] Auth auth_Group)
        {

            int result = -1;
            string insertQuery = "INSERT INTO auth_group (name) VALUES (@name) RETURNING Id";
            var queryArguments = new
            {
                name = auth_Group.name
            };
            try
            {
                connection.Open();
                result = connection.Execute(insertQuery, queryArguments);
                connection.Close();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region Update Auth Group
        [HttpPost("UpdateAuthGroup")]
        public async Task<IActionResult> UpdateAuthGroup([FromBody] Auth auth_Group)
        {

            int result = -1;
            string insertQuery = "UPDATE auth_group  SET name = @name WHERE id = @id";
            var queryArguments = new
            {
                name = auth_Group.name,
                id = auth_Group.id
            };
            try
            {
                connection.Open();
                result = connection.Execute(insertQuery, queryArguments);
                connection.Close();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
        }
        #endregion
        #region Delete Auth Group
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
                result = connection.Execute(insertQuery, queryArguments);
                connection.Close();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
        }
        #endregion
        #region
        [HttpGet("ListAllAuthGroups")]
        public async Task<IActionResult> ListAllAuthGroups()
        {

            try
            {
                string commandText = "SELECT * FROM   auth_group";
                connection.Open();
                var groups = connection.Query<Auth>(commandText);
                connection.Close();
                return Ok(groups);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        #endregion
        #endregion
        #region AUTH GROUP PERMISSIONS
        #region List Permissions by Group Id
        [HttpGet("ListAllAuthGroupPermissionss/{GroupId}")]
        public async Task<IActionResult> ListAllAuthGroupPermissionss(int GroupId)
        {

            try
            {
                string commandText = @"with pre_consulta as (
	                                    select 
		                                    agp.permission_id,
		                                    agp.group_id 
	                                    from auth_group_permissions agp 
	                                    join auth_group ag on ag.id  = agp.group_id 
	                                    where ag.id  = @GroupIdPar
                                    )
                                    select
                                        ap.id,
                                        ap.name,
                                        ap.content_type_id,
                                        act.app_label as content_type,
                                        ap.codename, 
                                        case when  pc.group_id is not null then true else false end as checqueado
                                    from auth_permission  ap
                                    join auth_content_type act on act.id = ap.content_type_id
                                    left join pre_consulta pc on pc. permission_id = ap.id 
                                    order by ap.id";
                var queryArguments = new
                {
                    GroupIdPar = GroupId
                };
                connection.Open();
                var groups = await connection.QueryAsync<AuthPermissions>(commandText, queryArguments);
                return Ok(groups);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally {
                connection.Close();
            }
        }


        #endregion
        #region Register Auth Group Permissions
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

            }
        }
        #endregion
        #region Update Auth Group Permissions
        [HttpPost("UpdateAuthGroupPermissions")]
        public async Task<IActionResult> UpdateAuthGroupPermissions([FromBody] AuthGroupPermissions auth_group_permissions)
        {

            int result = -1;
            string insertQuery = "UPDATE auth_group_permissions  SET  id=@, group_id=@, permission_id=@id=@id, group_id=@group_id, permission_id=@permission_id WHERE id = @id"; 
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
            }
        }
        #endregion
        #region Delete AuthGroup Permissions
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

            }
        }
        #endregion
        #region List All Auth Group Permissionss
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

            }
        }
        #endregion
        #endregion
        #region AUTH PERMISSIONS
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


            }
        }




        #endregion
        #region AUTH CONTENT TYPE
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


            }
        }





        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        #endregion
    }
}
