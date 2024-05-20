using AutoMapper;
using SALESSYSTEM.Domain.Entities;
using SALESSYSTEM.WebApi.Models.ViewModels;
using System.Globalization;

namespace SALESSYSTEM.WebApi.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Role, VMRol>()
                .ReverseMap();
            #endregion
            #region User
            CreateMap<User, VMUser>()
                .ForMember(x => x.IsActive,x=>x.MapFrom(y =>y.IsActive ==true ? 1 : 0))
                .ForMember(x => x.NameRole, x=> x.MapFrom(y => y.IdRoleNavigation!.Description));

            CreateMap<VMUser, User>()
                .ForMember(x => x.IsActive, x => x.MapFrom(y => y.IsActive == 1 ? true : false))
                .ForMember(x => x.IdRoleNavigation, x => x.Ignore());
            #endregion

            #region Bussines
            CreateMap<Business, VMBussines>()
                .ForMember(x => x.TaxPercentage, x => x.MapFrom(y => Convert.ToString(y.TaxPercentage!.Value, new CultureInfo("es-CO"))));

            CreateMap<VMBussines, Business>()
               .ForMember(x => x.TaxPercentage, x => x.MapFrom(y => Convert.ToDecimal(y.TaxPercentage, new CultureInfo("es-CO"))));

            #endregion

            #region Category
            CreateMap<Category, VMCategory>()
                .ForMember(x => x.IsActive, x => x.MapFrom(y => y.IsActive == true ? 1 : 0))
                .ReverseMap();

            #endregion

            #region Product
            CreateMap<Product, VMProduct>()
                .ForMember(x => x.IsActive, x => x.MapFrom(y => y.IsActive == true ? 1 : 0))
                .ForMember(x => x.NameCatgory, x => x.MapFrom(y => y.IdCategoryNavigation!.Description))
                .ForMember(x => x.Price, x => x.MapFrom(y => Convert.ToString(y.Price!.Value, new CultureInfo("es-CO"))));

            CreateMap<VMProduct, Product > ()
                .ForMember(x => x.IsActive, x => x.MapFrom(y => y.IsActive == 1 ? true : false))
                .ForMember(x => x.IdCategoryNavigation, x => x.Ignore())
                .ForMember(x => x.Price, x => x.MapFrom(y => Convert.ToDecimal(y.Price, new CultureInfo("es-CO"))));

            #endregion

            #region DocumentType
            CreateMap<DocumentTypeSale, VMDocumentType>()
                .ReverseMap();

            #endregion

            #region Sale
            CreateMap<Sale, VMSale>()
                .ForMember(x => x.DocumentTypeSale, x => x.MapFrom(y => y.IdDocumentTypeSaleNavigation!.Description))
                .ForMember(x => x.User, x => x.MapFrom(y => y.IdUserNavigation!.Name))
                .ForMember(x => x.SubTotal, x => x.MapFrom(y => Convert.ToString(y.SubTotal!.Value, new CultureInfo("es-CO"))))
                .ForMember(x => x.TaxTotal, x => x.MapFrom(y => Convert.ToString(y.TaxTotal!.Value, new CultureInfo("es-CO"))))
                .ForMember(x => x.Total, x => x.MapFrom(y => Convert.ToString(y.Total!.Value, new CultureInfo("es-CO"))))
                .ForMember(x => x.RegistrationDate, x => x.MapFrom(y => y.RegistrationDate!.Value.ToString("dd/MM/yyyy")));

            CreateMap<VMSale, Sale>()
                .ForMember(x => x.SubTotal, x => x.MapFrom(y => Convert.ToDecimal(y.SubTotal, new CultureInfo("es-CO"))))
                .ForMember(x => x.TaxTotal, x => x.MapFrom(y => Convert.ToDecimal(y.TaxTotal, new CultureInfo("es-CO"))))
                .ForMember(x => x.Total, x => x.MapFrom(y => Convert.ToDecimal(y.Total, new CultureInfo("es-CO"))));

            #endregion

            #region SaleDetail
            CreateMap<SaleDetail, VMSaleDetail>()
                .ForMember(x => x.Price, x => x.MapFrom(y => Convert.ToString(y.Price!.Value, new CultureInfo("es-CO"))))
               .ForMember(x => x.Total, x => x.MapFrom(y => Convert.ToString(y.Total!.Value, new CultureInfo("es-CO"))));
       
            CreateMap<VMSaleDetail, SaleDetail>()
                .ForMember(x => x.Price, x => x.MapFrom(y => Convert.ToDecimal(y.Price, new CultureInfo("es-CO"))))
               .ForMember(x => x.Total, x => x.MapFrom(y => Convert.ToDecimal(y.Total, new CultureInfo("es-CO"))));

            #endregion

            #region SaleReport
            CreateMap<SaleDetail, VMSaleReport>()
               .ForMember(x => x.RegisterDate, x => x.MapFrom(y => y.IdSaleNavigation!.RegistrationDate!.Value.ToString("dd/MM/yyyy")))
               .ForMember(x => x.SaleNumber, x => x.MapFrom(y => y.IdSaleNavigation!.SaleNumber))
               .ForMember(x => x.DocumentType, x => x.MapFrom(y => y.IdSaleNavigation!.IdDocumentTypeSaleNavigation!.Description))
               .ForMember(x => x.DocumentClient, x =>x.MapFrom(y => y.IdSaleNavigation!.ClientDocument))
               .ForMember(x => x.ClientName, x => x.MapFrom(y => y.IdSaleNavigation!.ClientName))
               .ForMember(x => x.SubTotalSale, x => x.MapFrom(y => y.IdSaleNavigation!.ClientDocument))
               .ForMember(x => x.SubTotalSale, x => x.MapFrom(y => Convert.ToString(y.IdSaleNavigation!.SubTotal!.Value, new CultureInfo("es-CO"))))
               .ForMember(x => x.TaxTotalSale, x => x.MapFrom(y => Convert.ToString(y.IdSaleNavigation!.TaxTotal!.Value, new CultureInfo("es-CO"))))
               .ForMember(x => x.TotalSale, x => x.MapFrom(y => Convert.ToString(y.IdSaleNavigation!.Total!.Value, new CultureInfo("es-CO"))))
               .ForMember(x => x.Product, x => x.MapFrom(y =>y.ProductDescription))
               .ForMember(x => x.Price, x => x.MapFrom(y => Convert.ToString(y.Price!.Value, new CultureInfo("es-CO"))))
               .ForMember(x => x.Total, x => x.MapFrom(y => Convert.ToString(y.Total!.Value, new CultureInfo("es-CO"))));

            #endregion

            #region DocumentType
            CreateMap<Menu, VMMenu>()
                .ForMember(x => x.SubMenus, x => x.MapFrom(y => y.InverseParent));

            #endregion




        }
    }
}
