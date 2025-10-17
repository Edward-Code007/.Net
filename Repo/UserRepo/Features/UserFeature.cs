using Microsoft.EntityFrameworkCore;
using tests.Data;
using tests.Models;
using tests.Services.IServices;

namespace tests.Repo.UserRepo.Features
{
    public class UserFeature(AppDbContext context,ICrypto crypto):IUserFeature
    {
     private readonly AppDbContext _context = context;
     private readonly ICrypto _crypto = crypto;

        public AppDbContext getContext()
        {
            return _context;
        }
        public ICrypto GetCrypto() {
        return _crypto;
        }
    }

}
