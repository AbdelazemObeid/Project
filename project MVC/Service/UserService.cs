using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_MVC.Models;
using project_MVC.Repositories;

namespace project_MVC.Service
{
    public class UserService : GenericService<User>, IUserService
    {
        IUserRepository userRepository;
        public UserService(IGenericRepository<User> _repository , IUserRepository _userRepository) : base(_repository)
        {
            userRepository = _userRepository;
        }
        public bool EmailExists(string email)
        {
            return userRepository.EmailExists(email);
        }
        public User? Login(string email, string password)
        {
            return userRepository.Login(email, password);
        }
    }
}
