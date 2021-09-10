using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Repositories
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private string stringConexao = @"Data source=DESKTOP-SV3M4A7\SQLEXPRESS; initial catalog=catalogo_m; user id = sa; pwd=Senai@132";
        public void Atualizar(UsuariosDomain novoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdate = "UPDATE Usuarios SET NomeUsuario = @NomeUsuario, Email = @Email, Senha = @Senha, IdTipoUsuario = @IdTipoUsuario WHERE IdUsuario = @IdUsuario";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@NomeUsuario", novoUsuario.NomeUsuario);
                    cmd.Parameters.AddWithValue("@Email", novoUsuario.email);
                    cmd.Parameters.AddWithValue("@Senha", novoUsuario.senha);
                    cmd.Parameters.AddWithValue("@IdTipoUsuario", novoUsuario.IdTipoUsuario);
                    cmd.Parameters.AddWithValue("@IdUsuario", novoUsuario.IdUsuario);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public UsuariosDomain Autenticar(string email, string senha)
        {
            if (email != null && senha != null)
            {
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    string queryAutenticacao = "SELECT IdUsuario, IdTipoUsuario, NomeUsuario, Email, NomeTipoUsuario FROM Usuarios WHERE Email = @Email AND Senha = @Senha";

                    con.Open();

                    SqlDataReader rdr;

                    using (SqlCommand cmd = new SqlCommand(queryAutenticacao, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Senha", senha);

                        rdr = cmd.ExecuteReader();

                        if (rdr.Read())
                        {
                            TiposUsuariosDomain tipoUsuario = new TiposUsuariosDomain()
                            {
                                IdTipoUsuario = Convert.ToInt32(rdr[1]),
                                NomeTipoUsuario = rdr[4].ToString(),
                            };

                            UsuariosDomain usuario = new UsuariosDomain()
                            {
                                IdUsuario = Convert.ToInt32(rdr[0]),
                                IdTipoUsuario = Convert.ToInt32(rdr[1]),
                                NomeUsuario = rdr[2].ToString(),
                                email = rdr[3].ToString(),
                            };
                            return usuario;
                        }
                    }
                }
            }
            return null;
        }

        public void Cadastrar(UsuariosDomain Usuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsertInto = "INSERT INTO Usuarios (NomeUsuario, Email, Senha, IdTipoUsuario) VALUES (@NomeUsuario, @Email, @Senha, @IdTipoUsuario)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsertInto, con))
                {
                    cmd.Parameters.AddWithValue("@NomeUsuario", Usuario.NomeUsuario);
                    cmd.Parameters.AddWithValue("@Email", Usuario.email);
                    cmd.Parameters.AddWithValue("@Senha", Usuario.senha);
                    cmd.Parameters.AddWithValue("@IdTipoUsuario", Usuario.IdTipoUsuario);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM Usuarios WHERE IdUsuario = @IdUsuario";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public UsuariosDomain ListarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectALL = "SELECT IdUsuario, Usuarios.IdTipoUsuario, NomeUsuario, Email, NomeTipoUsuario FROM Usuarios INNER JOIN TiposUsuarios ON Usuarios.IdTipoUsuario = TiposUsuarios.IdTipoUsuario WHERE IdUsuario = @IdUsuario;";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectALL, con))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", id);
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        TiposUsuariosDomain TipoUsuario = new TiposUsuariosDomain()
                        {
                            IdTipoUsuario = Convert.ToInt32(rdr[1]),
                            NomeTipoUsuario = rdr[4].ToString()
                        };

                        UsuariosDomain Usuario = new UsuariosDomain()
                        {
                            IdUsuario = Convert.ToInt32(rdr[0]),
                            IdTipoUsuario = Convert.ToInt32(rdr[1]),
                            NomeUsuario = rdr[2].ToString(),
                            email = rdr[3].ToString(),
                            TipoUsuario = TipoUsuario
                        };
                        return Usuario;
                    }
                    return null;
                }               
            }
        }

        public List<UsuariosDomain> ListarTodos()
        {
            List<UsuariosDomain> listaUsuarios = new List<UsuariosDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectALL = "SELECT IdUsuario, Usuarios.IdTipoUsuario, NomeUsuario, Email, NomeTipoUsuario FROM Usuarios INNER JOIN TiposUsuarios ON Usuarios.IdTipoUsuario = TiposUsuarios.IdTipoUsuario;";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectALL, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        TiposUsuariosDomain TipoUsuario = new TiposUsuariosDomain()
                        {
                            IdTipoUsuario = Convert.ToInt32(rdr[1]),
                            NomeTipoUsuario = rdr[4].ToString()
                        };

                        UsuariosDomain Usuario = new UsuariosDomain()
                        {
                            IdUsuario = Convert.ToInt32(rdr[0]),
                            IdTipoUsuario = Convert.ToInt32(rdr[1]),
                            NomeUsuario = rdr[2].ToString(),
                            email = rdr[3].ToString(),
                            TipoUsuario = TipoUsuario
                        };

                        listaUsuarios.Add(Usuario);
                    }
                }
                return listaUsuarios;
            }
        }
    }
}
