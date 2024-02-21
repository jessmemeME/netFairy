using System.ComponentModel.DataAnnotations;

namespace FairyBE.Models
{
    public class BusinessInvoiceData
    {
        public int id { get; set; }
        [Required] public string? name { get; set; }
        [Required] public string? document_number { get; set; }
        [Required] public string? description { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }

        /*"id"	"bigint"
"name"	"character varying"
"document_number"	"character varying"
"description"	"text"
"created_date"	"timestamp with time zone"
"updated_date"	"timestamp with time zone"
"is_active"	"boolean"
"created_user_id"	"bigint"
"updated_user_id"	"bigint"
          TABLE_NAME = 'business_invoice_data'
         */

    }


    //Datos del tipo de relación que se tiene con la empresa (cliente, proveedor)
    public class BusinessColor
    {
        public int id { get; set; }
        [Required] public string? color_origin { get; set; }//Gama de color al que pertenece rojos, verdes,amarillos,azules,blancos, etc.
        [Required] public string? name { get; set; }
        [Required] public string? description { get; set; }
        [Required] public string? code_RGB { get; set; }
        [Required] public string? code_hexadecimal { get; set; }
        [Required] public string? code_CMK { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        /*

"id"	"bigint"
"color_origin"	"character varying"
"name"	"character varying"
"description"	"text"
"code_RGB"	"character varying"
"code_hexadecimal"	"character varying"
"code_CMK"	"character varying"
"created_date"	"timestamp with time zone"
"updated_date"	"timestamp with time zone"
"is_active"	"boolean"
"created_user_id"	"bigint"
"updated_user_id"	"bigint"
            TABLE_NAME = 'business_color'
         */
    }    
    
    public class BusinessInvoiceType
    {
        public int id { get; set; }
        [Required] public string? name { get; set; }
        [Required] public string? description { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        /*

"id"	"bigint"
"name"	"character varying"
"description"	"text"
"created_date"	"timestamp with time zone"
"updated_date"	"timestamp with time zone"
"is_active"	"boolean"
"created_user_id"	"bigint"
"updated_user_id"	"bigint"
            TABLE_NAME = 'business_invoice_type'
         */
    }    
    public class BusinessProduct
    {
        public int id { get; set; }
        [Required] public string? name { get; set; }
        [Required] public string? description { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public int id_product_type_id { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        /*

"id"	"bigint"
"name"	"character varying"
"description"	"text"
"created_date"	"timestamp with time zone"
"updated_date"	"timestamp with time zone"
"is_active"	"boolean"
"created_user_id"	"bigint"
"id_product_type_id"	"bigint"
"updated_user_id"	"bigint"
            TABLE_NAME = 'business_product'
         */
    }

    public class BusinessProductType
    {
        public int id { get; set; }
        [Required] public string? name { get; set; }
        [Required] public string? description { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        /*

"id"	"bigint"
"name"	"character varying"
"description"	"text"
"created_date"	"timestamp with time zone"
"updated_date"	"timestamp with time zone"
"is_active"	"boolean"
"created_user_id"	"bigint"
"updated_user_id"	"bigint"
            TABLE_NAME = 'business_product_type'
         */
    }

    
    public class BusinessService
    {
        public int id { get; set; }
        [Required] public string? name { get; set; }
        [Required] public string? description { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public int id_service_type_id { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        /*

"id"	"bigint"
"name"	"character varying"
"description"	"text"
"created_date"	"timestamp with time zone"
"updated_date"	"timestamp with time zone"
"is_active"	"boolean"
"created_user_id"	"bigint"
"id_service_type_id"	"bigint"
"updated_user_id"	"bigint"
            TABLE_NAME = 'business_service'
         */
    }    


    public class BusinessServiceType
    {
        public int id { get; set; }
        [Required] public string? name { get; set; }
        [Required] public string? description { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        /*

"id"	"bigint"
"name"	"character varying"
"description"	"text"
"created_date"	"timestamp with time zone"
"updated_date"	"timestamp with time zone"
"is_active"	"boolean"
"created_user_id"	"bigint"
"updated_user_id"	"bigint"
            TABLE_NAME = 'business_service_type'
         */
    }
    

    public class BusinessTax
    {
        public int id { get; set; }
        [Required] public string? name { get; set; }
        [Required] public string? description { get; set; }
        [Required] public int amount { get; set; }
        [Required] public int percent { get; set; }
        [Required] public string? time_present { get; set; }
        [Required] public int id_tax_type_id { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        /*

"id"	"bigint"
"name"	"character varying"
"description"	"text"
"amount"	"integer"
"percent"	"integer"
"time_present"	"character varying"
"created_date"	"timestamp with time zone"
"updated_date"	"timestamp with time zone"
"is_active"	"boolean"
"created_user_id"	"bigint"
"id_tax_type_id"	"bigint"
"updated_user_id"	"bigint"
            TABLE_NAME = 'business_tax'
         */
    }
    
    public class BusinessTaxType
    {
        public int id { get; set; }
        [Required] public string? name { get; set; }
        [Required] public string? siglas { get; set; }
        [Required] public string? description { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        /*

"id"	"bigint"
"name"	"character varying"
"siglas"	"character varying"
"description"	"text"
"created_date"	"timestamp with time zone"
"updated_date"	"timestamp with time zone"
"is_active"	"boolean"
"created_user_id"	"bigint"
"updated_user_id"	"bigint"
            TABLE_NAME = 'business_tax_type'
         */
    }

        
    public class BusinessTaxTypeCountry
    {
        public int id { get; set; }
        [Required] public int tax_type_id { get; set; }
        [Required] public int country_id { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        /*

"id"	"bigint"
"tax_type_id"	"bigint"
"country_id"	"bigint"
"is_active"	"boolean"
"created_user_id"	"bigint"
"updated_user_id"	"bigint"
"created_date"	"timestamp without time zone"
"updated_date"	"timestamp without time zone"
            TABLE_NAME = 'business_tax_type_id_country'
         */
    }



}
