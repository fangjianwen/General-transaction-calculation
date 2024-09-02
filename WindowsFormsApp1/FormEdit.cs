using JW.Dal;
using System;
using System.Windows.Forms;
using WindowsFormsApp1.Model;

namespace WindowsFormsAppFruitCalc
{
    public partial class FormEdit : Form
    {
        public FormEdit()
        {
            InitializeComponent();
        }
        OrderDetail model;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="num"></param>
        public FormEdit(long id,int num)
        {
            InitializeComponent();
            model = BaseDal.GetModelById<OrderDetail>(id);
           
            txtPackAgeCount.Text = model.PackAgeCount.ToString();
            txtPrice.Text = model.Price.ToString();
            txtSinglePackAgeWeight.Text = model.SinglePackAgeWeight.ToString();
            txtWeight.Text = model.MaoWeight.ToString();
            lblOrderName.Text = model.OrderName;
            rtbRemark.Text = model.Remark;
            lblNum.Text = num.ToString();
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
        /// 修改称重记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditDetail_Click(object sender, EventArgs e)
        {


            try
            {

               
                
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

                decimal weight = 0;
                if (!decimal.TryParse(txtWeight.Text, out weight))
                {
                    MessageBox.Show("重量不正确", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtWeight.Focus();
                    return;
                }

                if (weight <= 0)
                {
                    MessageBox.Show("重量必须大于0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtWeight.Focus();
                    return;
                }
                if (Common.GetDecimalPlaces(weight) > 2)
                {
                    MessageBox.Show("重量最多保留两位小数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtWeight.Focus();
                    return;
                }

                if (MessageBox.Show("确认修改称重记录?", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {

                   
                  
                    model.SinglePackAgeWeight = Math.Round(singlePackAgeWeight, 2, MidpointRounding.AwayFromZero);//四舍五入
                    model.PackAgeCount = packAgeCount;
                    model.TotalPackAgeWeight = Math.Round(model.SinglePackAgeWeight * model.PackAgeCount, 2, MidpointRounding.AwayFromZero);//四舍五入
                    model.MaoWeight = Math.Round(weight, 2, MidpointRounding.AwayFromZero);//四舍五入;
                    model.RealWeight = Math.Round((model.MaoWeight - model.TotalPackAgeWeight), 2, MidpointRounding.AwayFromZero);//四舍五入
                    model.Price = Math.Round(price, 2, MidpointRounding.AwayFromZero);//四舍五入
                    model.Amount = Math.Round(model.RealWeight * model.Price, 4, MidpointRounding.AwayFromZero);//四舍五入
                    model.Remark = rtbRemark.Text;

                    if (model.RealWeight <= 0)
                    {
                        MessageBox.Show("净重必须大于0,请检查", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    if (BaseDal.Update(model))
                    {
                        MessageBox.Show("修改成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        var form = Application.OpenForms["FormMain"]; //
                        if (form != null)
                        {
                            ((FormMain)parentForm).LoadData();//
                           
                        }

                    }
                    else
                    {
                        MessageBox.Show("修改失败,请重试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }



        
       
        private void FormEdit_Load(object sender, EventArgs e)
        {
            parentForm = Application.OpenForms["FormMain"];
        }

        private void FormEdit_Activated(object sender, EventArgs e)
        {
            txtWeight.Focus();
        }
    }
}
