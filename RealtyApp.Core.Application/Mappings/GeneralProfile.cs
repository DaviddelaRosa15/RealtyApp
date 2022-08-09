using AutoMapper;
using RealtyApp.Core.Application.Dtos.Account;
using RealtyApp.Core.Application.ViewModels.FavoriteImmovable;
using RealtyApp.Core.Application.ViewModels.ImmovableAsset;
using RealtyApp.Core.Application.ViewModels.ImmovableAssetType;
using RealtyApp.Core.Application.ViewModels.Improvement;
using RealtyApp.Core.Application.ViewModels.Improvement_Immovable;
using RealtyApp.Core.Application.ViewModels.SellType;
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
            #region User
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
            #endregion

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

            #region ImmovableAssetType

            CreateMap<ImmovableAssetTypeViewModel, ImmovableAssetType>()
                .ForMember(dest => dest.Created, options => options.Ignore())
                .ForMember(dest => dest.LastModified, options => options.Ignore())
                .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                .ForMember(dest => dest.LastModifiedBy, options => options.Ignore());

            CreateMap<ImmovableAssetTypeSaveViewModel, ImmovableAssetType>()
                .ForMember(dest => dest.Created, options => options.Ignore())
                .ForMember(dest => dest.LastModified, options => options.Ignore())
                .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                .ForMember(dest => dest.LastModifiedBy, options => options.Ignore())
                .ForMember(dest => dest.ImmovableAssets, options => options.Ignore());

            #endregion

            #region Improvement
            CreateMap<ImprovementViewModel, Improvement>()
                .ForMember(dest => dest.Created, options => options.Ignore())
                .ForMember(dest => dest.LastModified, options => options.Ignore())
                .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                .ForMember(dest => dest.LastModifiedBy, options => options.Ignore());

            CreateMap<ImprovementSaveViewModel, Improvement>()
                .ForMember(dest => dest.Created, options => options.Ignore())
                .ForMember(dest => dest.LastModified, options => options.Ignore())
                .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                .ForMember(dest => dest.LastModifiedBy, options => options.Ignore())
                .ForMember(dest => dest.Improvement_Immovables, options => options.Ignore());
            #endregion

            #region Improvement_Immovable
            CreateMap<Improvement_ImmovableViewModel, Improvement_Immovable>()
                .ForMember(dest => dest.Created, options => options.Ignore())
                .ForMember(dest => dest.LastModified, options => options.Ignore())
                .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                .ForMember(dest => dest.LastModifiedBy, options => options.Ignore());

            CreateMap<SaveImprovement_ImmovableViewModel, Improvement_Immovable>()
                .ForMember(dest => dest.Created, options => options.Ignore())
                .ForMember(dest => dest.LastModified, options => options.Ignore())
                .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                .ForMember(dest => dest.LastModifiedBy, options => options.Ignore());
            #endregion

            #region SellType
            CreateMap<SellTypeViewModel, SellType>()
                .ForMember(dest => dest.Created, options => options.Ignore())
                .ForMember(dest => dest.LastModified, options => options.Ignore())
                .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                .ForMember(dest => dest.LastModifiedBy, options => options.Ignore());

            CreateMap<SaveSellTypeViewModel, SellType>()
                .ForMember(dest => dest.Created, options => options.Ignore())
                .ForMember(dest => dest.LastModified, options => options.Ignore())
                .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                .ForMember(dest => dest.LastModifiedBy, options => options.Ignore());
            #endregion
        }
    }
}
