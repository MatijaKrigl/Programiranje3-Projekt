using System.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace ClovekNeJeziSe
{
    public partial class FormStart : Form
    {
        public bool ContinueGame { get; private set; }
        SoundPlayer soundPlayer;
        public FormStart()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            btnContinue = new Button();
            btnNewGame = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnContinue
            // 
            btnContinue.Location = new Point(49, 145);
            btnContinue.Margin = new Padding(4, 5, 4, 5);
            btnContinue.Name = "btnContinue";
            btnContinue.Size = new Size(100, 62);
            btnContinue.TabIndex = 0;
            btnContinue.Text = "Nadaljuj igro";
            btnContinue.UseVisualStyleBackColor = true;
            btnContinue.Click += btnContinue_Click;
            // 
            // btnNewGame
            // 
            btnNewGame.Location = new Point(227, 145);
            btnNewGame.Margin = new Padding(4, 5, 4, 5);
            btnNewGame.Name = "btnNewGame";
            btnNewGame.Size = new Size(100, 62);
            btnNewGame.TabIndex = 1;
            btnNewGame.Text = "Začni novo igro";
            btnNewGame.UseVisualStyleBackColor = true;
            btnNewGame.Click += btnNewGame_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label1.Location = new Point(121, 65);
            label1.Name = "label1";
            label1.Size = new Size(144, 31);
            label1.TabIndex = 2;
            label1.Text = "Pozdravljeni!";
            label1.Click += label1_Click;
            // 
            // FormStart
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(379, 302);
            Controls.Add(label1);
            Controls.Add(btnNewGame);
            Controls.Add(btnContinue);
            Margin = new Padding(4, 5, 4, 5);
            Name = "FormStart";
            Text = "Izbira igre";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Button btnContinue;
        private Label label1;
        private System.Windows.Forms.Button btnNewGame;

        private void btnContinue_Click(object sender, EventArgs e)
        {
            //soundPlayer = new SoundPlayer(Properties.Resources.game_start);
            ContinueGame = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            ContinueGame = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}