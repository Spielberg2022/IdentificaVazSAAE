using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using IdentificaVazSAAE.Domínio;
using IdentificaVazSAAE.Aplicação;
using IdentificaVazSAAE.Persistência;

namespace IdentificaVazSAAE.Apresentação
{
    public partial class FormConfigBD : Form
    {
        DialogResult resultado;
        ClassVerificaVazamentoAp verificaVaz = new ClassVerificaVazamentoAp();
        ClassBD config = new ClassBD();
        ClassConfiguracaoBD bd = new ClassConfiguracaoBD();
        BindingSource relacionamento = new BindingSource();
        char[] pesquisa = { '\\' };

        public FormConfigBD()
        {
            InitializeComponent();
        }
    }
}
