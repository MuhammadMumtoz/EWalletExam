using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class UserController
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }
    [HttpGet("GetAllUsers")]
    public async Task<Response<List<GetUserDto>>> GetAllUsers()
    {
        return await _userService.GetAllUsers();
    }
    [HttpGet("GetUserById")]
    public async Task<Response<GetUserDto>> GetById(int id)
    {
        return await _userService.GetById(id);
    }
    [HttpGet("GetFullUserWalletInfo")]
    public async Task<Response<GetUserWalletDto>> GetUserFullInfo(int id)
    {
        return await _userService.GetUserFullInfo(id);
    }
    [HttpPost("AddUser")]
    public async Task<Response<AddUserDto>> AddUser(AddUserDto User)
    {
        return await _userService.AddUser(User);
    }
    [HttpPut("UpdateUser")]
    public async Task<Response<AddUserDto>> UpdateUser(AddUserDto User)
    {
        return await _userService.UpdateUser(User);
    }
    [HttpDelete("DeleteUser")]
    public async Task<Response<string>> DeleteUser(int id)
    {
        return await _userService.DeleteUser(id);
    }
}
