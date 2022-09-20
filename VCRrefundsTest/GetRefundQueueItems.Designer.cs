namespace VCRrefunds
{
	partial class GetRefundQueueItems
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
			if(disposing && (components != null))
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
			this.txtDisplay = new System.Windows.Forms.RichTextBox();
			this.cmdGetRecords = new System.Windows.Forms.Button();
			this.txtSabreId = new System.Windows.Forms.TextBox();
			this.txtSabrePswd = new System.Windows.Forms.TextBox();
			this.txtQueueName = new System.Windows.Forms.TextBox();
			this.lblSabreId = new System.Windows.Forms.Label();
			this.lblSabrePswd = new System.Windows.Forms.Label();
			this.lblQueueName = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblFileName = new System.Windows.Forms.Label();
			this.txtFileName = new System.Windows.Forms.TextBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtDisplay
			// 
			this.txtDisplay.Location = new System.Drawing.Point(12, 12);
			this.txtDisplay.Name = "txtDisplay";
			this.txtDisplay.Size = new System.Drawing.Size(363, 555);
			this.txtDisplay.TabIndex = 0;
			this.txtDisplay.Text = "";
			// 
			// cmdGetRecords
			// 
			this.cmdGetRecords.Location = new System.Drawing.Point(421, 214);
			this.cmdGetRecords.Name = "cmdGetRecords";
			this.cmdGetRecords.Size = new System.Drawing.Size(121, 39);
			this.cmdGetRecords.TabIndex = 1;
			this.cmdGetRecords.Text = "Get Queue Records";
			this.cmdGetRecords.UseVisualStyleBackColor = true;
			this.cmdGetRecords.Click += new System.EventHandler(this.cmdGetRecords_Click);
			// 
			// txtSabreId
			// 
			this.txtSabreId.Location = new System.Drawing.Point(9, 26);
			this.txtSabreId.Name = "txtSabreId";
			this.txtSabreId.Size = new System.Drawing.Size(116, 20);
			this.txtSabreId.TabIndex = 2;
			// 
			// txtSabrePswd
			// 
			this.txtSabrePswd.Location = new System.Drawing.Point(9, 69);
			this.txtSabrePswd.Name = "txtSabrePswd";
			this.txtSabrePswd.PasswordChar = '*';
			this.txtSabrePswd.Size = new System.Drawing.Size(116, 20);
			this.txtSabrePswd.TabIndex = 3;
			// 
			// txtQueueName
			// 
			this.txtQueueName.Location = new System.Drawing.Point(9, 112);
			this.txtQueueName.Name = "txtQueueName";
			this.txtQueueName.Size = new System.Drawing.Size(116, 20);
			this.txtQueueName.TabIndex = 4;
			this.txtQueueName.Text = "ETTS";
			// 
			// lblSabreId
			// 
			this.lblSabreId.AutoSize = true;
			this.lblSabreId.Location = new System.Drawing.Point(9, 8);
			this.lblSabreId.Name = "lblSabreId";
			this.lblSabreId.Size = new System.Drawing.Size(47, 13);
			this.lblSabreId.TabIndex = 5;
			this.lblSabreId.Text = "Sabre Id";
			// 
			// lblSabrePswd
			// 
			this.lblSabrePswd.AutoSize = true;
			this.lblSabrePswd.Location = new System.Drawing.Point(9, 51);
			this.lblSabrePswd.Name = "lblSabrePswd";
			this.lblSabrePswd.Size = new System.Drawing.Size(64, 13);
			this.lblSabrePswd.TabIndex = 6;
			this.lblSabrePswd.Text = "Sabre Pswd";
			// 
			// lblQueueName
			// 
			this.lblQueueName.AutoSize = true;
			this.lblQueueName.Location = new System.Drawing.Point(9, 94);
			this.lblQueueName.Name = "lblQueueName";
			this.lblQueueName.Size = new System.Drawing.Size(70, 13);
			this.lblQueueName.TabIndex = 7;
			this.lblQueueName.Text = "Queue Name";
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.lblFileName);
			this.panel1.Controls.Add(this.txtFileName);
			this.panel1.Controls.Add(this.txtSabrePswd);
			this.panel1.Controls.Add(this.lblSabreId);
			this.panel1.Controls.Add(this.lblSabrePswd);
			this.panel1.Controls.Add(this.lblQueueName);
			this.panel1.Controls.Add(this.txtQueueName);
			this.panel1.Controls.Add(this.txtSabreId);
			this.panel1.Location = new System.Drawing.Point(389, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(193, 196);
			this.panel1.TabIndex = 8;
			// 
			// lblFileName
			// 
			this.lblFileName.AutoSize = true;
			this.lblFileName.Location = new System.Drawing.Point(9, 137);
			this.lblFileName.Name = "lblFileName";
			this.lblFileName.Size = new System.Drawing.Size(87, 13);
			this.lblFileName.TabIndex = 10;
			this.lblFileName.Text = "Result File Name";
			// 
			// txtFileName
			// 
			this.txtFileName.Location = new System.Drawing.Point(9, 155);
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.Size = new System.Drawing.Size(116, 20);
			this.txtFileName.TabIndex = 9;
			// 
			// GetRefundQueueItems
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(605, 579);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.cmdGetRecords);
			this.Controls.Add(this.txtDisplay);
			this.Name = "GetRefundQueueItems";
			this.Text = "GetRefundQueueItems";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox txtDisplay;
		private System.Windows.Forms.Button cmdGetRecords;
		private System.Windows.Forms.TextBox txtSabreId;
		private System.Windows.Forms.TextBox txtSabrePswd;
		private System.Windows.Forms.TextBox txtQueueName;
		private System.Windows.Forms.Label lblSabreId;
		private System.Windows.Forms.Label lblSabrePswd;
		private System.Windows.Forms.Label lblQueueName;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblFileName;
		private System.Windows.Forms.TextBox txtFileName;
	}
}