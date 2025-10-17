using tests.Data;
using tests.Models;

namespace tests.Repo.UserRepo.Features
{
    public static class GetByIDFeature
    {
        public static IQueryable<UserModel> GetByID(this IUserFeature feature, int id)
        {
            AppDbContext context = feature.getContext();
            IQueryable<UserModel> userById =from user in context.User
                                            where user.Id == id
                                            select user;
            
            return userById;
        }
    }
}
