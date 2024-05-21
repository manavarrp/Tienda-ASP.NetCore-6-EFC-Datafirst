using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SALESSYSTEM.BLL.Commons.Bases.Response;
using SALESSYSTEM.BLL.Interfaces;
using SALESSYSTEM.WebApi.Models.ViewModels;
using SALESSYSTEM.Domain.Entities;
using SALESSYSTEM.BLL.Implementation;

namespace SALESSYSTEM.WebApi.Controllers
{
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _service;
        private readonly IRolService _rolService;
        private readonly IgnoreSSL _ignore;

        public UserController(IMapper mapper, IUserService service, IRolService rolService, IgnoreSSL ignore)
        {
            _mapper = mapper;
            _service = service;
            _rolService = rolService;
            _ignore = ignore;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
       public async Task<IActionResult> ListRols() {

            List<VMRol> vmListRols = _mapper.Map<List<VMRol>>(await _rolService.List());
            return StatusCode(StatusCodes.Status200OK,vmListRols);
        }

        [HttpGet]
        public async Task<IActionResult> ListUser()
        {

            List<VMUser> vmListUser = _mapper.Map<List<VMUser>>(await _service.List());
            return StatusCode(StatusCodes.Status200OK, new {data=vmListUser });
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] VMUser model)
        {
            var baseResponse = new BaseResponse<VMUser>();

            try
            {
                // Log para depuración
                Console.WriteLine("Received model: " + JsonConvert.SerializeObject(model));

                // Verificar que el modelo no es nulo
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model), "El modelo de usuario no puede ser nulo");
                }

              //  string urlTemplateEmail = $"{this.Request.Scheme}://{this.Request.Host}/Template/SendPassword?email=[email]&password=[password]";


               User new_user = await _service.CreateUser(_mapper.Map<User>(model));

               // string htmlContent = await _ignore.GetHtmlFromUrl(urlTemplateEmail);
                VMUser vmUser = _mapper.Map<VMUser>(new_user);

                baseResponse.IsSuccess = true;
                baseResponse.Data = vmUser;
            }
            catch (Exception ex)
            {
                baseResponse.IsSuccess = false;
                baseResponse.Message = $"Ocurrió un error mientras se creaba el usuario: {ex.Message}";
                Console.WriteLine("Exception: " + ex.ToString()); // Log completo de la excepción
            }

            return StatusCode(StatusCodes.Status200OK, baseResponse);
        }


        [HttpPut]
        public async Task<IActionResult> Edituser([FromBody] VMUser model)
        {
            BaseResponse<VMUser> baseResponse = new BaseResponse<VMUser>();
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model), "El modelo de usuario no puede ser nulo");
                }

                User new_user = await _service.Edituser(_mapper.Map<User>(model));


                VMUser vmUser = _mapper.Map<VMUser>(new_user);

                baseResponse.IsSuccess = true;
                baseResponse.Data = vmUser;

            }
            catch (Exception ex)
            {
                baseResponse.IsSuccess = false;
                baseResponse.Message = $"Ocurrio un error mientras se editaba el usuario  {ex.Message}";
            }

            return StatusCode(StatusCodes.Status200OK, baseResponse);
        }
        [HttpDelete]
        public async Task<IActionResult> Remove(int IdUser)
        {
            var baseResponse = new BaseResponse<VMUser>();
            try
            {
                baseResponse.IsSuccess = await _service.DeleteUser(IdUser);
            }
            catch (Exception ex)
            {
                baseResponse.IsSuccess = false;
                baseResponse.Message = $"Ocurrio un error mientras se eliminaba el usuario ${ex.Message}";
            }
            return StatusCode(StatusCodes.Status200OK, baseResponse);
        }

    }
}
