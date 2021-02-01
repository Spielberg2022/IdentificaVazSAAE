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
		public ClassVerificaVazamento_Per verificaVazamento_Per = new ClassVerificaVazamento_Per();
		public ClassVerificaVazamento_Dom verificaVazamento_Dom = new ClassVerificaVazamento_Dom();
		public string erro;
		public bool vf;

		public DataTable ListaLeituras(ClassVerificaVazamento_Dom vazamento_Dom)
        {
			verificaVazamento_Per.sqlConnection = connection;
			vazamento_Dom.leituras = verificaVazamento_Per.LocalizarLigacao(vazamento_Dom);
			verificaVazamento_Dom.leituras = vazamento_Dom.leituras;
			return vazamento_Dom.leituras;
		}

        public DataTable ListaRol(ClassVerificaVazamento_Dom vazamento_Dom)
        {
			verificaVazamento_Per.sqlConnection = connection;
			vazamento_Dom.rol = verificaVazamento_Per.ListaRol(vazamento_Dom);
			verificaVazamento_Dom.rol = vazamento_Dom.rol;
			return vazamento_Dom.rol;
		}

        public string MediaJan(ClassVerificaVazamento_Dom vazamento_Dom)
        {
            verificaVazamento_Per.sqlConnection = connection;
            vazamento_Dom.mediaJan = verificaVazamento_Per.CalcMediaJan(vazamento_Dom);
            verificaVazamento_Dom.mediaJan = vazamento_Dom.mediaJan;
            return vazamento_Dom.mediaJan.ToString();
        }

        public string MediaFev(ClassVerificaVazamento_Dom vazamento_Dom)
        {
            verificaVazamento_Per.sqlConnection = connection;
            vazamento_Dom.mediaFev = verificaVazamento_Per.CalcMediaFev(vazamento_Dom);
            verificaVazamento_Dom.mediaFev = vazamento_Dom.mediaFev;
            return vazamento_Dom.mediaFev.ToString();
        }

        public string MediaMar(ClassVerificaVazamento_Dom vazamento_Dom)
        {
            verificaVazamento_Per.sqlConnection = connection;
            vazamento_Dom.mediaMar = verificaVazamento_Per.CalcMediaMar(vazamento_Dom);
            verificaVazamento_Dom.mediaMar = vazamento_Dom.mediaMar;
            return vazamento_Dom.mediaMar.ToString();
        }

        public string MediaAbr(ClassVerificaVazamento_Dom vazamento_Dom)
        {
            verificaVazamento_Per.sqlConnection = connection;
            vazamento_Dom.mediaAbr = verificaVazamento_Per.CalcMediaAbr(vazamento_Dom);
            verificaVazamento_Dom.mediaAbr = vazamento_Dom.mediaAbr;
            return vazamento_Dom.mediaAbr.ToString();
        }

        public string MediaMai(ClassVerificaVazamento_Dom vazamento_Dom)
        {
            verificaVazamento_Per.sqlConnection = connection;
            vazamento_Dom.mediaMai = verificaVazamento_Per.CalcMediaMai(vazamento_Dom);
            verificaVazamento_Dom.mediaMai = vazamento_Dom.mediaMai;
            return vazamento_Dom.mediaMai.ToString();
        }

        public double VerConsumoMinimo(ClassVerificaVazamento_Dom vazamento_Dom)
        {
            verificaVazamento_Per.sqlConnection = connection;
            vazamento_Dom.consumoMinimo = verificaVazamento_Per.ConsumoMinimoFaturado(vazamento_Dom);
            verificaVazamento_Dom.consumoMinimo = vazamento_Dom.consumoMinimo;
            return vazamento_Dom.consumoMinimo;
        }

        public double VerDesvioPadrao(ClassVerificaVazamento_Dom vazamento_Dom)
        {
            verificaVazamento_Per.sqlConnection = connection;
            vazamento_Dom.desvioPadraoGeral = verificaVazamento_Per.DesvioPadrao(vazamento_Dom);
            verificaVazamento_Dom.desvioPadraoGeral = vazamento_Dom.desvioPadraoGeral;
            return vazamento_Dom.desvioPadraoGeral;
        }

        public double VerMediaGeral(ClassVerificaVazamento_Dom vazamento_Dom)
        {
            verificaVazamento_Per.sqlConnection = connection;
            vazamento_Dom.mediaGeral = verificaVazamento_Per.MediaGeral(vazamento_Dom);
            verificaVazamento_Dom.mediaGeral = vazamento_Dom.mediaGeral;
            return vazamento_Dom.mediaGeral;
        }

        public double VerConsumoMaximo(ClassVerificaVazamento_Dom vazamento_Dom)
        {
            verificaVazamento_Per.sqlConnection = connection;
            vazamento_Dom.consumoMaximo = verificaVazamento_Per.ConsumoMaximoFaturado(vazamento_Dom);
            verificaVazamento_Dom.consumoMaximo = vazamento_Dom.consumoMaximo;
            return vazamento_Dom.consumoMaximo;
        }

        public string MediaJun(ClassVerificaVazamento_Dom vazamento_Dom)
        {
            verificaVazamento_Per.sqlConnection = connection;
            vazamento_Dom.mediaJun = verificaVazamento_Per.CalcMediaJun(vazamento_Dom);
            verificaVazamento_Dom.mediaJun = vazamento_Dom.mediaJun;
            return vazamento_Dom.mediaJun.ToString();
        }

        public string MediaJul(ClassVerificaVazamento_Dom vazamento_Dom)
        {
            verificaVazamento_Per.sqlConnection = connection;
            vazamento_Dom.mediaJul = verificaVazamento_Per.CalcMediaJul(vazamento_Dom);
            verificaVazamento_Dom.mediaJul = vazamento_Dom.mediaJul;
            return vazamento_Dom.mediaJul.ToString();
        }

        public string MediaSet(ClassVerificaVazamento_Dom vazamento_Dom)
        {
            verificaVazamento_Per.sqlConnection = connection;
            vazamento_Dom.mediaSet = verificaVazamento_Per.CalcMediaSet(vazamento_Dom);
            verificaVazamento_Dom.mediaSet = vazamento_Dom.mediaSet;
            return vazamento_Dom.mediaSet.ToString();
        }

        public string MediaNov(ClassVerificaVazamento_Dom vazamento_Dom)
        {
            verificaVazamento_Per.sqlConnection = connection;
            vazamento_Dom.mediaNov = verificaVazamento_Per.CalcMediaNov(vazamento_Dom);
            verificaVazamento_Dom.mediaNov = vazamento_Dom.mediaNov;
            return vazamento_Dom.mediaNov.ToString();
        }

        public string MediaDez(ClassVerificaVazamento_Dom vazamento_Dom)
        {
            verificaVazamento_Per.sqlConnection = connection;
            vazamento_Dom.mediaDez = verificaVazamento_Per.CalcMediaDez(vazamento_Dom);
            verificaVazamento_Dom.mediaDez = vazamento_Dom.mediaDez;
            return vazamento_Dom.mediaDez.ToString();
        }

        public string MediaOut(ClassVerificaVazamento_Dom vazamento_Dom)
        {
            verificaVazamento_Per.sqlConnection = connection;
            vazamento_Dom.mediaOut = verificaVazamento_Per.CalcMediaOut(vazamento_Dom);
            verificaVazamento_Dom.mediaOut = vazamento_Dom.mediaOut;
            return vazamento_Dom.mediaOut.ToString();
        }

        public string MediaAgo(ClassVerificaVazamento_Dom vazamento_Dom)
        {
            verificaVazamento_Per.sqlConnection = connection;
            vazamento_Dom.mediaAgo = verificaVazamento_Per.CalcMediaAgo(vazamento_Dom);
            verificaVazamento_Dom.mediaAgo = vazamento_Dom.mediaAgo;
            return vazamento_Dom.mediaAgo.ToString();
        }

        public DataTable PreencheTabelaMediasMensais(ClassVerificaVazamento_Dom vazamento_Dom)
        {
            DataTable mMedias = new DataTable();
            mMedias = verificaVazamento_Per.mMensais(vazamento_Dom);
            return mMedias;
        }
    }
}
