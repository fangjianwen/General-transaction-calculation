using JW.Dal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Model;
using WindowsFormsAppFruitCalc;

namespace WindowsFormsAppFruitCalc
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 增加称重记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDetail_Click(object sender, EventArgs e)
        {

            string orderName = txtOrderName.Text;
            if (string.IsNullOrEmpty(orderName))
            {
                MessageBox.Show("请输入订单名称");
                txtOrderName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtSinglePackAgeWeight.Text))
            {
                MessageBox.Show("请输入单个包装重量");
                txtSinglePackAgeWeight.Focus();
                return;
            }

            decimal singlePackAgeWeight = 0;
            if (!decimal.TryParse(txtSinglePackAgeWeight.Text, out singlePackAgeWeight))
            {
                MessageBox.Show("单个包装重量不正确");
                txtSinglePackAgeWeight.Focus();
                return;
            }
            if (singlePackAgeWeight < 0)
            {
                MessageBox.Show("单个包装重量不能小于0");
                txtSinglePackAgeWeight.Focus();
                return;
            }


            if (Common.GetDecimalPlaces(singlePackAgeWeight) > 2)
            {
                MessageBox.Show("单个包装重量最多保留两位小数");
                txtSinglePackAgeWeight.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPackAgeCount.Text))
            {
                MessageBox.Show("请输入每次称重包装数量");
                txtPackAgeCount.Focus();
                return;
            }
            int packAgeCount = 0;
            if (!int.TryParse(txtPackAgeCount.Text, out packAgeCount))
            {
                MessageBox.Show("每次称重包装数量不正确");
                txtPackAgeCount.Focus();
                return;
            }
            if (packAgeCount < 0)
            {
                MessageBox.Show("每次称重包装数量不能小于0");
                txtPackAgeCount.Focus();
                return;
            }


            if (string.IsNullOrEmpty(txtPrice.Text))
            {
                MessageBox.Show("请输入单价");
                txtPrice.Focus();
                return;
            }
            decimal price = 0;
            if (!decimal.TryParse(txtPrice.Text, out price))
            {
                MessageBox.Show("单价不正确");
                txtPrice.Focus();
                return;
            }
            if (price <= 0)
            {
                MessageBox.Show("单价必须大于0");
                txtPrice.Focus();
                return;
            }
            if (Common.GetDecimalPlaces(price) > 2)
            {
                MessageBox.Show("单价最多保留两位小数");
                txtPrice.Focus();
                return;
            }

            FormAddDetail form = new FormAddDetail(orderName, singlePackAgeWeight, packAgeCount, price);
            form.Show();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            txtOrderName.Text = DateTime.Now.ToString("yyyy-MM-dd订单");
            BindComboxData();


            //BaseDal.ExecuteNonQuery("delete from OrderDetail where ordername='2024-04-10订单'", null);

            //List<OrderDetail> modelList = new List<OrderDetail>();


            //for (int i = 0; i < 105; i++)
            //{

            //    OrderDetail model = new OrderDetail();
            //    model.AddTime = DateTime.Now;
            //    model.OrderName = "2024-04-10订单";
            //    model.SinglePackAgeWeight = (decimal)4.2;
            //    model.PackAgeCount = 8;
            //    model.Price = (decimal)2.5;


            //    model.MaoWeight = 350;

            //    model.MaoWeight += new Random().Next(1, 10);




            //    modelList.Add(model);

            //    Thread.Sleep(50);

            //}

            //if (BaseDal.AddList(modelList))
            //{
            //    MessageBox.Show("添加测试数据成功");
            //}


        }
        /// <summary>
        ///已有订单选择
        /// </summary>
        public void BindComboxData()
        {

            DataTable dt = BaseDal.ExecuteDataTable("Select distinct(OrderName) from OrderDetail", null);
            DataRow row = dt.NewRow();
            row["OrderName"] = "查看已有订单";
            dt.Rows.InsertAt(row, 0);
            cbOrderNameList.DropDownStyle = ComboBoxStyle.DropDownList;
            cbOrderNameList.DataSource = dt;
            cbOrderNameList.DisplayMember = "OrderName";
            cbOrderNameList.ValueMember = "OrderName";

        }
        /// <summary>
        /// 计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnCalc_Click(object sender, EventArgs e)
        {
            bool result = CalcResult();
            if (result)
            {

                LoadData();
                MessageBox.Show("计算完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        /// <summary>
        /// 订单选择改变,加载对应数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbOrderNameList_SelectedValueChanged(object sender, EventArgs e)
        {

            string selectValue = cbOrderNameList.SelectedValue.ToString();
            if (selectValue != "查看已有订单" && selectValue != "System.Data.DataRowView")
            {
                txtOrderName.Text = selectValue;
                LoadData();
            }



        }
        /// <summary>
        /// 菜单 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                var rows = grvDetail.SelectedRows;
                if (rows.Count == 0)
                {
                    MessageBox.Show("请先选择一条记录");
                    return;
                }
                if (rows[0].Cells["Id"].Value == null)
                {
                    MessageBox.Show("请先选择一条记录");
                    return;
                }
                var id = rows[0].Cells["Id"].Value.ToString();
                if (!string.IsNullOrEmpty(id))
                {
                    var num = rows[0].Cells["Num"].Value.ToString();
                    FormEdit form = new FormEdit(long.Parse(id), int.Parse(num));
                    form.Show();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:" + ex.ToString());

            }
        }


        private void grvDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var rows = grvDetail.SelectedRows;
                if (rows.Count == 0)
                {
                    MessageBox.Show("请先选择一条记录");
                    return;
                }
                if (rows[0].Cells["Id"].Value == null)
                {
                    MessageBox.Show("请先选择一条记录");
                    return;
                }
                var id = rows[0].Cells["Id"].Value.ToString();
                if (!string.IsNullOrEmpty(id))
                {
                    var num = rows[0].Cells["Num"].Value.ToString();
                    FormEdit form = new FormEdit(long.Parse(id), int.Parse(num));
                    form.Show();
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:" + ex.ToString());

            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var rows = grvDetail.SelectedRows;
                if (rows.Count == 0)
                {
                    MessageBox.Show("请先选择一条记录");
                    return;
                }
                if (rows[0].Cells["Id"].Value == null)
                {
                    MessageBox.Show("请先选择一条记录");
                    return;
                }
                var id = rows[0].Cells["Id"].Value.ToString();

                
                if (!string.IsNullOrEmpty(id))
                {
                    var num = rows[0].Cells["Num"].Value.ToString();
                    if (MessageBox.Show("确认删除序号为 " + num + " 的称重记录?", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        OrderDetail model = BaseDal.GetModelById<OrderDetail>(long.Parse(id));
                        if (BaseDal.Delete(model))
                        {
                            MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {

                            MessageBox.Show("删除失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }








            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:" + ex.ToString());

            }
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout form = new FormAbout();
            form.Show();
        }
        /// <summary>
        /// 加载订单数据
        /// </summary>
        public void LoadData()
        {

            try
            {
                List<SqlParameter> parList = new List<SqlParameter>
                {
                    new SqlParameter("@OrderName", txtOrderName.Text)
                };
                List<OrderDetail> list = BaseDal.GetList<OrderDetail>(string.Format("OrderName=@OrderName Order By Id ASC"), parList);

                DataTable dt = BaseDal.ExecuteDataTable(string.Format("select ROW_NUMBER()  OVER (ORDER BY Id Asc) AS Num, *from OrderDetail where OrderName=@OrderName"), parList);
                dt.Columns.Remove("AddTime");
                grvDetail.DataSource = dt;
                if (list.Count == 0)
                {
                    lblTotalMaoWeight.Text = "0";
                    lblTotalPackAgeWeight.Text = "0";
                    lblTotalPackAgeCount.Text = "0";
                    lblCount.Text = "0";
                    lblTotalRealWeight.Text = "0";
                    lblTotalAmount.Text = "0";
                    lblTotalAmountSql.Text = "0";
                    return;
                }
                lblTotalMaoWeight.Text = list.Sum(p => p.MaoWeight).ToString();
                decimal totalMaoWeight = decimal.Parse(BaseDal.ExecuteScalar(string.Format("Select Sum(MaoWeight) From [OrderDetail] Where OrderName=@OrderName"), parList).ToString());
                if (decimal.Parse(lblTotalMaoWeight.Text) != totalMaoWeight)
                {
                    MessageBox.Show("【总毛重】 有偏差,请点击 计算 按钮", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                lblTotalPackAgeWeight.Text = list.Sum(p => p.TotalPackAgeWeight).ToString();
                decimal totalPackAgeWeight = decimal.Parse(BaseDal.ExecuteScalar(string.Format("Select Sum(SinglePackAgeWeight*PackAgeCount) From [OrderDetail] Where OrderName=@OrderName"), parList).ToString());
                if (decimal.Parse(lblTotalPackAgeWeight.Text) != totalPackAgeWeight)
                {
                    MessageBox.Show("【总皮重】 有偏差,请点击 计算 按钮", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                lblTotalPackAgeCount.Text = list.Sum(p => p.PackAgeCount).ToString();
                int totalPackAgeCount = int.Parse(BaseDal.ExecuteScalar(string.Format("Select Sum(PackAgeCount) From [OrderDetail] Where OrderName=@OrderName"), parList).ToString());
                if (int.Parse(lblTotalPackAgeCount.Text) != totalPackAgeCount)
                {
                    MessageBox.Show("【总包装数量】 有偏差,请点击 计算 按钮", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                lblCount.Text = list.Count.ToString();
                int totalCount = int.Parse(BaseDal.ExecuteScalar(string.Format("Select Count(1) From [OrderDetail] Where OrderName=@OrderName"), parList).ToString());
                if (int.Parse(lblCount.Text) != totalCount)
                {
                    MessageBox.Show("【头数】 有偏差,请点击 计算 按钮", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                lblTotalRealWeight.Text = list.Sum(p => p.RealWeight).ToString();
                decimal totalRealWeight = decimal.Parse(BaseDal.ExecuteScalar(string.Format("Select Sum(MaoWeight-(SinglePackAgeWeight*PackAgeCount)) From [OrderDetail] Where OrderName=@OrderName"), parList).ToString());
                if (decimal.Parse(lblTotalRealWeight.Text) != totalRealWeight)
                {
                    MessageBox.Show("【总净重】 有偏差,请点击 计算 按钮", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                lblTotalAmount.Text = Math.Round(list.Sum(p => p.Amount), 2, MidpointRounding.AwayFromZero).ToString();//四舍五入 保留两位小数                                                                                                                      
                decimal totalAmount = decimal.Parse(BaseDal.ExecuteScalar(string.Format("Select Sum((MaoWeight-(SinglePackAgeWeight*PackAgeCount))*Price) From [OrderDetail] Where OrderName=@OrderName"), parList).ToString());
                lblTotalAmountSql.Text = Math.Round(totalAmount, 2, MidpointRounding.AwayFromZero).ToString();//sql 计算的总金额  四舍五入 保留两位小数                
                if (lblTotalAmount.Text!= lblTotalAmountSql.Text)
                {
                    MessageBox.Show("【总金额】 有偏差,请点击 计算 按钮", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("加载数据异常:" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        /// <summary>
        /// 计算结果
        /// </summary>
        public bool CalcResult()
        {
            try
            {
                List<SqlParameter> parList = new List<SqlParameter>();
                parList.Add(new SqlParameter("@OrderName", txtOrderName.Text));
                List<OrderDetail> list = BaseDal.GetList<OrderDetail>(string.Format("OrderName=@OrderName Order By Id ASC"), parList);
                foreach (var model in list)
                {
                    model.TotalPackAgeWeight = Math.Round(model.SinglePackAgeWeight * model.PackAgeCount, 2, MidpointRounding.AwayFromZero);//四舍五入                  
                    model.RealWeight = Math.Round((model.MaoWeight - model.TotalPackAgeWeight), 2, MidpointRounding.AwayFromZero);//四舍五入
                    model.Amount = Math.Round(model.RealWeight * model.Price, 4, MidpointRounding.AwayFromZero);//四舍五入                   
                }
                if (list.Count == 0)
                {
                    MessageBox.Show("订单明细数量为0,不需要计算", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (BaseDal.UpdateList(list))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("计算失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("计算异常:" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;

        }

        private void 批量录入称重记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string orderName = txtOrderName.Text;
            if (string.IsNullOrEmpty(orderName))
            {
                MessageBox.Show("请输入订单名称");
                txtOrderName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtSinglePackAgeWeight.Text))
            {
                MessageBox.Show("请输入单个包装重量");
                txtSinglePackAgeWeight.Focus();
                return;
            }

            decimal singlePackAgeWeight = 0;
            if (!decimal.TryParse(txtSinglePackAgeWeight.Text, out singlePackAgeWeight))
            {
                MessageBox.Show("单个包装重量不正确");
                txtSinglePackAgeWeight.Focus();
                return;
            }
            if (singlePackAgeWeight < 0)
            {
                MessageBox.Show("单个包装重量不能小于0");
                txtSinglePackAgeWeight.Focus();
                return;
            }


            if (Common.GetDecimalPlaces(singlePackAgeWeight) > 2)
            {
                MessageBox.Show("单个包装重量最多保留两位小数");
                txtSinglePackAgeWeight.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPackAgeCount.Text))
            {
                MessageBox.Show("请输入每次称重包装数量");
                txtPackAgeCount.Focus();
                return;
            }
            int packAgeCount = 0;
            if (!int.TryParse(txtPackAgeCount.Text, out packAgeCount))
            {
                MessageBox.Show("每次称重包装数量不正确");
                txtPackAgeCount.Focus();
                return;
            }
            if (packAgeCount < 0)
            {
                MessageBox.Show("每次称重包装数量不能小于0");
                txtPackAgeCount.Focus();
                return;
            }


            if (string.IsNullOrEmpty(txtPrice.Text))
            {
                MessageBox.Show("请输入单价");
                txtPrice.Focus();
                return;
            }
            decimal price = 0;
            if (!decimal.TryParse(txtPrice.Text, out price))
            {
                MessageBox.Show("单价不正确");
                txtPrice.Focus();
                return;
            }
            if (price <= 0)
            {
                MessageBox.Show("单价必须大于0");
                txtPrice.Focus();
                return;
            }
            if (Common.GetDecimalPlaces(price) > 2)
            {
                MessageBox.Show("单价最多保留两位小数");
                txtPrice.Focus();
                return;
            }

            FormAddDetailBatch form = new FormAddDetailBatch(orderName, singlePackAgeWeight, packAgeCount, price);
            form.Show();
        }
        //private void button1_Click(object sender, EventArgs e)
        //{


        //}




        //
    }
}
