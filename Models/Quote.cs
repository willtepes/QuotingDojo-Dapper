using System.ComponentModel.DataAnnotations;
using System;
namespace quotingDojo2.Models
{
 
 public class Quote : BaseEntity
 {
  [Key]
  public long Id { get; set; }
  [Required]
  [MinLength(5)]
  public string quote { get; set; }
  public User user { get; set;}
  public long Users_id {get; set;}
  public DateTime created_at { get; set; }
 }
}