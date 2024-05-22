using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SALESSYSTEM.BLL.Commons.Bases.Response;
using SALESSYSTEM.BLL.Interfaces;
using SALESSYSTEM.Domain.Entities;
using SALESSYSTEM.WebApi.Models.ViewModels;

namespace SALESSYSTEM.WebApi.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public CategoryController(IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            List<VMCategory> vMCategories = _mapper.Map<List<VMCategory>>(await _categoryService.List());
            return StatusCode(StatusCodes.Status200OK,  new {data = vMCategories });

        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] VMCategory model)
        {
            var baseResponse = new BaseResponse<VMCategory>();

            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model), "El modelo de categoria no puede ser nulo");
                }
                Category category = await _categoryService.CreateCategory(_mapper.Map<Category>(model));    
                VMCategory vMCategory = _mapper.Map<VMCategory>(category);
                baseResponse.IsSuccess = true;
                baseResponse.Data = vMCategory;
            }
            catch (Exception ex) {
                baseResponse.IsSuccess = false;
                baseResponse.Message = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, baseResponse);
        }

        [HttpPut]
        public async Task<IActionResult> EditCategory([FromBody] VMCategory model)
        {
            BaseResponse<VMCategory> baseResponse = new BaseResponse<VMCategory>();

            try
            {
                Category category = await _categoryService.UpdateCategory(_mapper.Map<Category>(model));
                VMCategory vMCategory = _mapper.Map<VMCategory>(category);
                baseResponse.IsSuccess = true;
                baseResponse.Data = vMCategory;
            }
            catch (Exception ex)
            {
                baseResponse.IsSuccess = false;
                baseResponse.Message = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, baseResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCategory(int idCategory)
        {
            BaseResponse<bool> baseResponse = new BaseResponse<bool>();

            try
            {
                baseResponse.Data = await _categoryService.DeleteCategory(idCategory);
                baseResponse.IsSuccess = true;
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
