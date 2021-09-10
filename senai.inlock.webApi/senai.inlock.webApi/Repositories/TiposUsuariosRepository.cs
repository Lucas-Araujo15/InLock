using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Repositories
{
    public class TiposUsuariosRepository : ITiposUsuariosRepository
    {
        private string stringConexao = @"Data source=DESKTOP-SV3M4A7\SQLEXPRESS; initial catalog=catalogo_m; user id = sa; pwd=Senai@132";
        public void Atualizar(TiposUsuariosDomain novoTipoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdate = "UPDATE TiposUsuarios SET NomeTipoUsuario = @NomeTipoUsuario WHERE IdTipoUsuario = @IdTipoUsuario";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@NomeTipoUsuario", novoTipoUsuario.NomeTipoUsuario);
                    cmd.Parameters.AddWithValue("@IdTipoUsuario", novoTipoUsuario.IdTipoUsuario);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Cadastrar(TiposUsuariosDomain TipoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsertInto = "INSERT INTO TiposUsuarios (NomeTipoUsuario) VALUES (@NomeTipoUsuario)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsertInto, con))
                {
                    cmd.Parameters.AddWithValue("@NomeTipoUsuario", TipoUsuario.NomeTipoUsuario);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM TiposUsuarios WHERE IdTipoUsuario = @IdTipoUsuario";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@IdTipoUsuario", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public TiposUsuariosDomain ListarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectALL = "SELECT IdTipoUsuario, NomeTipoUsuario FROM TiposUsuarios WHERE IdTipoUsuario = @IdTipoUsuario;";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectALL, con))
                {
                    cmd.Parameters.AddWithValue("@IdTipoUsuario", id);
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        TiposUsuariosDomain TipoUsuario = new TiposUsuariosDomain()
                        {
                            IdTipoUsuario = Convert.ToInt32(rdr[0]),
                            NomeTipoUsuario = rdr[1].ToString()
                        };

                        return TipoUsuario;
                    }
                    return null;
                }
            }
        }

        public List<TiposUsuariosDomain> ListarTodos()
        {
            List<TiposUsuariosDomain> listaTiposUsuarios = new List<TiposUsuariosDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectALL = "SELECT IdTipoUsuario, NomeTipoUsuario FROM TiposUsuarios";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectALL, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        TiposUsuariosDomain TipoUsuario = new TiposUsuariosDomain()
                        {
                            IdTipoUsuario = Convert.ToInt32(rdr[0]),
                            NomeTipoUsuario = rdr[1].ToString()
                        };

                        listaTiposUsuarios.Add(TipoUsuario);
                    }
                    return listaTiposUsuarios;
                }
            }
        }
    }
}
