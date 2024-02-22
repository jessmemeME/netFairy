using System.ComponentModel.DataAnnotations;

namespace FairyBE.Models
{
    public class Company
    {
        public int id { get; set; }
        [Required] public string? name { get; set; }
        [Required] public int document_type_id { get; set; }
        [Required] public string? document_number { get; set; }
        [Required] public string? logo_company { get; set; }
        [Required] public string? description { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        /*
"id"	"bigint"
"name"	"character varying"
"document_number"	"character varying"
"logo_company"	"character varying"
"description"	"text"
"created_date"	"timestamp with time zone"
"updated_date"	"timestamp with time zone"
"is_active"	"boolean"
"created_user_id"	"bigint"
"document_type_id"	"bigint"
"updated_user_id"	"bigint"
            TABLE_NAME = 'basic_info_company'
         */
    }

    //Datos de contacto de las empresas (RELACION)
    public class CompanyContact
    {
        public int id { get; set; }
        [Required] public int company_id { get; set; }
        [Required] public int contact_tb_id { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        /*

            "id"	"bigint"
        "company_id"	"bigint"
        "contact_tb_id"	"bigint"
        "is_active"	"boolean"
        "created_user_id"	"bigint"
        "updated_user_id"	"bigint"
        "created_date"	"timestamp without time zone"
        "updated_date"	"timestamp without time zone"
            TABLE_NAME = 'basic_info_company_contact'
         */
    }    //Datos de facturacion de las empresas (RELACION)
    public class CompanyInvoiceData
    {
        public int id { get; set; }
        [Required] public int company_id { get; set; }
        [Required] public int invoice_data_id { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        /*

            "id"	"bigint"
        "company_id"	"bigint"
        "invoice_data_id"	"bigint"
        "is_active"	"boolean"
        "created_user_id"	"bigint"
        "updated_user_id"	"bigint"
        "created_date"	"timestamp without time zone"
        "updated_date"	"timestamp without time zone"
            TABLE_NAME = 'basic_info_company_invoice_data'
         */
    }
     //Datos de ubicaciones de las sucursaales de las empresas (RELACION)
    public class CompanyLocation
    {
        public int id { get; set; }
        [Required] public int company_id { get; set; }
        [Required] public int location_tb_id { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        /*

"id"	"bigint"
"company_id"	"bigint"
"location_tb_id"	"bigint"
"is_active"	"boolean"
"created_user_id"	"bigint"
"updated_user_id"	"bigint"
"created_date"	"timestamp without time zone"
"updated_date"	"timestamp without time zone"
            TABLE_NAME = 'basic_info_company_location'
         */
    }    
    
    //Datos del tipo de relación que se tiene con la empresa (cliente, proveedor)
    public class CompanyRelationshipBusiness
    {
        public int id { get; set; }
        [Required] public int company_id { get; set; }
        [Required] public int relationship_business_id { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        /*

"id"	"bigint"
"company_id"	"bigint"
"relationship_business_id"	"bigint"
"is_active"	"boolean"
"created_user_id"	"bigint"
"updated_user_id"	"bigint"
"created_date"	"timestamp without time zone"
"updated_date"	"timestamp without time zone"
            TABLE_NAME = 'basic_info_company_relationship_business'
         */
    }

}
