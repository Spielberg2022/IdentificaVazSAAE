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
    }
}
