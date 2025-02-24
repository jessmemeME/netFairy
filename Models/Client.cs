using System.ComponentModel.DataAnnotations;

namespace FairyBE.Models
{
    public class Client
    {
        public int id { get; set; }
        [Required] public string? type { get; set; }
        [Required] public string? name { get; set; }
        [Required] public string? description { get; set; }
        [Required] public bool is_confirmated { get; set; }
        [Required] public bool is_active { get; set; }
        [Required] public DateTime created_date { get; set; }
        [Required] public DateTime updated_date { get; set; }
        [Required] public int created_user_id { get; set; }
        [Required] public int people_id { get; set; }
        [Required] public int updated_user_id { get; set; }

    }

	public class RegisterClientsWithPeopleRequest
	{
		public Client ClientsClient { get; set; }
		public People BasicInfoPeople { get; set; }
		public IEnumerable<Contacts>? Contacts { get; set; }
		public IEnumerable<Locations>? Locations { get; set; }
		public IEnumerable<BusinessInvoiceData>? BusinessInvoiceData { get; set; }
	}

	public class ClientPeopleModel
	{
		public Client Client { get; set; }
		public People People { get; set; }
	}
}
