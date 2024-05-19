using SALESSYSTEM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALESSYSTEM.BLL.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> List();
        Task<User> CreateUser(User user, Stream Photo = null, string NamePhoto = "", string UrlTemplateEmail="" );
        Task<User> Edituser(User user, Stream Photo = null, string NamePhoto = "");
        Task<bool> Deleteuser(int IdUser);
        Task<User> GetByCrendentials(string email, string password);
        Task<User> GetById(int IdUser);
        Task<bool> SaveProfile(User user);
        Task<bool> ChangePassword(int IdUser, string password, string newPassword);
        Task<bool> ResetPassword(string email, string UrlTemplateEmail = "");
    }
}
