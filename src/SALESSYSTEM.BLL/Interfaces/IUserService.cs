using SALESSYSTEM.Domain.Entities;

namespace SALESSYSTEM.BLL.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> List();
        Task<User> CreateUser(User user, string UrlTemplateEmail="" );
        Task<User> Edituser(User user);
        Task<bool> DeleteUser(int IdUser);
        Task<User> GetByCrendentials(string email, string password);
        Task<User> GetById(int IdUser);
        Task<bool> SaveProfile(User user);
        Task<bool> ChangePassword(int IdUser, string password, string newPassword);
        Task<bool> ResetPassword(string email, string UrlTemplateEmail = "");
    }
}
