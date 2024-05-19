using Microsoft.EntityFrameworkCore;
using SALESSYSTEM.BLL.Interfaces;
using SALESSYSTEM.DAL.Interfaces;
using SALESSYSTEM.Domain.Entities;
using System.Net;
using System.Text;

namespace SALESSYSTEM.BLL.Implementation
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IUtilitiesService _utilitiesService;
        private readonly IEmailService _emailService;

        public UserService(IGenericRepository<User> repository, IUtilitiesService utilitiesService, IEmailService emailService)
        {
            _repository = repository;
            _utilitiesService = utilitiesService;
            _emailService = emailService;
        }

        public async Task<List<User>> List()
        {
            IQueryable<User> query = await _repository.GetEntityQuery();
            return query.Include(r => r.IdRoleNavigation).ToList();
        }
        public async Task<User> CreateUser(User user, Stream Photo = null, string NamePhoto = "", string UrlTemplateEmail = "")
        {
            User userExist = await _repository.Get(u=>u.Email == user.Email);
            if(userExist != null) 
            {
                throw new TaskCanceledException("El correo ya existe");
            }

            try
            {
                string password = _utilitiesService.CreatePassword();
                user.Password = _utilitiesService.ConvertSha256(password);
                user.PhotoName = NamePhoto;

                if(Photo != null)
                {
                    //string urlPhoto = await _utilitiesService.SubirFoto(, )

                }

                User newUser = await _repository.Create(user);

                if(newUser.IdUser == 0) {
                    throw new TaskCanceledException("No se pudo crear el usuario");
                }

                if(UrlTemplateEmail != "")
                {
                    UrlTemplateEmail= UrlTemplateEmail.Replace("[email]", newUser.Email).Replace("[password]",password);

                    string htmlEmail = "";

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UrlTemplateEmail);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    if(response.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream dataStream = response.GetResponseStream())
                        {
                            StreamReader streamReader = null;

                            if(response.CharacterSet == null) {
                            streamReader = new StreamReader(dataStream);
                            }
                            else
                            {
                                streamReader = new StreamReader(dataStream,Encoding.GetEncoding(response.CharacterSet));
                            }

                            htmlEmail = streamReader.ReadToEnd();
                            response.Close();
                            streamReader.Close();

                        }
                    }
                    if(htmlEmail != "")
                    {
                        await _emailService.SendEmail(newUser.Email!, "Cuenta Creada", htmlEmail);
                    }
                }

                IQueryable<User> query = await _repository.GetEntityQuery(u=>u.IdUser == newUser.IdUser);
                newUser = query.Include(r =>r.IdRoleNavigation).First();

                return newUser;


            }
            catch
            {
                throw;
            }
        }

        public async Task<User> Edituser(User user, Stream Photo = null, string NamePhoto = "")
        {
            User userExist = await _repository.Get(u => u.Email == user.Email && u.IdUser != user.IdUser);
            if (userExist != null)
            {
                throw new TaskCanceledException("El correo ya existe");
            }

            try 
            {
                IQueryable<User> queryUser = await _repository.GetEntityQuery(u => u.IdUser == user.IdUser);

                User updateUser = queryUser.First();

                updateUser.Name = user.Name;
                updateUser.Email = user.Email;
                updateUser.Phone = user.Phone;
                updateUser.IdRole = user.IdRole;

                if(updateUser.PhotoName == "")
                {
                    updateUser.PhotoName = NamePhoto;
                }

                if(Photo != null)
                {

                }

                bool res = await _repository.Update(updateUser);
                if(!res)
                {
                    throw new TaskCanceledException("No se pudo editar el usuario");
                }

                User updatedUser = queryUser.Include(r=>r.IdRoleNavigation).First();

                return updatedUser;

            }
            catch 
            {
                throw;
            }

        }
        public async Task<bool> Deleteuser(int IdUser)
        {
            try
            {
                User userExist = await _repository.Get(u => u.IdUser == IdUser);
                if (userExist != null) throw new TaskCanceledException("El usuario no encontrado");

                bool res = await _repository.Remove(userExist!);

                return res;
                
            }
            catch
            {
                throw;
            }

        }

        public async Task<User> GetByCrendentials(string email, string password)
        {
            string passwordCry = _utilitiesService.ConvertSha256(password);

            User user_found = await _repository.Get(u=>u.Email!.Equals(email) && u.Password.Equals(passwordCry));

            return user_found;
            
        }
        public async Task<User> GetById(int IdUser)
        {
            IQueryable<User> query = await _repository.GetEntityQuery(u=>u.IdUser == IdUser);

            User user = query.Include(r=>r.IdRoleNavigation).FirstOrDefault()!;
            return user;

        }
        public async Task<bool> SaveProfile(User user)
        {
            try
            {
                User userExist = await _repository.Get(u => u.IdUser == user.IdUser);
                if (userExist == null) throw new TaskCanceledException("El usuario no existe");

                userExist!.Email = user.Email;
                userExist.Phone = user.Phone;

                bool res = await _repository.Update(userExist);

                return res;


            }
            catch
            {
                throw;
            }
        }


        public async Task<bool> ChangePassword(int IdUser, string password, string newPassword)
        {
            try
            {
                User userExist = await _repository.Get(u => u.IdUser == IdUser);
                if (userExist == null) throw new TaskCanceledException("El usuario no existe");

                if (userExist.Password != _utilitiesService.ConvertSha256(password))
                    throw new TaskCanceledException("La contraseña ingresada como la actual no es correcta");

                userExist.Password = _utilitiesService.ConvertSha256(newPassword);
                bool res = await _repository.Update(userExist); 
                return res;



            }
            catch (Exception ex) 
            {
                throw;
            }
        }
      
        public async Task<bool> ResetPassword(string email, string UrlTemplateEmail = "")
        {
            try
            {
                User userExist = await _repository.Get(u => u.Email == email);
                if (userExist == null)
                    throw new TaskCanceledException("No encontramos ningun usuario asociado a ese correo");

                string generatedPassword = _utilitiesService.CreatePassword();
                userExist.Password = _utilitiesService.ConvertSha256(generatedPassword);


                UrlTemplateEmail = UrlTemplateEmail.Replace("[password]", generatedPassword);

                string htmlEmail = "";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UrlTemplateEmail);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        StreamReader streamReader = null;

                        if (response.CharacterSet == null)
                        {
                            streamReader = new StreamReader(dataStream);
                        }
                        else
                        {
                            streamReader = new StreamReader(dataStream, Encoding.GetEncoding(response.CharacterSet));
                        }

                        htmlEmail = streamReader.ReadToEnd();
                        response.Close();
                        streamReader.Close();

                    }
                }

                bool emailSend = false;
                if (htmlEmail != "")
                {
                    await _emailService.SendEmail(email!, "Contraseña establecida", htmlEmail);
                }

                if(emailSend) {
                    throw new TaskCanceledException("Tenemos problemas por favor intentalo mas tarde");
                }

                bool res = await _repository.Update(userExist);

                return res;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

      
    }
}
