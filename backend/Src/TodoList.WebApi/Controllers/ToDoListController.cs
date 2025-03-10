using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TodoList.WebApi.DataTransferObjects;
using TodoList.WebApi.Services;

namespace TodoList.WebApi.Controllers
{
    public class ToDoListController : Controller
    {
        private readonly IToDoListManagementService _service;
        public ToDoListController(IToDoListManagementService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("todo/assignments")]
        public IActionResult GetAllToDoAssignments()
        {
            var response = _service.GetAllAssignemnts();
            return Ok(response);
        }

        [HttpPost]
        [Route("todo/assignments/add")]
        public async Task<IActionResult> AddToDo([FromBody] ToDoListAssignmentDTO toDoListAssignmentDTO)
        {

            if (toDoListAssignmentDTO == null || string.IsNullOrWhiteSpace(toDoListAssignmentDTO.Description))
            {
                return BadRequest("Invalid input: Description is required.");
            }

            var response = await _service.AddNewToDoAssignment(toDoListAssignmentDTO);
            return response == null ? BadRequest("Could not add item") : Ok(response);
        }

        [HttpDelete]
        [Route("todo/assignments/delete")]
        public IActionResult DeleteToDo (int id)
        {
            var response = _service.DeleteAssignmentFromToDo(id);
            if (response == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("todo/assignments/update/{id}")]
        public async Task<IActionResult> UpdateToDo(int id, [FromBody] ToDoListAssignmentDTO toDoDto)
        {
            if (string.IsNullOrWhiteSpace(toDoDto.Description))
            {
                return BadRequest("Description cannot be empty.");
            }

            var updatedNote = await _service.UpdateToDoAssignment(id, toDoDto);

            if (updatedNote == null)
            {
                return NotFound("Task not found.");
            }

            return Ok(updatedNote);
        }
    }
}
