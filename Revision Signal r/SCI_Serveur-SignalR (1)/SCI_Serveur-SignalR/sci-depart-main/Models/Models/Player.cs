using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Models.Interfaces;

namespace Super_Cartes_Infinies.Models
{
	public class Player : IModel
    {
		public Player()
		{
		}

		public int Id { get; set; }
		public string Name { get; set; } = "";
		public required string UserId { get; set; }
		[JsonIgnore]
		public virtual IdentityUser User { get; set; }
    }
}

