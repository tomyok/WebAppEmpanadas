using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Pedido
    {
        public int IDPedido { get; set; }
        public int IDCliente { get; set; }
        public List<DetallePedido> Detalles { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total
        {
            get
            {
                return Detalles.Sum(d => d.Subtotal);
            }
            set
            {
                Total = value;
            }
        }
        public bool Estado { get; set; }
    }
}
