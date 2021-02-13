﻿using System;
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
		public DataTable leituras, rol, media, consumo, desvio, moda, vazamentos, mediaUlt3meses, valorAgua, dadosConsumoAtual, anexo;

		public double consPorEcon, valorConta, valorContaA, valorContaB, valorContaC, valorContaD, valorContaO;
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

        public double MediaUlt3Meses(ClassVerificaVazamento_Dom vazamento_Dom)
        {
			try
			{
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				mediaUlt3meses = new DataTable();
				erro = "";

				comando.Connection = sqlConnection;
				comando.Parameters.Add("@ligacao", SqlDbType.Int);
				comando.Parameters["@ligacao"].Value = Int64.Parse(vazamento_Dom.ligacao.ToString());
				adaptador.SelectCommand = comando;

				comando.CommandText = "select AVG(consumo_faturado) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),1,6) -1) and Ocorrencia = 0";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(mediaUlt3meses);
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return double.Parse(mediaUlt3meses.Rows[0][0].ToString());
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

        public DataTable VerificaVazamentos(ClassVerificaVazamento_Dom vazamento_Dom)
        {
			try
			{
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				vazamentos = new DataTable();
				erro = "";

				comando.Connection = sqlConnection;
				comando.Parameters.Add("@ligacao", SqlDbType.Int);
				comando.Parameters["@ligacao"].Value = Int64.Parse(vazamento_Dom.ligacao.ToString());
				comando.Parameters.Add("@consumoMaximo", SqlDbType.Float);
                comando.Parameters["@consumoMaximo"].Value = Double.Parse(vazamento_Dom.consumoPadraoMaximo.ToString());
				adaptador.SelectCommand = comando;

				comando.CommandText = "select (substring(data_ref, 5, 2) + '/' + substring(data_ref, 1, 4)) as [Data Ref], data_leitura as [Data da Leitura], leitura_orig as Leitura, consumo_faturado as [Consumo Faturado], hidrometro as Hidrometro from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),1,6) + 1 - 300) and consumo_faturado > @consumoMaximo and ocorrencia = 0 and data_ref in(select data_ref from ESPELHO_CONTA where cod_ligacao = @ligacao and situacao = '0') order by data_ref desc";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(vazamentos);
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return vazamentos;
		}

        public double ValorConta(ClassVerificaVazamento_Dom vazamento_Dom)
        {
			try
			{
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				valorAgua = new DataTable();
				anexo = new DataTable();
				erro = "";
				string data_ref;
				int consEconDom, consEconCom, consEconInd, consEconPub, consEconOut, consFaturado;
				double consPorEcon;

				anexo = DadosAnexo();

				comando.Connection = sqlConnection;
				comando.Parameters.Add("@ligacao", SqlDbType.Int);
				comando.Parameters["@ligacao"].Value = Int64.Parse(vazamento_Dom.ligacao.ToString());
				adaptador.SelectCommand = comando;

				comando.CommandText = "select ROUND(STDEVP(consumo_faturado), 0) from leituras where cod_ligacao = @ligacao and data_ref >= (select SUBSTRING((select data_ult_fech from controle),1,6) + 1 - 300) and ocorrencia = 0";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(valorAgua);

				data_ref = valorAgua.Rows[0]["Data_Ref"].ToString();
				consEconDom = int.Parse(valorAgua.Rows[0]["economia_dom"].ToString());
				consEconCom = int.Parse(valorAgua.Rows[0]["economia_com"].ToString());
				consEconInd = int.Parse(valorAgua.Rows[0]["economia_ind"].ToString());
				consEconPub = int.Parse(valorAgua.Rows[0]["economia_pub"].ToString());
				consEconOut = int.Parse(valorAgua.Rows[0]["economia_out"].ToString());
				consFaturado = int.Parse(valorAgua.Rows[0]["Consumo_faturado"].ToString());

				consPorEcon = consFaturado / (consEconDom + consEconCom + consEconInd + consEconPub + consEconOut);

				if (consEconDom > 0)
                {
					if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons6"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons5"].ToString()))
						valorContaA = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual5"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons5"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual6"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons5"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual5"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons5"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual6"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons5"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))/2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons5"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
						valorContaA = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual5"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual5"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))/2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
						valorContaA = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) -(double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))/2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
						valorContaA = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())))/2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if(consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
						valorContaA = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))/2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if(consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
						valorContaA = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString()) * consPorEcon) 
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString()) * consPorEcon)/2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());				
                }
				if (consEconCom > 0)
				{
					if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons6"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons5"].ToString()))
						valorContaB = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual5"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons5"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual6"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons5"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual5"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons5"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual6"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons5"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons5"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
						valorContaB = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual5"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual5"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
						valorContaB = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
						valorContaB = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
						valorContaB = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
						valorContaB = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString()) * consPorEcon)
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString()) * consPorEcon) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());
				}
				if (consEconInd > 0)
				{
					if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons6"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons5"].ToString()))
						valorContaC = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual5"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons5"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual6"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons5"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual5"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons5"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual6"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons5"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons5"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
						valorContaC = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual5"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual5"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
						valorContaC = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
						valorContaC = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
						valorContaC = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
						valorContaC = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString()) * consPorEcon)
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString()) * consPorEcon) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());
				}
				if (consEconPub > 0)
				{
					if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons6"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons5"].ToString()))
						valorContaD = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual5"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons5"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual6"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons5"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual5"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons5"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual6"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons5"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons5"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
						valorContaD = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual5"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual5"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
						valorContaD = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
						valorContaD = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
						valorContaD = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
						valorContaD = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString()) * consPorEcon)
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString()) * consPorEcon) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());
				}
				if (consEconOut > 0)
				{
					if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons6"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons5"].ToString()))
						valorContaO = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual5"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons5"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual6"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons5"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual5"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons5"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual6"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons5"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons5"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
						valorContaO = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual5"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual5"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons4"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons4"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
						valorContaO = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual4"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons3"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons3"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
						valorContaO = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual3"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString())) - (double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()))) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons2"].ToString()) && consPorEcon > double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
						valorContaO = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString())))
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString())) * (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
							+ (double.Parse(anexo.Rows[0]["vl_atual2"].ToString())) * (consPorEcon - (double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());

					else if (consPorEcon <= double.Parse(anexo.Rows[0]["faixa_cons1"].ToString()))
						valorContaO = (double.Parse(anexo.Rows[0]["vl_atual1"].ToString()) * consPorEcon)
							+ ((double.Parse(anexo.Rows[0]["vl_atual1"].ToString()) * consPorEcon) / 2)
							+ double.Parse(anexo.Rows[0]["vr_TBO_agua_Atual"].ToString())
							+ double.Parse(anexo.Rows[0]["vr_TBO_esgo_Atual"].ToString());
				}

				valorConta = valorContaA + valorContaB + valorContaC + valorContaD + valorContaO;
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return valorConta;
		}

		public DataTable DadosConsumoAtual(ClassVerificaVazamento_Dom vazamento_Dom)
        {
            try
            {
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				dadosConsumoAtual = new DataTable();
				erro = "";

				comando.Connection = sqlConnection;
				comando.Parameters.Add("@ligacao", SqlDbType.Int);
				comando.Parameters["@ligacao"].Value = Int64.Parse(vazamento_Dom.ligacao.ToString());
				adaptador.SelectCommand = comando;

				comando.CommandText = "select leituras.Data_Ref, leituras.Consumo_faturado, usuario.economia_dom, usuario.economia_com, usuario.economia_ind, usuario.economia_pub, usuario.economia_out from usuario, LEITURAS where usuario.cod_ligacao = @ligacao and leituras.cod_ligacao = usuario.cod_ligacao and leituras.Data_Ref in(select SUBSTRING((select data_ult_fech from controle),1,6) + 1) and usuario.cod_ligacao in(select cod_ligacao from DADOSADICIONAISUSUARIO where D02 = 0)";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(valorAgua);
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return dadosConsumoAtual;
		}

		internal DataTable DadosAnexo()
		{
			try
			{
				adaptador = new SqlDataAdapter();
				comando = new SqlCommand();
				anexo = new DataTable();
				erro = "";

				comando.Connection = sqlConnection;
				adaptador.SelectCommand = comando;

				comando.CommandText = "select categ, sub_categ, vr_TBO_agua_Atual, vr_TBO_esgo_Atual, faixa_cons1, faixa_cons2, faixa_cons3, faixa_cons4, faixa_cons5, faixa_cons6, vl_atual1, vl_atual2, vl_atual3, vl_atual4, vl_atual5, vl_atual6, Social from ANEXO where tipo = 'H'";
				sqlConnection.Open();
				adaptador.SelectCommand.ExecuteNonQuery();
				adaptador.Fill(anexo);
			}
			catch (Exception error)
			{
				erro = error.Message;
			}
			finally
			{
				sqlConnection.Close();
			}

			return anexo;
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
