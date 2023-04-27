using ClickHouse.Client.ADO;
using ClickHouse.Client.Utility;
using ClickHouseDemo.Models;
using Newtonsoft.Json.Linq;

namespace ClickHouseDemo.Services;

public class ClickHouseService : IClickHouseService
{
    private const string ConnectionString = "Host=localhost;Protocol=http;Port=8123";
    private readonly ClickHouseConnection _clickHouseConnection =  new(ConnectionString);

    public async Task AddUserTable()
    {
        await _clickHouseConnection.ExecuteScalarAsync(@"
        CREATE OR REPLACE TABLE user
            (
                id UInt64,
                name String,
                age UInt64
            )
        ENGINE = MergeTree
        PRIMARY KEY id;");
    }

    public async Task DeleteUserTable()
    {
        await _clickHouseConnection.ExecuteScalarAsync(@"DROP TABLE IF EXISTS user");
    }

    public async Task Adduser(UserRequestDto user)
    {
        var id = new Random().Next();
        var sql = $"INSERT INTO user (id, name,age) values ({id},'{user.Name}',{user.Age})";
        await _clickHouseConnection.ExecuteScalarAsync(sql);
    }

    public async Task RemoveUser(string id)
    {
        await _clickHouseConnection.ExecuteScalarAsync(
            $"DELETE FROM user WHERE id= {id}");
    }   

    public async Task<User?> FindUserByNameAsync(string name)
    {
        var sql =  $"SELECT id,name,age FROM user WHERE name = '{name}' FORMAT JSONColumns";
        var user = await FindUserBySql(sql);
        return user;
    }

    public async Task<User?> FindUserByAgeAsync(int age)
    {
        var sql =  $"SELECT id,name,age FROM user WHERE age = {age} FORMAT JSONColumns";
        var user = await FindUserBySql(sql);
        Console.WriteLine(user);
        return user;
    }

    private async Task<User?> FindUserBySql(string sql)
    {
        await using var command = _clickHouseConnection.CreateCommand();
        command.CommandText = sql;
        using var result = await command.ExecuteRawResultAsync(CancellationToken.None);
        await using var stream = await result.ReadAsStreamAsync();
        var json = await new StreamReader(stream).ReadToEndAsync();
        dynamic data = JObject.Parse(json);
        return data.id.Count == 0 ? null : new User
        {
            Id = data.id[0],
            Name = data.name[0],
            Age = data.age[0]
        };
    }
    
}