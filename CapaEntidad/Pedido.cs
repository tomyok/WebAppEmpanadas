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
        public Cliente Cliente { get; set; }
        public Producto Producto { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public bool Estado { get; set; }
    }
}
