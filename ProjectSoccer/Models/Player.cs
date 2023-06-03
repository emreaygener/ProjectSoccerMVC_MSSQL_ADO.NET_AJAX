using ProjectSoccer.Models.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectSoccer.Models;

public class Player : BaseEntity
{
    [Display(Name = "Name")]
    public string FirstName { get; set; }
    [Display(Name = "Surname")]
    public string LastName { get; set; }
    [Display(Name = "Age")]
    [Required(ErrorMessage = "Required Property")]
    public DateTime DateOfBirth { get; set; }
    [Display(Name = "Club")]
    [Required(ErrorMessage = "Required Property")]
    [ForeignKey(nameof(Club))]
    public int ClubId { get; set; }
    public string CurrentClub { get; set; }
}
