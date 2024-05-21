using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SALESSYSTEM.BLL.Commons.Bases.Response;
using SALESSYSTEM.BLL.Interfaces;
using SALESSYSTEM.Domain.Entities;
using SALESSYSTEM.WebApi.Models.ViewModels;

namespace SALESSYSTEM.WebApi.Controllers
{
    public class BussinesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBussinesService _bussinesService;

        public BussinesController(IMapper mapper, IBussinesService bussinesService)
        {
            _mapper = mapper;
            _bussinesService = bussinesService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            BaseResponse<VMBussines> baseResponse = new BaseResponse<VMBussines>();
            try
            {
                VMBussines vMBussines = _mapper.Map<VMBussines>(await _bussinesService.Get());

                baseResponse.IsSuccess = true;
                baseResponse.Data = vMBussines;

            }
            catch (Exception ex)
            {
                baseResponse.IsSuccess = false;
                baseResponse.Message = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, baseResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBussines([FromBody] VMBussines model)
        {
            var baseResponse = new BaseResponse<VMBussines>();
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model), "El modelo de usuario no puede ser nulo");
                }
                Business business = await _bussinesService.SaveChanges(_mapper.Map<Business>(model));

                VMBussines vMBussines = _mapper.Map<VMBussines>(business);


            }
            catch (Exception ex)
            {
                baseResponse.IsSuccess = false;
                baseResponse.Message = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, baseResponse);
        }
    }
}
