using System.ComponentModel.DataAnnotations;

namespace TodoList.WebApi.Models
{
    public class ToDoListAssignment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
