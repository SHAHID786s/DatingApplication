using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly DataContext _ctx;
        private readonly IUserRepository _repo;
        private readonly IMapper _map; // comes from automapper library
        public UsersController(DataContext context, IUserRepository repo, IMapper map)
        {
            _ctx = context;
            _repo = repo;
            _map = map;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {

            // var user = await _repo.GetUsersAsync();
            // var mappedUserList = _map.Map<IEnumerable<MemberDto>>(user); // this function asks us what the current object which is user is going to map to which is a IEnumerable of MemberDto
            // return Ok(mappedUserList); // must return http result as we are using Actionresult return type

            return Ok(await _repo.GetMembersAsync());

        }
        //Api/Users/lisa
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _repo.GetMemberAsync(username);            
        }
    }
}