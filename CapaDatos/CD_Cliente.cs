using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Cliente
    {

        public List<Cliente> ListarClientes()
        {
            List<Cliente> lista = new List<Cliente>();

            try
            {
                using (SqlConnection Oconnection = new SqlConnection(Conexion.Cn))
                {
                    string query = "Select ID_Cliente, Nombre, Apellido, Telefono, Estado From Clientes";

                    SqlCommand cmd = new SqlCommand(query, Oconnection);

                    cmd.CommandType = CommandType.Text;

                    Oconnection.Open();

                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            lista.Add(
                                new Cliente()
                                {
                                    IDCliente = (int)Reader["ID_Cliente"],
                                    Nombre = Reader["Nombre"].ToString(),
                                    Apellido = Reader["Apellido"].ToString(),
                                    Telefono = Reader["Telefono"].ToString(),
                                    Estado = (bool)Reader["Estado"]
                                }
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int Registrar(Cliente clienteARegistrar, out string Mensaje)
        {
            int IDCliente;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection Oconnection = new SqlConnection(Conexion.Cn))
                {

                    SqlCommand cmd = new SqlCommand("SP_InsertarCliente", Oconnection);

                    cmd.Parameters.AddWithValue("Nombre", clienteARegistrar.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", clienteARegistrar.Apellido);
                    cmd.Parameters.AddWithValue("Telefono", clienteARegistrar.Telefono);
                    cmd.Parameters.AddWithValue("Estado", clienteARegistrar.Estado);
                    cmd.Parameters.Add("IDCliente", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    Oconnection.Open();

                    cmd.ExecuteNonQuery();

                    IDCliente = Convert.ToInt32(cmd.Parameters["IDCliente"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                IDCliente = 0;
                Mensaje = "Error: " + ex.Message;
            }
            return IDCliente;
        }

        public bool Editar(Cliente clienteAEditar, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection Oconnection = new SqlConnection(Conexion.Cn))
                {

                    SqlCommand cmd = new SqlCommand("SP_EditarCliente", Oconnection);

                    cmd.Parameters.AddWithValue("IDCliente", clienteAEditar.IDCliente);
                    cmd.Parameters.AddWithValue("Nombre", clienteAEditar.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", clienteAEditar.Apellido);
                    cmd.Parameters.AddWithValue("Telefono", clienteAEditar.Telefono);
                    cmd.Parameters.AddWithValue("Estado", clienteAEditar.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    Oconnection.Open();

                    cmd.ExecuteNonQuery();

                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = "Error: " + ex.Message;
            }
            return resultado;
        }

        public bool Eliminar(int IDCliente, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection Oconnection = new SqlConnection(Conexion.Cn))
                {

                    SqlCommand cmd = new SqlCommand("Delete top (1) from Clientes Where ID_Cliente = @ID", Oconnection);
                    cmd.Parameters.AddWithValue("@ID", IDCliente);
                    cmd.CommandType = CommandType.Text;
                    Oconnection.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = "Error: " + ex.Message;
            }
            return resultado;
        }

    }

}
