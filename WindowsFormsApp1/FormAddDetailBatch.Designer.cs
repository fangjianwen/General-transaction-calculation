namespace WindowsFormsAppFruitCalc
{
    partial class FormAddDetailBatch
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
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPackAgeCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSinglePackAgeWeight = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddDetail = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lblOrderName = new System.Windows.Forms.Label();
            this.rtbWeightList = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.lblTotalMaoWeight = new System.Windows.Forms.Label();
            this.lblM = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtPrice
            // 
            this.txtPrice.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPrice.ForeColor = System.Drawing.Color.Red;
            this.txtPrice.Location = new System.Drawing.Point(216, 48);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(248, 41);
            this.txtPrice.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(132, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 19);
            this.label4.TabIndex = 19;
            this.label4.Text = "单价:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(3, 251);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 19);
            this.label3.TabIndex = 17;
            this.label3.Text = "电子称重量记录:";
            // 
            // txtPackAgeCount
            // 
            this.txtPackAgeCount.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPackAgeCount.ForeColor = System.Drawing.Color.Red;
            this.txtPackAgeCount.Location = new System.Drawing.Point(216, 162);
            this.txtPackAgeCount.Name = "txtPackAgeCount";
            this.txtPackAgeCount.Size = new System.Drawing.Size(248, 41);
            this.txtPackAgeCount.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(12, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 19);
            this.label2.TabIndex = 15;
            this.label2.Text = "每次称重包装数量:";
            // 
            // txtSinglePackAgeWeight
            // 
            this.txtSinglePackAgeWeight.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSinglePackAgeWeight.ForeColor = System.Drawing.Color.Red;
            this.txtSinglePackAgeWeight.Location = new System.Drawing.Point(216, 106);
            this.txtSinglePackAgeWeight.Name = "txtSinglePackAgeWeight";
            this.txtSinglePackAgeWeight.Size = new System.Drawing.Size(248, 41);
            this.txtSinglePackAgeWeight.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(52, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 19);
            this.label1.TabIndex = 13;
            this.label1.Text = "单个包装重量:";
            // 
            // btnAddDetail
            // 
            this.btnAddDetail.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddDetail.Location = new System.Drawing.Point(16, 599);
            this.btnAddDetail.Name = "btnAddDetail";
            this.btnAddDetail.Size = new System.Drawing.Size(448, 57);
            this.btnAddDetail.TabIndex = 11;
            this.btnAddDetail.Text = "增加称重记录";
            this.btnAddDetail.UseVisualStyleBackColor = true;
            this.btnAddDetail.Click += new System.EventHandler(this.btnAddDetail_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(92, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 19);
            this.label6.TabIndex = 23;
            this.label6.Text = "订单名称:";
            // 
            // lblOrderName
            // 
            this.lblOrderName.AutoSize = true;
            this.lblOrderName.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOrderName.Location = new System.Drawing.Point(212, 21);
            this.lblOrderName.Name = "lblOrderName";
            this.lblOrderName.Size = new System.Drawing.Size(89, 19);
            this.lblOrderName.TabIndex = 24;
            this.lblOrderName.Text = "订单名称";
            // 
            // rtbWeightList
            // 
            this.rtbWeightList.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtbWeightList.Location = new System.Drawing.Point(216, 251);
            this.rtbWeightList.Name = "rtbWeightList";
            this.rtbWeightList.Size = new System.Drawing.Size(248, 321);
            this.rtbWeightList.TabIndex = 25;
            this.rtbWeightList.Text = "";
            this.rtbWeightList.TextChanged += new System.EventHandler(this.rtbWeightList_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(5, 295);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 19);
            this.label5.TabIndex = 26;
            this.label5.Text = "记录数:";
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalCount.ForeColor = System.Drawing.Color.Red;
            this.lblTotalCount.Location = new System.Drawing.Point(86, 286);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(34, 35);
            this.lblTotalCount.TabIndex = 27;
            this.lblTotalCount.Text = "0";
            // 
            // lblTotalMaoWeight
            // 
            this.lblTotalMaoWeight.AutoSize = true;
            this.lblTotalMaoWeight.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalMaoWeight.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalMaoWeight.Location = new System.Drawing.Point(92, 343);
            this.lblTotalMaoWeight.Name = "lblTotalMaoWeight";
            this.lblTotalMaoWeight.Size = new System.Drawing.Size(23, 24);
            this.lblTotalMaoWeight.TabIndex = 29;
            this.lblTotalMaoWeight.Text = "0";
            // 
            // lblM
            // 
            this.lblM.AutoSize = true;
            this.lblM.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblM.Location = new System.Drawing.Point(8, 346);
            this.lblM.Name = "lblM";
            this.lblM.Size = new System.Drawing.Size(80, 19);
            this.lblM.TabIndex = 28;
            this.lblM.Text = "总毛重:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(13, 416);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 14);
            this.label7.TabIndex = 30;
            this.label7.Text = "录入说明:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(93, 416);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 14);
            this.label8.TabIndex = 31;
            this.label8.Text = "输入一条记录后";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(12, 449);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(122, 14);
            this.label9.TabIndex = 32;
            this.label9.Text = "请按【Enter】键";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(13, 485);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(127, 14);
            this.label10.TabIndex = 33;
            this.label10.Text = "一行表示一条记录";
            // 
            // FormAddDetailBatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 671);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblTotalMaoWeight);
            this.Controls.Add(this.lblM);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rtbWeightList);
            this.Controls.Add(this.lblOrderName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPackAgeCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSinglePackAgeWeight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddDetail);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAddDetailBatch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批量增加称重记录";
            this.Activated += new System.EventHandler(this.FormAddDetail_Activated);
            this.Load += new System.EventHandler(this.FormAddDetail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPackAgeCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSinglePackAgeWeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddDetail;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblOrderName;
        private System.Windows.Forms.RichTextBox rtbWeightList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Label lblTotalMaoWeight;
        private System.Windows.Forms.Label lblM;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}