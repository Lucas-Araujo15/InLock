using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Repositories
{
    public class EstudiosRepository : IEstudiosRepository
    {
        private string stringConexao = @"Data source=DESKTOP-SV3M4A7\SQLEXPRESS; initial catalog=InLock_Games; user id = sa; pwd=Senai@132";
        public void Atualizar(EstudiosDomain novoEstudio)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdate = "UPDATE Estudios SET NomeEstudio = @NomeEstudio WHERE IdEstudio = @IdEstudio";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@NomeEstudio", novoEstudio.NomeEstudio);
                    cmd.Parameters.AddWithValue("@IdEstudio", novoEstudio.IdEstudio);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Cadastrar(EstudiosDomain estudio)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsertInto = "INSERT INTO Estudios (NomeEstudio) VALUES (@NomeEstudio)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsertInto, con))
                {
                    cmd.Parameters.AddWithValue("@NomeEstudio", estudio.NomeEstudio);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM Estudios WHERE IdEstudio = @IdEstudio";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@IdEstudio", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<EstudiosDomain> ListarComJogos()
        {
            List<EstudiosDomain> listaEstudios = new List<EstudiosDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectALL = "SELECT Estudios.IdEstudio, Estudios.NomeEstudio FROM Estudios";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectALL, con))
                {
                    
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        List<JogosDomain> jogosEstudio = new List<JogosDomain>();

                        EstudiosDomain Estudio = new EstudiosDomain()
                        {
                            IdEstudio = Convert.ToInt32(rdr[0]),
                            NomeEstudio = rdr[1].ToString()
                        };

                        using (SqlConnection connection = new SqlConnection(stringConexao))
                        {
                            string query = "SELECT IdJogo, NomeJogo, DescricaoJogo, DataLancamento, ValorJogo FROM Jogos WHERE IdEstudio = @IdEstudio";

                            connection.Open();

                            SqlDataReader reader;

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@IdEstudio", Estudio.IdEstudio);
                                reader = command.ExecuteReader();

                                while (reader.Read())
                                {
                                    JogosDomain Jogo = new JogosDomain()
                                    {
                                        IdJogo = Convert.ToInt32(reader[0]),
                                        NomeJogo = reader[1].ToString(),
                                        DescricaoJogo = reader[2].ToString(),
                                        DataLancamento = Convert.ToDateTime(reader[3]),
                                        ValorJogo = Convert.ToDecimal(reader[4]),
                                    };

                                    jogosEstudio.Add(Jogo);
                                }
                            }
                        }
                        Estudio.listaDeJogos = jogosEstudio;

                        listaEstudios.Add(Estudio);
                    }
                }
            }
            return listaEstudios;
        }

        public EstudiosDomain ListarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectALL = "SELECT IdEstudio, NomeEstudio FROM Estudios WHERE IdEstudio = @IdEstudio;";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectALL, con))
                {
                    cmd.Parameters.AddWithValue("@IdEstudio", id);
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        EstudiosDomain Estudio = new EstudiosDomain()
                        {
                            IdEstudio = Convert.ToInt32(rdr[0]),
                            NomeEstudio = rdr[1].ToString()
                        };

                        return Estudio;
                    }
                    return null;
                }
            }
        }

        public List<EstudiosDomain> ListarTodos()
        {
            List<EstudiosDomain> listaEstudios = new List<EstudiosDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectALL = "SELECT IdEstudio, NomeEstudio FROM Estudios";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectALL, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        EstudiosDomain Estudio = new EstudiosDomain()
                        {
                            IdEstudio = Convert.ToInt32(rdr[0]),
                            NomeEstudio = rdr[1].ToString()
                        };

                        listaEstudios.Add(Estudio);
                    }
                    return listaEstudios;
                }
            }
        }
    }
}
