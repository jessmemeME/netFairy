using Dapper;
using FairyBE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace FairyBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BriefController : ControllerBase
    {
        private NpgsqlConnection connection;//Atributo para conectar con Postgresql
        public IConfiguration Configuration { get; }//Se inicializa la interfaz de configuracion

        public BriefController(IConfiguration configuration)
        {
            Configuration = configuration;
            connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection"));

        }

        [HttpGet("ListClassificationSegmentStyle")]
        public async Task<IActionResult> ListClassificationSegmentStyle()
        {
            const string sql = @"
    SELECT 
        sc.id AS Id, 
        sc.name AS Name, 
        sc.description AS Description, 
        sc.is_active AS IsActive,
        sc.created_date AS CreatedDate, 
        sc.updated_date AS UpdatedDate,

        ss.id AS Id, 
        ss.name AS Name, 
        ss.description AS Description, 
        ss.is_active AS IsActive,
        ss.created_date AS CreatedDate, 
        ss.updated_date AS UpdatedDate, 
        ss.classification_id AS ClassificationId,

        s.id AS Id, 
        s.name AS Name, 
        s.description AS Description, 
        s.is_active AS IsActive,
        s.created_date AS CreatedDate, 
        s.updated_date AS UpdatedDate, 
        s.segment_id AS SegmentId
    FROM wedding_style_classification sc
    LEFT JOIN wedding_style_segment ss ON sc.id = ss.classification_id
    LEFT JOIN wedding_style s ON ss.id = s.segment_id
    WHERE sc.is_active = true
    AND ss.is_active = true
    AND s.is_active = true
    ORDER BY sc.id, ss.id, s.id;";

            try
            {
                connection.Open();

                var classificationDictionary = new Dictionary<int, StyleClassification>();

                var data = await connection.QueryAsync<StyleClassification, StyleSegment, Style, StyleClassification>(
                    sql,
                    (classification, segment, style) =>
                    {
                        if (!classificationDictionary.TryGetValue(classification.Id, out var classificationEntry))
                        {
                            classificationEntry = classification;
                            classificationEntry.Segments = new List<StyleSegment>();
                            classificationDictionary.Add(classificationEntry.Id, classificationEntry);
                        }

                        if (segment != null && !classificationEntry.Segments.Any(s => s.Id == segment.Id))
                        {
                            segment.Styles = new List<Style>();
                            classificationEntry.Segments.Add(segment);
                        }

                        if (style != null && segment != null)
                        {
                            var segmentEntry = classificationEntry.Segments.FirstOrDefault(s => s.Id == segment.Id);
                            if (segmentEntry != null && !segmentEntry.Styles.Any(st => st.Id == style.Id))
                            {
                                segmentEntry.Styles.Add(style);
                            }
                        }

                        return classificationEntry;
                    },
                    splitOn: "Id,Id"
                );

                var result = classificationDictionary.Values.ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Cierra conexión siempre
                connection.Close();

                // Para debugging: muestra el mensaje real del error (Opcional)
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
    }
    }
