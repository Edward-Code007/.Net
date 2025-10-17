using Microsoft.EntityFrameworkCore;
using tests.Data;
using tests.Models;
using tests.Services.IServices;

namespace tests.Repo.UserRepo.Features
{
    public interface IUserFeature
    {
        public AppDbContext getContext();
        public ICrypto GetCrypto();
    }
}
