# To-Do List API with ASP.NET Core

A simple multi-user authenticated To-Do List application built with **ASP.NET Core** to learn and demonstrate building RESTful APIs with JWT authentication.

---

## Overview

This project is a practical and fun way I learned how to build secure, RESTful APIs using ASP.NET Core. It features:

- User registration and login with JWT-based authentication
- Secure endpoints for managing user-specific To-Do tasks
- Full CRUD operations: add, read, update (complete), and delete tasks
- A clean and responsive frontend (HTML/CSS/JavaScript) that consumes the API
- Demonstrates token handling, authorization, and client-server interaction

---

## Technologies Used

- **ASP.NET Core 8** - Web API framework
- **Entity Framework Core** - Database ORM and migrations
- **Microsoft SQL Server** - Database backend
- **JWT (JSON Web Tokens)** - Secure authentication tokens
- **Vanilla JavaScript, HTML, CSS** - Frontend UI consuming the API

---

## Features

- **User Authentication:** Register and login with username and password
- **JWT Secured:** All To-Do operations require a valid JWT token
- **Task Management:** Add new tasks, mark as completed/uncompleted, delete tasks
- **User-specific Data:** Each user only accesses their own tasks
- **Simple Responsive UI:** Dashboard with task list, add task form, and logout

---

## How I Built It

1. **Set up ASP.NET Core API project:** Created models, database context, and configured EF Core for SQL Server.
2. **Implemented JWT Authentication:** Used JWT tokens for secure user login and protecting API endpoints.
3. **Created To-Do Controller:** Developed RESTful endpoints for managing tasks with authorization.
4. **Built Frontend:** Created a simple, clean UI with HTML, CSS, and JavaScript to interact with API.
5. **Tested & Debugged:** Verified APIs using Swagger and frontend integration with proper error handling.
6. **Polished UI:** Styled forms, buttons, and task list for a professional look.

---

## Getting Started

1. Clone the repository.
2. Configure the SQL Server connection string in `appsettings.json`.
3. Run EF Core migrations to create the database.
4. Run the API project.
5. Open the frontend HTML pages in your browser.
6. Register a new user, login, and start managing your tasks!

---

## Screenshots

<img width="1919" height="1079" alt="Screenshot 2025-08-05 121909" src="https://github.com/user-attachments/assets/9599af7c-cf74-43b8-965a-4fd963bf556c" />
<img width="1919" height="1079" alt="Screenshot 2025-08-05 121932" src="https://github.com/user-attachments/assets/bd872a69-69bd-4b4f-a37b-f4c953b76742" />


---

## Learnings & Next Steps

This project helped me understand:

- REST principles and API design
- Token-based authentication and authorization
- Entity Framework migrations and database seeding
- Frontend-backend integration with Fetch API

Next, I plan to:

- Add user profile editing
- Improve UI with a frontend framework like React or Angular

---

Feel free to explore, suggest improvements, or fork it for your own learning!

---


