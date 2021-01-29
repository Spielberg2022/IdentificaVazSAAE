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
            jan_textBox.Text = verificaVazamento_Apl.MediaJan(verificaVazamento_Dom);
            fev_textBox.Text = verificaVazamento_Apl.MediaFev(verificaVazamento_Dom);
            mar_textBox.Text = verificaVazamento_Apl.MediaMar(verificaVazamento_Dom);
            abr_textBox.Text = verificaVazamento_Apl.MediaAbr(verificaVazamento_Dom);
            mai_textBox.Text = verificaVazamento_Apl.MediaMai(verificaVazamento_Dom);
            jun_textBox.Text = verificaVazamento_Apl.MediaJun(verificaVazamento_Dom);
            jul_textBox.Text = verificaVazamento_Apl.MediaJul(verificaVazamento_Dom);
            ago_textBox.Text = verificaVazamento_Apl.MediaAgo(verificaVazamento_Dom);
            set_textBox.Text = verificaVazamento_Apl.MediaSet(verificaVazamento_Dom);
            out_textBox.Text = verificaVazamento_Apl.MediaOut(verificaVazamento_Dom);
            nov_textBox.Text = verificaVazamento_Apl.MediaNov(verificaVazamento_Dom);
            dez_textBox.Text = verificaVazamento_Apl.MediaDez(verificaVazamento_Dom);
        }

        private void FormVerificaVazamento_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
