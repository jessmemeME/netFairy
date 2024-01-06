namespace FairyBE.Models
{
    public class Accounts
    {
        public int id { get; set; }
        public required string password { get; set; }
        public required Boolean is_superuser { get; set; }
        public required string email { get; set; }
        public required Boolean is_staff { get; set; }
        public required Boolean is_active { get; set; }
        public required DateTime date_joined { get; set; }
        public required DateTime last_updated { get; set; }
        public required DateTime last_login { get; set; }

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
