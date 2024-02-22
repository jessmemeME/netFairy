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
            string insertQuery = "INSERT INTO business_invoice_data (name, document_number, description, created_date, updated_date, is_active, created_user_id, updated_user_id) VALUES (@name,@document_number,@description,@created_date,@updated_date,@is_active,@created_user_id,@updated_user_id) RETURNING Id";
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
            string insertQuery = "UPDATE business_invoice_data  SET name=@name, document_number=@document_number, description=@description, created_date=@created_date, updated_date=@updated_date, is_active=@is_active, created_user_id=@created_user_id, updated_user_id=@updated_user_id WHERE id = @id";
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
        #region
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
            string insertQuery = "INSERT INTO business_color (color_origin, name, description, code_RGB, code_hexadecimal,code_CMK, created_date, updated_date, is_active, created_user_id, updated_user_id) VALUES (@color_origin,@name,@description,@code_RGB,@code_hexadecimal,@code_CMK,@created_date,@updated_date,@is_active,@created_user_id,@updated_user_id) RETURNING Id";
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
            string insertQuery = "UPDATE business_color  SET color_origin=@color_origin, name=@name, description=@description, code_RGB=@code_RGB, code_hexadecimal=@code_hexadecimal, code_CMK=@code_CMK, created_date=@created_date, updated_date=@updated_date, is_active=@is_active, created_user_id=@created_user_id, updated_user_id=@updated_user_id WHERE id = @id";
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


    }
}
