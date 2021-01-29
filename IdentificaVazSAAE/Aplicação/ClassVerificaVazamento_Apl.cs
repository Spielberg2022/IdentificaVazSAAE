using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IdentificaVazSAAE.Persistência;
using IdentificaVazSAAE.Domínio;

namespace IdentificaVazSAAE.Aplicação
{
    public class ClassVerificaVazamento_Apl
    {
		public SqlConnection connection = new SqlConnection();
		public ClassVerificaVazamento_Dom verificaVazamento_Dom = new ClassVerificaVazamento_Dom();
		public ClassVerificaVazamento_Per verificaVazamento_Per = new ClassVerificaVazamento_Per();
		public string erro;
		public bool vf;

		public DataTable ListaLeituras(ClassVerificaVazamento_Dom vazamento_Dom)
        {
			verificaVazamento_Per.sqlConnection = connection;
			return verificaVazamento_Per.LocalizarLigacao(vazamento_Dom);
		}
	}
}
