using System;
using System.Data.SqlClient;
using System.Data;

namespace JW.Dal
{
    /// <summary>
    /// Sql 帮助类
    /// </summary>
    public class SQLHelper
    {
        #region 常数定义
        /// <summary>数据库链接超时</summary>
        private const int DB_CONNECT_TIME_OUT = 300;
        /// <summary>配置文件路径中的数据库链接节点</summary>
        private const string CONFIG_CON_FIELD = "DBCONN";
        /// <summary>链接字符串，带参数</summary>
        private const string DB_CONNECT_STRING_FORMAT = "user id={0};pwd={1};Server={2};Database={3}";
        /// <summary>当访问数据库造成数据库错误时，终止语句</summary>
        private const string DB_SET_ARITHABORT_ON = "SET ARITHABORT ON";
        private const string RETURN_VALUE = "ReturnValue";

        #endregion

        #region 变量定义
        private SqlConnection dbConn;
        private SqlCommand dbComm;
        private SqlTransaction dbTran;
        private int iTimeOut;
        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SQLHelper()
        {
            // 创建数据库链接
            string strConnString = DataBaseConfig.ConnectionString;
            dbConn = new SqlConnection(strConnString);

            // 数据库链接超时时间
            iTimeOut = DB_CONNECT_TIME_OUT;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strConn">链接字符串</param>
        public SQLHelper(string strConn)
        {
            // 创建数据库链接
            dbConn = new SqlConnection(strConn);
            // 数据库链接超时时间
            iTimeOut = DB_CONNECT_TIME_OUT;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strConn">链接字符串</param>
        /// <param name="iTimeOut">数据库链接超时时间</param>
        public SQLHelper(string strConn, int iTimeOut)
        {
            // 创建数据库链接
            dbConn = new SqlConnection(strConn);
            // 数据库链接超时时间
            this.iTimeOut = iTimeOut;
            //dbConn.ConnectionTimeout = iTimeOut;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public SQLHelper(SqlConnection sqlConn)
        {
            // 创建数据库链接
            dbConn = sqlConn;
            // 数据库链接超时时间
            iTimeOut = DB_CONNECT_TIME_OUT;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strDBServer">数据库服务器名</param>
        /// <param name="strDatabase">数据库名</param>
        /// <param name="strUser">用户名</param>
        /// <param name="strPass">用户密码</param>
        public SQLHelper(string strDBServer, string strDatabase, string strUser, string strPass)
        {
            // 连接数据库
            string strConn;

            //生成数据库链接字符串
            strConn = string.Format(DB_CONNECT_STRING_FORMAT, strUser, strPass, strDBServer, strDatabase);
            dbConn = new SqlConnection(strConn);
            //dbConn.ConnectionTimeout = DB_CONNECT_TIME_OUT;
            // 数据库链接超时时间
            iTimeOut = DB_CONNECT_TIME_OUT;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strDBServer">数据库服务器名</param>
        /// <param name="strDatabase">数据库名</param>
        /// <param name="strUser">用户名</param>
        /// <param name="strPass">用户密码</param>
        /// <param name="iTimeOut">数据库链接超时时间</param>
        public SQLHelper(string strDBServer, string strDatabase, string strUser, string strPass, int iTimeOut)
        {
            // 连接数据库
            string strConn;
            //生成数据库链接字符串
            strConn = string.Format(DB_CONNECT_STRING_FORMAT, strUser, strPass, strDBServer, strDatabase);
            dbConn = new SqlConnection(strConn);
            // 数据库链接超时时间
            this.iTimeOut = iTimeOut;
        }
        #endregion

        #region 数据库连接

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public void Open()
        {
            // 连接数据库
            if (dbConn.State == ConnectionState.Closed)
            { dbConn.Open(); }
            dbComm = new SqlCommand();
            //将数据库链接赋给SqlCommand
            dbComm.Connection = dbConn;
            //当访问数据库造成数据库错误时，终止语句
            ExecuteNonQuery(DB_SET_ARITHABORT_ON);
        }
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public void Open(SqlConnection conn)
        {
            // 连接数据库
            if (dbConn.State == ConnectionState.Closed)
            { dbConn.Open(); }
            dbComm = new SqlCommand();
            //将数据库链接赋给SqlCommand
            dbComm.Connection = conn;
            //当访问数据库造成数据库错误时，终止语句
        }
        /// <summary>
        /// 关闭数据库链接
        /// </summary>
        public void Close()
        {
            if (dbConn.State == ConnectionState.Open)
            {
                dbConn.Close();
                dbConn.Dispose();
            }
        }

        #endregion

        #region 事务管理
        /// <summary>
        /// 开启事务    
        /// </summary>
        public void BeginTran()
        {
            dbTran = dbConn.BeginTransaction(IsolationLevel.RepeatableRead);
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTran()
        {
            dbTran.Commit();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTran()
        {
            dbTran.Rollback();
        }
        #endregion

        #region 数据库实体存在性检验

        /// <summary>
        /// SQL文的查询数据是否存在
        /// </summary>
        /// <param name="strSql">SQL文</param>
        /// <returns>是否存在</returns>
        public bool Exists(string strSql)
        {

            int cmdresult;
            object obj = ExecuteScalar(strSql);
            //如果返回空，则表示肯定不存在
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }

            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// SQL文的查询数据是否存在
        /// </summary>
        /// <param name="strSql">SQL文</param>
        /// <returns>是否存在</returns>
        public bool Exists(string strSql, SqlParameter[] sqlParameter)
        {
            bool cmdresult;
            //拼写查询语句
            PrepareCommand(strSql, sqlParameter);
            //执行查询语句
            cmdresult = Exists(strSql);
            return cmdresult;
        }

        /// <summary>
        /// 判断是否存在某表的某个列
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="columnName">列名称</param>
        /// <returns>是否存在</returns>
        public bool ColumnExists(string tableName, string columnName)
        {
            //查询字符串
            string sql = "select count(1) from syscolumns where [id]=object_id('" + tableName + "') and [name]='" + columnName + "'";
            //执行命令
            object res = ExecuteScalar(sql);
            if (res == null)
            {
                return false;
            }
            return Convert.ToInt32(res) > 0;
        }

        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <returns>是否存在</returns>
        public bool TableExists(string tableName)
        {
            //查询字符串
            string strsql = "select count(*) from sysobjects where id = object_id(N'[" + tableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
            //执行命令
            object obj = ExecuteScalar(strsql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        /// <summary>
        /// 执行查询,返回DataSet
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <returns>返回查询结果</returns>
        public DataSet ExecuteDataSet(string strSql)
        {
            //初始化容器
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(strSql, dbConn);
            //设置事务
            dbComm.CommandText = strSql;
            dbComm.CommandType = CommandType.Text;
            adapter.SelectCommand = dbComm;
            adapter.SelectCommand.Transaction = dbTran;
           
            //填充DataSet
            adapter.Fill(ds);
            return ds;
        }

        /// <summary>
        /// 执行查询,返回DataSet
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <param name="iTimeOut">iTimeOut时间</param>
        /// <returns>返回查询结果</returns>
        public DataSet ExecuteDataSet(string strSql, int iTimeOut)
        {
            //初始化容器
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(strSql, dbConn);
            //设置超时
            dbComm.CommandTimeout = iTimeOut;
            //设置事务
            dbComm.Transaction = dbTran;
            dbComm.CommandType = CommandType.Text;
            adapter.SelectCommand = dbComm;
            //填充DataSet
            adapter.Fill(ds);
            return ds;
        }

        /// <summary>
        /// 执行查询,返回DataSet
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <param name="sqlParameter">SQL参数</param>
        /// <returns>返回查询结果</returns>
        public DataSet ExecuteDataSet(string strSql, SqlParameter[] sqlParameter)
        {
            //初始化容器
            DataSet ds = new DataSet();
            //拼写查询语句
            PrepareCommand(strSql, sqlParameter);
            ds = ExecuteDataSet(strSql);
            dbComm.Parameters.Clear();
            return ds;
        }

        /// <summary>
        /// 执行查询,返回DataSet
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <param name="sqlParameter">SQL参数</param>
        /// <param name="iTimeOut">iTimeOut时间</param>
        /// <returns>返回查询结果</returns>
        public DataSet ExecuteDataSet(string strSql, SqlParameter[] sqlParameter, int iTimeOut)
        {
            //初始化容器
            DataSet ds = new DataSet();
            //拼写查询语句
            PrepareCommand(strSql, sqlParameter);
            ds = ExecuteDataSet(strSql, iTimeOut);
            dbComm.Parameters.Clear();
            return ds;

        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteNonQuery(string strSql)
        {
            //设置查询语句
            dbComm.CommandText = strSql;
            //设置事务
            dbComm.Transaction = dbTran;
            dbComm.CommandType = CommandType.Text;
            //返回影响的记录数
            int rows = dbComm.ExecuteNonQuery();
            return rows;
        }
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="tran">事务</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteNonQuery(string strSql, SqlTransaction tran)
        {
            //设置查询语句
            dbComm.CommandText = strSql;
            //设置事务
            dbComm.Transaction = tran;
            dbComm.CommandType = CommandType.Text;
            //返回影响的记录数
            int rows = dbComm.ExecuteNonQuery();
            return rows;
        }
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteNonQuery(string strSql, int iTimeOut)
        {
            //设置查询语句
            dbComm.CommandText = strSql;
            //设置事务
            dbComm.Transaction = dbTran;
            //设置超时时间
            dbComm.CommandTimeout = iTimeOut;
            dbComm.CommandType = CommandType.Text;
            //返回影响的记录数
            int rows = dbComm.ExecuteNonQuery();
            return rows;
        }

        /// <summary>
        /// 执行SQL文，返回SQL文影响的行数
        /// </summary>
        /// <param name="strSql">SQL文</param>
        /// <param name="sqlParameter">SQL参数</param>
        /// <returns>SQL文影响的行数</returns>
        public int ExecuteNonQuery(string strSql, SqlParameter[] sqlParameter)
        {
            int cmdresult;
            //拼写查询语句
            PrepareCommand(strSql, sqlParameter);
            //返回影响的记录数
            cmdresult = ExecuteNonQuery(strSql);
            dbComm.Parameters.Clear();
            return cmdresult;
        }
        /// <summary>
        /// 执行SQL文，返回SQL文影响的行数
        /// </summary>
        /// <param name="strSql">SQL文</param>
        /// <param name="sqlParameter">SQL参数</param>
        /// <param name="tran">事务</param>
        /// <returns>SQL文影响的行数</returns>
        public int ExecuteNonQuery(string strSql, SqlParameter[] sqlParameter, SqlTransaction tran)
        {
            int cmdresult;
            //拼写查询语句
            PrepareCommand(strSql, sqlParameter, tran);
            //返回影响的记录数
            cmdresult = ExecuteNonQuery(strSql, tran);
            dbComm.Parameters.Clear();
            return cmdresult;
        }
        /// <summary>
        /// 执行SQL文，返回SQL文影响的行数
        /// </summary>
        /// <param name="strSql">SQL文</param>
        /// <param name="sqlParameter">SQL参数</param>
        /// <param name="iTimeOut">timeout时间</param>
        /// <returns>SQL文影响的行数</returns>
        public int ExecuteNonQuery(string strSql, SqlParameter[] sqlParameter, int iTimeOut)
        {
            int cmdresult;
            //拼写查询语句
            PrepareCommand(strSql, sqlParameter);
            //返回影响的记录数
            cmdresult = ExecuteNonQuery(strSql, iTimeOut);
            dbComm.Parameters.Clear();
            return cmdresult;
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="strSql">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object ExecuteScalar(string strSql)
        {
            //设置查询语句
            dbComm.CommandText = strSql;
            dbComm.CommandType = CommandType.Text;
            //设置事务
            dbComm.Transaction = dbTran;
            object obj = dbComm.ExecuteScalar();

            //如果返回值为DBNull，则返回null
            if (Object.Equals(obj, System.DBNull.Value))
            {
                return null;
            }
            else
            {
                return obj;
            }
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="strSql">计算查询结果语句</param>
        /// <param name="tran">事务</param>
        /// <returns>查询结果（object）</returns>
        public object ExecuteScalar(string strSql, SqlTransaction tran)
        {
            //设置查询语句
            dbComm.CommandText = strSql;
            dbComm.CommandType = CommandType.Text;
            //设置事务
            dbComm.Transaction = tran;
            object obj = dbComm.ExecuteScalar();

            //如果返回值为DBNull，则返回null
            if (Object.Equals(obj, System.DBNull.Value))
            {
                return null;
            }
            else
            {
                return obj;
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="strSql">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object ExecuteScalar(string strSql, int iTimeOut)
        {
            //设置查询语句
            dbComm.CommandText = strSql;
            //设置超时
            dbComm.CommandTimeout = iTimeOut;
            //设置事务
            dbComm.Transaction = dbTran;
            dbComm.CommandType = CommandType.Text;
            //执行查询语句
            object obj = dbComm.ExecuteScalar();

            //如果返回值为DBNull，则返回null
            if (Object.Equals(obj, System.DBNull.Value))
            {
                return null;
            }
            else
            {
                return obj;
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="strSql">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object ExecuteScalar(string strSql, params SqlParameter[] cmdParms)
        {
            object cmdresult;
            //拼写查询语句
            PrepareCommand(strSql, cmdParms);
            //执行查询语句
            cmdresult = ExecuteScalar(strSql);
            dbComm.Parameters.Clear();
            return cmdresult;
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="strSql">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object ExecuteScalar(SqlTransaction tran, string strSql, params SqlParameter[] cmdParms)
        {
            object cmdresult;
            //拼写查询语句
            PrepareCommand(strSql, cmdParms, tran);
            //执行查询语句
            cmdresult = ExecuteScalar(strSql, tran);
            dbComm.Parameters.Clear();
            return cmdresult;
        }
        /// <summary>
        /// 拼写SQL语句
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        public void PrepareCommand(string cmdText, SqlParameter[] cmdParms)
        {
            //带参数的SQL语句
            dbComm.CommandText = cmdText;
            //设置事务
            if (dbTran != null)
            {
                dbComm.Transaction = dbTran;
            }
            //设置命令类型
            dbComm.CommandType = CommandType.Text;//cmdType;
            //清空参数列表
            dbComm.Parameters.Clear();
            if (cmdParms != null)
            {
                //设置参数
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    dbComm.Parameters.Add(parameter);
                }
            }
        }
        /// <summary>
        /// 拼写SQL语句
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        public void PrepareCommand(string cmdText, SqlParameter[] cmdParms, SqlTransaction tran)
        {
            //带参数的SQL语句
            dbComm.CommandText = cmdText;
            //设置事务
            if (tran != null)
            {
                dbComm.Transaction = tran;
            }
            //设置命令类型
            dbComm.CommandType = CommandType.Text;//cmdType;
            //清空参数列表
            dbComm.Parameters.Clear();
            if (cmdParms != null)
            {
                //设置参数
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    dbComm.Parameters.Add(parameter);
                }
            }
        }

        /// <summary>
        /// 返回单表存在的最大ID值加1，作为新增数据的ID
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public int GetMaxID(string FieldName, string TableName)
        {
            //拼写SQL语句
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            //执行SQL语句
            object obj = ExecuteScalar(strsql);
            //如果返回值为空，则代表原先没有记录，新增的记录为第一条记录
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            //初始化返回值
            DataSet dataSet = new DataSet();
            SqlDataAdapter sqlDA = new SqlDataAdapter(dbComm);
            //拼写SQL
            BuildProcedureCommand(storedProcName, parameters);
            //填充DataSet
            sqlDA.Fill(dataSet, tableName);
            dbComm.Parameters.Clear();
            return dataSet;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int iTimeOut)
        {
            //初始化返回值
            DataSet dataSet = new DataSet();
            SqlDataAdapter sqlDA = new SqlDataAdapter(dbComm);
            //拼写SQL
            BuildProcedureCommand(storedProcName, parameters);
            //设置超时
            dbComm.CommandTimeout = iTimeOut;
            //填充DataSet
            sqlDA.Fill(dataSet, tableName);
            dbComm.Parameters.Clear();
            return dataSet;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="sqlParameter">存储过程参数</param>
        /// <returns>执行结果</returns>
        public DataSet RunProcedure(string storedProcName, SqlParameter[] sqlParameter, string tableName)
        {
            //初始化返回值
            DataSet dataSet = new DataSet();
            SqlDataAdapter sqlDA = new SqlDataAdapter(dbComm);
            //拼写SQL
            BuildProcedureCommand(storedProcName, sqlParameter);
            //填充DataSet
            sqlDA.Fill(dataSet, tableName);
            dbComm.Parameters.Clear();
            return dataSet;
        }

        /// <summary>
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            int result;
            //拼写SQL
            BuildIntCommand(storedProcName, parameters);
            //执行SQL
            rowsAffected = dbComm.ExecuteNonQuery();
            result = (int)dbComm.Parameters[RETURN_VALUE].Value;
            dbComm.Parameters.Clear();
            return result;
        }

        /// <summary>
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand对象</returns>
        public SqlCommand RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            //拼写SQL
            BuildProcedureCommand(storedProcName, parameters);
            //执行SQL
            dbComm.ExecuteNonQuery();
            dbComm.Parameters.Clear();
            return dbComm;
        }

        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private void BuildProcedureCommand(string storedProcName, IDataParameter[] parameters)
        {
            //设置存储过程命令
            dbComm.CommandText = storedProcName;
            dbComm.CommandType = CommandType.StoredProcedure;
            //设置事务
            dbComm.Transaction = dbTran;
            //清除参数列表
            dbComm.Parameters.Clear();
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input || parameter.Direction == ParameterDirection.InputOutput) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    dbComm.Parameters.Add(parameter);
                }
            }

            return;
        }

        /// <summary>
        /// 创建 SqlCommand 对象实例(用来返回一个整数值)	
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        private void BuildIntCommand(string storedProcName, IDataParameter[] parameters)
        {
            BuildProcedureCommand(storedProcName, parameters);
            dbComm.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return;
        }

        public string GetNextSeq(string tableName, string idName)
        {
            string billCode = string.Empty;
            dbComm.CommandText = "up_CMNGetNewBillCode";
            dbComm.CommandType = System.Data.CommandType.StoredProcedure;
            dbComm.Parameters.Clear();
            dbComm.Transaction = dbTran;

            // tableName
            SqlParameter tableNamePar = new SqlParameter("@TableName", SqlDbType.VarChar, 36);
            tableNamePar.Direction = ParameterDirection.Input;
            tableNamePar.Value = tableName;
            dbComm.Parameters.Add(tableNamePar);

            // idName
            SqlParameter idNamePar = new SqlParameter("@BillCode", SqlDbType.Char, 16);
            idNamePar.Direction = ParameterDirection.InputOutput;
            idNamePar.Value = billCode;
            dbComm.Parameters.Add(idNamePar);

            // 调用存储过程 
            dbComm.ExecuteNonQuery();

            return dbComm.Parameters["@BillCode"].Value.ToString();
        }


    }
}
