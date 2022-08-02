using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using WebApiRest.Models;
using System.Data;

namespace WebApiRest.Data
{
    public class UsuarioData
    {
        public static bool Registrar(Usuario oUsuario)
        {
            //Generacion de token
            var randomnumber = new Random().Next(100000, 999999);
            oUsuario.Token = randomnumber;
            bool Validacion = false;

            using (MySqlConnection connection = new MySqlConnection(Conexion.ConexionString))
            {
                //Validacion de que el token no existe en la base de datos
                while (Validacion == false) {
                    try
                    {
                        MySqlCommand cmd1 = new MySqlCommand("Usersp_token", connection);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("token", oUsuario.Token);
                        connection.Open();
                        cmd1.ExecuteNonQuery();
                        using (MySqlDataReader dr = cmd1.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                randomnumber = new Random().Next(1000000, 99999999);
                                oUsuario.Token = randomnumber;
                            }
                            else
                            {
                                Validacion = true;
                            }

                            connection.Close();
                        }
                    } catch (Exception ex) { Validacion = true; }
                    finally { connection.Close(); }
                }
                //Registro de datos
                MySqlCommand cmd = new MySqlCommand("Usersp_registrar", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("token", oUsuario.Token);
                cmd.Parameters.AddWithValue("user", oUsuario.User);
                ///cmd.Parameters.AddWithValue("password", BCrypt.Net.BCrypt.HashPassword(oUsuario.Password));
                cmd.Parameters.AddWithValue("password", oUsuario.Password);
                cmd.Parameters.AddWithValue("tipoUsuario", oUsuario.TipoUsuario);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally { connection.Close(); }

            }

        }

        public static bool Modificar(Usuario oUsuario)
        {
            using (MySqlConnection connection = new MySqlConnection(Conexion.ConexionString))
            {
                MySqlCommand cmd = new MySqlCommand("Usersp_modificar", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", oUsuario.Token);
                cmd.Parameters.AddWithValue("user", oUsuario.User);
                cmd.Parameters.AddWithValue("password", oUsuario.Password);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally { connection.Close(); }
            }
        }

        public static List<Usuario> Listar()
        {
            List<Usuario> oListaRegistro = new List<Usuario>();
            using (MySqlConnection connection = new MySqlConnection(Conexion.ConexionString))
            {
                MySqlCommand cmd = new MySqlCommand("Usersp_listar", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oListaRegistro.Add(new Usuario()
                            {
                                Token = Convert.ToInt32(dr["Token"]),
                                User = dr["User"].ToString(),
                                Password = dr["Password"].ToString(),
                                TipoUsuario = Convert.ToInt32(dr["Tipo_Usuario"])
                            });
                        }
                    }
                    return oListaRegistro;
                }
                catch (Exception ex)
                {
                    return oListaRegistro;
                }
                finally { connection.Close(); }

            }
        }

        public static Usuario Obtener(string Usuario, string Password)
        {
            Usuario oUsuario = new Usuario();
            //bool checkpassword = false;
            using (MySqlConnection connection = new MySqlConnection(Conexion.ConexionString))
            {
                MySqlCommand cmd = new MySqlCommand("Usersp_obtener", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("usuario", Usuario);
                cmd.Parameters.AddWithValue("contrasena", Password);
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oUsuario = new Usuario()
                            {
                                Token = Convert.ToInt32(dr["Token"]),
                                User = dr["User"].ToString(),
                                Password = dr["Password"].ToString(),
                                TipoUsuario = Convert.ToInt32(dr["Tipo_Usuario"])
                            };

                            //checkpassword = BCrypt.Net.BCrypt.Verify(password, oUsuario.Password);
                        }
                    }

                    //return checkpassword;
                    return oUsuario;
                }
                catch (Exception ex)
                {
                    //return checkpassword;
                    return oUsuario;
                }
                finally { connection.Close(); }
            }
        }
        public static bool Eliminar(int token)
        {
            using (MySqlConnection connection = new MySqlConnection(Conexion.ConexionString))
            {
                MySqlCommand cmd = new MySqlCommand("Usersp_eliminar", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", token);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally { connection.Close(); }
            }
        }
    }
}