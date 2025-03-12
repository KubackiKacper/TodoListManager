# TodoList Assignment  
## Preview:
![til](/readme_photos/app.gif)

## Backend
### This is my implementation of To Do list manager. Backend services are implemented using .Net Core Web API. For data storage I decided to use SQLite database and EntityFrameworkCore for manimulating data. After creating migration database is seeded with one example ToDo assignment. I am using data transfer object to ensure that original object remain unchanged. My implementation rewolves around service *ToDoListManagementService* that handles all buisness logic:
    -Creating new ToDo task,
    -Reading already existing ToDo tasks.
    -Updating already existing ToDo task,
    -Deleting ToDo task.
## Then, I configure API endpoint in the controller of the application, that calls necesarry methods from the service. I use swagger as graphical representation of every API endpoint. You can access it by redirecting to url:
    https://localhost:7213/swagger
![swagger](/readme_photos/swagger.PNG)
### ________________________________________________
## Integration Test
### I implemented integration tests, that call the API endpoints from controller and validate that all CRUD operations work correctly
![testExplorer](/readme_photos/testExplorer.PNG)
### ________________________________________________
## Frontend
### As you can see on the top of the readme file, I implemented user interface based on components and theirs state responsible for managing all CRUD operations. To inform user about state of desired action I use custom allerts from *react-toastify*. They ensure to provide user with necesarry information whether action was completed successfully or not. The frontend communicates with the backend API for all CRUD operations. To install all dependencies used in development I suggest to call this method in terminal:
    npm install