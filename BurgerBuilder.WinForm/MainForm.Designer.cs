
namespace BurgerBuilder.WinForm
{
    partial class MainForm
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
            this.buttonMenuI = new System.Windows.Forms.Button();
            this.buttonMenuII = new System.Windows.Forms.Button();
            this.buttonSalad = new System.Windows.Forms.Button();
            this.buttonMeat = new System.Windows.Forms.Button();
            this.buttonTomato = new System.Windows.Forms.Button();
            this.buttonCheese = new System.Windows.Forms.Button();
            this.buttonSkip = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonMenuI
            // 
            this.buttonMenuI.Location = new System.Drawing.Point(46, 30);
            this.buttonMenuI.Name = "buttonMenuI";
            this.buttonMenuI.Size = new System.Drawing.Size(175, 74);
            this.buttonMenuI.TabIndex = 0;
            this.buttonMenuI.Text = "Menu I";
            this.buttonMenuI.UseVisualStyleBackColor = true;
            // 
            // buttonMenuII
            // 
            this.buttonMenuII.Location = new System.Drawing.Point(46, 116);
            this.buttonMenuII.Name = "buttonMenuII";
            this.buttonMenuII.Size = new System.Drawing.Size(175, 74);
            this.buttonMenuII.TabIndex = 1;
            this.buttonMenuII.Text = "Menu II";
            this.buttonMenuII.UseVisualStyleBackColor = true;
            // 
            // buttonSalad
            // 
            this.buttonSalad.Location = new System.Drawing.Point(269, 30);
            this.buttonSalad.Name = "buttonSalad";
            this.buttonSalad.Size = new System.Drawing.Size(175, 74);
            this.buttonSalad.TabIndex = 2;
            this.buttonSalad.Text = "Xà lách";
            this.buttonSalad.UseVisualStyleBackColor = true;
            // 
            // buttonMeat
            // 
            this.buttonMeat.Location = new System.Drawing.Point(269, 116);
            this.buttonMeat.Name = "buttonMeat";
            this.buttonMeat.Size = new System.Drawing.Size(175, 74);
            this.buttonMeat.TabIndex = 3;
            this.buttonMeat.Text = "Thịt";
            this.buttonMeat.UseVisualStyleBackColor = true;
            // 
            // buttonTomato
            // 
            this.buttonTomato.Location = new System.Drawing.Point(488, 116);
            this.buttonTomato.Name = "buttonTomato";
            this.buttonTomato.Size = new System.Drawing.Size(175, 74);
            this.buttonTomato.TabIndex = 4;
            this.buttonTomato.Text = "Cà chua";
            this.buttonTomato.UseVisualStyleBackColor = true;
            // 
            // buttonCheese
            // 
            this.buttonCheese.Location = new System.Drawing.Point(488, 30);
            this.buttonCheese.Name = "buttonCheese";
            this.buttonCheese.Size = new System.Drawing.Size(175, 74);
            this.buttonCheese.TabIndex = 5;
            this.buttonCheese.Text = "Phô mai";
            this.buttonCheese.UseVisualStyleBackColor = true;
            // 
            // buttonSkip
            // 
            this.buttonSkip.Location = new System.Drawing.Point(46, 220);
            this.buttonSkip.Name = "buttonSkip";
            this.buttonSkip.Size = new System.Drawing.Size(617, 41);
            this.buttonSkip.TabIndex = 6;
            this.buttonSkip.Text = "Bỏ qua";
            this.buttonSkip.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 298);
            this.Controls.Add(this.buttonSkip);
            this.Controls.Add(this.buttonCheese);
            this.Controls.Add(this.buttonTomato);
            this.Controls.Add(this.buttonMeat);
            this.Controls.Add(this.buttonSalad);
            this.Controls.Add(this.buttonMenuII);
            this.Controls.Add(this.buttonMenuI);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Burger Builder";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonMenuI;
        private System.Windows.Forms.Button buttonMenuII;
        private System.Windows.Forms.Button buttonSalad;
        private System.Windows.Forms.Button buttonMeat;
        private System.Windows.Forms.Button buttonTomato;
        private System.Windows.Forms.Button buttonCheese;
        private System.Windows.Forms.Button buttonSkip;
    }
}

