using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LavaCar.ETL
{
    public partial class VehiculoETL
    {
        public VehiculoETL()
        {
            VehiculoServicios = new HashSet<VehiculoServicioETL>();
        }

        [DisplayName("ID")]
        public int IdVehiculo { get; set; }
        [DisplayName("Placa"), MaxLength(30)]
        [Required(ErrorMessage = "La placa es requerida")]
        [RegularExpression(@"^[a-zA-Z0-9ñÑ\s]*$", ErrorMessage = "Solo se permite ingresar caracteres alfanuméricos")]
        public string Placa { get; set; }
        [DisplayName("Dueño")]
        [Required(ErrorMessage = "El nombre del dueño del vehículo es requerido")]
        [RegularExpression(@"^[a-zA-Z0-9ñÑ\s]*$", ErrorMessage = "Solo se permite ingresar caracteres alfanuméricos")]
        public string Dueno { get; set; }
        [DisplayName("Marca"), MaxLength(50)]
        [Required(ErrorMessage = "La marca del vehículo es requerida")]
        [RegularExpression(@"^[a-zA-Z0-9ñÑ\s]*$", ErrorMessage = "Solo se permite ingresar caracteres alfanuméricos")]
        public string Marca { get; set; }

        public virtual ICollection<VehiculoServicioETL> VehiculoServicios { get; set; }
    }
}