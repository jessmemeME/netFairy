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
            string insertQuery = "INSERT INTO basic_info_people (first_name, last_name, document_number, photo_people, date_of_birth, date_of_death, description, created_date, updated_date, is_active, age_group_id, created_user_id, document_type_id, gender_id, type_of_diner_id, updated_user_id) VALUES (@first_name,@last_name,@document_number,@photo_people,@date_of_birth,@date_of_death,@description,now(),now(),@is_active,@age_group_id,@created_user_id,@document_type_id,@gender_id,@type_of_diner_id,@updated_user_id) RETURNING id";
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


    }
}
