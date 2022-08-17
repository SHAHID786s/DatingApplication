using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _ctx;
        private readonly IMapper _map;
        public UserRepository(DataContext ctx, IMapper map)
        {
            _ctx = ctx;
            _map = map;
        }
        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _ctx.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string name)
        {
            return await _ctx.Users.Include(p => p.Photos).SingleOrDefaultAsync(x => x.UserName == name);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _ctx.Users.Include(p => p.Photos).ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _ctx.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _ctx.Entry(user).State = EntityState.Modified; /// adds a flag to say that it has been modified ef will now know
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _ctx.Users.ProjectTo<MemberDto>(_map.ConfigurationProvider).ToListAsync();
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _ctx.Users.Where(x => x.UserName == username)
            .ProjectTo<MemberDto>(_map.ConfigurationProvider).SingleOrDefaultAsync(); // so get user with username map to memberdto which then looks for automapper profile which is our maper configuration and returns the single element.

        }

    }
}