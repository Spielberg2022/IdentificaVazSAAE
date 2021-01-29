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
        ClassVerificaVazamento_Apl verificaVazamento_Apl = new ClassVerificaVazamento_Apl();
        ClassVerificaVazamento_Per verificaVazamento_Per = new ClassVerificaVazamento_Per();
        ClassVerificaVazamento_Dom verificaVazamento_Dom = new ClassVerificaVazamento_Dom();

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
        }

        private void FormVerificaVazamento_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
