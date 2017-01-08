using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace quotingDojo2.Models
{
 public abstract class BaseEntity {}
 public class User : BaseEntity
 {
  [Key]
  public long Id { get; set; }
  [Required]
  [MinLength(2)]
  [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Names cannot contain numbers.")]
  public string first_name { get; set; }
  [Required]
  [MinLength(2)]
  [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Names cannot contain numbers.")]
  public string last_name { get; set; }
  [Required]
  [EmailAddress]
  public string email { get; set; }
  [Required]
  [MinLength(8)]
  public string password { get; set; }
  [Required]
  [MinLength(8)]
  [Compare("password", ErrorMessage = "The passwords entered do not match")]
  public string confirm_password  { get; set; }
  public ICollection<Quote> quotes { get; set; }
 }
}