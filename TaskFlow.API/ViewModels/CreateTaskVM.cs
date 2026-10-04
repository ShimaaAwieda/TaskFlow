using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.ViewModels
{
    public class CreateTaskVM
    {
        [Required]
        [StringLength(100, ErrorMessage = "Title can't exceed 100 characters")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Description can't exceed 500 characters")]
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? AssignedUserId { get; set; }
    }
}
