using System.Collections.Generic;
using System.Data;

namespace JW.Dal
{
    /// <summary>
    /// 单张表结构
    /// Create By FangJianWen
    /// </summary>
    public class MetaTable : MetaBase
    {
        /// <summary>
        /// 单张表结构
        /// </summary>
        /// <param name="name"></param>
        public MetaTable(string name)
        {
            Name = name;

        }
        /// <summary>
        /// 单张表结构
        /// </summary>
        public MetaTable()
        {

        }
        /// <summary>
        /// 表名
        /// </summary>
        private string _name;   //table name
        /// <summary>
        /// 表字段名，字段类型列表
        /// </summary>
        private Dictionary<string, SqlDbType> _colums = new Dictionary<string,SqlDbType>(); //
        /// <summary>
        ///keys[0]为主键，其他为索引
        /// </summary>
        List<List<string>> _keys = new List<List<string>>();
        /// <summary>
        ///表名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 列集合
        /// </summary>
        public Dictionary<string, SqlDbType> Columns
        {
            get { return _colums; }
            set { _colums = value; }
        }
        /// <summary>
        /// 主键集合
        /// </summary>
        public List<List<string>> Keys
        {
            get { return _keys; }
            set { _keys = value; }
        }
    }
}
