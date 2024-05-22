using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SALESSYSTEM.BLL.Commons.Bases.Response;
using SALESSYSTEM.BLL.Interfaces;
using SALESSYSTEM.Domain.Entities;
using SALESSYSTEM.WebApi.Models.ViewModels;

namespace SALESSYSTEM.WebApi.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IMapper mapper, IProductService productService, ICategoryService categoryService)
        {
            _mapper = mapper;
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {

            List<VMCategory> vMCategories = _mapper.Map<List<VMCategory>>(await _categoryService.List());
            return StatusCode(StatusCodes.Status200OK,  vMCategories );
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            List<VMProduct> vMProduct = _mapper.Map<List<VMProduct>>(await _productService.List());
            return StatusCode(StatusCodes.Status200OK, new { data = vMProduct });
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] VMProduct model)
        {
            var baseResponse = new BaseResponse<VMProduct>();

            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model), "El modelo de categoria no puede ser nulo");
                }
                Product product = await _productService.CreateProduct(_mapper.Map<Product>(model));
                VMProduct vMProduct = _mapper.Map<VMProduct>(product);
                baseResponse.IsSuccess = true;
                baseResponse.Data = vMProduct;
            }
            catch (Exception ex)
            {
                baseResponse.IsSuccess = false;
                baseResponse.Message = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, baseResponse);
        }

        [HttpPut]
        public async Task<IActionResult> EditProduct([FromBody] VMProduct model)
        {
            BaseResponse<VMProduct> baseResponse = new();

            try
            {
                Product product = await _productService.UpdateProduct(_mapper.Map<Product>(model));
                VMProduct vMProduct = _mapper.Map<VMProduct>(product);
                baseResponse.IsSuccess = true;
                baseResponse.Data = vMProduct;
            }
            catch (Exception ex)
            {
                baseResponse.IsSuccess = false;
                baseResponse.Message = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, baseResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveProduct(int idCategory)
        {
            BaseResponse<bool> baseResponse = new();

            try
            {
                baseResponse.Data = await _productService.DeleteProduct(idCategory);
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
