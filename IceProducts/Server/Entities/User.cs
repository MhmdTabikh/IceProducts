using System.ComponentModel.DataAnnotations;

namespace IceProducts.Server.Entities;
public class User
{
    [Key]
    public Guid Id { get; set; }

    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
