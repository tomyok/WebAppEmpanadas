using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Cliente
    {
        private CD_Cliente AuxCapaDatoCliente = new CD_Cliente();
        public List<Cliente> ListarClientes()
        {
            return AuxCapaDatoCliente.ListarClientes();
        }

        public int Registrar(Cliente clienteARegistrar, out string Mensaje)
        {
            Mensaje = string.Empty;

            if(string.IsNullOrEmpty(clienteARegistrar.Nombre) || string.IsNullOrWhiteSpace(clienteARegistrar.Nombre))
            {
                Mensaje = "El campo Nombre es obligatorio";
            }
            else if (string.IsNullOrEmpty(clienteARegistrar.Apellido) || string.IsNullOrWhiteSpace(clienteARegistrar.Apellido))
            {
                Mensaje = "El campo Apellido es obligatorio";
            }
            else if (string.IsNullOrEmpty(clienteARegistrar.Telefono) || string.IsNullOrWhiteSpace(clienteARegistrar.Telefono))
            {
                Mensaje = "El campo Telefono es obligatorio";
            }

            if(string.IsNullOrEmpty(Mensaje))
            {
                return AuxCapaDatoCliente.Registrar(clienteARegistrar, out Mensaje);
            }
            else
            {
                return 0;
            }

        }

        public bool Editar(Cliente clienteAEditar, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(clienteAEditar.Nombre) || string.IsNullOrWhiteSpace(clienteAEditar.Nombre))
            {
                Mensaje = "El campo Nombre es obligatorio";
            }
            else if (string.IsNullOrEmpty(clienteAEditar.Apellido) || string.IsNullOrWhiteSpace(clienteAEditar.Apellido))
            {
                Mensaje = "El campo Apellido es obligatorio";
            }
            else if (string.IsNullOrEmpty(clienteAEditar.Telefono) || string.IsNullOrWhiteSpace(clienteAEditar.Telefono))
            {
                Mensaje = "El campo Telefono es obligatorio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return AuxCapaDatoCliente.Editar(clienteAEditar, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool Eliminar(int IDCliente, out string Mensaje)
        {
            Mensaje = string.Empty;

            if(IDCliente <= 0)
            {
                Mensaje = "El ID del cliente no es válido";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return AuxCapaDatoCliente.Eliminar(IDCliente, out Mensaje);
            }
            else
            {
                return false;
            }
        }
    }
}
