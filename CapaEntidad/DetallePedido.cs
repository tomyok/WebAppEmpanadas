using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DetallePedido
    {
        public int IDDetallePedido { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal
        {
            get
            {
                return Producto.Precio * Cantidad;
            }
        }
    }
}
