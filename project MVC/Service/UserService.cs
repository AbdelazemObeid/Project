using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_MVC.Models;
using project_MVC.Repositories;

namespace project_MVC.Service
{
    public class UserService : GenericService<User>, IUserService
    {
        IUserReposatory userReposatory;
        public UserService(IGenericRepository<User> _repository , IUserReposatory _userReposatory) : base(_repository)
        {
            userReposatory = _userReposatory;
        }
        bool IUserService.getByEmail(string email)
        {
            return userReposatory.getByEmail(email);
        }
        public User? GetUserByEmail(string email)
        {
            return userReposatory.GetUserByEmail(email);
        }
    }
}
