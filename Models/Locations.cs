using System.ComponentModel.DataAnnotations;

namespace FairyBE.Models
{
    public class Locations
    {
        public int id { get; set; }
        [Required] public string? name { get; set; }
        [Required] public string? description { get; set; }
        [Required] public string? street1 { get; set; }
        [Required] public string? street2 { get; set; }
        [Required] public string? house_number { get; set; }
         public string? floor { get; set; }
        public string? building_name { get; set; }
        [Required] public int latitude { get; set; }
        [Required] public int longitude { get; set; }
        [Required] public string? observation { get; set; }
         public string? photo { get; set; }
        [Required] public bool is_main_location { get; set; }
        [Required] public int city_id { get; set; }
        [Required] public int departament_id { get; set; }
        [Required] public int country_id { get; set; }
        [Required] public int id_location_type_id { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        /*
         "id"	"bigint"
"name"	"character varying"
"description"	"text"
"street1"	"character varying"
"street2"	"character varying"
"house_number"	"character varying"
"floor"	"character varying"
"building_name"	"character varying"
"latitude"	"numeric"
"longitude"	"numeric"
"observation"	"text"
"photo"	"character varying"
"is_main_location"	"boolean"
"created_date"	"timestamp with time zone"
"updated_date"	"timestamp with time zone"
"is_active"	"boolean"
"city_id"	"bigint"
"country_id"	"bigint"
"created_user_id"	"bigint"
"departament_id"	"bigint"
"id_location_type_id"	"bigint"
"updated_user_id"	"bigint"
         TABLE_NAME = 'locations_location_tb'
         */
    }


}
