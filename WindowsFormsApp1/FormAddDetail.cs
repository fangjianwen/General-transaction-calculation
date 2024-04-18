using JW.Dal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp1.Model;
using WindowsFormsAppFruitCalc;

namespace WindowsFormsAppFruitCalc
{
    public partial class FormAddDetail : Form
    {
        /// <summary>
        /// 订单名称
        /// </summary>
        string OrderName;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="orderName">订单名称</param>
        /// <param name="singlePackAgeWeight">单个包装数量(斤)</param>
        /// <param name="packAgeCount">包装数量</param>
        /// <param name="price">单价(元/斤)</param>
        public FormAddDetail(string orderName, decimal singlePackAgeWeight, int packAgeCount, decimal price)
        {
            InitializeComponent();
            OrderName = orderName;
            txtPackAgeCount.Text = packAgeCount.ToString();
            txtPrice.Text = price.ToString();
            txtSinglePackAgeWeight.Text = singlePackAgeWeight.ToString();
            lblOrderName.Text = OrderName;
        }
        /// <summary>
        /// 父窗口
        /// </summary>
        Form parentForm;
        private void FormAddDetail_Load(object sender, EventArgs e)
        {
            parentForm = Application.OpenForms["FormMain"];
            List<SqlParameter> parList = new List<SqlParameter>();
            parList.Add(new SqlParameter("@OrderName", OrderName));
            lblNumber.Text = (BaseDal.GetCount<OrderDetail>("OrderName=@OrderName",parList)+1).ToString();

           
        }
        /// <summary>
        /// 增加称重记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDetail_Click(object sender, EventArgs e)
        {


            try
            {
                if (string.IsNullOrEmpty(OrderName))
                {
                    MessageBox.Show("请输入订单名称");

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
                int packAgeCount = 0;
                if (!int.TryParse(txtPackAgeCount.Text, out packAgeCount))
                {
                    MessageBox.Show("包装数量不正确");
                    txtPackAgeCount.Focus();
                    return;
                }
                if (packAgeCount<0)
                {
                    MessageBox.Show("包装数量不能小于0");
                    txtPackAgeCount.Focus();
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

                decimal weight = 0;
                if (!decimal.TryParse(txtWeight.Text, out weight))
                {
                    MessageBox.Show("重量不正确");
                    txtWeight.Focus();
                    return;
                }

                if (weight <= 0)
                {
                    MessageBox.Show("重量必须大于0");
                    txtWeight.Focus();
                    return;
                }
                if (Common.GetDecimalPlaces(weight) > 2)
                {
                    MessageBox.Show("重量最多保留两位小数");
                    txtWeight.Focus();
                    return;
                }

                if (MessageBox.Show("确认添加称重记录?", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {

                    OrderDetail model = new OrderDetail();
                    model.OrderName = OrderName;
                    model.AddTime = DateTime.Now;
                    model.SinglePackAgeWeight = Math.Round(singlePackAgeWeight, 2, MidpointRounding.AwayFromZero);//四舍五入
                    model.PackAgeCount = packAgeCount;
                    model.TotalPackAgeWeight = Math.Round(model.SinglePackAgeWeight * model.PackAgeCount,2,MidpointRounding.AwayFromZero);//四舍五入
                    model.MaoWeight = Math.Round(weight, 2, MidpointRounding.AwayFromZero);//四舍五入;
                    model.RealWeight = Math.Round((model.MaoWeight - model.TotalPackAgeWeight),2, MidpointRounding.AwayFromZero);//四舍五入
                    model.Price = Math.Round(price, 2, MidpointRounding.AwayFromZero);//四舍五入
                    model.Amount = Math.Round(model.RealWeight * model.Price,4, MidpointRounding.AwayFromZero);//四舍五入
                    model.Remark = rtbRemark.Text;

                    if (model.RealWeight<=0)
                    {
                        MessageBox.Show("净重必须大于0,请检查");                      
                        return;
                    }


                    if (BaseDal.Add(model))
                    {
                        MessageBox.Show("添加成功","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        this.Close();
                        var form = Application.OpenForms["FormMain"]; //
                        if (form != null)
                        {
                            ((FormMain)parentForm).LoadData();//

                            ((FormMain)parentForm).BindComboxData();//
                        }

                    }
                    else
                    {                   
                        MessageBox.Show("添加记录失败,请重试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }

        }

       

        private void FormAddDetail_Activated(object sender, EventArgs e)
        {
            txtWeight.Focus();
        }

        private void FormAddDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//判断回车键

            {
                btnAddDetail_Click(null,null);



            }
        }
    }
}
