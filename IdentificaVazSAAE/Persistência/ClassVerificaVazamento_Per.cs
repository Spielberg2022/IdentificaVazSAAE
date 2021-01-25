using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentificaVazSAAE.Persistência
{
    struct Leituras
    {
        DateTime dataLeitura;
        int leitura;
        int consumo;
    }

    class ClassVerificaVazamento_Per
    {
        /// <summary>
        /// Define a quantidade de anos a serem analisados
        /// </summary>
        public int QtdAnos { get; set; }

        /// <summary>
        /// Define a quantidade de meses a serem verificados o vazamento
        /// </summary>
        public int QtdMeses { get; set; }

        /// <summary>
        /// Leituras efetuadas no período da análise
        /// </summary>
        public Leituras[] leituras { get; set; }

        /// <summary>
        /// Média de consumo anual
        /// </summary>
        public double mediaAnual { get; set; }

        /// <summary>
        /// Moda do consumo anual
        /// </summary>
        public double modaAnual { get; set; }

        /// <summary>
        /// Mediana do consumo anual
        /// </summary>
        public double mediadaAnual { get; set; }

        /// <summary>
        /// Desvio padrão do consumo anual
        /// </summary>
        public double desvioPadraoAnual { get; set; }
    }
}
