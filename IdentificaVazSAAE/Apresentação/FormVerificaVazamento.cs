using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using IdentificaVazSAAE.Domínio;
using IdentificaVazSAAE.Aplicação;
using IdentificaVazSAAE.Persistência;

namespace IdentificaVazSAAE.Apresentação
{
    public partial class FormVerificaVazamento : Form
    {
        public ClassVerificaVazamento_Apl verificaVazamento_Apl = new ClassVerificaVazamento_Apl();
        public ClassVerificaVazamento_Per verificaVazamento_Per = new ClassVerificaVazamento_Per();
        public ClassVerificaVazamento_Dom verificaVazamento_Dom = new ClassVerificaVazamento_Dom();

        public ClassConfigBD_Apl configBD_Apl = new ClassConfigBD_Apl();
        public ClassConfigBD_Per configBD_Per = new ClassConfigBD_Per();
        public ClassConfigBD_Dom configBD_Dom = new ClassConfigBD_Dom();

        public SqlConnection connection = new SqlConnection();

        public FormVerificaVazamento()
        {
            InitializeComponent();
        }

        private void localizar_button_Click(object sender, EventArgs e)
        {
            verificaVazamento_Apl.connection = connection;
            verificaVazamento_Dom.ligacao = Int64.Parse(codLigacao_textBox.Text);
            leituras_dataGridView.DataSource = verificaVazamento_Apl.ListaLeituras(verificaVazamento_Dom);
            rol_dataGridView.DataSource = verificaVazamento_Apl.ListaRol(verificaVazamento_Dom);
            verificaVazamento_Dom.mMensais = verificaVazamento_Apl.PreencheTabelaMediasMensais(verificaVazamento_Dom);
            jan_textBox.Text = verificaVazamento_Dom.mMensais.Rows[0][0].ToString();
            fev_textBox.Text = verificaVazamento_Dom.mMensais.Rows[0][1].ToString();
            mar_textBox.Text = verificaVazamento_Dom.mMensais.Rows[0][2].ToString();
            abr_textBox.Text = verificaVazamento_Dom.mMensais.Rows[0][3].ToString();
            mai_textBox.Text = verificaVazamento_Dom.mMensais.Rows[0][4].ToString();
            jun_textBox.Text = verificaVazamento_Dom.mMensais.Rows[0][5].ToString();
            jul_textBox.Text = verificaVazamento_Dom.mMensais.Rows[0][6].ToString();
            ago_textBox.Text = verificaVazamento_Dom.mMensais.Rows[0][7].ToString();
            set_textBox.Text = verificaVazamento_Dom.mMensais.Rows[0][8].ToString();
            out_textBox.Text = verificaVazamento_Dom.mMensais.Rows[0][9].ToString();
            nov_textBox.Text = verificaVazamento_Dom.mMensais.Rows[0][10].ToString();
            dez_textBox.Text = verificaVazamento_Dom.mMensais.Rows[0][11].ToString();
            verificaVazamento_Dom.consumoMaximo = verificaVazamento_Apl.VerConsumoMaximo(verificaVazamento_Dom);
            consumoMaxFaturado_textBox.Text = verificaVazamento_Dom.consumoMaximo.ToString();
            verificaVazamento_Dom.consumoMinimo = verificaVazamento_Apl.VerConsumoMinimo(verificaVazamento_Dom);
            consumoMinFaturado_textBox.Text = verificaVazamento_Dom.consumoMinimo.ToString();
            verificaVazamento_Dom.mediaGeral = verificaVazamento_Apl.VerMediaGeral(verificaVazamento_Dom);
            mediaGeral_textBox.Text = verificaVazamento_Dom.mediaGeral.ToString();
            verificaVazamento_Dom.desvioPadraoGeral = verificaVazamento_Apl.VerDesvioPadrao(verificaVazamento_Dom);
            desvioPadrao_textBox.Text = verificaVazamento_Dom.desvioPadraoGeral.ToString();
        }

        private void FormVerificaVazamento_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
