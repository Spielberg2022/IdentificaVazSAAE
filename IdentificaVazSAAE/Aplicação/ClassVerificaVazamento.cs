using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IdentificaVazSAAE.Persistência;
using IdentificaVazSAAE.Domínio;

namespace IdentificaVazSAAE.Aplicação
{
    class ClassVerificaVazamento
    {
        public ClassConfiguracaoBD configBD = new ClassConfiguracaoBD();
        public ClassBD Bd = new ClassBD();
        public string erro;
        public bool vf;

		/// <summary>
		/// Lista todos os servidores da rede
		/// </summary>
		/// <returns> DataTable com todos os servidores.</returns>
		public DataTable pegaServidores()
		{
			return Bd.Servidores();
		}

		/// <summary>
		/// Lista todos os banco de dados
		/// </summary>
		/// <returns> DataTable com todos os banco de dados.</returns>
		public DataTable pegaBDs()
		{
			return Bd.Bds();
		}

		/// <summary>
		/// Conecta o banco de dados
		/// </summary>
		/// <param name="BD"> Dados do banco de dados</param>
		/// <returns> True para conexão realizada com sucesso e False para conexão não realizada.</returns>
		public bool Conectar(ClassConfiguracaoBD BD)
		{
			if (!Bd.Conectar(BD))
			{
				erro = Bd.erro;
				return false;
			}
			else
				return true;
		}
	}
}
