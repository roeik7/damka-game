namespace B18_Ex05_WinForm
{
    public partial class GameSettingsForm
    {
        private System.Windows.Forms.Label labelBoardSize;
        private System.Windows.Forms.RadioButton radioButtonSix;
        private System.Windows.Forms.RadioButton radioButtonEight;
        private System.Windows.Forms.RadioButton radioButtonTen;
        private System.Windows.Forms.Label labelPlayer;
        private System.Windows.Forms.Label labelPlayerOne;
        private System.Windows.Forms.TextBox textBoxplayer1;
        private System.Windows.Forms.Label labelPlayer2;
        private System.Windows.Forms.TextBox textBoxPlayer2;
        private System.Windows.Forms.CheckBox checkBoxPlayer2;
        private System.Windows.Forms.Button buttonDone;

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
            this.labelBoardSize = new System.Windows.Forms.Label();
            this.radioButtonSix = new System.Windows.Forms.RadioButton();
            this.radioButtonEight = new System.Windows.Forms.RadioButton();
            this.radioButtonTen = new System.Windows.Forms.RadioButton();
            this.labelPlayer = new System.Windows.Forms.Label();
            this.labelPlayerOne = new System.Windows.Forms.Label();
            this.textBoxplayer1 = new System.Windows.Forms.TextBox();
            this.labelPlayer2 = new System.Windows.Forms.Label();
            this.textBoxPlayer2 = new System.Windows.Forms.TextBox();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.buttonDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelBoardSize
            // 
            this.labelBoardSize.AutoSize = true;
            this.labelBoardSize.Location = new System.Drawing.Point(27, 23);
            this.labelBoardSize.Name = "labelBoardSize";
            this.labelBoardSize.Size = new System.Drawing.Size(137, 29);
            this.labelBoardSize.TabIndex = 0;
            this.labelBoardSize.Text = "Board Size:";
            // 
            // radioButtonSix
            // 
            this.radioButtonSix.AutoSize = true;
            this.radioButtonSix.Location = new System.Drawing.Point(68, 69);
            this.radioButtonSix.Name = "radioButtonSix";
            this.radioButtonSix.Size = new System.Drawing.Size(81, 33);
            this.radioButtonSix.TabIndex = 1;
            this.radioButtonSix.TabStop = true;
            this.radioButtonSix.Text = "6x6";
            this.radioButtonSix.UseVisualStyleBackColor = true;
            // 
            // radioButtonEight
            // 
            this.radioButtonEight.AutoSize = true;
            this.radioButtonEight.Location = new System.Drawing.Point(202, 69);
            this.radioButtonEight.Name = "radioButtonEight";
            this.radioButtonEight.Size = new System.Drawing.Size(81, 33);
            this.radioButtonEight.TabIndex = 2;
            this.radioButtonEight.TabStop = true;
            this.radioButtonEight.Text = "8x8";
            this.radioButtonEight.UseVisualStyleBackColor = true;
            // 
            // radioButtonTen
            // 
            this.radioButtonTen.AutoSize = true;
            this.radioButtonTen.Location = new System.Drawing.Point(326, 69);
            this.radioButtonTen.Name = "radioButtonTen";
            this.radioButtonTen.Size = new System.Drawing.Size(107, 33);
            this.radioButtonTen.TabIndex = 3;
            this.radioButtonTen.TabStop = true;
            this.radioButtonTen.Text = "10x10";
            this.radioButtonTen.UseVisualStyleBackColor = true;
            // 
            // labelPlayer
            // 
            this.labelPlayer.AutoSize = true;
            this.labelPlayer.Location = new System.Drawing.Point(27, 151);
            this.labelPlayer.Name = "labelPlayer";
            this.labelPlayer.Size = new System.Drawing.Size(99, 29);
            this.labelPlayer.TabIndex = 4;
            this.labelPlayer.Text = "Players:";
            // 
            // labelPlayerOne
            // 
            this.labelPlayerOne.AutoSize = true;
            this.labelPlayerOne.Location = new System.Drawing.Point(27, 221);
            this.labelPlayerOne.Name = "labelPlayerOne";
            this.labelPlayerOne.Size = new System.Drawing.Size(106, 29);
            this.labelPlayerOne.TabIndex = 5;
            this.labelPlayerOne.Text = "Player 1:";
            // 
            // textBoxplayer1
            // 
            this.textBoxplayer1.Location = new System.Drawing.Point(221, 221);
            this.textBoxplayer1.Name = "textBoxplayer1";
            this.textBoxplayer1.Size = new System.Drawing.Size(212, 35);
            this.textBoxplayer1.TabIndex = 6;
            // 
            // labelPlayer2
            // 
            this.labelPlayer2.AutoSize = true;
            this.labelPlayer2.Location = new System.Drawing.Point(58, 292);
            this.labelPlayer2.Name = "labelPlayer2";
            this.labelPlayer2.Size = new System.Drawing.Size(106, 29);
            this.labelPlayer2.TabIndex = 7;
            this.labelPlayer2.Text = "Player 2:";
            // 
            // textBoxPlayer2
            // 
            this.textBoxPlayer2.Enabled = false;
            this.textBoxPlayer2.Location = new System.Drawing.Point(221, 292);
            this.textBoxPlayer2.Name = "textBoxPlayer2";
            this.textBoxPlayer2.Size = new System.Drawing.Size(212, 35);
            this.textBoxPlayer2.TabIndex = 8;
            this.textBoxPlayer2.Text = "[Computer]";
            // 
            // checkBoxPlayer2
            // 
            this.checkBoxPlayer2.AutoSize = true;
            this.checkBoxPlayer2.Location = new System.Drawing.Point(24, 292);
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.Size = new System.Drawing.Size(28, 27);
            this.checkBoxPlayer2.TabIndex = 9;
            this.checkBoxPlayer2.UseVisualStyleBackColor = true;
            this.checkBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBoxPlayer2_CheckedChanged);
            // 
            // buttonDone
            // 
            this.buttonDone.Location = new System.Drawing.Point(362, 376);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(225, 49);
            this.buttonDone.TabIndex = 10;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // GameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 448);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.textBoxPlayer2);
            this.Controls.Add(this.labelPlayer2);
            this.Controls.Add(this.textBoxplayer1);
            this.Controls.Add(this.labelPlayerOne);
            this.Controls.Add(this.labelPlayer);
            this.Controls.Add(this.radioButtonTen);
            this.Controls.Add(this.radioButtonEight);
            this.Controls.Add(this.radioButtonSix);
            this.Controls.Add(this.labelBoardSize);
            this.Name = "GameSettings";
            this.ShowIcon = false;
            this.Text = "GameSettings";
            this.Load += new System.EventHandler(this.GameSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}