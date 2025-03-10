using System.ComponentModel.DataAnnotations;

namespace TodoList.WebApi.DataTransferObjects
{
    public class ToDoListAssignmentDTO
    {
        
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
