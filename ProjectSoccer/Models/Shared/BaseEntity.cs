using System.ComponentModel.DataAnnotations;

namespace ProjectSoccer.Models.Shared;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
}
