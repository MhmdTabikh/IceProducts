using System.ComponentModel.DataAnnotations;

namespace IceProducts.Server.Data;
public class User
{
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
