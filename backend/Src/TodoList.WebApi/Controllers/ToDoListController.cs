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
        public async Task <IActionResult> AddToDo(ToDoListAssignmentDTO toDoListAssignmentDTO)
        {
            var response = await _service.AddNewToDoAssignment(toDoListAssignmentDTO);
            if (response==null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
