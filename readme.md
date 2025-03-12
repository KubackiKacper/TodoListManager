# TodoList Assignment  

![til](/readme_photos/app.gif)

## This is my implementation of To Do list manager. Backend services are implemented using .Net Core Web API. For data storage I decided to use SQLite and EntityFrameworkCore. After creating initial migration database is seeded with one example object. I am using data transfer object to ensure that original object remain unchanged. My implementation resolves around service *ToDoListManagementService* that handles all buisness logic. Then, I configure API endpoint in the controller of the application, that calls necesarry methods from the service