using System.ComponentModel;

namespace LavaCar.ETL
{
    public class VehiculoServicioETL
    {
        [DisplayName("ID")]
        public int IdVehiculoServicio { get; set; }
        [DisplayName("Servicio")]
        public int IdServicio { get; set; }
        [DisplayName("Vehículo")]
        public int IdVehiculo { get; set; }

        public virtual ServicioETL IdServicioNavigation { get; set; }
        public virtual VehiculoETL IdVehiculoNavigation { get; set; }
    }
}