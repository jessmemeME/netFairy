using System.ComponentModel.DataAnnotations;

namespace FairyBE.Models
{
    public class AgeGroup
    {
        public int id { get; set; }
        [Required] public string name { get; set; }
        [Required] public string description { get; set; }
        [Required] public string age_range { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
    }

    public class DocumentType
    {
        public int id { get; set; }
        [Required] public string name { get; set; }
        [Required] public string description { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        /*
            "id"	"bigint"
            "name"	"character varying"
            "description"	"text"
            "created_date"	"timestamp with time zone"
            "updated_date"	"timestamp with time zone"
            "is_active"	"boolean"
            "created_user_id"	"bigint"
            "updated_user_id"	"bigint"
         */

    }

    public class Gender
    {
        public int id { get; set; }
        [Required] public string name { get; set; }
        [Required] public string description { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        /*
            "id"	"bigint"
            "name"	"character varying"
            "description"	"text"
            "created_date"	"timestamp with time zone"
            "updated_date"	"timestamp with time zone"
            "is_active"	"boolean"
            "created_user_id"	"bigint"
            "updated_user_id"	"bigint"
         */

    }

    public class RelationshipBusiness
    {
        public int id { get; set; }
        [Required] public string name { get; set; }
        [Required] public string description { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }

        /*
            "id"	"bigint"
            "name"	"character varying"
            "description"	"text"
            "created_date"	"timestamp with time zone"
            "updated_date"	"timestamp with time zone"
            "is_active"	"boolean"
            "created_user_id"	"bigint"
            "updated_user_id"	"bigint"
         */

    }
    public class TypeOfDiner
    {
        public int id { get; set; }
        [Required] public string name { get; set; }
        [Required] public string description { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        /*
            "id"	"bigint"
            "name"	"character varying"
            "description"	"text"
            "created_date"	"timestamp with time zone"
            "updated_date"	"timestamp with time zone"
            "is_active"	"boolean"
            "created_user_id"	"bigint"
            "updated_user_id"	"bigint"
       */
    }

    public class Tradition
    {
        public int id { get; set; }
        [Required] public string name { get; set; }
        [Required] public string description { get; set; }
        [Required] public string requisitos { get; set; }
        [Required] public string reglas { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        /*
            "id"	"bigint"
            "name"	"character varying"
            "description"	"text"
            "requisitos"	"text"
            "reglas"	"text"
            "created_date"	"timestamp with time zone"
            "updated_date"	"timestamp with time zone"
            "is_active"	"boolean"
            "created_user_id"	"bigint"
            "updated_user_id"	"bigint"
         */
    }

    public class Culture
    {
        public int id { get; set; }
        [Required] public string name { get; set; }
        [Required] public string description { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        /*
            "id"	"bigint"
            "name"	"character varying"
            "description"	"text"
            "created_date"	"timestamp with time zone"
            "updated_date"	"timestamp with time zone"
            "is_active"	"boolean"
            "created_user_id"	"bigint"
            "updated_user_id"	"bigint"
         */
    }
    public class Religion
    {
        public int id { get; set; }
        [Required] public string name { get; set; }
        [Required] public string description { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        /*
            "id"	"bigint"
            "name"	"character varying"
            "description"	"text"
            "created_date"	"timestamp with time zone"
            "updated_date"	"timestamp with time zone"
            "is_active"	"boolean"
            "created_user_id"	"bigint"
            "updated_user_id"	"bigint"
         */

    }

}
