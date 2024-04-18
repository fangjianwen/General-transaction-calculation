using System.Data;
using System.Data.SqlClient;

namespace JW.Dal
{
    /// <summary>
    /// 数据库事务 Create By FangJianWen
    /// </summary>
    public class DalTransaction
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        private SqlConnection conn;
        /// <summary>
        /// 事务
        /// </summary>
        private SqlTransaction trans;
        /// <summary>
        /// 事务
        /// </summary>
        public SqlTransaction Transaction
        {
            get { return trans; }
        }
        /// <summary>
        /// 数据库连接
        /// </summary>
        public SqlConnection Connection
        {
            get { return conn; }
        }
        /// <summary>
        /// 构造函数 事务锁级别
        /// </summary>
        /// <param name="iso"></param>
        public DalTransaction(IsolationLevel iso)
        {
            conn = new SqlConnection(DataBaseConfig.ConnectionString);
            conn.Open();
            trans = conn.BeginTransaction(iso);
        }
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DalTransaction()
        {
            conn = new SqlConnection(DataBaseConfig.ConnectionString);
            conn.Open();
            trans = conn.BeginTransaction();
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            trans.Commit();
            conn.Close();
            conn.Dispose();
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            trans.Rollback();
            conn.Close();
            conn.Dispose();
        }
        /// <summary>
        /// Finalize
        /// </summary>
        protected void Finalize()
        {
            trans.Rollback();
            conn.Close();
            conn.Dispose();
        }

    }
}
