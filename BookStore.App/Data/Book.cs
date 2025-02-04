using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.App.Data;

public class Book
{
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Title { get; set; }
    
    [Required]
    [StringLength(20)]
    [Column(TypeName = "VARCHAR")]
    public string Isbn { get; set; }
    public string Category { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(20)")]
    public string Publisher { get; set; }

    [Required]
    public DateTime PublishedAt { get; set; }

    public string UserId { get; set; }
    public IdentityUser User { get; set; }
}