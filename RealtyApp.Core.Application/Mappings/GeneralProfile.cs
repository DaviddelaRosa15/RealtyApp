using AutoMapper;
using RealtyApp.Core.Application.Dtos.Account;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.ImmovableAssetType;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.Improvement;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.Improvement_Immovable;
using RealtyApp.Core.Application.Features.ImmovableAssetType.Commands.CreateAssetType;
using RealtyApp.Core.Application.Features.ImmovableAssetType.Commands.UpdateAssetType;
using RealtyApp.Core.Application.Features.Improvement.Commands.CreateImprovement;
using RealtyApp.Core.Application.Features.Improvement.Commands.UpdateImprovement;
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
                .ForMember(x => x.TypeUser, opt => opt.Ignore())
                .ForMember(x => x.File, opt => opt.Ignore())
                .ForMember(x => x.IsVerified, opt => opt.Ignore())
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
               .ForMember(dest => dest.Created, options => options.Ignore())
                .ForMember(dest => dest.LastModified, options => options.Ignore())
                .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                .ForMember(dest => dest.LastModifiedBy, options => options.Ignore());

            CreateMap<FavoriteImmovable, SaveFavoriteImmovableViewModel>()
              .ReverseMap()
              .ForMember(dest => dest.Created, options => options.Ignore())
                .ForMember(dest => dest.LastModified, options => options.Ignore())
                .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                .ForMember(dest => dest.LastModifiedBy, options => options.Ignore());
            #endregion

            #region  ImmovableAsset
            CreateMap<ImmovableAsset, ImmovableAssetViewModel>()
              .ForMember(opt => opt.ImmovableAssetTypeName, opt => opt.Ignore())
              .ForMember(opt => opt.SellTypeName, opt => opt.Ignore())
              .ReverseMap()
              .ForMember(opt => opt.ImmovableAssetType, opt => opt.Ignore())
              .ForMember(opt => opt.SellType, opt => opt.Ignore())
              .ForMember(opt => opt.FavoriteImmovables, opt => opt.Ignore())
              .ForMember(opt => opt.Improvement_Immovables, opt => opt.Ignore())
              .ForMember(dest => dest.Created, options => options.Ignore())
              .ForMember(dest => dest.LastModified, options => options.Ignore())
              .ForMember(dest => dest.CreatedBy, options => options.Ignore())
              .ForMember(dest => dest.LastModifiedBy, options => options.Ignore());

            CreateMap<ImmovableAsset, SaveImmovableAssetViewModel>()
             .ReverseMap()
             .ForMember(opt => opt.ImmovableAssetType, opt => opt.Ignore())
             .ForMember(opt => opt.SellType, opt => opt.Ignore())
             .ForMember(opt => opt.FavoriteImmovables, opt => opt.Ignore())
             .ForMember(opt => opt.Improvement_Immovables, opt => opt.Ignore())
              .ForMember(dest => dest.Created, options => options.Ignore())
              .ForMember(dest => dest.LastModified, options => options.Ignore())
              .ForMember(dest => dest.CreatedBy, options => options.Ignore())
              .ForMember(dest => dest.LastModifiedBy, options => options.Ignore());
            #endregion

            #region ImmovableAssetType

            CreateMap<ImmovableAssetType, ImmovableAssetTypeViewModel>()
                 .ForMember(dest => dest.CountUseType, options => options.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Created, options => options.Ignore())
                .ForMember(dest => dest.LastModified, options => options.Ignore())
                .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                .ForMember(dest => dest.LastModifiedBy, options => options.Ignore());
          

            CreateMap<ImmovableAssetType,ImmovableAssetTypeSaveViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, options => options.Ignore())
                .ForMember(dest => dest.LastModified, options => options.Ignore())
                .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                .ForMember(dest => dest.LastModifiedBy, options => options.Ignore())
                .ForMember(dest => dest.ImmovableAssets, options => options.Ignore());

            #endregion

            #region Improvement
            CreateMap<Improvement,ImprovementViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, options => options.Ignore())
                .ForMember(dest => dest.LastModified, options => options.Ignore())
                .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                .ForMember(dest => dest.LastModifiedBy, options => options.Ignore());

            CreateMap<Improvement,ImprovementSaveViewModel>()
                .ReverseMap()
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
            CreateMap<SellType,SellTypeViewModel>()
                .ForMember(dest => dest.CountUseType, options => options.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Created, options => options.Ignore())
                .ForMember(dest => dest.LastModified, options => options.Ignore())
                .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                .ForMember(dest => dest.LastModifiedBy, options => options.Ignore());

            CreateMap<SellType,SaveSellTypeViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, options => options.Ignore())
                .ForMember(dest => dest.LastModified, options => options.Ignore())
                .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                .ForMember(dest => dest.LastModifiedBy, options => options.Ignore());
            #endregion

            #region DTOs_CQRS

            #region ImmovableAssetType

            CreateMap<ImmovableAssetTypeDTO, ImmovableAssetType>()
                        .ForMember(dest => dest.Created, options => options.Ignore())
                        .ForMember(dest => dest.LastModified, options => options.Ignore())
                        .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                        .ForMember(dest => dest.LastModifiedBy, options => options.Ignore())
                        .ForMember(dest => dest.ImmovableAssets, option => option.Ignore())
                        .ReverseMap()
                        .ForMember(src => src.ImmovableAssets, option => option.Ignore());

            CreateMap<ImmovableAssetTypeSaveDTO, ImmovableAssetType>()
                .ForMember(dest => dest.Created, options => options.Ignore())
                .ForMember(dest => dest.LastModified, options => options.Ignore())
                .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                .ForMember(dest => dest.LastModifiedBy, options => options.Ignore())
                .ForMember(dest => dest.ImmovableAssets, options => options.Ignore());

            CreateMap<CreateAssetTypeCommand, ImmovableAssetType>()
                   .ForMember(dest => dest.Created, options => options.Ignore())
                   .ForMember(dest => dest.LastModified, options => options.Ignore())
                   .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                   .ForMember(dest => dest.LastModifiedBy, options => options.Ignore())
                   .ForMember(dest => dest.ImmovableAssets, options => options.Ignore());

            CreateMap<UpdateAssetTypeCommand, ImmovableAssetType>()
                        .ForMember(dest => dest.Created, options => options.Ignore())
                        .ForMember(dest => dest.LastModified, options => options.Ignore())
                        .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                        .ForMember(dest => dest.LastModifiedBy, options => options.Ignore())
                        .ForMember(dest => dest.ImmovableAssets, options => options.Ignore());

            //Here something is happending.
            CreateMap<ImmovableAssetType, UpdateAssetTypeResponse>()
                       .ReverseMap()
                       .ForMember(src => src.Created, options => options.Ignore())
                       .ForMember(src => src.LastModified, options => options.Ignore())
                       .ForMember(src => src.CreatedBy, options => options.Ignore())
                       .ForMember(src => src.LastModifiedBy, options => options.Ignore())
                       .ForMember(src => src.ImmovableAssets, options => options.Ignore());

            #endregion

            #region Improvement

            CreateMap<ImprovementDTO, Improvement>()
              .ForMember(dest => dest.Created, options => options.Ignore())
              .ForMember(dest => dest.LastModified, options => options.Ignore())
              .ForMember(dest => dest.CreatedBy, options => options.Ignore())
              .ForMember(dest => dest.LastModifiedBy, options => options.Ignore());

            CreateMap<CreateImprovementCommand, Improvement>()
              .ForMember(dest => dest.Created, options => options.Ignore())
              .ForMember(dest => dest.LastModified, options => options.Ignore())
              .ForMember(dest => dest.CreatedBy, options => options.Ignore())
              .ForMember(dest => dest.LastModifiedBy, options => options.Ignore())
              .ForMember(dest => dest.Improvement_Immovables, options => options.Ignore());

            CreateMap<UpdateImprovementResponse, Improvement>()
                     .ForMember(dest => dest.Created, options => options.Ignore())
                     .ForMember(dest => dest.LastModified, options => options.Ignore())
                     .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                     .ForMember(dest => dest.LastModifiedBy, options => options.Ignore())
                     .ForMember(dest => dest.Improvement_Immovables, options => options.Ignore());

            CreateMap<UpdateImprovementCommand, Improvement>()
                     .ForMember(dest => dest.Created, options => options.Ignore())
                     .ForMember(dest => dest.LastModified, options => options.Ignore())
                     .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                     .ForMember(dest => dest.LastModifiedBy, options => options.Ignore())
                     .ForMember(dest => dest.Improvement_Immovables, options => options.Ignore());

            #endregion

            #region Improvement_Immovable
            CreateMap<Improvement_ImmovableDTO, Improvement_Immovable>()
                       .ForMember(dest => dest.Created, options => options.Ignore())
                       .ForMember(dest => dest.LastModified, options => options.Ignore())
                       .ForMember(dest => dest.CreatedBy, options => options.Ignore())
                       .ForMember(dest => dest.LastModifiedBy, options => options.Ignore());
            #endregion

            #endregion


        }
    }
}
