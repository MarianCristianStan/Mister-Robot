using System.ComponentModel.DataAnnotations;

namespace Mister_Robot.Models
{
	public class ContactMessage
	{
		[Key]
		[MaxLength(50)]
		public string ContactMessageId { get; set; } = $"CONTACT-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

		[Required]
		[MaxLength(50)]
		public required string Name { get; set; }

		[Required]
		[EmailAddress]
		[MaxLength(50)]
		public required string Email { get; set; }

		[Required]
		[MaxLength(500)]
		public required string Message { get; set; }

		public DateTime SentAt { get; set; } = DateTime.UtcNow;

		public bool IsReplied { get; set; }

		[MaxLength(500)]
		public string? AdminReply { get; set; }

		public string? Username { get; set; }
		public string? RespondedBy { get; set; }
	}

}
