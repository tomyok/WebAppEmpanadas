using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Producto
    {
        public List<Producto> ListarProductos()
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                using (SqlConnection Oconnection = new SqlConnection(Conexion.Cn))
                {
                    string query = "Select ID_Producto, Nombre, Descripcion, Precio, Estado From Productos";

                    SqlCommand cmd = new SqlCommand(query, Oconnection);

                    cmd.CommandType = CommandType.Text;

                    Oconnection.Open();

                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            lista.Add(
                                new Producto()
                                {
                                    IDProducto = (int)Reader["ID_Producto"],
                                    Nombre = Reader["Nombre"].ToString(),
                                    Descripcion = Reader["Descripcion"].ToString(),
                                    Precio = Convert.ToDecimal(Reader["Precio"]),
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
        public int Registrar(Producto ProductoARegistrar, out string Mensaje)
        {
            int IDProducto;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection Oconnection = new SqlConnection(Conexion.Cn))
                {

                    SqlCommand cmd = new SqlCommand("SP_CrearProducto", Oconnection);

                    cmd.Parameters.AddWithValue("Nombre", ProductoARegistrar.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", ProductoARegistrar.Descripcion);
                    cmd.Parameters.AddWithValue("Precio", ProductoARegistrar.Precio);
                    cmd.Parameters.AddWithValue("Estado", ProductoARegistrar.Estado);
                    cmd.Parameters.Add("IDProducto", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    Oconnection.Open();

                    cmd.ExecuteNonQuery();

                    IDProducto = Convert.ToInt32(cmd.Parameters["IDProducto"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                IDProducto = 0;
                Mensaje = "Error: " + ex.Message;
            }
            return IDProducto;
        }

        public bool Editar(Producto ProductoAEditar, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection Oconnection = new SqlConnection(Conexion.Cn))
                {

                    SqlCommand cmd = new SqlCommand("SP_EditarProducto", Oconnection);

                    cmd.Parameters.AddWithValue("IDProducto", ProductoAEditar.IDProducto);
                    cmd.Parameters.AddWithValue("Nombre", ProductoAEditar.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", ProductoAEditar.Descripcion);
                    cmd.Parameters.AddWithValue("Precio", ProductoAEditar.Precio);
                    cmd.Parameters.AddWithValue("Estado", ProductoAEditar.Estado);
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
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

        public bool Eliminar(int IDProducto, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection Oconnection = new SqlConnection(Conexion.Cn))
                {

                    SqlCommand cmd = new SqlCommand("SP_EliminarProducto", Oconnection);

                    cmd.Parameters.AddWithValue("IDProducto", IDProducto);
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

    }
}
