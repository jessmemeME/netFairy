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

    //Definición del CONTROLADOR DEL API BasicInfoController
    public class BasicInfoController : Controller
    {


        private NpgsqlConnection connection;//Atributo para conectar con Postgresql
        public IConfiguration Configuration { get; }//Se inicializa la interfaz de configuracion

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //CREAMOS UN CONSTRUCTOR DE LA CLASE PARA INICIALIZAR LA CONEXION A LA BD
        public BasicInfoController(IConfiguration config)
        {
            //Se asigna la interfaz de configuraciion a la configuracion local
            Configuration = config;
            // Se obtiene la cadena de conexion alojada en el json de configuracion
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            //aqui se crea la conexion a la bd
            connection = new NpgsqlConnection(connectionString);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //************************* AGE GROUPS *************************

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //ENDPOINT PARA CREAR UN NUEVO REGISTRO

                [HttpPost("RegisterAgeGroup")]
                public async Task<IActionResult> RegisterAgeGroupAsync([FromBody] AgeGroup basic_info_age_group)
                {
                    int result = -1;
                    string insertQuery = "INSERT INTO basic_info_age_group (id, name, description, age_range, created_date, updated_date, is_active, created_user_id, updated_user_id) VALUES (@id,@name,@description,@age_range,@created_date,@updated_date,@is_active,@created_user_id,@updated_user_id) RETURNING Id";
                    var queryArguments = new
                    {
                        id = basic_info_age_group.id,
                        name = basic_info_age_group.name,
                        description = basic_info_age_group.description,
                        age_range = basic_info_age_group.age_range,
                        created_date = basic_info_age_group.created_date,
                        updated_date = basic_info_age_group.updated_date,
                        is_active = basic_info_age_group.is_active,
                        created_user_id = basic_info_age_group.created_user_id,
                        updated_user_id = basic_info_age_group.updated_user_id

                        /*
                            "id"	"bigint"
                            "name"	"character varying"
                            "description"	"text"
                            "age_range"	"character varying"
                            "created_date"	"timestamp with time zone"
                            "updated_date"	"timestamp with time zone"
                            "is_active"	"boolean"
                            "created_user_id"	"bigint"
                            "updated_user_id"	"bigint"
                         */
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

                [HttpPost("UpdateAgeGroups")]
                public async Task<IActionResult> UpdateAgeGroups([FromBody] AgeGroup basic_info_age_group)
                {

                    int result = -1;
                    string insertQuery = "UPDATE basic_info_age_group  SET name=@name, description=@description, age_range=@age_range, created_date=@created_date, updated_date=@updated_date, is_active=@is_active, created_user_id=@created_user_id, updated_user_id=@updated_user_id WHERE id = @id";
                    var queryArguments = new
                    {
                        id = basic_info_age_group.id,
                        name = basic_info_age_group.name,
                        description = basic_info_age_group.description,
                        age_range = basic_info_age_group.age_range,
                        created_date = basic_info_age_group.created_date,
                        updated_date = basic_info_age_group.updated_date,
                        is_active = basic_info_age_group.is_active,
                        created_user_id = basic_info_age_group.created_user_id,
                        updated_user_id = basic_info_age_group.updated_user_id
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

                [HttpPost("DeleteAgeGroups")]
                public async Task<IActionResult> DeleteAgeGroups([FromBody] AgeGroup basic_info_age_group)
                {

                    int result = -1;
                    string insertQuery = "DELETE FROM basic_info_age_group WHERE id = @id";
                    var queryArguments = new
                    {
                        id = basic_info_age_group.id,
                        name = basic_info_age_group.name,
                        description = basic_info_age_group.description,
                        age_range = basic_info_age_group.age_range,
                        created_date = basic_info_age_group.created_date,
                        updated_date = basic_info_age_group.updated_date,
                        is_active = basic_info_age_group.is_active,
                        created_user_id = basic_info_age_group.created_user_id,
                        updated_user_id = basic_info_age_group.updated_user_id
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

                [HttpGet("ListAllAgeGroups")]
                public async Task<IActionResult> ListAllAgeGroups()
                {

                    try
                    {
                        string commandText = "SELECT * FROM   basic_info_age_group";
                        connection.Open();
                        var groups = await connection.QueryAsync<AgeGroup>(commandText);
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

        //************************* DocumentType *************************

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //ENDPOINT PARA CREAR UN NUEVO REGISTRO

        [HttpPost("RegisterDocumentType")]
        public async Task<IActionResult> RegisterDocumentTypeAsync([FromBody] DocumentType basic_info_document_type)
        {
            int result = -1;
            string insertQuery = "INSERT INTO basic_info_document_type (id, name, description, created_date, updated_date, is_active, created_user_id, updated_user_id) VALUES (@id,@name,@description,@created_date,@updated_date,@is_active,@created_user_id,@updated_user_id) RETURNING Id";
            var queryArguments = new
            {
                id = basic_info_document_type.id,
                name = basic_info_document_type.name,
                description = basic_info_document_type.description,
                created_date = basic_info_document_type.created_date,
                updated_date = basic_info_document_type.updated_date,
                is_active = basic_info_document_type.is_active,
                created_user_id = basic_info_document_type.created_user_id,
                updated_user_id = basic_info_document_type.updated_user_id

                /*
                    "id"	"bigint"
                    "name"	"character varying"
                    "description"	"text"
                    "created_date"	"timestamp with time zone"
                    "updated_date"	"timestamp with time zone"
                    "is_active"	"boolean"
                    "created_user_id"	"bigint"
                    "updated_user_id"	"bigint"
                 */
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

        [HttpPost("UpdateDocumentType")]
        public async Task<IActionResult> UpdateDocumentType([FromBody] DocumentType basic_info_document_type)
        {

            int result = -1;
            string insertQuery = "UPDATE basic_info_document_type  SET name=@name, description=@description, created_date=@created_date, updated_date=@updated_date, is_active=@is_active, created_user_id=@created_user_id, updated_user_id=@updated_user_id WHERE id = @id";
            var queryArguments = new
            {
                id = basic_info_document_type.id,
                name = basic_info_document_type.name,
                description = basic_info_document_type.description,
                created_date = basic_info_document_type.created_date,
                updated_date = basic_info_document_type.updated_date,
                is_active = basic_info_document_type.is_active,
                created_user_id = basic_info_document_type.created_user_id,
                updated_user_id = basic_info_document_type.updated_user_id
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

        [HttpPost("DeleteDocumentType")]
        public async Task<IActionResult> DeleteDocumentType([FromBody] DocumentType basic_info_document_type)
        {

            int result = -1;
            string insertQuery = "DELETE FROM basic_info_document_type WHERE id = @id";
            var queryArguments = new
            {
                id = basic_info_document_type.id,
                name = basic_info_document_type.name,
                description = basic_info_document_type.description,
                created_date = basic_info_document_type.created_date,
                updated_date = basic_info_document_type.updated_date,
                is_active = basic_info_document_type.is_active,
                created_user_id = basic_info_document_type.created_user_id,
                updated_user_id = basic_info_document_type.updated_user_id
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

        [HttpGet("ListAllDocumentTypes")]
        public async Task<IActionResult> ListAllDocumentTypes()
        {

            try
            {
                string commandText = "SELECT * FROM   basic_info_document_type";
                connection.Open();
                var groups = await connection.QueryAsync<DocumentType>(commandText);
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

        //************************* Gender *************************

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //ENDPOINT PARA CREAR UN NUEVO REGISTRO

        [HttpPost("RegisterGender")]
        public async Task<IActionResult> RegisterGenderAsync([FromBody] Gender basic_info_gender)
        {
            int result = -1;
            string insertQuery = "INSERT INTO basic_info_gender (id, name, description, created_date, updated_date, is_active, created_user_id, updated_user_id) VALUES (@id,@name,@description,@created_date,@updated_date,@is_active,@created_user_id,@updated_user_id) RETURNING Id";
            var queryArguments = new
            {
                id = basic_info_gender.id,
                name = basic_info_gender.name,
                description = basic_info_gender.description,
                created_date = basic_info_gender.created_date,
                updated_date = basic_info_gender.updated_date,
                is_active = basic_info_gender.is_active,
                created_user_id = basic_info_gender.created_user_id,
                updated_user_id = basic_info_gender.updated_user_id
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

        [HttpPost("UpdateGender")]
        public async Task<IActionResult> UpdateGender([FromBody] Gender basic_info_gender)
        {

            int result = -1;
            string insertQuery = "UPDATE basic_info_gender  SET name=@name, description=@description, created_date=@created_date, updated_date=@updated_date, is_active=@is_active, created_user_id=@created_user_id, updated_user_id=@updated_user_id WHERE id = @id"; var queryArguments = new
            {
                id = basic_info_gender.id,
                name = basic_info_gender.name,
                description = basic_info_gender.description,
                created_date = basic_info_gender.created_date,
                updated_date = basic_info_gender.updated_date,
                is_active = basic_info_gender.is_active,
                created_user_id = basic_info_gender.created_user_id,
                updated_user_id = basic_info_gender.updated_user_id
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

        [HttpPost("DeleteGender")]
        public async Task<IActionResult> DeleteGender([FromBody] Gender basic_info_gender)
        {

            int result = -1;
            string insertQuery = "DELETE FROM basic_info_gender WHERE id = @id";
            var queryArguments = new
            {
                id = basic_info_gender.id,
                name = basic_info_gender.name,
                description = basic_info_gender.description,
                created_date = basic_info_gender.created_date,
                updated_date = basic_info_gender.updated_date,
                is_active = basic_info_gender.is_active,
                created_user_id = basic_info_gender.created_user_id,
                updated_user_id = basic_info_gender.updated_user_id
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

        [HttpGet("ListAllGenders")]
        public async Task<IActionResult> ListAllGenders()
        {

            try
            {
                string commandText = "SELECT * FROM   basic_info_gender";
                connection.Open();
                var groups = await connection.QueryAsync<Gender>(commandText);
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
        //************************* RelationshipBusiness *************************

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //ENDPOINT PARA CREAR UN NUEVO REGISTRO

        [HttpPost("RegisterRelationshipBusiness")]
        public async Task<IActionResult> RegisterRelationshipBusinessAsync([FromBody] RelationshipBusiness basic_info_relationship_business)
        {
            int result = -1;
            string insertQuery = "INSERT INTO basic_info_relationship_business (id, name, description, created_date, updated_date, is_active, created_user_id, updated_user_id) VALUES (@id,@name,@description,@created_date,@updated_date,@is_active,@created_user_id,@updated_user_id) RETURNING Id";
            var queryArguments = new
            {
                id = basic_info_relationship_business.id,
                name = basic_info_relationship_business.name,
                description = basic_info_relationship_business.description,
                created_date = basic_info_relationship_business.created_date,
                updated_date = basic_info_relationship_business.updated_date,
                is_active = basic_info_relationship_business.is_active,
                created_user_id = basic_info_relationship_business.created_user_id,
                updated_user_id = basic_info_relationship_business.updated_user_id
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

        [HttpPost("UpdateRelationshipBusiness")]
        public async Task<IActionResult> UpdateRelationshipBusiness([FromBody] RelationshipBusiness basic_info_relationship_business)
        {

            int result = -1;
            string insertQuery = "UPDATE basic_info_relationship_business  SET name=@name, description=@description, created_date=@created_date, updated_date=@updated_date, is_active=@is_active, created_user_id=@created_user_id, updated_user_id=@updated_user_id WHERE id = @id"; var queryArguments = new
            {
                id = basic_info_relationship_business.id,
                name = basic_info_relationship_business.name,
                description = basic_info_relationship_business.description,
                created_date = basic_info_relationship_business.created_date,
                updated_date = basic_info_relationship_business.updated_date,
                is_active = basic_info_relationship_business.is_active,
                created_user_id = basic_info_relationship_business.created_user_id,
                updated_user_id = basic_info_relationship_business.updated_user_id
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

        [HttpPost("DeleteRelationshipBusiness")]
        public async Task<IActionResult> DeleteRelationshipBusiness([FromBody] RelationshipBusiness basic_info_relationship_business)
        {

            int result = -1;
            string insertQuery = "DELETE FROM basic_info_relationship_business WHERE id = @id";
            var queryArguments = new
            {
                id = basic_info_relationship_business.id,
                name = basic_info_relationship_business.name,
                description = basic_info_relationship_business.description,
                created_date = basic_info_relationship_business.created_date,
                updated_date = basic_info_relationship_business.updated_date,
                is_active = basic_info_relationship_business.is_active,
                created_user_id = basic_info_relationship_business.created_user_id,
                updated_user_id = basic_info_relationship_business.updated_user_id
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

        [HttpGet("ListAllRelationshipBusiness")]
        public async Task<IActionResult> ListAllRelationshipBusiness()
        {

            try
            {
                string commandText = "SELECT * FROM   basic_info_relationship_business";
                connection.Open();
                var groups = await connection.QueryAsync<RelationshipBusiness>(commandText);
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
        //************************* TypeOfDiner *************************

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------





        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //************************* Tradition *************************

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------




        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //************************* Culture *************************

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------




        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //************************* Religion *************************

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------




    }
}

