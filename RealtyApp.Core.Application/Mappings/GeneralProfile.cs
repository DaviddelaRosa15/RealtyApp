using AutoMapper;
using RealtyApp.Core.Application.Dtos.Account;
using RealtyApp.Core.Application.ViewModels.FavoriteImmovable;
using RealtyApp.Core.Application.ViewModels.ImmovableAsset;
using RealtyApp.Core.Application.ViewModels.User;
using RealtyApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyApp.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            #region FeaturesCqrs
            //CreateMap<GetAllProductsQuery, GetAllProductsParameter>()            
            //   .ReverseMap();

            //CreateMap<CreateProductCommand, Product>()
            //    .ForMember(x => x.Category, opt => opt.Ignore())
            //    .ForMember(x => x.Created, opt => opt.Ignore())
            //    .ForMember(x => x.LastModified, opt => opt.Ignore())
            //    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
            //    .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            //    .ReverseMap();

            //CreateMap<UpdateProductCommand, Product>()
            //    .ForMember(x => x.Category, opt => opt.Ignore())
            //    .ForMember(x => x.Created, opt => opt.Ignore())
            //    .ForMember(x => x.LastModified, opt => opt.Ignore())
            //    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
            //    .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            //    .ReverseMap();

            //CreateMap<ProductUpdateResponse, Product>()
            //  .ForMember(x => x.Category, opt => opt.Ignore())
            //  .ForMember(x => x.Created, opt => opt.Ignore())
            //  .ForMember(x => x.LastModified, opt => opt.Ignore())
            //  .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
            //  .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            //  .ReverseMap();

            //CreateMap<CreateCategoryCommand, Category>()
            //.ForMember(x => x.Products, opt => opt.Ignore())
            //.ForMember(x => x.Created, opt => opt.Ignore())
            //.ForMember(x => x.CreatedBy, opt => opt.Ignore())
            //.ForMember(x => x.LastModified, opt => opt.Ignore())
            //.ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
            //.ReverseMap();

            //CreateMap<UpdateCategoryCommand, Category>()
            //    .ForMember(x => x.Products, opt => opt.Ignore())
            //    .ForMember(x => x.Created, opt => opt.Ignore())
            //    .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            //    .ForMember(x => x.LastModified, opt => opt.Ignore())
            //    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
            //    .ReverseMap();

            //CreateMap<CategoryUpdateResponse, Category>()
            // .ForMember(x => x.Products, opt => opt.Ignore())
            // .ForMember(x => x.Created, opt => opt.Ignore())
            // .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            // .ForMember(x => x.LastModified, opt => opt.Ignore())
            // .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
            // .ReverseMap();
            #endregion

            #region FavoriteImmovable
            CreateMap<FavoriteImmovable, FavoriteImmovableViewModel>()
               .ReverseMap()
               .ForMember(opt => opt.ImmovableAsset, opt => opt.Ignore());
            CreateMap<FavoriteImmovable, SaveFavoriteImmovableViewModel>()
              .ReverseMap()
              .ForMember(opt => opt.ImmovableAsset, opt => opt.Ignore());
            #endregion

            #region  ImmovableAsset
            CreateMap<ImmovableAsset, ImmovableAssetViewModel>()
              .ReverseMap()
              .ForMember(opt => opt.ImmovableAssetType, opt => opt.Ignore())
              .ForMember(opt => opt.SellType, opt => opt.Ignore())
              .ForMember(opt => opt.FavoriteImmovables, opt => opt.Ignore())
              .ForMember(opt => opt.Improvement_Immovables, opt => opt.Ignore());
            CreateMap<ImmovableAsset, SaveImmovableAssetViewModel>()
             .ReverseMap()
             .ForMember(opt => opt.ImmovableAssetType, opt => opt.Ignore())
             .ForMember(opt => opt.SellType, opt => opt.Ignore())
             .ForMember(opt => opt.FavoriteImmovables, opt => opt.Ignore())
             .ForMember(opt => opt.Improvement_Immovables, opt => opt.Ignore());
            #endregion
        }
    }
}
