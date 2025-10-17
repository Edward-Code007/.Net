using System.Threading.Tasks;
using tests.Data;
using tests.Models;

namespace tests.Repo.UserRepo.Features
{
    public static class DeleteFeature
    {
        public static async Task<UserModel?> deleteById(this IUserFeature feature,int id)
        {
            AppDbContext context = feature.getContext();
            UserModel? user = context.User.SingleOrDefault(x => x.Id == id);
            if (user == null) {
                return null;
            }
            user.isActive = false;
           await context.SaveChangesAsync(); 
            return user;
        }
    }
}
