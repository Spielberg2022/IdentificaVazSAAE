using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IdentificaVazSAAE.Domínio;

namespace IdentificaVazSAAE.Persistência
{
    public class ClassVerificaVazamento_Per
    {
		public SqlConnection sqlConnection = new SqlConnection();
		public SqlDataAdapter adaptador;
		public SqlCommand comando;
		public DataTable leituras, rol, media;

		public string erro;

		public DataTable LocalizarLigacao (ClassVerificaVazamento_Dom verificaVazamento)
        {
			try
			{
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				leituras = new DataTable();
				erro = "";

				comando.Connection = sqlConnection;
				comando.Parameters.Add("@ligacao", SqlDbType.Int);
				comando.Parameters["@ligacao"].Value = Int64.Parse(verificaVazamento.ligacao.ToString());
				adaptador.SelectCommand = comando;

				comando.CommandText = "select (substring(data_ref, 5, 2) + '/' + substring(data_ref, 1, 4)) as [Data Ref], data_leitura as [Data da Leitura], leitura_orig as Leitura, ocorrencia_orig as Ocorrencia,consumo_faturado as [Consumo Faturado], hidrometro as Hidrometro from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),1,6) + 1 - 300)";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(leituras);
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return leituras;
		}

        public double CalcMediaJan(ClassVerificaVazamento_Dom vazamento_Dom)
        {
			try
			{
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				media = new DataTable();
				erro = "";

				comando.Connection = sqlConnection;
				comando.Parameters.Add("@ligacao", SqlDbType.Int);
				comando.Parameters["@ligacao"].Value = Int64.Parse(vazamento_Dom.ligacao.ToString());
				adaptador.SelectCommand = comando;

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),0,7) + 1 - 300) and SUBSTRING(data_ref,5,2) = 01 and Ocorrencia = 0";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(media);
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return double.Parse(media.Rows[0][0].ToString());
		}

		public double CalcMediaFev(ClassVerificaVazamento_Dom vazamento_Dom)
		{
			try
			{
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				media = new DataTable();
				erro = "";

				comando.Connection = sqlConnection;
				comando.Parameters.Add("@ligacao", SqlDbType.Int);
				comando.Parameters["@ligacao"].Value = Int64.Parse(vazamento_Dom.ligacao.ToString());
				adaptador.SelectCommand = comando;

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),0,7) + 1 - 300) and SUBSTRING(data_ref,5,2) = 02 and Ocorrencia = 0";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(media);
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return double.Parse(media.Rows[0][0].ToString());
		}

		public double CalcMediaMar(ClassVerificaVazamento_Dom vazamento_Dom)
		{
			try
			{
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				media = new DataTable();
				erro = "";

				comando.Connection = sqlConnection;
				comando.Parameters.Add("@ligacao", SqlDbType.Int);
				comando.Parameters["@ligacao"].Value = Int64.Parse(vazamento_Dom.ligacao.ToString());
				adaptador.SelectCommand = comando;

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),0,7) + 1 - 300) and SUBSTRING(data_ref,5,2) = 03 and Ocorrencia = 0";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(media);
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return double.Parse(media.Rows[0][0].ToString());
		}

		public double CalcMediaAbr(ClassVerificaVazamento_Dom vazamento_Dom)
		{
			try
			{
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				media = new DataTable();
				erro = "";

				comando.Connection = sqlConnection;
				comando.Parameters.Add("@ligacao", SqlDbType.Int);
				comando.Parameters["@ligacao"].Value = Int64.Parse(vazamento_Dom.ligacao.ToString());
				adaptador.SelectCommand = comando;

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),0,7) + 1 - 300) and SUBSTRING(data_ref,5,2) = 04 and Ocorrencia = 0";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(media);
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return double.Parse(media.Rows[0][0].ToString());
		}

		public double CalcMediaMai(ClassVerificaVazamento_Dom vazamento_Dom)
		{
			try
			{
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				media = new DataTable();
				erro = "";

				comando.Connection = sqlConnection;
				comando.Parameters.Add("@ligacao", SqlDbType.Int);
				comando.Parameters["@ligacao"].Value = Int64.Parse(vazamento_Dom.ligacao.ToString());
				adaptador.SelectCommand = comando;

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),0,7) + 1 - 300) and SUBSTRING(data_ref,5,2) = 05 and Ocorrencia = 0";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(media);
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return double.Parse(media.Rows[0][0].ToString());
		}

		public double CalcMediaJun(ClassVerificaVazamento_Dom vazamento_Dom)
		{
			try
			{
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				media = new DataTable();
				erro = "";

				comando.Connection = sqlConnection;
				comando.Parameters.Add("@ligacao", SqlDbType.Int);
				comando.Parameters["@ligacao"].Value = Int64.Parse(vazamento_Dom.ligacao.ToString());
				adaptador.SelectCommand = comando;

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),0,7) + 1 - 300) and SUBSTRING(data_ref,5,2) = 06 and Ocorrencia = 0";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(media);
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return double.Parse(media.Rows[0][0].ToString());
		}

		public double CalcMediaJul(ClassVerificaVazamento_Dom vazamento_Dom)
		{
			try
			{
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				media = new DataTable();
				erro = "";

				comando.Connection = sqlConnection;
				comando.Parameters.Add("@ligacao", SqlDbType.Int);
				comando.Parameters["@ligacao"].Value = Int64.Parse(vazamento_Dom.ligacao.ToString());
				adaptador.SelectCommand = comando;

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),0,7) + 1 - 300) and SUBSTRING(data_ref,5,2) = 07 and Ocorrencia = 0";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(media);
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return double.Parse(media.Rows[0][0].ToString());
		}

		public double CalcMediaAgo(ClassVerificaVazamento_Dom vazamento_Dom)
		{
			try
			{
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				media = new DataTable();
				erro = "";

				comando.Connection = sqlConnection;
				comando.Parameters.Add("@ligacao", SqlDbType.Int);
				comando.Parameters["@ligacao"].Value = Int64.Parse(vazamento_Dom.ligacao.ToString());
				adaptador.SelectCommand = comando;

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),0,7) + 1 - 300) and SUBSTRING(data_ref,5,2) = 08 and Ocorrencia = 0";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(media);
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return double.Parse(media.Rows[0][0].ToString());
		}

		public double CalcMediaSet(ClassVerificaVazamento_Dom vazamento_Dom)
		{
			try
			{
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				media = new DataTable();
				erro = "";

				comando.Connection = sqlConnection;
				comando.Parameters.Add("@ligacao", SqlDbType.Int);
				comando.Parameters["@ligacao"].Value = Int64.Parse(vazamento_Dom.ligacao.ToString());
				adaptador.SelectCommand = comando;

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),0,7) + 1 - 300) and SUBSTRING(data_ref,5,2) = 09 and Ocorrencia = 0";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(media);
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return double.Parse(media.Rows[0][0].ToString());
		}

		public double CalcMediaOut(ClassVerificaVazamento_Dom vazamento_Dom)
		{
			try
			{
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				media = new DataTable();
				erro = "";

				comando.Connection = sqlConnection;
				comando.Parameters.Add("@ligacao", SqlDbType.Int);
				comando.Parameters["@ligacao"].Value = Int64.Parse(vazamento_Dom.ligacao.ToString());
				adaptador.SelectCommand = comando;

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),0,7) + 1 - 300) and SUBSTRING(data_ref,5,2) = 10 and Ocorrencia = 0";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(media);
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return double.Parse(media.Rows[0][0].ToString());
		}

		public double CalcMediaNov(ClassVerificaVazamento_Dom vazamento_Dom)
		{
			try
			{
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				media = new DataTable();
				erro = "";

				comando.Connection = sqlConnection;
				comando.Parameters.Add("@ligacao", SqlDbType.Int);
				comando.Parameters["@ligacao"].Value = Int64.Parse(vazamento_Dom.ligacao.ToString());
				adaptador.SelectCommand = comando;

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),0,7) + 1 - 300) and SUBSTRING(data_ref,5,2) = 11 and Ocorrencia = 0";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(media);
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return double.Parse(media.Rows[0][0].ToString());
		}

		public double CalcMediaDez(ClassVerificaVazamento_Dom vazamento_Dom)
		{
			try
			{
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				media = new DataTable();
				erro = "";

				comando.Connection = sqlConnection;
				comando.Parameters.Add("@ligacao", SqlDbType.Int);
				comando.Parameters["@ligacao"].Value = Int64.Parse(vazamento_Dom.ligacao.ToString());
				adaptador.SelectCommand = comando;

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),0,7) + 1 - 300) and SUBSTRING(data_ref,5,2) = 12 and Ocorrencia = 0";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(media);
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return double.Parse(media.Rows[0][0].ToString());
		}

		public DataTable ListaRol(ClassVerificaVazamento_Dom vazamento_Dom)
        {
			try
			{
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				rol = new DataTable();
				erro = "";

				comando.Connection = sqlConnection;
				comando.Parameters.Add("@ligacao", SqlDbType.Int);
				comando.Parameters["@ligacao"].Value = Int64.Parse(vazamento_Dom.ligacao.ToString());
				adaptador.SelectCommand = comando;

				comando.CommandText = "select (substring(data_ref, 5, 2) + '/' + substring(data_ref, 1, 4)) as [Data Ref], data_leitura as [Data da Leitura], leitura_orig as Leitura, ocorrencia_orig as Ocorrencia, consumo_faturado as [Consumo Faturado], hidrometro as Hidrometro from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),0,7) + 1 - 300) order by [Consumo Faturado]";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(rol);
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return rol;
		}
    }
}
