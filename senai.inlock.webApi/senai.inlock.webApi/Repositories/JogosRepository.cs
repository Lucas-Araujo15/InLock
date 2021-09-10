using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Repositories
{
    public class JogosRepository : IJogosRepository
    {
        private string stringConexao = @"Data source=DESKTOP-SV3M4A7\SQLEXPRESS; initial catalog=catalogo_m; user id = sa; pwd=Senai@132";
        public void Atualizar(JogosDomain novoJogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdate = "UPDATE Jogos SET NomeJogo = @NomeJogo, DescricaoJogo = @DescricaoJogo, DataLancamento = @DataLancamento, ValorJogo = @ValorJogo, IdEstudio = @IdEstudio WHERE IdJogo = @IdJogo";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@NomeJogo", novoJogo.NomeJogo);
                    cmd.Parameters.AddWithValue("@DescricaoJogo", novoJogo.DescricaoJogo);
                    cmd.Parameters.AddWithValue("@DataLancamento", novoJogo.DataLancamento);
                    cmd.Parameters.AddWithValue("@ValorJogo", novoJogo.ValorJogo);
                    cmd.Parameters.AddWithValue("@IdEstudio", novoJogo.IdEstudio);
                    cmd.Parameters.AddWithValue("@IdJogo", novoJogo.IdJogo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Cadastrar(JogosDomain jogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsertInto = "INSERT INTO Jogos (NomeJogo, DescricaoJogo, DataLancamento, ValorJogo, IdEstudio) VALUES (@NomeJogo, @DescricaoJogo, @DataLancamento, @ValorJogo, @IdEstudio);";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsertInto, con))
                {
                    cmd.Parameters.AddWithValue("@NomeJogo", jogo.NomeJogo);
                    cmd.Parameters.AddWithValue("@DescricaoJogo", jogo.DescricaoJogo);
                    cmd.Parameters.AddWithValue("@DataLancamento", jogo.DataLancamento);
                    cmd.Parameters.AddWithValue("@ValorJogo", jogo.ValorJogo);
                    cmd.Parameters.AddWithValue("@IdEstudio", jogo.IdEstudio);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM Jogos WHERE IdJogo = @IdJogo";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@IdJogo", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public JogosDomain ListarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectALL = "SELECT IdJogo, NomeJogo, DescricaoJogo, DataLancamento, ValorJogo, Jogos.IdEstudio, NomeEstudio FROM Jogos INNER JOIN Estudios ON Jogos.IdEstudio = Estudios.IdEstudio WHERE IdJogo = @IdJogo;";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectALL, con))
                {
                    cmd.Parameters.AddWithValue("@IdJogo", id);
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        EstudiosDomain Estudio = new EstudiosDomain()
                        {
                            IdEstudio = Convert.ToInt32(rdr[5]),
                            NomeEstudio = rdr[6].ToString()
                        };

                        JogosDomain Jogo = new JogosDomain()
                        {
                            IdJogo = Convert.ToInt32(rdr[0]),
                            IdEstudio = Convert.ToInt32(rdr[5]),
                            NomeJogo = rdr[1].ToString(),    
                            DescricaoJogo = rdr[2].ToString(),
                            DataLancamento = Convert.ToDateTime(rdr[3]),
                            ValorJogo = Convert.ToDecimal(rdr[4]),
                            Estudio = Estudio
                        };
                        return Jogo;
                    }
                    return null;
                }
            }
        }

        public List<JogosDomain> ListarTodos()
        {
            List<JogosDomain> listaJogos = new List<JogosDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectALL = "SELECT IdJogo, NomeJogo, DescricaoJogo, DataLancamento, ValorJogo, Jogos.IdEstudio, NomeEstudio FROM Jogos INNER JOIN Estudios ON Jogos.IdEstudio = Estudios.IdEstudio;";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectALL, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        EstudiosDomain Estudio = new EstudiosDomain()
                        {
                            IdEstudio = Convert.ToInt32(rdr[5]),
                            NomeEstudio = rdr[6].ToString()
                        };

                        JogosDomain Jogo = new JogosDomain()
                        {
                            IdJogo = Convert.ToInt32(rdr[0]),
                            IdEstudio = Convert.ToInt32(rdr[5]),
                            NomeJogo = rdr[1].ToString(),
                            DescricaoJogo = rdr[2].ToString(),
                            DataLancamento = Convert.ToDateTime(rdr[3]),
                            ValorJogo = Convert.ToDecimal(rdr[4]),
                            Estudio = Estudio
                        };

                        listaJogos.Add(Jogo);                        
                    }
                    return listaJogos;
                }
            }
        }
    }
}
