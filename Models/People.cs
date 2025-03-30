using System.ComponentModel.DataAnnotations;


namespace FairyBE.Models
{
    public class People
    {
        public int id { get; set; }
        [Required] public string? first_name { get; set; }
        [Required] public string? last_name { get; set; }
        [Required] public string? document_number { get; set; }
        [Required] public string? photo_people { get; set; }
        [Required] public DateTime date_of_birth { get; set; }
        public DateTime date_of_death { get; set; }
        [Required] public string? description { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public int age_group_id { get; set; }
        [Required] public int document_type_id { get; set; }
        [Required] public int gender_id { get; set; }
        [Required] public int type_of_diner_id { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        /*
            "id"	"bigint"
            "first_name"	"character varying"
            "last_name"	"character varying"
            "document_number"	"character varying"
            "photo_people"	"character varying"
            "date_of_birth"	"date"
            "date_of_death"	"date"
            "description"	"text"
            "created_date"	"timestamp with time zone"
            "updated_date"	"timestamp with time zone"
            "is_active"	"boolean"
            "age_group_id"	"bigint"
            "created_user_id"	"bigint"
            "document_type_id"	"bigint"
            "gender_id"	"bigint"
            "type_of_diner_id"	"bigint"
            "updated_user_id"	"bigint"
        TABLE_NAME = 'basic_info_people'
         */

    }
    //En caso de que una persona tenga mas de un contacto. Se relaciona al contacto con la persona
    public class PeopleContact
    {
        public int id { get; set; }
        [Required] public int people_id { get; set; }
        [Required] public int contact_tb_id { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        /*
            "id"	"bigint"
            "people_id"	"bigint"
            "contact_tb_id"	"bigint"
            "is_active"	"boolean"
            "created_user_id"	"bigint"
            "updated_user_id"	"bigint"
            "created_date"	"timestamp without time zone"
            "updated_date"	"timestamp without time zone"
          TABLE_NAME = 'basic_info_people_contact'
         */
    }

    public class PeopleInvoiceData
    {
        public int id { get; set; }
        [Required] public int people_id { get; set; }
        [Required] public int invoice_data_id { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        /*
             "id"	"bigint"
            "people_id"	"bigint"
            "invoice_data_id"	"bigint"
            "is_active"	"boolean"
            "created_date"	"timestamp without time zone"
            "updated_date"	"timestamp without time zone"
            "created_user_id"	"bigint"
            "updated_user_id"	"bigint"
                  TABLE_NAME = 'basic_info_people_invoice_data'
         */

    }


    public class PeopleLocation
    {
        public int id { get; set; }
        [Required] public int people_id { get; set; }
        [Required] public int location_tb_id { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        /*
            "id"	"bigint"
            "people_id"	"bigint"
            "location_tb_id"	"bigint"
            "is_active"	"boolean"
            "created_user_id"	"bigint"
            "updated_user_id"	"bigint"
            "created_date"	"timestamp without time zone"
            "updated_date"	"timestamp without time zone"
            TABLE_NAME = 'basic_info_people_location'
         */
    }
    public class PeopleRelationshipBusiness
    {
        public int id { get; set; }
        [Required] public int people_id { get; set; }
        [Required] public int relationship_business_id { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        /*
"id"	"bigint"
"people_id"	"bigint"
"relationship_business_id"	"bigint"
"is_active"	"boolean"
"created_user_id"	"bigint"
"updated_user_id"	"bigint"
"created_date"	"timestamp without time zone"
"updated_date"	"timestamp without time zone"
            TABLE_NAME = 'basic_info_people_relationship_business'
         */
    }


	public class RegisterPeopleWithDataRequest
	{
		public People BasicInfoPeople { get; set; }
		public IEnumerable<Contacts>? Contacts { get; set; }
		public IEnumerable<Locations>? Locations { get; set; }
		public IEnumerable<BusinessInvoiceData>? BusinessInvoiceData { get; set; }
	}



}
