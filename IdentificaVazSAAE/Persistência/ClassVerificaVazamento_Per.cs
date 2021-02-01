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
		public DataTable leituras, rol, media, consumo, desvio, moda;

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

		public double ConsumoMaximoFaturado(ClassVerificaVazamento_Dom vazamento_Dom)
		{
			try
			{
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				consumo = new DataTable();
				erro = "";

				comando.Connection = sqlConnection;
				comando.Parameters.Add("@ligacao", SqlDbType.Int);
				comando.Parameters["@ligacao"].Value = Int64.Parse(vazamento_Dom.ligacao.ToString());
				adaptador.SelectCommand = comando;

				comando.CommandText = "select MAX(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),1,6) + 1 - 300)";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(consumo);
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return double.Parse(consumo.Rows[0][0].ToString());
		}

        public DataTable ModaGeral(ClassVerificaVazamento_Dom vazamento_Dom)
        {
			try
			{
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				moda = new DataTable();
				erro = "";

				comando.Connection = sqlConnection;
				comando.Parameters.Add("@ligacao", SqlDbType.Int);
				comando.Parameters["@ligacao"].Value = Int64.Parse(vazamento_Dom.ligacao.ToString());
				adaptador.SelectCommand = comando;

				comando.CommandText = "select count(consumo_faturado) as Quantidade, consumo_faturado as [Consumo Faturado] from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),1,6) + 1 - 300) and ocorrencia = 0 group by Consumo_faturado order by Quantidade Desc";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(moda);
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return moda;
		}

        public double ConsumoMinimoFaturado(ClassVerificaVazamento_Dom vazamento_Dom)
		{
			try
			{
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				consumo = new DataTable();
				erro = "";

				comando.Connection = sqlConnection;
				comando.Parameters.Add("@ligacao", SqlDbType.Int);
				comando.Parameters["@ligacao"].Value = Int64.Parse(vazamento_Dom.ligacao.ToString());
				adaptador.SelectCommand = comando;

				comando.CommandText = "select MIN(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),1,6) + 1 - 300)";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(consumo);
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return double.Parse(consumo.Rows[0][0].ToString());
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

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),1,6) + 1 - 300) and SUBSTRING(data_ref,5,2) = 01 and Ocorrencia = 0";
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

        public double DesvioPadrao(ClassVerificaVazamento_Dom vazamento_Dom)
        {
			try
			{
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				desvio = new DataTable();
				erro = "";

				comando.Connection = sqlConnection;
				comando.Parameters.Add("@ligacao", SqlDbType.Int);
				comando.Parameters["@ligacao"].Value = Int64.Parse(vazamento_Dom.ligacao.ToString());
				adaptador.SelectCommand = comando;

				comando.CommandText = "select ROUND(STDEVP(consumo_faturado), 0) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),1,6) + 1 - 300) and ocorrencia = 0";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(desvio);
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return double.Parse(desvio.Rows[0][0].ToString());
		}

        internal double MediaGeral(ClassVerificaVazamento_Dom vazamento_Dom)
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

				comando.CommandText = "select avg(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),1,6) + 1 - 300) and ocorrencia = 0";
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

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),1,6) + 1 - 300) and SUBSTRING(data_ref,5,2) = 02 and Ocorrencia = 0";
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

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),1,6) + 1 - 300) and SUBSTRING(data_ref,5,2) = 03 and Ocorrencia = 0";
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

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),1,6) + 1 - 300) and SUBSTRING(data_ref,5,2) = 04 and Ocorrencia = 0";
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

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),1,6) + 1 - 300) and SUBSTRING(data_ref,5,2) = 05 and Ocorrencia = 0";
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

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),1,6) + 1 - 300) and SUBSTRING(data_ref,5,2) = 06 and Ocorrencia = 0";
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

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),1,6) + 1 - 300) and SUBSTRING(data_ref,5,2) = 07 and Ocorrencia = 0";
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

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),1,6) + 1 - 300) and SUBSTRING(data_ref,5,2) = 08 and Ocorrencia = 0";
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

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),1,6) + 1 - 300) and SUBSTRING(data_ref,5,2) = 09 and Ocorrencia = 0";
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

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),1,6) + 1 - 300) and SUBSTRING(data_ref,5,2) = 10 and Ocorrencia = 0";
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

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),1,6) + 1 - 300) and SUBSTRING(data_ref,5,2) = 11 and Ocorrencia = 0";
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

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),1,6) + 1 - 300) and SUBSTRING(data_ref,5,2) = 12 and Ocorrencia = 0";
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

				comando.CommandText = "select (substring(data_ref, 5, 2) + '/' + substring(data_ref, 1, 4)) as [Data Ref], data_leitura as [Data da Leitura], leitura_orig as Leitura, ocorrencia_orig as Ocorrencia, consumo_faturado as [Consumo Faturado], hidrometro as Hidrometro from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),1,6) + 1 - 300) order by [Consumo Faturado]";
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
		
		public DataTable mMensais(ClassVerificaVazamento_Dom vazamento_Dom)
        {
			DataTable m_mensais = new DataTable();
			DataColumn dcjan = new DataColumn("jan", typeof(double));
			DataColumn dcfev = new DataColumn("fev", typeof(double));
			DataColumn dcmar = new DataColumn("mar", typeof(double));
			DataColumn dcabr = new DataColumn("abr", typeof(double));
			DataColumn dcmai = new DataColumn("mai", typeof(double));
			DataColumn dcjun = new DataColumn("jun", typeof(double));
			DataColumn dcjul = new DataColumn("jul", typeof(double));
			DataColumn dcago = new DataColumn("ago", typeof(double));
			DataColumn dcset = new DataColumn("set", typeof(double));
			DataColumn dcout = new DataColumn("out", typeof(double));
			DataColumn dcnov = new DataColumn("nov", typeof(double));
			DataColumn dcdez = new DataColumn("dez", typeof(double));

			m_mensais.Columns.Add(dcjan);
			m_mensais.Columns.Add(dcfev);
			m_mensais.Columns.Add(dcmar);
			m_mensais.Columns.Add(dcabr);
			m_mensais.Columns.Add(dcmai);
			m_mensais.Columns.Add(dcjun);
			m_mensais.Columns.Add(dcjul);
			m_mensais.Columns.Add(dcago);
			m_mensais.Columns.Add(dcset);
			m_mensais.Columns.Add(dcout);
			m_mensais.Columns.Add(dcnov);
			m_mensais.Columns.Add(dcdez);

			DataRow dr = m_mensais.NewRow();

			m_mensais.NewRow();

			dr[0] = CalcMediaJan(vazamento_Dom);
			dr[1] = CalcMediaFev(vazamento_Dom);
			dr[2] = CalcMediaMar(vazamento_Dom);
			dr[3] = CalcMediaAbr(vazamento_Dom);
			dr[4] = CalcMediaMai(vazamento_Dom);
			dr[5] = CalcMediaJun(vazamento_Dom);
			dr[6] = CalcMediaJul(vazamento_Dom);
			dr[7] = CalcMediaAgo(vazamento_Dom);
			dr[8] = CalcMediaSet(vazamento_Dom);
			dr[9] = CalcMediaOut(vazamento_Dom);
			dr[10] = CalcMediaNov(vazamento_Dom);
			dr[11] = CalcMediaDez(vazamento_Dom);

			m_mensais.Rows.Add(dr);

			return m_mensais;
        }
    }
}
