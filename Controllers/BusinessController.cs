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
    public class BusinessController : Controller
    {
        #region VARIABLES INTERNAS
        private NpgsqlConnection connection;//Atributo para conectar con Postgresql
        public IConfiguration Configuration { get; }//Se inicializa la interfaz de configuracion
        #endregion

        #region CONSTRUCTOR
        public BusinessController(IConfiguration config)
        {
            //Se asigna la interfaz de configuraciion a la configuracion local
            Configuration = config;
            // Se obtiene la cadena de conexion alojada en el json de configuracion
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            //aqui se crea la conexion a la bd
            connection = new NpgsqlConnection(connectionString);
        }
        #endregion

        #region BusinessInvoiceData
        #region Register BusinessInvoiceData
        [HttpPost("RegisterBusinessInvoiceData")]
        public async Task<IActionResult> RegisterBusinessInvoiceDataAsync([FromBody] BusinessInvoiceData business_invoice_data)
        {

            int result = -1;
            string insertQuery = "INSERT INTO business_invoice_data (name, document_number, description, created_date, updated_date, is_active, created_user_id, updated_user_id) VALUES (@name,@document_number,@description,now(),now(),@is_active,@created_user_id,@updated_user_id) RETURNING Id";
            var queryArguments = new
            {
                name = business_invoice_data.name,
                document_number = business_invoice_data.document_number,
                description = business_invoice_data.description,
                created_date = business_invoice_data.created_date,
                updated_date = business_invoice_data.updated_date,
                is_active = business_invoice_data.is_active,
                created_user_id = business_invoice_data.created_user_id,
                updated_user_id = business_invoice_data.updated_user_id
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

        #region Update BusinessInvoiceData
        [HttpPost("UpdateBusinessInvoiceData")]
        public async Task<IActionResult> UpdateBusinessInvoiceData([FromBody] BusinessInvoiceData business_invoice_data)
        {

            int result = -1;
            string insertQuery = "UPDATE business_invoice_data  SET name=@name, document_number=@document_number, description=@description, created_date=@created_date, updated_date=now(), is_active=@is_active, created_user_id=@created_user_id, updated_user_id=@updated_user_id WHERE id = @id";
            var queryArguments = new
            {
                id = business_invoice_data.id,
                name = business_invoice_data.name,
                document_number = business_invoice_data.document_number,
                description = business_invoice_data.description,
                created_date = business_invoice_data.created_date,
                updated_date = business_invoice_data.updated_date,
                is_active = business_invoice_data.is_active,
                created_user_id = business_invoice_data.created_user_id,
                updated_user_id = business_invoice_data.updated_user_id
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
        #region Delete BusinessInvoiceData
        [HttpPost("DeleteBusinessInvoiceData")]
        public async Task<IActionResult> DeleteBusinessInvoiceData([FromBody] BusinessInvoiceData business_invoice_data)
        {

            int result = -1;
            string insertQuery = "DELETE FROM business_invoice_data WHERE id = @id";
            var queryArguments = new
            {
                id = business_invoice_data.id,
                name = business_invoice_data.name,
                document_number = business_invoice_data.document_number,
                description = business_invoice_data.description,
                created_date = business_invoice_data.created_date,
                updated_date = business_invoice_data.updated_date,
                is_active = business_invoice_data.is_active,
                created_user_id = business_invoice_data.created_user_id,
                updated_user_id = business_invoice_data.updated_user_id
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
        #region Listar ListAllBusinessInvoiceDatas
        [HttpGet("ListAllBusinessInvoiceDatas")]
        public async Task<IActionResult> ListAllBusinessInvoiceDatas()
        {

            try
            {
                string commandText = "SELECT * FROM   business_invoice_data";
                connection.Open();
                var groups = await connection.QueryAsync<BusinessInvoiceData>(commandText);
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

        #region BusinessColor
        #region Register BusinessColor
        [HttpPost("RegisterBusinessColor")]
        public async Task<IActionResult> RegisterBusinessColorAsync([FromBody] BusinessColor business_color)
        {

            int result = -1;
            string insertQuery = "INSERT INTO business_color (color_origin, name, description, code_RGB, code_hexadecimal,code_CMK, created_date, updated_date, is_active, created_user_id, updated_user_id) VALUES (@color_origin,@name,@description,@code_RGB,@code_hexadecimal,@code_CMK,now(),now(),@is_active,@created_user_id,@updated_user_id) RETURNING Id";
            var queryArguments = new
            {
                color_origin = business_color.color_origin,
                name = business_color.name,
                description = business_color.description,
                code_RGB = business_color.code_RGB,
                code_hexadecimal = business_color.code_hexadecimal,
                code_CMK = business_color.code_CMK,
                created_date = business_color.created_date,
                updated_date = business_color.updated_date,
                is_active = business_color.is_active,
                created_user_id = business_color.created_user_id,
                updated_user_id = business_color.updated_user_id
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

        #region Update BusinessColor
        [HttpPost("UpdateBusinessColor")]
        public async Task<IActionResult> UpdateBusinessColor([FromBody] BusinessColor business_color)
        {

            int result = -1;
            string insertQuery = "UPDATE business_color  SET color_origin=@color_origin, name=@name, description=@description, code_RGB=@code_RGB, code_hexadecimal=@code_hexadecimal, code_CMK=@code_CMK, created_date=@created_date, updated_date=now(), is_active=@is_active, created_user_id=@created_user_id, updated_user_id=@updated_user_id WHERE id = @id";
            var queryArguments = new
            {
                id = business_color.id,
                color_origin = business_color.color_origin,
                name = business_color.name,
                description = business_color.description,
                code_RGB = business_color.code_RGB,
                code_hexadecimal = business_color.code_hexadecimal,
                code_CMK = business_color.code_CMK,
                created_date = business_color.created_date,
                updated_date = business_color.updated_date,
                is_active = business_color.is_active,
                created_user_id = business_color.created_user_id,
                updated_user_id = business_color.updated_user_id
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
        #region Delete BusinessColor
        [HttpPost("DeleteBusinessColor")]
        public async Task<IActionResult> DeleteBusinessColor([FromBody] BusinessColor business_color)
        {

            int result = -1;
            string insertQuery = "DELETE FROM business_color WHERE id = @id";
            var queryArguments = new
            {
                id = business_color.id,
                color_origin = business_color.color_origin,
                name = business_color.name,
                description = business_color.description,
                code_RGB = business_color.code_RGB,
                code_hexadecimal = business_color.code_hexadecimal,
                code_CMK = business_color.code_CMK,
                created_date = business_color.created_date,
                updated_date = business_color.updated_date,
                is_active = business_color.is_active,
                created_user_id = business_color.created_user_id,
                updated_user_id = business_color.updated_user_id
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
        [HttpGet("ListAllBusinessColors")]
        public async Task<IActionResult> ListAllBusinessColors()
        {

            try
            {
                string commandText = "SELECT * FROM   business_color";
                connection.Open();
                var groups = await connection.QueryAsync<BusinessColor>(commandText);
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

        #region BusinessInvoiceType
        #region Register BusinessInvoiceType
        [HttpPost("RegisterBusinessInvoiceType")]
        public async Task<IActionResult> RegisterBusinessInvoiceTypeAsync([FromBody] BusinessInvoiceType business_invoice_type)
        {

            int result = -1;
            string insertQuery = "INSERT INTO business_invoice_type (name,description,created_date,updated_date,is_active,created_user_id,updated_user_id) VALUES (@name,@description,now(),now(),@is_active,@created_user_id,@updated_user_id) RETURNING id";
            var queryArguments = new
            {
                name = business_invoice_type.name,
                description = business_invoice_type.description,
                created_date = business_invoice_type.created_date,
                updated_date = business_invoice_type.updated_date,
                is_active = business_invoice_type.is_active,
                created_user_id = business_invoice_type.created_user_id,
                updated_user_id = business_invoice_type.updated_user_id
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

        #region Update BusinessInvoiceType
        [HttpPost("UpdateBusinessInvoiceType")]
        public async Task<IActionResult> UpdateBusinessInvoiceType([FromBody] BusinessInvoiceType business_invoice_type)
        {

            int result = -1;
            string insertQuery = "UPDATE business_invoice_type  SET name=@name, description=@description, created_date=@created_date, updated_date=now(), is_active=@is_active, created_user_id=@created_user_id, updated_user_id=@updated_user_id	WHERE id = @id";
            var queryArguments = new
            {
                id = business_invoice_type.id,
                name = business_invoice_type.name,
                description = business_invoice_type.description,
                created_date = business_invoice_type.created_date,
                updated_date = business_invoice_type.updated_date,
                is_active = business_invoice_type.is_active,
                created_user_id = business_invoice_type.created_user_id,
                updated_user_id = business_invoice_type.updated_user_id
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
        #region Delete BusinessInvoiceType
        [HttpPost("DeleteBusinessInvoiceType")]
        public async Task<IActionResult> DeleteBusinessInvoiceType([FromBody] BusinessInvoiceType business_invoice_type)
        {

            int result = -1;
            string insertQuery = "DELETE FROM business_invoice_type WHERE id = @id";
            var queryArguments = new
            {
                id = business_invoice_type.id,
                name = business_invoice_type.name,
                description = business_invoice_type.description,
                created_date = business_invoice_type.created_date,
                updated_date = business_invoice_type.updated_date,
                is_active = business_invoice_type.is_active,
                created_user_id = business_invoice_type.created_user_id,
                updated_user_id = business_invoice_type.updated_user_id
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
        [HttpGet("ListAllBusinessInvoiceTypes")]
        public async Task<IActionResult> ListAllBusinessInvoiceTypes()
        {

            try
            {
                string commandText = "SELECT * FROM   business_invoice_type";
                connection.Open();
                var groups = await connection.QueryAsync<BusinessInvoiceType>(commandText);
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

        #region BusinessProduct
        #region Register BusinessProduct
        [HttpPost("RegisterBusinessProduct")]
        public async Task<IActionResult> RegisterBusinessProductAsync([FromBody] BusinessProduct business_product)
        {

            int result = -1;
            string insertQuery = "INSERT INTO business_product (name, description, created_date, updated_date, is_active, created_user_id, id_product_type_id, updated_user_id) VALUES (@name,@description,now(),now(),@is_active,@created_user_id,@id_product_type_id,@updated_user_id) RETURNING id";
            var queryArguments = new
            {
                name = business_product.name,
                description = business_product.description,
                created_date = business_product.created_date,
                updated_date = business_product.updated_date,
                is_active = business_product.is_active,
                created_user_id = business_product.created_user_id,
                id_product_type_id = business_product.id_product_type_id,
                updated_user_id = business_product.updated_user_id
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

        #region Update BusinessProduct
        [HttpPost("UpdateBusinessProduct")]
        public async Task<IActionResult> UpdateBusinessProduct([FromBody] BusinessProduct business_product)
        {

            int result = -1;
            string insertQuery = @"UPDATE business_product  SET 
								name=@name, 
								description=@description, 
								created_date=@created_date, 
								updated_date=now(), 
								is_active=@is_active, 
								created_user_id=@created_user_id, 
								id_product_type_id=@id_product_type_id, 
								updated_user_id=@updated_user_id	
							WHERE id = @id";
            var queryArguments = new
            {
                id = business_product.id,
                name = business_product.name,
                description = business_product.description,
                created_date = business_product.created_date,
                updated_date = business_product.updated_date,
                is_active = business_product.is_active,
                created_user_id = business_product.created_user_id,
                id_product_type_id = business_product.id_product_type_id,
                updated_user_id = business_product.updated_user_id
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
        #region Delete BusinessProduct
        [HttpPost("DeleteBusinessProduct")]
        public async Task<IActionResult> DeleteBusinessProduct([FromBody] BusinessProduct business_product)
        {

            int result = -1;
            string insertQuery = "DELETE FROM business_product WHERE id = @id";
            var queryArguments = new
            {
                id = business_product.id,
                name = business_product.name,
                description = business_product.description,
                created_date = business_product.created_date,
                updated_date = business_product.updated_date,
                is_active = business_product.is_active,
                created_user_id = business_product.created_user_id,
                id_product_type_id = business_product.id_product_type_id,
                updated_user_id = business_product.updated_user_id
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
        [HttpGet("ListAllBusinessProducts")]
        public async Task<IActionResult> ListAllBusinessProducts()
        {

            try
            {
                string commandText = "SELECT * FROM   business_product";
                connection.Open();
                var groups = await connection.QueryAsync<BusinessProduct>(commandText);
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

        #region BusinessProductType
        #region Register BusinessProductType
        [HttpPost("RegisterBusinessProductType")]
        public async Task<IActionResult> RegisterBusinessProductTypeAsync([FromBody] BusinessProductType business_product_type)
        {

            int result = -1;
            string insertQuery = "INSERT INTO business_product_type (name, description, created_date, updated_date, is_active, created_user_id, updated_user_id) VALUES (@name,@description,now(),now(),@is_active,@created_user_id,@updated_user_id) RETURNING id";
            var queryArguments = new
            {
                name = business_product_type.name,
                description = business_product_type.description,
                created_date = business_product_type.created_date,
                updated_date = business_product_type.updated_date,
                is_active = business_product_type.is_active,
                created_user_id = business_product_type.created_user_id,
                updated_user_id = business_product_type.updated_user_id
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

        #region Update BusinessProductType
        [HttpPost("UpdateBusinessProductType")]
        public async Task<IActionResult> UpdateBusinessProductType([FromBody] BusinessProductType business_product_type)
        {

            int result = -1;
            string insertQuery = @"UPDATE business_product_type  SET 
								name=@name, 
								description=@description, 
								created_date=@created_date, 
								updated_date=now(), 
								is_active=@is_active, 
								created_user_id=@created_user_id, 
								updated_user_id=@updated_user_id
							WHERE id = @id";
            var queryArguments = new
            {
                id = business_product_type.id,
                name = business_product_type.name,
                description = business_product_type.description,
                created_date = business_product_type.created_date,
                updated_date = business_product_type.updated_date,
                is_active = business_product_type.is_active,
                created_user_id = business_product_type.created_user_id,
                updated_user_id = business_product_type.updated_user_id
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
        #region Delete BusinessProductType
        [HttpPost("DeleteBusinessProductType")]
        public async Task<IActionResult> DeleteBusinessProductType([FromBody] BusinessProductType business_product_type)
        {

            int result = -1;
            string insertQuery = "DELETE FROM business_product_type WHERE id = @id";
            var queryArguments = new
            {
                id = business_product_type.id,
                name = business_product_type.name,
                description = business_product_type.description,
                created_date = business_product_type.created_date,
                updated_date = business_product_type.updated_date,
                is_active = business_product_type.is_active,
                created_user_id = business_product_type.created_user_id,
                updated_user_id = business_product_type.updated_user_id
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
        [HttpGet("ListAllBusinessProductTypes")]
        public async Task<IActionResult> ListAllBusinessProductTypes()
        {

            try
            {
                string commandText = "SELECT * FROM   business_product_type";
                connection.Open();
                var groups = await connection.QueryAsync<BusinessProductType>(commandText);
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

        #region BusinessService
        #region Register BusinessService
        [HttpPost("RegisterBusinessService")]
        public async Task<IActionResult> RegisterBusinessServiceAsync([FromBody] BusinessService business_service)
        {

            int result = -1;
            string insertQuery = "INSERT INTO business_service (name, description, created_date, updated_date, is_active, created_user_id, id_service_type_id, updated_user_id) VALUES (@name,@description,now(),now(),@is_active,@created_user_id,@id_service_type_id,@updated_user_id) RETURNING id";
            var queryArguments = new
            {
                name = business_service.name,
                description = business_service.description,
                is_active = business_service.is_active,
                created_date = business_service.created_date,
                updated_date = business_service.updated_date,
                created_user_id = business_service.created_user_id,
                id_service_type_id = business_service.id_service_type_id,
                updated_user_id = business_service.updated_user_id
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

        #region Update BusinessService
        [HttpPost("UpdateBusinessService")]
        public async Task<IActionResult> UpdateBusinessService([FromBody] BusinessService business_service)
        {

            int result = -1;
            string insertQuery = @"UPDATE business_service  SET name=@name, description=@description, created_date=@created_date, updated_date=now(), is_active=@is_active, created_user_id=@created_user_id, id_service_type_id=@id_service_type_id, updated_user_id=@updated_user_id	WHERE id = @id";
            var queryArguments = new
            {
                id = business_service.id,
                name = business_service.name,
                description = business_service.description,
                is_active = business_service.is_active,
                created_date = business_service.created_date,
                updated_date = business_service.updated_date,
                created_user_id = business_service.created_user_id,
                id_service_type_id = business_service.id_service_type_id,
                updated_user_id = business_service.updated_user_id
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
        #region Delete BusinessService
        [HttpPost("DeleteBusinessService")]
        public async Task<IActionResult> DeleteBusinessService([FromBody] BusinessService business_service)
        {

            int result = -1;
            string insertQuery = "DELETE FROM business_service WHERE id = @id";
            var queryArguments = new
            {
                id = business_service.id,
                name = business_service.name,
                description = business_service.description,
                is_active = business_service.is_active,
                created_date = business_service.created_date,
                updated_date = business_service.updated_date,
                created_user_id = business_service.created_user_id,
                id_service_type_id = business_service.id_service_type_id,
                updated_user_id = business_service.updated_user_id
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
        [HttpGet("ListAllBusinessServices")]
        public async Task<IActionResult> ListAllBusinessServices()
        {

            try
            {
                string commandText = "SELECT * FROM   business_service";
                connection.Open();
                var groups = await connection.QueryAsync<BusinessService>(commandText);
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

        #region BusinessServiceType
        #region Register BusinessServiceType
        [HttpPost("RegisterBusinessServiceType")]
        public async Task<IActionResult> RegisterBusinessServiceTypeAsync([FromBody] BusinessServiceType business_service_type)
        {

            int result = -1;
            string insertQuery = "INSERT INTO business_service_type (name, description, created_date, updated_date, is_active, created_user_id, updated_user_id) VALUES (@name,@description,now(),now(),@is_active,@created_user_id,@updated_user_id) RETURNING id";
            var queryArguments = new
            {
                name = business_service_type.name,
                description = business_service_type.description,
                is_active = business_service_type.is_active,
                created_date = business_service_type.created_date,
                updated_date = business_service_type.updated_date,
                created_user_id = business_service_type.created_user_id,
                updated_user_id = business_service_type.updated_user_id

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

        #region Update BusinessServiceType
        [HttpPost("UpdateBusinessServiceType")]
        public async Task<IActionResult> UpdateBusinessServiceType([FromBody] BusinessServiceType business_service_type)
        {

            int result = -1;
            string insertQuery = @"UPDATE business_service_type  SET name=@name, description=@description, created_date=@created_date, updated_date=now(), is_active=@is_active, created_user_id=@created_user_id, updated_user_id=@updated_user_id	WHERE id = @id";
            var queryArguments = new
            {
                id = business_service_type.id,
                name = business_service_type.name,
                description = business_service_type.description,
                is_active = business_service_type.is_active,
                created_date = business_service_type.created_date,
                updated_date = business_service_type.updated_date,
                created_user_id = business_service_type.created_user_id,
                updated_user_id = business_service_type.updated_user_id
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
        #region Delete BusinessServiceType
        [HttpPost("DeleteBusinessServiceType")]
        public async Task<IActionResult> DeleteBusinessServiceType([FromBody] BusinessServiceType business_service_type)
        {

            int result = -1;
            string insertQuery = "DELETE FROM business_service_type WHERE id = @id";
            var queryArguments = new
            {
                id = business_service_type.id,
                name = business_service_type.name,
                description = business_service_type.description,
                is_active = business_service_type.is_active,
                created_date = business_service_type.created_date,
                updated_date = business_service_type.updated_date,
                created_user_id = business_service_type.created_user_id,
                updated_user_id = business_service_type.updated_user_id
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
        [HttpGet("ListAllBusinessServiceTypes")]
        public async Task<IActionResult> ListAllBusinessServiceTypes()
        {

            try
            {
                string commandText = "SELECT * FROM   business_service_type";
                connection.Open();
                var groups = await connection.QueryAsync<BusinessServiceType>(commandText);
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
