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

    //Definición del CONTROLADOR DEL API ClientsController
    public class ClientsController : Controller
    {


        private NpgsqlConnection connection;//Atributo para conectar con Postgresql
        public IConfiguration Configuration { get; }//Se inicializa la interfaz de configuracion

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //CREAMOS UN CONSTRUCTOR DE LA CLASE PARA INICIALIZAR LA CONEXION A LA BD
        public ClientsController(IConfiguration config)
        {
            //Se asigna la interfaz de configuraciion a la configuracion local
            Configuration = config;
            // Se obtiene la cadena de conexion alojada en el json de configuracion
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            //aqui se crea la conexion a la bd
            connection = new NpgsqlConnection(connectionString);
        }

        //AQUI CONFIGURAMOS UN ENDPOINT (la ultima palabra de la URL que define la funcion a la que va llamar)
        [HttpPost("RegisterClients")]
        public async Task<IActionResult> RegisterClientsAsync([FromBody] Client clients_client)
        {
            int result = -1;
            string insertQuery = "INSERT INTO clients_client (id, type, name, description, is_confirmated, created_date, updated_date, is_active, created_user_id, people_id, updated_user_id) VALUES (@id,@type,@name,@description,@is_confirmated,@created_date,@updated_date,@is_active,@created_user_id,@people_id,@updated_user_id) RETURNING Id";
            var queryArguments = new
            {
                id = clients_client.id,
                type = clients_client.type,
                name = clients_client.name,
                description = clients_client.description,
                is_confirmated = clients_client.is_confirmated,
                created_date = clients_client.created_date,
                updated_date = clients_client.updated_date,
                is_active = clients_client.is_active,
                created_user_id = clients_client.created_user_id,
                people_id = clients_client.people_id,
                updated_user_id = clients_client.updated_user_id
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
		///---------------------
		[HttpPost("RegisterClientsWithPeopleStep1")]
		public async Task<IActionResult> RegisterClientsWithPeopleStep1Async([FromBody] RegisterClientsWithPeopleRequest request)
		{
			int peopleId = -1;

			string peopleInsertQuery = @"
			INSERT INTO basic_info_people 
			(first_name, last_name, document_number, description, created_date, updated_date, is_active, age_group_id, created_user_id, document_type_id, gender_id, type_of_diner_id, updated_user_id) 
			VALUES 
			(@first_name, @last_name, @document_number, @description, now(), now(), @is_active, @age_group_id, @created_user_id, @document_type_id, @gender_id, @type_of_diner_id, @updated_user_id) 
			RETURNING id";

			var peopleQueryArguments = new
			{
				request.BasicInfoPeople.first_name,
				request.BasicInfoPeople.last_name,
				request.BasicInfoPeople.document_number,
				request.BasicInfoPeople.description,
				request.BasicInfoPeople.is_active,
				request.BasicInfoPeople.age_group_id,
				request.BasicInfoPeople.created_user_id,
				request.BasicInfoPeople.document_type_id,
				request.BasicInfoPeople.gender_id,
				request.BasicInfoPeople.type_of_diner_id,
				request.BasicInfoPeople.updated_user_id
			};

			try
			{
				connection.Open();
				peopleId = await connection.QuerySingleOrDefaultAsync<int>(peopleInsertQuery, peopleQueryArguments);
				connection.Close();

				if (peopleId > 0)
				{
					string clientInsertQuery = @"
                INSERT INTO clients_client 
                (type, name, description, is_confirmated, created_date, updated_date, is_active, created_user_id, people_id, updated_user_id) 
                VALUES 
                (@type, @name, @description, @is_confirmated, now(), now(), @is_active, @created_user_id, @people_id, @updated_user_id) 
                RETURNING id";

					var clientQueryArguments = new
					{
						request.ClientsClient.type,
						request.ClientsClient.name,
						request.ClientsClient.description,
						request.ClientsClient.is_confirmated,
						request.ClientsClient.is_active,
						request.ClientsClient.created_user_id,
						people_id = peopleId,
						request.ClientsClient.updated_user_id
					};

					connection.Open();
					var clientId = await connection.QuerySingleOrDefaultAsync<int>(clientInsertQuery, clientQueryArguments);
					connection.Close();

					return Ok(new { clientId, peopleId });
				}

				return BadRequest("Failed to insert People.");
			}
			catch (Exception ex)
			{
				connection.Close();
				return BadRequest(ex.Message);
			}
		}

		//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		[HttpPost("UpdateClients")]
        public async Task<IActionResult> UpdateClients([FromBody] Client clients_client)
        {

            int result = -1;
            string insertQuery = "UPDATE clients_client  SET type=@type, name=@name, description=@description, is_confirmated=@is_confirmated, created_date=@created_date, updated_date=@updated_date, is_active=@is_active, created_user_id=@created_user_id, people_id=@people_id, updated_user_id=@updated_user_id WHERE id = @id";
            var queryArguments = new
            {
                id = clients_client.id,
                type = clients_client.type,
                name = clients_client.name,
                description = clients_client.description,
                is_confirmated = clients_client.is_confirmated,
                created_date = clients_client.created_date,
                updated_date = clients_client.updated_date,
                is_active = clients_client.is_active,
                created_user_id = clients_client.created_user_id,
                people_id = clients_client.people_id,
                updated_user_id = clients_client.updated_user_id
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
        [HttpPost("DeleteClients")]
        public async Task<IActionResult> DeleteClients([FromBody] Client clients_client)
        {

            int result = -1;
            string insertQuery = "DELETE FROM clients_client WHERE id = @id";
            var queryArguments = new
            {
                id = clients_client.id,
                type = clients_client.type,
                name = clients_client.name,
                description = clients_client.description,
                is_confirmated = clients_client.is_confirmated,
                created_date = clients_client.created_date,
                updated_date = clients_client.updated_date,
                is_active = clients_client.is_active,
                created_user_id = clients_client.created_user_id,
                people_id = clients_client.people_id,
                updated_user_id = clients_client.updated_user_id
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
        [HttpGet("ListAllClients")]
        public async Task<IActionResult> ListAllClientss()
        {

            try
            {
                string commandText = "SELECT * FROM   clients_client";
                connection.Open();
                var groups = await connection.QueryAsync<Client>(commandText);
                connection.Close();
                return Ok(groups);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw ex;

            }
        }
    }
}

