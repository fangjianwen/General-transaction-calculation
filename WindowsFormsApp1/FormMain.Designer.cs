namespace WindowsFormsAppFruitCalc
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.grvDetail = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaoZhong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddDetail = new System.Windows.Forms.Button();
            this.btnCalc = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSinglePackAgeWeight = new System.Windows.Forms.TextBox();
            this.txtPackAgeCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOrderName = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblTotalMaoWeight = new System.Windows.Forms.Label();
            this.lblTotalPackAgeWeight = new System.Windows.Forms.Label();
            this.lblTotalPackAgeCount = new System.Windows.Forms.Label();
            this.lblTotalRealWeight = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cbOrderNameList = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblTotalAmountSql = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.批量录入称重记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.grvDetail)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grvDetail
            // 
            this.grvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Column9,
            this.Num,
            this.Column1,
            this.Column2,
            this.MaoZhong,
            this.Column6,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column8});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.OrangeRed;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grvDetail.DefaultCellStyle = dataGridViewCellStyle1;
            this.grvDetail.Location = new System.Drawing.Point(-4, 105);
            this.grvDetail.MultiSelect = false;
            this.grvDetail.Name = "grvDetail";
            this.grvDetail.ReadOnly = true;
            this.grvDetail.RowTemplate.Height = 23;
            this.grvDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvDetail.Size = new System.Drawing.Size(1180, 530);
            this.grvDetail.TabIndex = 0;
            this.grvDetail.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvDetail_CellDoubleClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "编号";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "OrderName";
            this.Column9.HeaderText = "订单名称";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Num
            // 
            this.Num.DataPropertyName = "Num";
            this.Num.HeaderText = "序号";
            this.Num.Name = "Num";
            this.Num.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "SinglePackAgeWeight";
            this.Column1.HeaderText = "单个包装重量";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "PackAgeCount";
            this.Column2.HeaderText = "包装数量";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // MaoZhong
            // 
            this.MaoZhong.DataPropertyName = "MaoWeight";
            this.MaoZhong.HeaderText = "毛重";
            this.MaoZhong.Name = "MaoZhong";
            this.MaoZhong.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "TotalPackAgeWeight";
            this.Column6.HeaderText = "皮重";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "RealWeight";
            this.Column3.HeaderText = "净重";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "Price";
            this.Column4.HeaderText = "单价";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Amount";
            this.Column5.HeaderText = "金额小计(元)";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 120;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "Remark";
            this.Column8.HeaderText = "备注";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 150;
            // 
            // btnAddDetail
            // 
            this.btnAddDetail.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddDetail.Location = new System.Drawing.Point(568, 21);
            this.btnAddDetail.Name = "btnAddDetail";
            this.btnAddDetail.Size = new System.Drawing.Size(403, 78);
            this.btnAddDetail.TabIndex = 1;
            this.btnAddDetail.Text = "增加单次称重记录";
            this.btnAddDetail.UseVisualStyleBackColor = true;
            this.btnAddDetail.Click += new System.EventHandler(this.btnAddDetail_Click);
            // 
            // btnCalc
            // 
            this.btnCalc.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCalc.Location = new System.Drawing.Point(1007, 49);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(174, 50);
            this.btnCalc.TabIndex = 2;
            this.btnCalc.Text = "计算";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "单个包装重量:";
            // 
            // txtSinglePackAgeWeight
            // 
            this.txtSinglePackAgeWeight.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSinglePackAgeWeight.ForeColor = System.Drawing.Color.Red;
            this.txtSinglePackAgeWeight.Location = new System.Drawing.Point(128, 23);
            this.txtSinglePackAgeWeight.Name = "txtSinglePackAgeWeight";
            this.txtSinglePackAgeWeight.Size = new System.Drawing.Size(100, 29);
            this.txtSinglePackAgeWeight.TabIndex = 4;
            // 
            // txtPackAgeCount
            // 
            this.txtPackAgeCount.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPackAgeCount.ForeColor = System.Drawing.Color.Red;
            this.txtPackAgeCount.Location = new System.Drawing.Point(161, 70);
            this.txtPackAgeCount.Name = "txtPackAgeCount";
            this.txtPackAgeCount.Size = new System.Drawing.Size(100, 29);
            this.txtPackAgeCount.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(6, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "每次称重包装数量:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(235, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 14);
            this.label3.TabIndex = 7;
            this.label3.Text = "订单名称:";
            // 
            // txtOrderName
            // 
            this.txtOrderName.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOrderName.Location = new System.Drawing.Point(313, 21);
            this.txtOrderName.Name = "txtOrderName";
            this.txtOrderName.Size = new System.Drawing.Size(238, 29);
            this.txtOrderName.TabIndex = 8;
            // 
            // txtPrice
            // 
            this.txtPrice.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPrice.ForeColor = System.Drawing.Color.Red;
            this.txtPrice.Location = new System.Drawing.Point(366, 70);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(185, 29);
            this.txtPrice.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(310, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "单价:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(13, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "总毛重:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(424, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 19);
            this.label6.TabIndex = 12;
            this.label6.Text = "总皮重:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(10, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 19);
            this.label7.TabIndex = 13;
            this.label7.Text = "总净重:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(297, 85);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(182, 19);
            this.label9.TabIndex = 15;
            this.label9.Text = "总金额(小计相加):";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalAmount.ForeColor = System.Drawing.Color.Red;
            this.lblTotalAmount.Location = new System.Drawing.Point(485, 75);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(31, 33);
            this.lblTotalAmount.TabIndex = 16;
            this.lblTotalAmount.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(720, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 19);
            this.label8.TabIndex = 18;
            this.label8.Text = "总包装数量:";
            // 
            // lblTotalMaoWeight
            // 
            this.lblTotalMaoWeight.AutoSize = true;
            this.lblTotalMaoWeight.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalMaoWeight.Location = new System.Drawing.Point(82, 10);
            this.lblTotalMaoWeight.Name = "lblTotalMaoWeight";
            this.lblTotalMaoWeight.Size = new System.Drawing.Size(31, 33);
            this.lblTotalMaoWeight.TabIndex = 19;
            this.lblTotalMaoWeight.Text = "0";
            // 
            // lblTotalPackAgeWeight
            // 
            this.lblTotalPackAgeWeight.AutoSize = true;
            this.lblTotalPackAgeWeight.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalPackAgeWeight.Location = new System.Drawing.Point(495, 8);
            this.lblTotalPackAgeWeight.Name = "lblTotalPackAgeWeight";
            this.lblTotalPackAgeWeight.Size = new System.Drawing.Size(31, 33);
            this.lblTotalPackAgeWeight.TabIndex = 20;
            this.lblTotalPackAgeWeight.Text = "0";
            // 
            // lblTotalPackAgeCount
            // 
            this.lblTotalPackAgeCount.AutoSize = true;
            this.lblTotalPackAgeCount.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalPackAgeCount.Location = new System.Drawing.Point(827, 10);
            this.lblTotalPackAgeCount.Name = "lblTotalPackAgeCount";
            this.lblTotalPackAgeCount.Size = new System.Drawing.Size(31, 33);
            this.lblTotalPackAgeCount.TabIndex = 21;
            this.lblTotalPackAgeCount.Text = "0";
            // 
            // lblTotalRealWeight
            // 
            this.lblTotalRealWeight.AutoSize = true;
            this.lblTotalRealWeight.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalRealWeight.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalRealWeight.Location = new System.Drawing.Point(85, 75);
            this.lblTotalRealWeight.Name = "lblTotalRealWeight";
            this.lblTotalRealWeight.Size = new System.Drawing.Size(31, 33);
            this.lblTotalRealWeight.TabIndex = 22;
            this.lblTotalRealWeight.Text = "0";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCount.Location = new System.Drawing.Point(1005, 8);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(31, 33);
            this.lblCount.TabIndex = 24;
            this.lblCount.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(953, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 19);
            this.label11.TabIndex = 23;
            this.label11.Text = "头数:";
            // 
            // cbOrderNameList
            // 
            this.cbOrderNameList.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbOrderNameList.FormattingEnabled = true;
            this.cbOrderNameList.Location = new System.Drawing.Point(1007, 19);
            this.cbOrderNameList.Name = "cbOrderNameList";
            this.cbOrderNameList.Size = new System.Drawing.Size(174, 24);
            this.cbOrderNameList.TabIndex = 25;
            this.cbOrderNameList.SelectedValueChanged += new System.EventHandler(this.cbOrderNameList_SelectedValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lblTotalAmountSql);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblCount);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblTotalRealWeight);
            this.groupBox1.Controls.Add(this.lblTotalAmount);
            this.groupBox1.Controls.Add(this.lblTotalPackAgeCount);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblTotalPackAgeWeight);
            this.groupBox1.Controls.Add(this.lblTotalMaoWeight);
            this.groupBox1.Location = new System.Drawing.Point(-4, 641);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1188, 130);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(729, 83);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(193, 19);
            this.label10.TabIndex = 25;
            this.label10.Text = "总金额(净重*单价):";
            // 
            // lblTotalAmountSql
            // 
            this.lblTotalAmountSql.AutoSize = true;
            this.lblTotalAmountSql.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalAmountSql.ForeColor = System.Drawing.Color.Red;
            this.lblTotalAmountSql.Location = new System.Drawing.Point(919, 73);
            this.lblTotalAmountSql.Name = "lblTotalAmountSql";
            this.lblTotalAmountSql.Size = new System.Drawing.Size(31, 33);
            this.lblTotalAmountSql.TabIndex = 26;
            this.lblTotalAmountSql.Text = "0";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1188, 25);
            this.menuStrip1.TabIndex = 28;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Edit,
            this.删除ToolStripMenuItem,
            this.批量录入称重记录ToolStripMenuItem,
            this.关于ToolStripMenuItem});
            this.ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem.Image")));
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            this.ToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.ToolStripMenuItem.Text = "菜单";
            // 
            // Menu_Edit
            // 
            this.Menu_Edit.Image = ((System.Drawing.Image)(resources.GetObject("Menu_Edit.Image")));
            this.Menu_Edit.Name = "Menu_Edit";
            this.Menu_Edit.Size = new System.Drawing.Size(172, 22);
            this.Menu_Edit.Text = "修改";
            this.Menu_Edit.Click += new System.EventHandler(this.Menu_Edit_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // 批量录入称重记录ToolStripMenuItem
            // 
            this.批量录入称重记录ToolStripMenuItem.Name = "批量录入称重记录ToolStripMenuItem";
            this.批量录入称重记录ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.批量录入称重记录ToolStripMenuItem.Text = "批量录入称重记录";
            this.批量录入称重记录ToolStripMenuItem.Click += new System.EventHandler(this.批量录入称重记录ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 774);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbOrderNameList);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtOrderName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPackAgeCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSinglePackAgeWeight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.btnAddDetail);
            this.Controls.Add(this.grvDetail);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "通用交易计算";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grvDetail)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grvDetail;
        private System.Windows.Forms.Button btnAddDetail;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSinglePackAgeWeight;
        private System.Windows.Forms.TextBox txtPackAgeCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOrderName;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTotalMaoWeight;
        private System.Windows.Forms.Label lblTotalPackAgeWeight;
        private System.Windows.Forms.Label lblTotalPackAgeCount;
        private System.Windows.Forms.Label lblTotalRealWeight;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbOrderNameList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblTotalAmountSql;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Menu_Edit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Num;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaoZhong;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 批量录入称重记录ToolStripMenuItem;
    }
}

