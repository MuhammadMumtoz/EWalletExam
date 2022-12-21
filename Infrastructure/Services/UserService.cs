namespace Infrastructure.Services;
using AutoMapper;
using Domain.Dtos;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Wrapper;
using System.Net;

public class UserService{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public UserService(DataContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<GetUserDto>>> GetAllUsers(){
        var list =  await _context.Users.ToListAsync();
        var response = _mapper.Map<List<GetUserDto>>(list);
        return new Response<List<GetUserDto>>(response);
    }

    public async Task<Response<GetUserDto>> GetById(int id){
        var user = await _context.Users.FindAsync(id);
        var response = _mapper.Map<GetUserDto>(user);
        return new Response<GetUserDto>(response);
    }
    public async Task<Response<GetUserWalletDto>> GetUserFullInfo(int id){
        var user = (from us in _context.Users
                    join wl in _context.Wallets on us.UserId equals wl.UserId
                    where us.UserId == id
                    select new GetUserWalletDto
                    {
                        UserId = us.UserId,
                        Firstname = us.FirstName,
                        Lastname = us.LastName,
                        PhoneNumber = us.PhoneNumber,
                        Email = us.Email,
                        Birthdate = us.BirthDate,
                        Gender = us.Gender,
                        Address = us.Address,
                        WalletId = wl.WalletId,
                        StartDate = wl.StartDate,
                        EndDate = wl.EndDate,
                        Balance = wl.Balance,
                        Status = wl.Status
                    }).FirstOrDefaultAsync();
        var response =  await user;
        return new Response<GetUserWalletDto>(response);
    }
    public async Task<Response<AddUserDto>> AddUser(AddUserDto user){
        var newUser = _mapper.Map<User>(user);
        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
        var response = user;
        return new Response<AddUserDto>(response);
    }
    public async Task<Response<AddUserDto>> UpdateUser(AddUserDto user){
        var newUser = await _context.Users.FindAsync(user.UserId);
        newUser.FirstName = user.Firstname;
        newUser.LastName = user.Lastname;
        newUser.PhoneNumber = user.PhoneNumber;
        newUser.Email = user.Email;
        newUser.BirthDate = user.Birthdate;
        newUser.Gender = user.Gender;
        newUser.Address = user.Address;
        // newUser.UserId = user.UserId;
        await _context.SaveChangesAsync();
        var response = user;
        return new Response<AddUserDto>(response);
    }
    public async Task<Response<string>> DeleteUser(int id){
        var find = await _context.Users.FindAsync(id);
        _context.Users.Remove(find);
         var response = await _context.SaveChangesAsync();
         if(response>0)
                return new Response<string>("Category deleted successfully");
                return new Response<string>(HttpStatusCode.BadRequest,"Category not found");
    }
}