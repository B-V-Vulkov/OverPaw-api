namespace OverPaw.Services
{
    using OverPaw.Data;
    using OverPaw.Services.Contracts;
    using OverPaw.Services.Models;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System;

    public class UserService : IUserService
    {
        private readonly OverPawDbContext dbContext;
        private readonly IMapper mapper;

        public UserService(OverPawDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TestUserServiceModel>> GetAll()
        {
            var users = await dbContext.Users
                .ProjectTo<TestUserServiceModel>(this.mapper.ConfigurationProvider)
                .Where(x => x.UserId.Equals("5E28AE6C-9FE4-46AC-8399-06750D0E88EF"))
                .ToListAsync();

            return users;
        }
    }
}
