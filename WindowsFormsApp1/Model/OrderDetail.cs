using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model
{
    public class OrderDetail
    {
        /// <summary>
        /// id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 订单名称
        /// </summary>
        public string OrderName { get; set; }
        /// <summary>
        /// 单个包装重量
        /// </summary>
        public decimal SinglePackAgeWeight { get; set; }
        /// <summary>
        /// 每次称重包装数量
        /// </summary>
        public int PackAgeCount { get; set; }
        /// <summary>
        /// 皮重=单个包装重量*每次称重包装数量
        /// </summary>
        public decimal TotalPackAgeWeight { get; set; }
        /// <summary>
        /// 毛重=皮重+净重
        /// </summary>
        public decimal MaoWeight { get; set; }
        /// <summary>
        /// 净重=毛重-皮重
        /// </summary>
        public decimal RealWeight { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 金额小计=净重*单价
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }


    }
}
