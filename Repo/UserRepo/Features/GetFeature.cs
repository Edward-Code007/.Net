using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using tests.Data;
using tests.Models;
using tests.Repo.RoleRepo;
using tests.Repo.UserRepo.Mapper;

namespace tests.Repo.UserRepo.Features
{
    public static class GetFeature
    {
        public static IQueryable<UserModel> getUsers(this IUserFeature _userFeature)
        {
            return (from user in _userFeature.getContext().User
                    where user.isActive == true
                    select user );
        }
    }
}



