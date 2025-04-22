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
            string insertQuery = @"INSERT INTO clients_client (type, name, description, is_confirmated, created_date, updated_date, is_active, created_user_id, people_id, updated_user_id,type_people) 
							VALUES (@type,@name,@description,@is_confirmated,now(),now(),@is_active,@created_user_id,@people_id,@updated_user_id,@type_people) RETURNING Id";
            var queryArguments = new
            {
                
                type = clients_client.type,
                name = clients_client.name,
                description = clients_client.description,
                is_confirmated = clients_client.is_confirmated,
                is_active = clients_client.is_active,
                created_user_id = clients_client.created_user_id,
                people_id = clients_client.people_id,
                updated_user_id = clients_client.updated_user_id,
				type_people=clients_client.type_people
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
		[HttpPost("RegisterClientsAllForm")]
		public async Task<IActionResult> RegisterClientsAllForm([FromBody] RegisterClientsWithPeopleRequest request)
		{
			int peopleId = -1;

			string peopleInsertQuery = @"
			INSERT INTO basic_info_people 
			(first_name, last_name, document_number, description, created_date, updated_date, is_active, age_group_id, created_user_id, document_type_id, gender_id, type_of_diner_id, updated_user_id) 
			VALUES 
			(@first_name, @last_name, @	, @description, now(), now(), @is_active, @age_group_id, @created_user_id, @document_type_id, @gender_id, @type_of_diner_id, @updated_user_id) 
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
				//connection.Close();

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

					//connection.Open();
					var clientId = await connection.QuerySingleOrDefaultAsync<int>(clientInsertQuery, clientQueryArguments);
					//connection.Close();

					string locationInsertQuery = @"";
					
					if (request.Locations != null)
					{
						foreach (var location in request.Locations)
						{
							locationInsertQuery = @"INSERT INTO public.locations_location_tb (name, description, street1, street2, house_number, floor, building_name,
                                          latitude, longitude, observation, photo, is_main_location, created_date,
                                          updated_date, is_active, city_id, country_id, created_user_id, departament_id,
                                          id_location_type_id, updated_user_id)
										  values (@name, @description, @street1, @street2, @house_number, '1', 'test',
										  @latitude, @longitude, @observation, ' ', @is_main_location, now(),
										  now(), @is_active, @city_id, @country_id, @created_user_id, @departament_id,
										  @id_location_type_id, @updated_user_id) RETURNING id";

							var locationArguments = new
							{
								location.name,
								location.description,
								location.street1,
								location.street2,
								location.house_number,
							
								location.latitude,
								location.longitude,
								location.observation,
								
								location.is_main_location,
								location.is_active,
								location.city_id,
								location.country_id,	
								location.created_user_id,
								location.departament_id,
								location.id_location_type_id,
								location.updated_user_id
							};

							//connection.Open();
							var locationId = await connection.QuerySingleOrDefaultAsync<int>(locationInsertQuery, locationArguments);
							//connection.Close();

							var basicInfoPeopleLocationInsertQuery =
							@"INSERT INTO public.basic_info_people_location (people_id, location_tb_id, is_active, created_user_id, updated_user_id,
                                               created_date, updated_date)
							values (@people_id, @location_tb_id, @is_active, @created_user_id, @updated_user_id, now(), now())";

							var basicInfoPeopleLocationArguments = new
							{
								people_id = peopleId,
								location_tb_id = locationId,
								is_active = true,
								created_user_id = request.BasicInfoPeople.created_user_id,
								updated_user_id = request.BasicInfoPeople.updated_user_id
							};

							var basicInfoPeopleLocationId = await connection.ExecuteAsync(basicInfoPeopleLocationInsertQuery, basicInfoPeopleLocationArguments);
						}
					}

					string contactInsertQuery = @"";

					if (request.Contacts != null)
					{
						foreach (var contact in request.Contacts)
						{
							contactInsertQuery = @"INSERT INTO public.contacts_contact_tb (name, contact_data, verificated_token, is_verified, is_main_contact, description,
										  created_date, updated_date, is_active, table_name, contact_type_id, created_user_id,
										  updated_user_id)
										  values (@name, @contact_data, true, @is_verified, @is_main_contact, @description, now(),
										  now(), @is_active, ' ', 1, @created_user_id, @updated_user_id) RETURNING id";

							var contactArguments = new
							{
								contact.name,
								contact.contact_data,
								
								contact.is_verified,
								contact.is_main_contact,
								contact.description,
								contact.is_active,
								contact.created_user_id,
								contact.updated_user_id
							};

							//connection.Open();
							var contactId = await connection.QuerySingleOrDefaultAsync<int>(contactInsertQuery, contactArguments);
							//connection.Close();

							var basicInfoPeopleContactInsertQuery =
							@"INSERT INTO public.basic_info_people_contact (people_id, contact_tb_id, is_active, created_user_id, updated_user_id,
											   created_date, updated_date)
							values (@people_id, @contact_tb_id, @is_active, @created_user_id, @updated_user_id, now(), now())";

							var basicInfoPeopleContactArguments = new
							{
								people_id = peopleId,
								contact_tb_id = contactId,
								is_active = true,
								created_user_id = request.BasicInfoPeople.created_user_id,
								updated_user_id = request.BasicInfoPeople.updated_user_id
							};

							var basicInfoPeopleContactId = await connection.ExecuteAsync(basicInfoPeopleContactInsertQuery, basicInfoPeopleContactArguments);
						}
					}

					string businessInvoiceDataInsertQuery = @"";
					/* [Required] public string? name { get; set; }
        [Required] public string? document_number { get; set; }
        [Required] public string? description { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }*/

					if (request.BusinessInvoiceData != null)
					{
						foreach (var businessInvoiceData in request.BusinessInvoiceData)
						{
							businessInvoiceDataInsertQuery = @"INSERT INTO public.business_invoice_data (name, document_number, description, created_date, updated_date, is_active, created_user_id, updated_user_id)
										  values (@name, @document_number, @description, now(), now(), @is_active, @created_user_id, @updated_user_id) RETURNING id";

							var businessInvoiceDataArguments = new
							{
								businessInvoiceData.name,
								businessInvoiceData.document_number,
								businessInvoiceData.description,
								businessInvoiceData.is_active,
								businessInvoiceData.created_user_id,
								businessInvoiceData.updated_user_id
							};

							//connection.Open();
							var businessInvoiceDataId = await connection.QuerySingleOrDefaultAsync<int>(businessInvoiceDataInsertQuery, businessInvoiceDataArguments);
							//connection.Close();

							var basicInfoPeopleBusinessInvoiceDataInsertQuery =
							@"INSERT INTO public.basic_info_people_invoice_data (people_id, invoice_data_id, is_active, created_user_id, updated_user_id,
											   created_date, updated_date)
							values (@people_id, @invoice_data_id, @is_active, @created_user_id, @updated_user_id, now(), now())";

							var basicInfoPeopleBusinessInvoiceDataArguments = new
							{
								people_id = peopleId,
								invoice_data_id = businessInvoiceDataId,
								is_active = true,
								created_user_id = request.BasicInfoPeople.created_user_id,
								updated_user_id = request.BasicInfoPeople.updated_user_id
							};

							var basicInfoPeopleBusinessInvoiceDataId = await connection.ExecuteAsync(basicInfoPeopleBusinessInvoiceDataInsertQuery, basicInfoPeopleBusinessInvoiceDataArguments);
						}

					}	




					return Ok(new { clientId, peopleId });
				}

				return BadRequest("Error al insertar la persona");
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
		//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		[HttpGet("ListAllClientsWithPeoplePag")]
		public async Task<IActionResult> ListAllClientsWithPeoplePag([FromQuery] int page, [FromQuery] int pageSize)
		{
			try
			{
				string commandText = @"SELECT cc.id,bp.first_name,bp.last_name, bdt.name as document_type, bp.document_number, cc.type as status
						FROM clients_client  cc
						INNER JOIN basic_info_people bp ON cc.people_id = bp.id 
						join basic_info_document_type bdt on bp.document_type_id = bdt.id
						ORDER BY cc.id 
						LIMIT @pageSize OFFSET @page";
				connection.Open();
				var groups = await connection.QueryAsync<ClientPageResum>(
						commandText,
						new { page = page-1, pageSize = pageSize }
					);
				connection.Close();
				return Ok(groups);

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
				throw ex;

			}
		}

		#region getClietntdetail
		[HttpGet("{clientId}")]
		public async Task<IActionResult> GetClientDetails(int clientId)
		{
			try
			{
				connection.Open();

				// 1. Obtener el registro del cliente
				string clientQuery = @"SELECT * FROM clients_client WHERE id = @clientId";
				var client = await connection.QuerySingleOrDefaultAsync<Client>(clientQuery, new { clientId });
				if (client == null)
					return NotFound($"No se encontró cliente con id {clientId}");

				// 2. Obtener la información básica de la persona asociada al cliente
				string peopleQuery = @"SELECT * FROM basic_info_people WHERE id = @peopleId";
				var basicInfoPeople = await connection.QuerySingleOrDefaultAsync<People>(
					peopleQuery, new { peopleId = client.people_id });

				// 3. Obtener las ubicaciones asociadas a la persona
				string locationsQuery = @"
            SELECT l.*
            FROM locations_location_tb l
            INNER JOIN basic_info_people_location bp ON l.id = bp.location_tb_id
            WHERE bp.people_id = @peopleId";
				var locations = (await connection.QueryAsync<Locations>(
					locationsQuery, new { peopleId = client.people_id })).ToList();

				// 4. Obtener los contactos asociados a la persona
				string contactsQuery = @"
            SELECT ct.*
            FROM contacts_contact_tb ct
            INNER JOIN basic_info_people_contact bc ON ct.id = bc.contact_tb_id
            WHERE bc.people_id = @peopleId";
				var contacts = (await connection.QueryAsync<Contacts>(
					contactsQuery, new { peopleId = client.people_id })).ToList();

				// 5. Obtener los datos de factura asociados a la persona
				string invoiceDataQuery = @"
            SELECT bid.*
            FROM business_invoice_data bid
            INNER JOIN basic_info_people_invoice_data bi ON bid.id = bi.invoice_data_id
            WHERE bi.people_id = @peopleId";
				var invoiceData = (await connection.QueryAsync<BusinessInvoiceData>(
					invoiceDataQuery, new { peopleId = client.people_id })).ToList();

				// Construir el objeto de respuesta respetando los modelos
				var result = new
				{
					ClientsClient = client,
					BasicInfoPeople = basicInfoPeople,
					Locations = locations,
					Contacts = contacts,
					BusinessInvoiceData = invoiceData
				};

				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			finally
			{
				connection.Close();
			}
		}


		#endregion



	}
}

