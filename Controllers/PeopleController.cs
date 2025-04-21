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
using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace FairyBE.Controllers
{
	//especificaciones del api
	[ApiController]//con esto se especifica que va ser un controlador de API xq hay distintos tipos de controladores
	[Route("[controller]")]//la ruta que va utilizar sera el nombre del controlador
	public class PeopleController : Controller
	{
		#region VARIABLES INTERNAS
		private NpgsqlConnection connection;//Atributo para conectar con Postgresql
		public IConfiguration Configuration { get; }//Se inicializa la interfaz de configuracion
		#endregion
		#region CONSTRUCTOR
		public PeopleController(IConfiguration config)
		{
			//Se asigna la interfaz de configuraciion a la configuracion local
			Configuration = config;
			// Se obtiene la cadena de conexion alojada en el json de configuracion
			var connectionString = Configuration.GetConnectionString("DefaultConnection");
			//aqui se crea la conexion a la bd
			connection = new NpgsqlConnection(connectionString);
		}
		#endregion

		#region People
		#region Register People
		[HttpPost("RegisterPeople")]
		public async Task<IActionResult> RegisterPeopleAsync([FromBody] People basic_info_people)
		{

			int result = -1;
			string insertQuery = "INSERT INTO basic_info_people (first_name, last_name, document_number, photo_people, date_of_birth, date_of_death, description, created_date, updated_date, is_active, age_group_id, created_user_id, document_type_id, gender_id, type_of_diner_id, updated_user_id) VALUES (@first_name,@last_name,@document_number,@photo_people,@date_of_birth,@date_of_death,@description,now(),now(),@is_active,@age_group_id,@created_user_id,@document_type_id,@gender_id,@type_of_diner_id,@updated_user_id,@religion_id) RETURNING id";
			var queryArguments = new
			{
				first_name = basic_info_people.first_name,
				last_name = basic_info_people.last_name,
				document_number = basic_info_people.document_number,
				photo_people = basic_info_people.photo_people,
				date_of_birth = basic_info_people.date_of_birth,
				date_of_death = basic_info_people.date_of_death,
				description = basic_info_people.description,
				created_date = basic_info_people.created_date,
				updated_date = basic_info_people.updated_date,
				is_active = basic_info_people.is_active,
				age_group_id = basic_info_people.age_group_id,
				created_user_id = basic_info_people.created_user_id,
				document_type_id = basic_info_people.document_type_id,
				gender_id = basic_info_people.gender_id,
				type_of_diner_id = basic_info_people.type_of_diner_id,
				updated_user_id = basic_info_people.updated_user_id,
				religion_id = basic_info_people.religion_id

			};
			try
			{
				connection.Open();
				// Aquí usamos QuerySingleOrDefaultAsync para obtener el ID
				var id = await connection.QuerySingleOrDefaultAsync<int>(insertQuery, queryArguments);

				connection.Close();

				// Retornar el ID insertado
				return Ok(new { id = id });
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		#endregion

		#region Update People
		[HttpPost("UpdatePeople")]
		public async Task<IActionResult> UpdatePeople([FromBody] People basic_info_people)
		{

			int result = -1;
			string insertQuery = @"UPDATE basic_info_people  SET first_name=@first_name, last_name=@last_name, document_number=@document_number, photo_people=@photo_people, date_of_birth=@date_of_birth, date_of_death=@date_of_death, description=@description, created_date=@created_date, updated_date=now(), is_active=@is_active, age_group_id=@age_group_id, created_user_id=@created_user_id, document_type_id=@document_type_id, gender_id=@gender_id, type_of_diner_id=@type_of_diner_id, updated_user_id=@updated_user_id WHERE id = @id";
			var queryArguments = new
			{
				id = basic_info_people.id,
				first_name = basic_info_people.first_name,
				last_name = basic_info_people.last_name,
				document_number = basic_info_people.document_number,
				photo_people = basic_info_people.photo_people,
				date_of_birth = basic_info_people.date_of_birth,
				date_of_death = basic_info_people.date_of_death,
				description = basic_info_people.description,
				created_date = basic_info_people.created_date,
				updated_date = basic_info_people.updated_date,
				is_active = basic_info_people.is_active,
				age_group_id = basic_info_people.age_group_id,
				created_user_id = basic_info_people.created_user_id,
				document_type_id = basic_info_people.document_type_id,
				gender_id = basic_info_people.gender_id,
				type_of_diner_id = basic_info_people.type_of_diner_id,
				updated_user_id = basic_info_people.updated_user_id
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
		#region Delete People
		[HttpPost("DeletePeople")]
		public async Task<IActionResult> DeletePeople([FromBody] People basic_info_people)
		{

			int result = -1;
			string insertQuery = "DELETE FROM basic_info_people WHERE id = @id";
			var queryArguments = new
			{
				id = basic_info_people.id,
				first_name = basic_info_people.first_name,
				last_name = basic_info_people.last_name,
				document_number = basic_info_people.document_number,
				photo_people = basic_info_people.photo_people,
				date_of_birth = basic_info_people.date_of_birth,
				date_of_death = basic_info_people.date_of_death,
				description = basic_info_people.description,
				created_date = basic_info_people.created_date,
				updated_date = basic_info_people.updated_date,
				is_active = basic_info_people.is_active,
				age_group_id = basic_info_people.age_group_id,
				created_user_id = basic_info_people.created_user_id,
				document_type_id = basic_info_people.document_type_id,
				gender_id = basic_info_people.gender_id,
				type_of_diner_id = basic_info_people.type_of_diner_id,
				updated_user_id = basic_info_people.updated_user_id
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
		#region
		[HttpGet("ListAllPeoples")]
		public async Task<IActionResult> ListAllPeoples()
		{

			try
			{
				string commandText = "SELECT * FROM   basic_info_people";
				connection.Open();
				var groups = await connection.QueryAsync<People>(commandText);
				connection.Close();
				return Ok(groups);

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);

			}
		}
		#endregion

		#region
		[HttpGet("GetPeoplePaginated")]
		public async Task<IActionResult> GetPeoplePaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
		{
			try
			{
				if (page < 1 || pageSize < 1)
				{
					return BadRequest("Page and pageSize must be greater than 0.");
				}

				int offset = (page - 1) * pageSize;

				string query = @"
            SELECT * 
            FROM basic_info_people 
            ORDER BY id 
            LIMIT @pageSize OFFSET @offset";

				var people = await connection.QueryAsync<People>(query, new { pageSize, offset });

				return Ok(people);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		#endregion
		#region
		[HttpGet("SearchPeople")]
		public async Task<IActionResult> SearchPeople(
		[FromQuery] string? document_number = null,
		[FromQuery] int page = 1,
		[FromQuery] int pageSize = 10)
		{
			try
			{
				if (page < 1 || pageSize < 1)
				{
					return BadRequest("Page and pageSize must be greater than 0.");
				}

				int offset = (page - 1) * pageSize;

				string query = @"
            SELECT * 
            FROM basic_info_people 
            WHERE (@document_number IS NULL OR document_number LIKE @document_number)
            ORDER BY id 
            LIMIT @pageSize OFFSET @offset";

				var people = await connection.QueryAsync<People>(query, new
				{
					document_number = string.IsNullOrEmpty(document_number) ? null : $"%{document_number}%",
					pageSize,
					offset
				});

				return Ok(people);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		#endregion
		#region
		[HttpGet("PeobleById")]
		public async Task<IActionResult> PeobleByDocumentNumber([FromQuery] string id)
		{

			try
			{
				string commandText = @"SELECT * FROM   basic_info_people where id=@id";
				var queryArguments = new
				{
					id
				};
				connection.Open();
				var groups = await connection.QueryAsync<People>(commandText);
				connection.Close();
				return Ok(groups);

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);

			}
		}
		#endregion

		#region
		[HttpGet("PeobleByDocumentNumber")]
		public async Task<IActionResult> PeobleByDocumentNumber([FromQuery] string document_number, [FromQuery] int document_type_id)
		{

			try
			{
				string query = @"SELECT * FROM basic_info_people where document_number=@document_number and document_type_id=@document_type_id";
				var queryArguments = new
				{
					document_number = document_number,
					document_type_id = document_type_id
				};
				connection.Open();
				Debug.WriteLine(query);
				Debug.WriteLine(queryArguments.ToString());
				var result = await connection.QueryAsync<People>(query, queryArguments);
				connection.Close();
				return Ok(result);

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);

			}
		}
		#endregion

		#endregion

		#region PeopleContact
		#region Register PeopleContact
		[HttpPost("RegisterPeopleContact")]
		public async Task<IActionResult> RegisterPeopleContactAsync([FromBody] PeopleContact basic_info_people_contact)
		{

			int result = -1;
			string insertQuery = "INSERT INTO basic_info_people_contact (people_id, contact_tb_id, is_active, created_user_id, updated_user_id, created_date, updated_date) VALUES (@people_id,@contact_tb_id,@is_active,@created_user_id,@updated_user_id,@created_date,@updated_date) RETURNING id";
			var queryArguments = new
			{
				people_id = basic_info_people_contact.people_id,
				contact_tb_id = basic_info_people_contact.contact_tb_id,
				is_active = basic_info_people_contact.is_active,
				created_date = basic_info_people_contact.created_date,
				updated_date = basic_info_people_contact.updated_date,
				created_user_id = basic_info_people_contact.created_user_id,
				updated_user_id = basic_info_people_contact.updated_user_id

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

		#region Update PeopleContact
		[HttpPost("UpdatePeopleContact")]
		public async Task<IActionResult> UpdatePeopleContact([FromBody] PeopleContact basic_info_people_contact)
		{

			int result = -1;
			string insertQuery = @"UPDATE basic_info_people_contact  SET people_id=@people_id, contact_tb_id=@contact_tb_id, is_active=@is_active, created_user_id=@created_user_id, updated_user_id=@updated_user_id, created_date=now(), updated_date=now()	WHERE id = @id";
			var queryArguments = new
			{
				id = basic_info_people_contact.id,
				people_id = basic_info_people_contact.people_id,
				contact_tb_id = basic_info_people_contact.contact_tb_id,
				is_active = basic_info_people_contact.is_active,
				created_date = basic_info_people_contact.created_date,
				updated_date = basic_info_people_contact.updated_date,
				created_user_id = basic_info_people_contact.created_user_id,
				updated_user_id = basic_info_people_contact.updated_user_id
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
		#region Delete PeopleContact
		[HttpPost("DeletePeopleContact")]
		public async Task<IActionResult> DeletePeopleContact([FromBody] PeopleContact basic_info_people_contact)
		{

			int result = -1;
			string insertQuery = "DELETE FROM basic_info_people_contact WHERE id = @id";
			var queryArguments = new
			{
				id = basic_info_people_contact.id,
				people_id = basic_info_people_contact.people_id,
				contact_tb_id = basic_info_people_contact.contact_tb_id,
				is_active = basic_info_people_contact.is_active,
				created_date = basic_info_people_contact.created_date,
				updated_date = basic_info_people_contact.updated_date,
				created_user_id = basic_info_people_contact.created_user_id,
				updated_user_id = basic_info_people_contact.updated_user_id
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
		#region
		[HttpGet("ListAllPeopleContacts")]
		public async Task<IActionResult> ListAllPeopleContacts()
		{

			try
			{
				string commandText = "SELECT * FROM   basic_info_people_contact";
				connection.Open();
				var groups = await connection.QueryAsync<PeopleContact>(commandText);
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

		#region PeopleInvoiceData
		#region Register PeopleInvoiceData
		[HttpPost("RegisterPeopleInvoiceData")]
		public async Task<IActionResult> RegisterPeopleInvoiceDataAsync([FromBody] PeopleInvoiceData basic_info_people_invoice_data)
		{

			int result = -1;
			string insertQuery = "INSERT INTO basic_info_people_invoice_data (people_id, invoice_data_id, is_active, created_date, updated_date, created_user_id, updated_user_id) VALUES (@people_id,@invoice_data_id,@is_active,@created_date,@updated_date,@created_user_id,@updated_user_id) RETURNING id";
			var queryArguments = new
			{
				people_id = basic_info_people_invoice_data.people_id,
				invoice_data_id = basic_info_people_invoice_data.invoice_data_id,
				is_active = basic_info_people_invoice_data.is_active,
				created_date = basic_info_people_invoice_data.created_date,
				updated_date = basic_info_people_invoice_data.updated_date,
				created_user_id = basic_info_people_invoice_data.created_user_id,
				updated_user_id = basic_info_people_invoice_data.updated_user_id

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

		#region Update PeopleInvoiceData
		[HttpPost("UpdatePeopleInvoiceData")]
		public async Task<IActionResult> UpdatePeopleInvoiceData([FromBody] PeopleInvoiceData basic_info_people_invoice_data)
		{

			int result = -1;
			string insertQuery = @"UPDATE basic_info_people_invoice_data  SET people_id=@people_id, invoice_data_id=@invoice_data_id, is_active=@is_active, created_date=@created_date, updated_date=now(), created_user_id=@created_user_id, updated_user_id=@updated_user_id WHERE id = @id";
			var queryArguments = new
			{
				id = basic_info_people_invoice_data.id,
				people_id = basic_info_people_invoice_data.people_id,
				invoice_data_id = basic_info_people_invoice_data.invoice_data_id,
				is_active = basic_info_people_invoice_data.is_active,
				created_date = basic_info_people_invoice_data.created_date,
				updated_date = basic_info_people_invoice_data.updated_date,
				created_user_id = basic_info_people_invoice_data.created_user_id,
				updated_user_id = basic_info_people_invoice_data.updated_user_id
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
		#region Delete PeopleInvoiceData
		[HttpPost("DeletePeopleInvoiceData")]
		public async Task<IActionResult> DeletePeopleInvoiceData([FromBody] PeopleInvoiceData basic_info_people_invoice_data)
		{

			int result = -1;
			string insertQuery = "DELETE FROM basic_info_people_invoice_data WHERE id = @id";
			var queryArguments = new
			{
				id = basic_info_people_invoice_data.id,
				people_id = basic_info_people_invoice_data.people_id,
				invoice_data_id = basic_info_people_invoice_data.invoice_data_id,
				is_active = basic_info_people_invoice_data.is_active,
				created_date = basic_info_people_invoice_data.created_date,
				updated_date = basic_info_people_invoice_data.updated_date,
				created_user_id = basic_info_people_invoice_data.created_user_id,
				updated_user_id = basic_info_people_invoice_data.updated_user_id
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
		#region
		[HttpGet("ListAllPeopleInvoiceDatas")]
		public async Task<IActionResult> ListAllPeopleInvoiceDatas()
		{

			try
			{
				string commandText = "SELECT * FROM   basic_info_people_invoice_data";
				connection.Open();
				var groups = await connection.QueryAsync<PeopleInvoiceData>(commandText);
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

		#region PeopleLocation
		#region Register PeopleLocation
		[HttpPost("RegisterPeopleLocation")]
		public async Task<IActionResult> RegisterPeopleLocationAsync([FromBody] PeopleLocation basic_info_people_location)
		{

			int result = -1;
			string insertQuery = "INSERT INTO basic_info_people_location (people_id, location_tb_id, is_active, created_user_id, updated_user_id, created_date, updated_date) VALUES (@people_id,@location_tb_id,@is_active,@created_user_id,@updated_user_id,now(),now()) RETURNING id";
			var queryArguments = new
			{
				people_id = basic_info_people_location.people_id,
				location_tb_id = basic_info_people_location.location_tb_id,
				is_active = basic_info_people_location.is_active,
				created_date = basic_info_people_location.created_date,
				updated_date = basic_info_people_location.updated_date,
				created_user_id = basic_info_people_location.created_user_id,
				updated_user_id = basic_info_people_location.updated_user_id

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

		#region Update PeopleLocation
		[HttpPost("UpdatePeopleLocation")]
		public async Task<IActionResult> UpdatePeopleLocation([FromBody] PeopleLocation basic_info_people_location)
		{

			int result = -1;
			string insertQuery = @"UPDATE basic_info_people_location  SET people_id=@people_id, location_tb_id=@location_tb_id, is_active=@is_active, created_user_id=@created_user_id, updated_user_id=@updated_user_id, created_date=@created_date, updated_date=now() WHERE id = @id";
			var queryArguments = new
			{
				id = basic_info_people_location.id,
				people_id = basic_info_people_location.people_id,
				location_tb_id = basic_info_people_location.location_tb_id,
				is_active = basic_info_people_location.is_active,
				created_date = basic_info_people_location.created_date,
				updated_date = basic_info_people_location.updated_date,
				created_user_id = basic_info_people_location.created_user_id,
				updated_user_id = basic_info_people_location.updated_user_id
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
		#region Delete PeopleLocation
		[HttpPost("DeletePeopleLocation")]
		public async Task<IActionResult> DeletePeopleLocation([FromBody] PeopleLocation basic_info_people_location)
		{

			int result = -1;
			string insertQuery = "DELETE FROM basic_info_people_location WHERE id = @id";
			var queryArguments = new
			{
				id = basic_info_people_location.id,
				people_id = basic_info_people_location.people_id,
				location_tb_id = basic_info_people_location.location_tb_id,
				is_active = basic_info_people_location.is_active,
				created_date = basic_info_people_location.created_date,
				updated_date = basic_info_people_location.updated_date,
				created_user_id = basic_info_people_location.created_user_id,
				updated_user_id = basic_info_people_location.updated_user_id
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
		#region
		[HttpGet("ListAllPeopleLocations")]
		public async Task<IActionResult> ListAllPeopleLocations()
		{

			try
			{
				string commandText = "SELECT * FROM   basic_info_people_location";
				connection.Open();
				var groups = await connection.QueryAsync<PeopleLocation>(commandText);
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

		#region PeopleRelationshipBusiness
		#region Register PeopleRelationshipBusiness
		[HttpPost("RegisterPeopleRelationshipBusiness")]
		public async Task<IActionResult> RegisterPeopleRelationshipBusinessAsync([FromBody] PeopleRelationshipBusiness basic_info_people_relationship_business)
		{

			int result = -1;
			string insertQuery = "INSERT INTO basic_info_people_relationship_business (people_id, relationship_business_id, is_active, created_user_id, updated_user_id, created_date, updated_date) VALUES (@people_id,@relationship_business_id,@is_active,@created_user_id,@updated_user_id,now(),now()) RETURNING id";
			var queryArguments = new
			{
				people_id = basic_info_people_relationship_business.people_id,
				relationship_business_id = basic_info_people_relationship_business.relationship_business_id,
				is_active = basic_info_people_relationship_business.is_active,
				created_date = basic_info_people_relationship_business.created_date,
				updated_date = basic_info_people_relationship_business.updated_date,
				created_user_id = basic_info_people_relationship_business.created_user_id,
				updated_user_id = basic_info_people_relationship_business.updated_user_id
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

		#region Update PeopleRelationshipBusiness
		[HttpPost("UpdatePeopleRelationshipBusiness")]
		public async Task<IActionResult> UpdatePeopleRelationshipBusiness([FromBody] PeopleRelationshipBusiness basic_info_people_relationship_business)
		{

			int result = -1;
			string insertQuery = @"UPDATE basic_info_people_relationship_business  SET people_id=@people_id, relationship_business_id=@relationship_business_id, is_active=@is_active, created_user_id=@created_user_id, updated_user_id=@updated_user_id, created_date=@created_date, updated_date=now()	WHERE id = @id";
			var queryArguments = new
			{
				id = basic_info_people_relationship_business.id,
				people_id = basic_info_people_relationship_business.people_id,
				relationship_business_id = basic_info_people_relationship_business.relationship_business_id,
				is_active = basic_info_people_relationship_business.is_active,
				created_date = basic_info_people_relationship_business.created_date,
				updated_date = basic_info_people_relationship_business.updated_date,
				created_user_id = basic_info_people_relationship_business.created_user_id,
				updated_user_id = basic_info_people_relationship_business.updated_user_id
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
		#region Delete PeopleRelationshipBusiness
		[HttpPost("DeletePeopleRelationshipBusiness")]
		public async Task<IActionResult> DeletePeopleRelationshipBusiness([FromBody] PeopleRelationshipBusiness basic_info_people_relationship_business)
		{

			int result = -1;
			string insertQuery = "DELETE FROM basic_info_people_relationship_business WHERE id = @id";
			var queryArguments = new
			{
				id = basic_info_people_relationship_business.id,
				people_id = basic_info_people_relationship_business.people_id,
				relationship_business_id = basic_info_people_relationship_business.relationship_business_id,
				is_active = basic_info_people_relationship_business.is_active,
				created_date = basic_info_people_relationship_business.created_date,
				updated_date = basic_info_people_relationship_business.updated_date,
				created_user_id = basic_info_people_relationship_business.created_user_id,
				updated_user_id = basic_info_people_relationship_business.updated_user_id
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
		#region ListAllPeopleRelationshipBusinesss
		[HttpGet("ListAllPeopleRelationshipBusinesss")]
		public async Task<IActionResult> ListAllPeopleRelationshipBusinesss()
		{

			try
			{
				string commandText = "SELECT * FROM   basic_info_people_relationship_business";
				connection.Open();
				var groups = await connection.QueryAsync<PeopleRelationshipBusiness>(commandText);
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

		#region registerPeopleWithAllData
		[HttpPost("RegisterPeopleWithAllData")]
		public async Task<IActionResult> RegisterClientsAllForm([FromBody] RegisterPeopleWithDataRequest request)
		{
			int peopleId = -1;

			string peopleInsertQuery = @"
			INSERT INTO basic_info_people 
			(first_name, last_name, document_number, description, created_date, updated_date, is_active, age_group_id, created_user_id, document_type_id, gender_id, type_of_diner_id, updated_user_id) 
			VALUES 
			(@first_name, @last_name, @document_number	, @description, now(), now(), @is_active, @age_group_id, @created_user_id, @document_type_id, @gender_id, @type_of_diner_id, @updated_user_id) 
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
					return Ok(new { peopleId });
				}
				return BadRequest("Error al insertar la persona");
			}
			catch (Exception ex)
			{
				connection.Close();
				return BadRequest(ex.Message);
			}
		} 
		#endregion
	}
}