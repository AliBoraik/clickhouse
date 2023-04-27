using ClickHouseDemo.Models;

namespace ClickHouseDemo.Services;

public interface IClickHouseService
{ 
    Task  AddUserTable();
    Task DeleteUserTable();
    Task Adduser(UserRequestDto user);
    Task RemoveUser(string id);
    Task<User?> FindUserByNameAsync(string name);
    Task<User?> FindUserByAgeAsync(int age);
}