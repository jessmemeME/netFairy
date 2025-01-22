using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;

namespace FairyBE.Models
{
	public class Contacts
	{
		public long id { get; set; }
		[Required] public string? name { get; set; }
		[Required] public string? contact_data { get; set; }
		[Required] public string? verificated_token { get; set; }
		[Required] public bool is_verified { get; set; }
		[Required] public bool is_main_contact { get; set; }
		public string? description { get; set; }
		[Required] public DateTime created_date { get; set; }
		[Required] public DateTime updated_date { get; set; }
		[Required] public bool is_active { get; set; }
		[Required] public string? table_name { get; set; }
		[Required] public long contact_type_id { get; set; }
		[Required] public long created_user_id { get; set; }
		[Required] public long updated_user_id { get; set; }
	}


}
