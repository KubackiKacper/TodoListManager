﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoList.WebApi.DataTransferObjects
{
    public class SaveToDoListAssignmentDTO
    {
        [Required]
        [MaxLength(255)]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [Required]
        [JsonPropertyName("createdDate")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [JsonPropertyName("completionStatus")]
        public bool CompletionStatus { get; set; }
    }
}
