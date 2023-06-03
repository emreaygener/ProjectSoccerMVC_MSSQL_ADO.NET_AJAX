using ProjectSoccer.Models.Shared;

namespace ProjectSoccer.Models.ViewModels
{
    public class PlayerUpdateVM:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string ClubId { get; set; }
        public string CurrentClub { get; set; }
    }
}
