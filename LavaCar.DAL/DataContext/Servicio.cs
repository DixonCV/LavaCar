using System;
using System.Collections.Generic;

#nullable disable

namespace LavaCar.DAL.DataContext
{
    public partial class Servicio
    {
        public Servicio()
        {
            VehiculoServicios = new HashSet<VehiculoServicio>();
        }

        public int IdServicio { get; set; }
        public string Descripcion { get; set; }
        public int Monto { get; set; }

        public virtual ICollection<VehiculoServicio> VehiculoServicios { get; set; }
    }
}
