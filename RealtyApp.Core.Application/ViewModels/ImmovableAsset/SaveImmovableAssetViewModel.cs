using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.ViewModels.ImmovableAsset
{
   public class SaveImmovableAssetViewModel
   {
        public int Id { get; set; }
        public string Code { get; set; }

        [Required(ErrorMessage = "Coloque la dirección de la propiedad.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Coloque la descripcion de la propiedad.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Coloque un precio valido.")]
        [Range(1,int.MaxValue,ErrorMessage = "El precio debe ser mayor a cero.")]
        public double Price { get; set; }
        
        public string UrlImage01 { get; set; }
        public string UrlImage02 { get; set; }
        public string UrlImage03 { get; set; }
        public string UrlImage04 { get; set; }

        //Falta la option para los improvements.

        [DataType(DataType.Upload)]
        public IFormFile FileImg01 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile FileImg02 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile FileImg03 { get; set; }
        
        [DataType(DataType.Upload)]
        public IFormFile FileImg04 { get; set; }


        [Required(ErrorMessage = "Coloque la cantidad de metros de la propiedad.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad de metros debe ser mayor a 0.")]
        public double Meters { get; set; }

        [Required(ErrorMessage = "Coloque la cantidad de habitaciones.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad de habitaciones debe ser mayor a 0.")]
        public double BedroomQuantity { get; set; }
       
        [Required(ErrorMessage = "Coloque la cantidad de baños.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad de baños debe ser mayor a 0.")]
        public double BathroomQuantity { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tipo de inmobiliario.")]
        public int ImmovableAssetTypeId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tipo de venta de inmobiliario.")]
        public int SellTypeId { get; set; }

        public string AgentId { get; set; }

        public List<int> Improvements { get; set; }

        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }

        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }

    }
}
