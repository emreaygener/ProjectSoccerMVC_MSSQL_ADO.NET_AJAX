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
    public DateTime DateOfBirth { get; set; }
    [Display(Name = "Club")]
    [ForeignKey(nameof(Club))]
    public int ClubId { get; set; }
    public string CurrentClub { get; set; }
}
