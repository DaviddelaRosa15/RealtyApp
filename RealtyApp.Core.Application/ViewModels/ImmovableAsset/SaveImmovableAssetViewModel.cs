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
        public string Code { get; set; }
        [Required(ErrorMessage = "Coloque la dirección de la propiedad.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Coloque la descripcion de la propiedad.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Coloque un precio valido.")]
        [Range(1,int.MaxValue,ErrorMessage = "El precion debe ser mayor a cero.")]
        public double Price { get; set; }
        public string UrlImage01 { get; set; }
        public string UrlImage02 { get; set; }
        public string UrlImage03 { get; set; }
        public string UrlImage04 { get; set; }
        [Required(ErrorMessage = "Coloque la cantidad de metros de la propiedad.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad de metros debe ser mayor a 0.")]
        public double Meters { get; set; }
        [Required(ErrorMessage = "Coloque la cantidad de habitaciones.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad de habitaciones debe ser mayor a 0.")]
        public double BedroomQuantity { get; set; }
        [Required(ErrorMessage = "Coloque la cantidad de baños.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad de baños debe ser mayor a 0.")]
        public double BathroomQuantity { get; set; }
        public int ImmovableAssetTypeId { get; set; }
        public int SellTypeId { get; set; }
        public string IdUser { get; set; }

        //Navigation Properties
        //public ImmovableAssetType ImmovableAssetType { get; set; }
        //public int ImmovableAssetTypeId { get; set; }
        //public SellType SellType { get; set; }
        //public int SellTypeId { get; set; }
        //public ICollection<FavoriteImmovable> FavoriteImmovables { get; set; }
        //public ICollection<Improvement_Immovable> Improvement_Immovables { get; set; }
    }
}
