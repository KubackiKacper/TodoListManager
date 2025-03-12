# TodoList Assignment  
## Preview:
![til](/readme_photos/app.gif)

## Backend
### This is my implementation of a To-Do list manager. The backend services are implemented using .NET Core Web API. For data storage, I decided to use an SQLite database and Entity Framework Core for manipulating data. After creating the migration, the database is seeded with one example To-Do assignment. I am using a Data Transfer Objects (DTO) to ensure that the original object remains unchanged. My implementation revolves around the service *ToDoListManagementService*, which handles all business logic:
    -Creating a new To-Do task,
    -Reading existing To-Do tasks.
    -Updating an existing To-Do task,
    -Deleting a To-Do task.
## Then, I configure the API endpoint in the application's controller, which calls the necessary methods from the service. I use Swagger as a graphical representation of every API endpoint. You can access it by navigating to the following URL:
    https://localhost:7213/swagger
![swagger](/readme_photos/swagger.PNG)

## Integration Test
### I implemented integration tests to ensure the reliability of the API. These tests call the API endpoints from the controller and validate that all CRUD operations:
    -Creating, 
    -Reading, 
    -Updating,
    -Deleting 
## are working correctly. The tests simulate real-world API interactions, verifying that data is correctly stored, retrieved, and modified in the additional SQLite database, that separates production data from test data. This helps maintain the integrity of the application and prevents regressions in future updates.
![testExplorer](/readme_photos/testExplorer.PNG)

## Frontend
### As shown in the preview at the top of the README file, I implemented a user interface based on React components and their state, which handle all CRUD operations. To keep users informed about the status of their actions, I use custom alerts from *react-toastify*. These notifications ensure that users receive the necessary feedback on whether an action was successfully completed or not. The frontend communicates with the backend API for all CRUD operations. To install all dependencies required for running the code, run the following command in the terminal:
    npm install