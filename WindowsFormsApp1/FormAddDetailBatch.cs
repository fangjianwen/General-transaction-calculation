using JW.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Model;

namespace WindowsFormsAppFruitCalc
{
    public partial class FormAddDetailBatch : Form
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
        public FormAddDetailBatch(string orderName, decimal singlePackAgeWeight, int packAgeCount, decimal price)
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
                    MessageBox.Show("单个包装重量不正确", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSinglePackAgeWeight.Focus();
                    return;
                }
                if (singlePackAgeWeight < 0)
                {
                    MessageBox.Show("单个包装重量不能小于0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSinglePackAgeWeight.Focus();
                    return;
                }
                if (Common.GetDecimalPlaces(singlePackAgeWeight) > 2)
                {
                    MessageBox.Show("单个包装重量最多保留两位小数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSinglePackAgeWeight.Focus();
                    return;
                }
                int packAgeCount = 0;
                if (!int.TryParse(txtPackAgeCount.Text, out packAgeCount))
                {
                    MessageBox.Show("包装数量不正确", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPackAgeCount.Focus();
                    return;
                }
                if (packAgeCount < 0)
                {
                    MessageBox.Show("包装数量不能小于0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPackAgeCount.Focus();
                    return;
                }

                decimal price = 0;
                if (!decimal.TryParse(txtPrice.Text, out price))
                {
                    MessageBox.Show("单价不正确", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrice.Focus();
                    return;
                }
                if (price <= 0)
                {
                    MessageBox.Show("单价必须大于0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrice.Focus();
                    return;
                }
                if (Common.GetDecimalPlaces(price) > 2)
                {
                    MessageBox.Show("单价最多保留两位小数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrice.Focus();
                    return;
                }

                var list = GetMaoWeightList();
                if (list.Count() == 0)
                {
                    MessageBox.Show("请输入称重记录,输入一条记录后,按【Enter】键", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rtbWeightList.Focus();
                    return;
                }


                if (MessageBox.Show("确认添加称重记录?", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {

                    List<OrderDetail> modelList = new List<OrderDetail>();
                    int index = 1;
                    foreach (var weight in list)
                    {

                        OrderDetail model = new OrderDetail();
                        model.OrderName = OrderName;
                        model.AddTime = DateTime.Now;
                        model.SinglePackAgeWeight = Math.Round(singlePackAgeWeight, 2, MidpointRounding.AwayFromZero);//四舍五入
                        model.PackAgeCount = packAgeCount;
                        model.TotalPackAgeWeight = Math.Round(model.SinglePackAgeWeight * model.PackAgeCount, 2, MidpointRounding.AwayFromZero);//四舍五入
                        model.MaoWeight = Math.Round(weight, 2, MidpointRounding.AwayFromZero);//四舍五入;
                        model.RealWeight = Math.Round((model.MaoWeight - model.TotalPackAgeWeight), 2, MidpointRounding.AwayFromZero);//四舍五入
                        model.Price = Math.Round(price, 2, MidpointRounding.AwayFromZero);//四舍五入
                        model.Amount = Math.Round(model.RealWeight * model.Price, 4, MidpointRounding.AwayFromZero);//四舍五入
                        model.Remark = "";
                        if (model.Amount < 0)
                        {
                            MessageBox.Show(string.Format("第{0}条记录为:{1} 计算后金额小于0,请检查数据",index, model.MaoWeight), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        modelList.Add(model);
                        index++;
                    }
                    if (BaseDal.AddList(modelList))
                    {
                        MessageBox.Show("添加成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            rtbWeightList.Focus();
        }



        private void rtbWeightList_TextChanged(object sender, EventArgs e)
       {
            try
            {
                var text = rtbWeightList.Text;
                //if (text.EndsWith("\n"))
                //{
                    var list = GetMaoWeightList();
                    if (list.Count>0)
                    {
                        lblTotalCount.Text = list.Count().ToString();
                        lblTotalMaoWeight.Text = list.Sum().ToString();
                        lblAvg.Text = Math.Round((list.Sum() / list.Count()), 0, MidpointRounding.AwayFromZero).ToString();
                        lblMax.Text = list.Max().ToString();
                        lblMin.Text = list.Min().ToString();
                    }
                    else
                    {
                        lblTotalCount.Text = "0";
                        lblTotalMaoWeight.Text = "0";
                        lblAvg.Text = "0";
                        lblMax.Text = "0";
                        lblMin.Text = "0";
                    }

               // }
                if (string.IsNullOrEmpty(text))
                {
                    lblTotalCount.Text = "0";
                    lblTotalMaoWeight.Text = "0";
                    lblAvg.Text = "0";
                    lblMax.Text = "0";
                    lblMin.Text = "0";
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("数据录入异常:" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        /// <summary>
        /// 获取用户输入的称重记录
        /// </summary>
        /// <returns></returns>
        private List<decimal> GetMaoWeightList()
        {


            List<decimal> list = new List<decimal>();
            var text = rtbWeightList.Text;
            var arry = text.Split('\n').ToList();
            foreach (var item in arry)
            {
                decimal weight = 0;
                if (decimal.TryParse(item, out weight))
                {
                    if (weight > 0)
                    {
                        list.Add(weight);
                    }

                }

            }
            return list;
        }


    }
}
