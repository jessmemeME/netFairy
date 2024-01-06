using System.ComponentModel.DataAnnotations;

namespace FairyBE.Models
{
    public class Accounts
    {
        public int id { get; set; }
        [Required] public  string password { get; set; }
        [Required] public  string firstName { get; set; }
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
         */
    }
}
