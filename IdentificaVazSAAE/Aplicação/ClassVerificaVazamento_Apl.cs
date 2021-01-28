using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IdentificaVazSAAE.Persistência;
using IdentificaVazSAAE.Domínio;

namespace IdentificaVazSAAE.Aplicação
{
    class ClassVerificaVazamento_Apl
    {
		public ClassVerificaVazamento_Dom verificaVazamento_Dom = new ClassVerificaVazamento_Dom();
		public ClassVerificaVazamento_Per verificaVazamento_Per = new ClassVerificaVazamento_Per();
		public string erro;
		public bool vf;

		public DataTable ListaLeituras(ClassVerificaVazamento_Dom vazamento_Dom)
        {
			return verificaVazamento_Per.LocalizarLigacao(vazamento_Dom);
		}
	}
}
