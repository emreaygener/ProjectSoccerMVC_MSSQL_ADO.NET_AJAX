using ProjectSoccer.Models.Shared;
using System.ComponentModel.DataAnnotations;

namespace ProjectSoccer.Models;

public class Club : BaseEntity
{
    public int Id { get; set; }
    [Display(Name = "Club Name")]
    public string Name { get; set; }
    [Display(Name = "Abbreviation")]
    public string ShortName { get; set; }
    public string Logo { get; set; }
}
