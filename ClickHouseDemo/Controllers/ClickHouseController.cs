using ClickHouseDemo.Models;
using ClickHouseDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClickHouseDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class ClickHouseController : ControllerBase
{
    private readonly IClickHouseService _clickHouseService;
    private readonly ILogger<ClickHouseController> _logger;


    public ClickHouseController(IClickHouseService clickHouseService, ILogger<ClickHouseController> logger)
    {
        _clickHouseService = clickHouseService;
        _logger = logger;
    }

    [HttpGet("GetUserByName/{name}")]
    public async Task<IActionResult> GetUserByName(string name)
    {
        try
        {
            var user = await _clickHouseService.FindUserByNameAsync(name);
            if (user == null) return NotFound();
            return Ok(user);
        }
        catch (Exception e)
        {
            _logger.LogError("LogError {0} with message : {Message}", Request.Path,e.Message);
           return StatusCode(StatusCodes.Status500InternalServerError ,  "Internal server error. Please retry later.");
        }
    }
    
    [HttpPut("InsertUserToTable")]
    public async Task<IActionResult> InsertUserToTable(UserRequestDto user)
    {
        try
        {
            await _clickHouseService.Adduser(user);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError("LogError {0} with message : {Message}", Request.Path,e.Message);
           return StatusCode(StatusCodes.Status500InternalServerError ,  "Internal server error. Please retry later.");
        }
    }
    
    [HttpGet("GetUserByNameOrAge/")]
    public async Task<IActionResult> GetUserByNameOrAge(string name,int age)
    {
        try
        {
            var user = await _clickHouseService.FindUserByNameAsync(name);
            if (user != null) return Ok(user);
            user = await _clickHouseService.FindUserByAgeAsync(age);
            if (user != null) return Ok(user);
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError("LogError {0} with message : {Message}", Request.Path,e.Message);             
           return StatusCode(StatusCodes.Status500InternalServerError ,  "Internal server error. Please retry later.");
        }
    }
    
    [HttpDelete("RemoveUserFormTable/")]
    public async Task<IActionResult> RemoveUser(string id)
    {
        try
        {
            await _clickHouseService.RemoveUser(id);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError("LogError {0} with message : {Message}", Request.Path,e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError ,  "Internal server error. Please retry later.");
        }
    }

    [HttpPost("CreateUserTable")]
    public async Task<IActionResult> CreateUserTable()
    {
        try
        {
            await _clickHouseService.AddUserTable();
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError("LogError {0} with message : {Message}", Request.Path,e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError ,  "Internal server error. Please retry later.");
        }
    }
    
    [HttpDelete("RemoveUserTable/")]
    public async Task<IActionResult> RemoveUserTable()
    {
        try
        {
            await _clickHouseService.DeleteUserTable();
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError("LogError {0} with message : {Message}", Request.Path,e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError ,  "Internal server error. Please retry later.");
        }
    }
}
