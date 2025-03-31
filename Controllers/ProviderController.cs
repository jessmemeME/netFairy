using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using FairyBE.Models;
using System.Text.RegularExpressions;
using Dapper;

namespace FairyBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private NpgsqlConnection connection;//Atributo para conectar con Postgresql
        public IConfiguration Configuration { get; }//Se inicializa la interfaz de configuracion

        public ProviderController(IConfiguration configuration)
        {
            Configuration = configuration;
            connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection"));

        }

        [HttpGet("ListAllProviders")]
        public async Task<IActionResult> ListAllProviders()
        {
            const string query = @"
                SELECT 
                    'PROV - ' || vv.id AS Id, 
                    vv.type AS Type, 
                    vv.name AS Name, 
                    vv.is_my_company AS IsMyCompany, 
                    vv.description AS Description, 
                    vv.is_confirmated AS IsConfirmated, 
                    vv.created_date AS CreatedDate, 
                    vv.updated_date AS UpdatedDate, 
                    CASE WHEN vv.is_active THEN 'ACTIVO' ELSE 'INACTIVO' END AS IsActive, 
                    vv.company_id AS CompanyId, 
                    bic.name AS CompanyName,
                    vv.created_user_id AS CreatedUserId, 
                    vv.people_id AS PeopleId, 
                    bip.first_name || ' ' || bip.last_name AS PeopleName,
                    vv.updated_user_id AS UpdatedUserId, 
                    CASE WHEN vv.type_people = 'J' THEN 'PERSONA JURIDICA' ELSE 'PERSONA FISICA' END AS TypePeople
                FROM public.vendor_vendor vv
                LEFT JOIN basic_info_company bic ON vv.company_id = bic.id
                LEFT JOIN basic_info_people bip ON vv.people_id = bip.id
                ORDER BY vv.id ASC";

            try
            {
                connection.Open();
                var providers = await connection.QueryAsync<Providers>(query);
                return Ok(providers);
            }
            catch (Exception ex)
            {
                connection.Close();
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving providers.");
            }

        }


    }
}

