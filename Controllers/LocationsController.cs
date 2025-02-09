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
using System.Text.Json;
namespace FairyBE.Controllers
{
    //especificaciones del api
    [ApiController]//con esto se especifica que va ser un controlador de API xq hay distintos tipos de controladores
    [Route("[controller]")]//la ruta que va utilizar sera el nombre del controlador
    public class LocationsController : Controller
    {
        #region VARIABLES INTERNAS
        private NpgsqlConnection connection;//Atributo para conectar con Postgresql
        public IConfiguration Configuration { get; }//Se inicializa la interfaz de configuracion
        #endregion
        #region CONSTRUCTOR
        public LocationsController(IConfiguration config)
        {
            //Se asigna la interfaz de configuraciion a la configuracion local
            Configuration = config;
            // Se obtiene la cadena de conexion alojada en el json de configuracion
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            //aqui se crea la conexion a la bd
            connection = new NpgsqlConnection(connectionString);
        }
		#endregion
		#region Listar departamentos y ciudades
		//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------       
		//ENDPOINT PARA LISTAR TODOS LOS REGISTROS DE LA TABLA

		[HttpGet("getAllDepartamentsCities")]
		public async Task<IActionResult> getAllDepartamentsCities()
		{

			try
			{
				string commandText = @"with data1 as (
							select
								ld.id,
								ld.name,
								COALESCE(jsonb_agg(
										 jsonb_build_object(
												 'id', lc.id,
												 'name', lc.name
										 ) order by lc.name
												  ) FILTER (WHERE lc.id IS NOT NULL), '[]'::jsonb) as cities
							from locations_departament ld
									 left join locations_city lc on ld.id = lc.id_departament_id
							group by ld.id, ld.name
						)
						select jsonb_agg(
									   jsonb_build_object(
											   'id', id,
											   'name', name,
											   'cities', cities
									   )
							   ) as result
						from data1";
				connection.Open();
				var result = await connection.QuerySingleOrDefaultAsync<string>(commandText);
				connection.Close();
				return new JsonResult(JsonDocument.Parse(result));

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
