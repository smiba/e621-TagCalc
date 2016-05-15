namespace e621_Tag_Calc
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.UsernameTxtbox = new System.Windows.Forms.TextBox();
            this.StartBtn = new System.Windows.Forms.Button();
            this.CheckThreadStatus = new System.Windows.Forms.Timer(this.components);
            this.TagList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // UsernameTxtbox
            // 
            this.UsernameTxtbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UsernameTxtbox.Location = new System.Drawing.Point(74, 7);
            this.UsernameTxtbox.Name = "UsernameTxtbox";
            this.UsernameTxtbox.Size = new System.Drawing.Size(144, 20);
            this.UsernameTxtbox.TabIndex = 1;
            // 
            // StartBtn
            // 
            this.StartBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartBtn.Location = new System.Drawing.Point(226, 6);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(62, 23);
            this.StartBtn.TabIndex = 2;
            this.StartBtn.Text = "Go!";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // CheckThreadStatus
            // 
            this.CheckThreadStatus.Interval = 200;
            this.CheckThreadStatus.Tick += new System.EventHandler(this.CheckThreadStatus_Tick);
            // 
            // TagList
            // 
            this.TagList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TagList.FormattingEnabled = true;
            this.TagList.Location = new System.Drawing.Point(12, 33);
            this.TagList.Name = "TagList";
            this.TagList.Size = new System.Drawing.Size(276, 342);
            this.TagList.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 383);
            this.Controls.Add(this.TagList);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.UsernameTxtbox);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(450, 850);
            this.MinimumSize = new System.Drawing.Size(281, 275);
            this.Name = "Form1";
            this.Text = "e621 Favorite Tags";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UsernameTxtbox;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Timer CheckThreadStatus;
        private System.Windows.Forms.ListBox TagList;
    }
}

