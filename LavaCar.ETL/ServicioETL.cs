using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LavaCar.ETL
{
    public class ServicioETL
    {
        public ServicioETL()
        {
            VehiculoServicios = new HashSet<VehiculoServicioETL>();
        }

        [DisplayName("ID")]
        public int IdServicio { get; set; }
        [DisplayName("Descripción")]
        [Required(ErrorMessage = "La descripción es requerida")]
        [RegularExpression(@"^[a-zA-Z0-9ñÑ\s]*$", ErrorMessage = "Solo se permite ingresar caracteres alfanuméricos")]
        public string Descripcion { get; set; }
        [DisplayName("Monto")]
        [Required(ErrorMessage = "El monto es requerido")]
        public int Monto { get; set; }

        public virtual ICollection<VehiculoServicioETL> VehiculoServicios { get; set; }
    }
}
