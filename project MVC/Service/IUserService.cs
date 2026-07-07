using Microsoft.AspNetCore.Mvc;
using project_MVC.Models;

namespace project_MVC.Service
{
    public interface IUserService : IGenericService<User>
    {
        Boolean getByEmail(string email);
    }
}
