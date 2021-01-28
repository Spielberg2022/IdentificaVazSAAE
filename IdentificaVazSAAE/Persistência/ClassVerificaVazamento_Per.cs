using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IdentificaVazSAAE.Domínio;

namespace IdentificaVazSAAE.Persistência
{
    class ClassVerificaVazamento_Per
    {
		public SqlConnection sqlConnection = new SqlConnection();
		public SqlDataAdapter adaptador;
		public SqlCommand comando;
		public DataTable leituras;
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
				comando.Parameters.Add("@ligacao");
				comando.Parameters["@ligacao"].Value = verificaVazamento.ligacao;
				adaptador.SelectCommand = comando;

				comando.CommandText = "select cod_ligacao as ligacao, data_ref as dataRef, data_leitura as dataLeitura, leitura_orig as leitura, ocorrencia_orig as ocorrencia,consumo_faturado as consumoFaturado, hidrometro as hidrometro from leituras where cod_ligacao = @ligacao and data_ref >= '202002' - 300";
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
    }
}
