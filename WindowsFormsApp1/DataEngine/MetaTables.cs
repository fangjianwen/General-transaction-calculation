using System.Collections.Generic;

namespace JW.Dal
{
    /// <summary>
    /// 所有数据表结构
    /// Create By FangJianWen
    /// </summary>
    public class MetaTables
    {
        /// <summary>
        /// 所有数据表结构
        /// </summary>
        private Dictionary<string, MetaTable> _metas = new Dictionary<string, MetaTable>();
        /// <summary>
        /// 所有数据表结构
        /// </summary>
        public Dictionary<string, MetaTable> Tables
        {
            get { return _metas; }

            set { _metas = value; }
        }

    }
}
