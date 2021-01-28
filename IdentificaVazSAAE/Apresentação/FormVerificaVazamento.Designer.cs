
namespace IdentificaVazSAAE.Apresentação
{
    partial class FormVerificaVazamento
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.codLigacao_label = new System.Windows.Forms.Label();
            this.codLigacao_textBox = new System.Windows.Forms.TextBox();
            this.localizar_button = new System.Windows.Forms.Button();
            this.leituras_tabControl = new System.Windows.Forms.TabControl();
            this.leituras_tabPage = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.leituras_dataGridView = new System.Windows.Forms.DataGridView();
            this.leituras_tabControl.SuspendLayout();
            this.leituras_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leituras_dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // codLigacao_label
            // 
            this.codLigacao_label.AutoSize = true;
            this.codLigacao_label.Location = new System.Drawing.Point(12, 19);
            this.codLigacao_label.Name = "codLigacao_label";
            this.codLigacao_label.Size = new System.Drawing.Size(99, 13);
            this.codLigacao_label.TabIndex = 0;
            this.codLigacao_label.Text = "Código de Ligação:";
            // 
            // codLigacao_textBox
            // 
            this.codLigacao_textBox.Location = new System.Drawing.Point(117, 16);
            this.codLigacao_textBox.Name = "codLigacao_textBox";
            this.codLigacao_textBox.Size = new System.Drawing.Size(100, 20);
            this.codLigacao_textBox.TabIndex = 1;
            // 
            // localizar_button
            // 
            this.localizar_button.Location = new System.Drawing.Point(223, 14);
            this.localizar_button.Name = "localizar_button";
            this.localizar_button.Size = new System.Drawing.Size(75, 23);
            this.localizar_button.TabIndex = 2;
            this.localizar_button.Text = "&Localizar";
            this.localizar_button.UseVisualStyleBackColor = true;
            this.localizar_button.Click += new System.EventHandler(this.localizar_button_Click);
            // 
            // leituras_tabControl
            // 
            this.leituras_tabControl.Controls.Add(this.leituras_tabPage);
            this.leituras_tabControl.Location = new System.Drawing.Point(12, 42);
            this.leituras_tabControl.Name = "leituras_tabControl";
            this.leituras_tabControl.SelectedIndex = 0;
            this.leituras_tabControl.Size = new System.Drawing.Size(673, 281);
            this.leituras_tabControl.TabIndex = 3;
            // 
            // leituras_tabPage
            // 
            this.leituras_tabPage.Controls.Add(this.leituras_dataGridView);
            this.leituras_tabPage.Controls.Add(this.label1);
            this.leituras_tabPage.Location = new System.Drawing.Point(4, 22);
            this.leituras_tabPage.Name = "leituras_tabPage";
            this.leituras_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.leituras_tabPage.Size = new System.Drawing.Size(665, 255);
            this.leituras_tabPage.TabIndex = 0;
            this.leituras_tabPage.Text = "Leituras";
            this.leituras_tabPage.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Leituras dos últimos 3 anos:";
            // 
            // leituras_dataGridView
            // 
            this.leituras_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.leituras_dataGridView.Location = new System.Drawing.Point(9, 38);
            this.leituras_dataGridView.Name = "leituras_dataGridView";
            this.leituras_dataGridView.Size = new System.Drawing.Size(650, 211);
            this.leituras_dataGridView.TabIndex = 1;
            // 
            // FormVerificaVazamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 335);
            this.Controls.Add(this.leituras_tabControl);
            this.Controls.Add(this.localizar_button);
            this.Controls.Add(this.codLigacao_textBox);
            this.Controls.Add(this.codLigacao_label);
            this.Name = "FormVerificaVazamento";
            this.Text = "Verifica Vazamento";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormVerificaVazamento_FormClosed);
            this.leituras_tabControl.ResumeLayout(false);
            this.leituras_tabPage.ResumeLayout(false);
            this.leituras_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leituras_dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label codLigacao_label;
        private System.Windows.Forms.TextBox codLigacao_textBox;
        private System.Windows.Forms.Button localizar_button;
        private System.Windows.Forms.TabControl leituras_tabControl;
        private System.Windows.Forms.TabPage leituras_tabPage;
        private System.Windows.Forms.DataGridView leituras_dataGridView;
        private System.Windows.Forms.Label label1;
    }
}