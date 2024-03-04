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
    public class CompanyController : Controller
    {
        #region VARIABLES INTERNAS
        private NpgsqlConnection connection;//Atributo para conectar con Postgresql
        public IConfiguration Configuration { get; }//Se inicializa la interfaz de configuracion
        #endregion
        #region CONSTRUCTOR
        public CompanyController(IConfiguration config)
        {
            //Se asigna la interfaz de configuraciion a la configuracion local
            Configuration = config;
            // Se obtiene la cadena de conexion alojada en el json de configuracion
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            //aqui se crea la conexion a la bd
            connection = new NpgsqlConnection(connectionString);
        }
        #endregion

        #region Company
        #region Register Company
        [HttpPost("RegisterCompany")]
        public async Task<IActionResult> RegisterCompanyAsync([FromBody] Company basic_info_company)
        {

            int result = -1;
            string insertQuery = "INSERT INTO basic_info_company (name, document_number, logo_company, description, created_date, updated_date, is_active, created_user_id, document_type_id, updated_user_id) VALUES (@name,@document_number,@logo_company,@description,now(),now(),@is_active,@created_user_id,@document_type_id,@updated_user_id) RETURNING id";
            var queryArguments = new
            {
                name = basic_info_company.name,
                document_number = basic_info_company.document_number,
                logo_company = basic_info_company.logo_company,
                description = basic_info_company.description,
                is_active = basic_info_company.is_active,
                created_date = basic_info_company.created_date,
                updated_date = basic_info_company.updated_date,
                created_user_id = basic_info_company.created_user_id,
                document_type_id = basic_info_company.document_type_id,
                updated_user_id = basic_info_company.updated_user_id
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

        #region Update Company
        [HttpPost("UpdateCompany")]
        public async Task<IActionResult> UpdateCompany([FromBody] Company basic_info_company)
        {

            int result = -1;
            string insertQuery = @"UPDATE basic_info_company  SET name=@name, document_number=@document_number, logo_company=@logo_company, description=@description, created_date=@created_date, updated_date=now(), is_active=@is_active, created_user_id=@created_user_id, document_type_id=@document_type_id, updated_user_id=@updated_user_id WHERE id = @id";
            var queryArguments = new
            {
                id = basic_info_company.id,
                name = basic_info_company.name,
                document_number = basic_info_company.document_number,
                logo_company = basic_info_company.logo_company,
                description = basic_info_company.description,
                is_active = basic_info_company.is_active,
                created_date = basic_info_company.created_date,
                updated_date = basic_info_company.updated_date,
                created_user_id = basic_info_company.created_user_id,
                document_type_id = basic_info_company.document_type_id,
                updated_user_id = basic_info_company.updated_user_id
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
        #region Delete Company
        [HttpPost("DeleteCompany")]
        public async Task<IActionResult> DeleteCompany([FromBody] Company basic_info_company)
        {

            int result = -1;
            string insertQuery = "DELETE FROM basic_info_company WHERE id = @id";
            var queryArguments = new
            {
                id = basic_info_company.id,
                name = basic_info_company.name,
                document_number = basic_info_company.document_number,
                logo_company = basic_info_company.logo_company,
                description = basic_info_company.description,
                is_active = basic_info_company.is_active,
                created_date = basic_info_company.created_date,
                updated_date = basic_info_company.updated_date,
                created_user_id = basic_info_company.created_user_id,
                document_type_id = basic_info_company.document_type_id,
                updated_user_id = basic_info_company.updated_user_id
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
        #region ListAllCompanys
        [HttpGet("ListAllCompanys")]
        public async Task<IActionResult> ListAllCompanys()
        {

            try
            {
                string commandText = "SELECT * FROM   basic_info_company";
                connection.Open();
                var groups = await connection.QueryAsync<Company>(commandText);
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

        #region CompanyContact
        #region Register CompanyContact
        [HttpPost("RegisterCompanyContact")]
        public async Task<IActionResult> RegisterCompanyContactAsync([FromBody] CompanyContact basic_info_company_contact)
        {

            int result = -1;
            string insertQuery = "INSERT INTO basic_info_company_contact (company_id, contact_tb_id, is_active, created_user_id, updated_user_id, created_date, updated_date) VALUES (@company_id,@contact_tb_id,@is_active,@created_user_id,@updated_user_id,now(),now()) RETURNING id";
            var queryArguments = new
            {
                company_id = basic_info_company_contact.company_id,
                contact_tb_id = basic_info_company_contact.contact_tb_id,
                is_active = basic_info_company_contact.is_active,
                created_date = basic_info_company_contact.created_date,
                updated_date = basic_info_company_contact.updated_date,
                created_user_id = basic_info_company_contact.created_user_id,
                updated_user_id = basic_info_company_contact.updated_user_id

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

        #region Update CompanyContact
        [HttpPost("UpdateCompanyContact")]
        public async Task<IActionResult> UpdateCompanyContact([FromBody] CompanyContact basic_info_company_contact)
        {

            int result = -1;
            string insertQuery = @"UPDATE basic_info_company_contact  SET company_id=@company_id, contact_tb_id=@contact_tb_id, is_active=@is_active, created_user_id=@created_user_id, updated_user_id=@updated_user_id, created_date=@created_date, updated_date=now() WHERE id = @id";
            var queryArguments = new
            {
                id = basic_info_company_contact.id,
                company_id = basic_info_company_contact.company_id,
                contact_tb_id = basic_info_company_contact.contact_tb_id,
                is_active = basic_info_company_contact.is_active,
                created_date = basic_info_company_contact.created_date,
                updated_date = basic_info_company_contact.updated_date,
                created_user_id = basic_info_company_contact.created_user_id,
                updated_user_id = basic_info_company_contact.updated_user_id
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
        #region Delete CompanyContact
        [HttpPost("DeleteCompanyContact")]
        public async Task<IActionResult> DeleteCompanyContact([FromBody] CompanyContact basic_info_company_contact)
        {

            int result = -1;
            string insertQuery = "DELETE FROM basic_info_company_contact WHERE id = @id";
            var queryArguments = new
            {
                id = basic_info_company_contact.id,
                company_id = basic_info_company_contact.company_id,
                contact_tb_id = basic_info_company_contact.contact_tb_id,
                is_active = basic_info_company_contact.is_active,
                created_date = basic_info_company_contact.created_date,
                updated_date = basic_info_company_contact.updated_date,
                created_user_id = basic_info_company_contact.created_user_id,
                updated_user_id = basic_info_company_contact.updated_user_id
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
        #region ListAllCompanyContacts
        [HttpGet("ListAllCompanyContacts")]
        public async Task<IActionResult> ListAllCompanyContacts()
        {

            try
            {
                string commandText = "SELECT * FROM   basic_info_company_contact";
                connection.Open();
                var groups = await connection.QueryAsync<CompanyContact>(commandText);
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

        #region CompanyInvoiceData
        #region Register CompanyInvoiceData
        [HttpPost("RegisterCompanyInvoiceData")]
        public async Task<IActionResult> RegisterCompanyInvoiceDataAsync([FromBody] CompanyInvoiceData basic_info_company_invoice_data)
        {

            int result = -1;
            string insertQuery = "INSERT INTO basic_info_company_invoice_data (company_id, invoice_data_id, is_active, created_user_id, updated_user_id, created_date, updated_date) VALUES (@company_id,@invoice_data_id,@is_active,@created_user_id,@updated_user_id,now(),now()) RETURNING id";
            var queryArguments = new
            {
                company_id = basic_info_company_invoice_data.company_id,
                invoice_data_id = basic_info_company_invoice_data.invoice_data_id,
                is_active = basic_info_company_invoice_data.is_active,
                created_date = basic_info_company_invoice_data.created_date,
                updated_date = basic_info_company_invoice_data.updated_date,
                created_user_id = basic_info_company_invoice_data.created_user_id,
                updated_user_id = basic_info_company_invoice_data.updated_user_id
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

        #region Update CompanyInvoiceData
        [HttpPost("UpdateCompanyInvoiceData")]
        public async Task<IActionResult> UpdateCompanyInvoiceData([FromBody] CompanyInvoiceData basic_info_company_invoice_data)
        {

            int result = -1;
            string insertQuery = @"UPDATE basic_info_company_invoice_data  SET company_id=@company_id, invoice_data_id=@invoice_data_id, is_active=@is_active, created_user_id=@created_user_id, updated_user_id=@updated_user_id, created_date=@created_date, updated_date=now() WHERE id = @id";
            var queryArguments = new
            {
                id = basic_info_company_invoice_data.id,
                company_id = basic_info_company_invoice_data.company_id,
                invoice_data_id = basic_info_company_invoice_data.invoice_data_id,
                is_active = basic_info_company_invoice_data.is_active,
                created_date = basic_info_company_invoice_data.created_date,
                updated_date = basic_info_company_invoice_data.updated_date,
                created_user_id = basic_info_company_invoice_data.created_user_id,
                updated_user_id = basic_info_company_invoice_data.updated_user_id
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
        #region Delete CompanyInvoiceData
        [HttpPost("DeleteCompanyInvoiceData")]
        public async Task<IActionResult> DeleteCompanyInvoiceData([FromBody] CompanyInvoiceData basic_info_company_invoice_data)
        {

            int result = -1;
            string insertQuery = "DELETE FROM basic_info_company_invoice_data WHERE id = @id";
            var queryArguments = new
            {
                id = basic_info_company_invoice_data.id,
                company_id = basic_info_company_invoice_data.company_id,
                invoice_data_id = basic_info_company_invoice_data.invoice_data_id,
                is_active = basic_info_company_invoice_data.is_active,
                created_date = basic_info_company_invoice_data.created_date,
                updated_date = basic_info_company_invoice_data.updated_date,
                created_user_id = basic_info_company_invoice_data.created_user_id,
                updated_user_id = basic_info_company_invoice_data.updated_user_id
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
        #region ListAllCompanyInvoiceDatas
        [HttpGet("ListAllCompanyInvoiceDatas")]
        public async Task<IActionResult> ListAllCompanyInvoiceDatas()
        {

            try
            {
                string commandText = "SELECT * FROM   basic_info_company_invoice_data";
                connection.Open();
                var groups = await connection.QueryAsync<CompanyInvoiceData>(commandText);
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

        #region CompanyLocation
        #region Register CompanyLocation
        [HttpPost("RegisterCompanyLocation")]
        public async Task<IActionResult> RegisterCompanyLocationAsync([FromBody] CompanyLocation basic_info_company_location)
        {

            int result = -1;
            string insertQuery = "INSERT INTO basic_info_company_location (company_id, location_tb_id, is_active, created_user_id, updated_user_id, created_date, updated_date) VALUES (@company_id,@location_tb_id,@is_active,@created_user_id,@updated_user_id,now(),now()) RETURNING id";
            var queryArguments = new
            {
                company_id = basic_info_company_location.company_id,
                location_tb_id = basic_info_company_location.location_tb_id,
                is_active = basic_info_company_location.is_active,
                created_date = basic_info_company_location.created_date,
                updated_date = basic_info_company_location.updated_date,
                created_user_id = basic_info_company_location.created_user_id,
                updated_user_id = basic_info_company_location.updated_user_id
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

        #region Update CompanyLocation
        [HttpPost("UpdateCompanyLocation")]
        public async Task<IActionResult> UpdateCompanyLocation([FromBody] CompanyLocation basic_info_company_location)
        {

            int result = -1;
            string insertQuery = @"UPDATE basic_info_company_location  SET company_id=@company_id, location_tb_id=@location_tb_id, is_active=@is_active, created_user_id=@created_user_id, updated_user_id=@updated_user_id, created_date=@created_date, updated_date=now() WHERE id = @id";
            var queryArguments = new
            {
                id = basic_info_company_location.id,
                company_id = basic_info_company_location.company_id,
                location_tb_id = basic_info_company_location.location_tb_id,
                is_active = basic_info_company_location.is_active,
                created_date = basic_info_company_location.created_date,
                updated_date = basic_info_company_location.updated_date,
                created_user_id = basic_info_company_location.created_user_id,
                updated_user_id = basic_info_company_location.updated_user_id
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
        #region Delete CompanyLocation
        [HttpPost("DeleteCompanyLocation")]
        public async Task<IActionResult> DeleteCompanyLocation([FromBody] CompanyLocation basic_info_company_location)
        {

            int result = -1;
            string insertQuery = "DELETE FROM basic_info_company_location WHERE id = @id";
            var queryArguments = new
            {
                id = basic_info_company_location.id,
                company_id = basic_info_company_location.company_id,
                location_tb_id = basic_info_company_location.location_tb_id,
                is_active = basic_info_company_location.is_active,
                created_date = basic_info_company_location.created_date,
                updated_date = basic_info_company_location.updated_date,
                created_user_id = basic_info_company_location.created_user_id,
                updated_user_id = basic_info_company_location.updated_user_id
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
        #region ListAllCompanyLocations
        [HttpGet("ListAllCompanyLocations")]
        public async Task<IActionResult> ListAllCompanyLocations()
        {

            try
            {
                string commandText = "SELECT * FROM   basic_info_company_location";
                connection.Open();
                var groups = await connection.QueryAsync<CompanyLocation>(commandText);
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

        #region CompanyRelationshipBusiness
        #region Register CompanyRelationshipBusiness
        [HttpPost("RegisterCompanyRelationshipBusiness")]
        public async Task<IActionResult> RegisterCompanyRelationshipBusinessAsync([FromBody] CompanyRelationshipBusiness basic_info_company_relationship_business)
        {

            int result = -1;
            string insertQuery = "INSERT INTO basic_info_company_relationship_business (company_id, relationship_business_id, is_active, created_user_id, updated_user_id, created_date, updated_date) VALUES (@company_id,@relationship_business_id,@is_active,@created_user_id,@updated_user_id,now(),now()) RETURNING id";
            var queryArguments = new
            {
                company_id = basic_info_company_relationship_business.company_id,
                relationship_business_id = basic_info_company_relationship_business.relationship_business_id,
                is_active = basic_info_company_relationship_business.is_active,
                created_date = basic_info_company_relationship_business.created_date,
                updated_date = basic_info_company_relationship_business.updated_date,
                created_user_id = basic_info_company_relationship_business.created_user_id,
                updated_user_id = basic_info_company_relationship_business.updated_user_id
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

        #region Update CompanyRelationshipBusiness
        [HttpPost("UpdateCompanyRelationshipBusiness")]
        public async Task<IActionResult> UpdateCompanyRelationshipBusiness([FromBody] CompanyRelationshipBusiness basic_info_company_relationship_business)
        {

            int result = -1;
            string insertQuery = @"UPDATE basic_info_company_relationship_business  SET company_id=@company_id, relationship_business_id=@relationship_business_id, is_active=@is_active, created_user_id=@created_user_id, updated_user_id=@updated_user_id, created_date=@created_date, updated_date=now() WHERE id = @id";
            var queryArguments = new
            {
                id = basic_info_company_relationship_business.id,
                company_id = basic_info_company_relationship_business.company_id,
                relationship_business_id = basic_info_company_relationship_business.relationship_business_id,
                is_active = basic_info_company_relationship_business.is_active,
                created_date = basic_info_company_relationship_business.created_date,
                updated_date = basic_info_company_relationship_business.updated_date,
                created_user_id = basic_info_company_relationship_business.created_user_id,
                updated_user_id = basic_info_company_relationship_business.updated_user_id
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
        #region Delete CompanyRelationshipBusiness
        [HttpPost("DeleteCompanyRelationshipBusiness")]
        public async Task<IActionResult> DeleteCompanyRelationshipBusiness([FromBody] CompanyRelationshipBusiness basic_info_company_relationship_business)
        {

            int result = -1;
            string insertQuery = "DELETE FROM basic_info_company_relationship_business WHERE id = @id";
            var queryArguments = new
            {
                id = basic_info_company_relationship_business.id,
                company_id = basic_info_company_relationship_business.company_id,
                relationship_business_id = basic_info_company_relationship_business.relationship_business_id,
                is_active = basic_info_company_relationship_business.is_active,
                created_date = basic_info_company_relationship_business.created_date,
                updated_date = basic_info_company_relationship_business.updated_date,
                created_user_id = basic_info_company_relationship_business.created_user_id,
                updated_user_id = basic_info_company_relationship_business.updated_user_id
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
        #region ListAllCompanyRelationshipBusinesss
        [HttpGet("ListAllCompanyRelationshipBusinesss")]
        public async Task<IActionResult> ListAllCompanyRelationshipBusinesss()
        {

            try
            {
                string commandText = "SELECT * FROM   basic_info_company_relationship_business";
                connection.Open();
                var groups = await connection.QueryAsync<CompanyRelationshipBusiness>(commandText);
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
