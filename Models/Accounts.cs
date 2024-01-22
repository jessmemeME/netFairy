using System.ComponentModel.DataAnnotations;

namespace FairyBE.Models
{
    public class Accounts // USER
    {
        public int id { get; set; }
        [Required] public  string password { get; set; }
        [Required] public  bool is_superuser { get; set; }
        [Required] public  string email { get; set; }
        [Required] public  bool is_staff { get; set; }
        [Required] public  bool is_active { get; set; }
        [Required] public  DateTime date_joined { get; set; }
        [Required] public  DateTime last_updated { get; set; }
        [Required] public  DateTime last_login { get; set; }

        /*
            id:number;
            password:string;
            is_superuser:boolean;
            email:string;
            is_staff:boolean;
            is_active:boolean;
            date_joined:date;
            last_updated:date;
            last_login:date;
        TABLE_NAME = 'accounts_user'
         */
    }

    public class Profile
    {
        public int id { get; set; }
        [Required] public string auth_token { get; set; }
        [Required] public bool is_verified { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int updated_user_id { get; set; }
        [Required] public int user_id { get; set; }
        /*
            "id"	"bigint"
            "auth_token"	"character varying"
            "is_verified"	"boolean"
            "is_active"	"boolean"
            "created_date"	"timestamp with time zone"
            "updated_date"	"timestamp with time zone"
            "created_user_id"	"bigint"
            "updated_user_id"	"bigint"
            "user_id"	"bigint"
            TABLE_NAME = 'accounts_profile'
         */
    }

    public class UserGroup
    {
        public int id { get; set; }
        [Required] public int user_id { get; set; }
        [Required] public int group_id { get; set; }


        /*
            "id"	"bigint"
            "user_id"	"bigint"
            "group_id"	"integer"
        TABLE_NAME = 'accounts_user_groups'
         */

    }

    public class UserPermissions
    { 
        public int id { get; set; }
        [Required] public int user_id { get; set; }
        [Required] public int permission_id { get; set; }

        /*
            "id"	"bigint"
            "user_id"	"bigint"
            "permission_id"	"integer"
        TABLE_NAME = 'accounts_user_permissions'
         */

    }

}
