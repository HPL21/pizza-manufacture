# Pizza Manufacture
## Project Overview

This project is a full-stack application, consisting of Web API and GUI, designed to manage pizza production and ordering. It was developed following the principles of Universal Design, ensuring that the system is as usable as possible for all people, regardless of their abilities.

A key part of this project was the creation of two distinct frontend versions:
- Standard Version: A classic user interface.
- Accessibility-focused Version: Specifically optimized for users with diverse needs, following WCAG guidelines.

To verify which approach provides a better user experience, eye-tracker research was conducted. This study allowed for a detailed analysis of user visual attention and interaction patterns, helping to determine which version is more intuitive and accessible.
All of this is documented in extensive project documentation available in polish language.

Additionally, the system supports features such as:

- Calorie counter for pizzas calculated in real-time during the ordering process

- Dynamic menu management allowing administrators to easily change price or ingredients of specific pizzas

- Employee dashboard for displaying active orders, and allowing status updates

- Role-based Access: Separate scopes for customers and administrators.

## Technologies

- Backend: ASP.NET Core 9 using

- Frontend: Vue.js 3

- Authentication: ASP.NET Core Identity with JWT authentication

- Database: PostgreSQL

- Environment: Docker, Docker Compose, Kubernetes with Helm

## Authentication

The project uses ASP.NET Core Identity with JWT-based authentication. After logging in, users access specific scopes:

- Customers can browse the menu and place orders.

- Administrators manage the ingredient database and pizza recipes.

- Secure communication is ensured via JWT tokens between the Vue.js GUI and the .NET API.

## How to launch it

1. Install Taskfile (https://taskfile.dev/docs/installation) - optional
2. Install docker (if needed)
3. Run `task up` (`docker compose up -d` if you don't have Taskfile installed)

## Screenshots

- Main menu
![Main Menu](https://i.postimg.cc/pL9JXT5v/menu-Gowne.png)

- Placing order
![Placing order](https://i.postimg.cc/tTz0gDnH/Zamowienie.png)

- Order summary
![Order summary](https://i.postimg.cc/gJK904Lb/Podsumowanie.png)

- Adding new ingredient
![Adding new ingredient](https://i.postimg.cc/bJRXvLDB/Nowy-Skladnik.png)

- Admin Panel
![Admin Panel](https://i.postimg.cc/qRGf7QhP/panel-Administratora.png)

- Modifying pizza menu
![Modifying pizza menu](https://i.postimg.cc/zBkmfpHr/Zarzadzanie-Menu.png)

- Some of the endpoints
![Endpoints](https://i.postimg.cc/RZ3LVF6p/endpoints.png)


