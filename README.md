[<img alt="ClickHouse — open source distributed column-oriented DBMS" width="400px" src="https://clickhouse.com/images/ch_gh_logo_rounded.png" />](https://clickhouse.com?utm_source=github)

# ClickHouse .NET App

This is a .NET application that integrates with ClickHouse, an open-source columnar database management system. The application provides an API for performing CRUD operations on user data stored in ClickHouse.

## Getting Started

### Prerequisites

Before running the application, ensure that you have the following dependencies installed:

- .NET SDK [link to installation guide]
- ClickHouse [link to ClickHouse installation guide]

### Installation

1. Clone the repository:
2. Build and run the Docker containers:
```docker
docker-compose up
```
3.  URL http://localhost:5105 to access the application.


### API Endpoints

The following API endpoints are available:

| Method | URL                                  | Description                       |
| ------ | ------------------------------------ | --------------------------------- |
| GET    | /clickhouse/GetUserByName/{name}     | Get user by name                  |
| PUT    | /clickhouse/InsertUserToTable        | Insert user into table            |
| GET    | /clickhouse/GetUserByNameOrAge       | Get user by name or age           |
| DELETE | /clickhouse/RemoveUserFromTable      | Remove user from table            |
| POST   | /clickhouse/CreateUserTable          | Create user table                 |
| DELETE | /clickhouse/RemoveUserTable          | Remove user table   




