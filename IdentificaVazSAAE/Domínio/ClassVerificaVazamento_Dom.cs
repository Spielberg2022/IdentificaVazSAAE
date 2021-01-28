using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentificaVazSAAE.Domínio
{
    class ClassVerificaVazamento_Dom
    {
        public DataTable leituras { get; set; }
        public Int64 ligacao { get; set; }
        public string dataRef { get; set; }
        public string dataRefAtual { get; set; }
        public DateTime dataLeitura { get; set; }
        public Int64 leitura { get; set; }
        public int ocorrencia { get; set; }
        public int consumoFaturado { get; set; }
        public string hidrometro { get; set; }
        public DataTable rol { get; set; }
        public int amplitude { get; set; }
        public int frequenciaAbsoluta { get; set; }
        public int frequenciaRelativa { get; set; }
        public int frequenciaAbsolutaAcumulada { get; set; }
        public int frequenciaRelativaAcumulada { get; set; }
        public double mediaSimples { get; set; }
        public double mediaPonderada { get; set; }
        public Int64 mediana { get; set; }
        public double mediaAparada { get; set; }
        public Int64 moda { get; set; }
        public double desvioMedio { get; set; }
        public double variancia { get; set; }
        public double desvioPadrao { get; set; }

    }
}
