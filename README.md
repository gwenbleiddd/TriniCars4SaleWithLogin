# Student Services Help Portal
**ITEC 325 – Final Group Project (Delta Team)**

## Project Overview
The Student Services Help Portal is a web-based system designed to allow students to submit service requests and track their status and associated staff comments. The application uses a client-side interface built with HTML, CSS, and JavaScript, and a PHP backend connected to a MySQL database.

The system does not require user registration or login. Instead, requests are accepted only from pre-existing users stored in the database. Students can later check the status of their submitted requests using their email address.

This project was developed collaboratively over two short Agile sprints.

---

## Features
- Web form for the submission of student service requests
- Validate requests against pre-existing users
- Store and retrieve data using MySQL
- View request, status and staff comments by student email
- Dynamically display request details
- Basic form validation and error handling
- Automated deployment via GitHub Actions

---

## Usage

Submitting a request is handled through the main form on the home page. You must provide a valid COSTAATT email address (e.g., user@costaatt.edu.tt), as the system performs a client-side check via a regular expression in validation.js and a server-side check against the database. Once a request is submitted, it is assigned a default priority and stored with the associated user ID.

To check the status of a submission, visit the Requests page and enter the same email address used for the request. The application will perform a SQL JOIN to find all tickets linked to that user account. You can click on individual request titles to expand the details, which includes the submission date, category, and a dedicated section for viewing comments left by staff members.

---

## Setup 

### Requirements

- Git: The Git version control system is required to clone the project repository, manage branch workflows, and maintain a history of changes to the source code.

- Code Editor: A professional-grade editor like Visual Studio Code is required to manage the project workspace, providing syntax highlighting for PHP and debugging tools for the validation.js script.

- Web Browser: A modern, standards-compliant web browser such as Google Chrome or Mozilla Firefox is necessary to render the CSS Flexbox layout and execute the client-side validation logic.

- Server Stack: This project requires XAMPP or WampServer (Windows, Apache, MySQL, PHP) to simulate a live web environment. It provides the Apache HTTP Server to serve the web pages, the PHP interpreter to process server-side logic, and the MySQL database engine for persistent data storage.

- PHP and Extensions: The system requires PHP 7.4 or higher. Within the WAMP configuration, the mysqli and pdo_mysql extensions must be enabled to support the database interaction logic used in index.php and view_request.php.

- Database Management: MySQL or MariaDB is used as the relational database management system. Access to phpMyAdmin, which is bundled with WAMP, is necessary for importing the initial schema_startup.sql and managing the users and service_requests tables.

### Setup Instructions

1. Prepare the Environment: Install a local server stack like XAMPP or WAMP that includes PHP 8.x and MySQL.

2. Project Placement: Create a folder named costaatt-service-portal in your server's root directory (e.g., htdocs or www) and move all project files, including index.php, view_request.php, validation.js, and the css folder, into this location.

3. Database Import: Open PHPMyAdmin in your browser. Log in using the default credentials (Username root, empty password). Create a new database named portal_db, click on the Import tab, and select the schema_startup.sql file to generate the required tables and sample user data. Verify t5hat the tables have been populated with sample data.

4. Access the Portal: Open your web browser and navigate to http://localhost/costaatt-service-portal/index.php to begin using the system.

---

## Technology Stack
**Frontend**
- HTML
- CSS
- JavaScript

**Backend**
- PHP

**Database**
- MySQL

**DevOps & Tooling**
- Git & GitHub
- GitHub Classroom
- GitHub Issues & Projects
- GitHub Actions (CI/CD)
- InfinityFree (Hosting)

---

## System Architecture (High-Level)
1. User submits a service request using the home page form (`index.php`)
2. PHP validates the email against the `users` table
3. Request data is stored in the `service_requests` table
4. Staff comments are stored in the `request_comments` table
5. Users check request status via `view_request.php`
6. Results are dynamically rendered on the same page

---

## Folder Structure

/  
├── css/  
├── images/  
├── db.php  
├── index.php  
├── view_request.php  
├── validation.js  
├── schema_startup.sql  
├── infinityfree-deploy.yml  
├── README.md  

---

## Database Overview
The system uses three primary tables:
- `users`
- `service_requests`
- `request_comments`

Detailed database documentation and relationships are provided in the GitHub Wiki.

---

## Validation & Error Handling
- Email validation on form submission
- Requests accepted only from existing users
- Consideration given to the handling of empty search results
- Conditional rendering of request data

---

## Agile Methodology
This project was developed using Agile methodology over two 2-day sprints.  
Sprint planning, task tracking, and collaboration were managed using GitHub Issues and GitHub Projects.

Detailed sprint documentation is available in the GitHub Wiki.

---

## CI/CD Pipeline
The project uses GitHub Actions to automatically deploy updates to InfinityFree whenever changes are pushed to the `main` branch.

Deployment is handled via FTP using secured GitHub Secrets.

---

## Team Members
- **Dominic Salandy** – Developer  
- **Leiah Charles** – Developer  
- **Lokanatha Phang** – Scrum Master / Assistant Developer  
- **Matthew Wingson** – Documentation / QA / Developer  
- **Simone Johnson** – Scrum Assistant / Developer  

---

## Deployment
Hosting is configured on InfinityFree.  
[(https://itec325-final-group-project-delta.fwh.is)]

---

## License
This project is developed for academic purposes as part of the ITEC 325 course.

