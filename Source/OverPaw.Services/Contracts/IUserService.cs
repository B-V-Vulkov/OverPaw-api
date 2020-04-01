namespace OverPaw.Services.Contracts
{
    using OverPaw.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<IEnumerable<TestUserServiceModel>> GetAll();
    }
}
