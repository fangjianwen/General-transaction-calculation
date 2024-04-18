
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace JW.Dal
{
    /// <summary>
    /// 数据库操作类
    /// Create By FangJianWen  2022/03
    /// </summary>
    public class BaseDal
    {

        /// <summary>
        /// 获取单个实体 根据Id
        /// </summary>
        /// <typeparam name="T">数据表</typeparam>
        /// <param name="id">数据表id字段的值</param>
        /// <returns></returns>
        public static T GetModelById<T>(long id) where T : new()
        {        
            return GetModelByWhere<T>("id=@id",new SqlParameter("@id", id));
        }
        /// <summary>
        /// 获取单个实体 根据where 后的sql 及参数
        /// </summary>
        /// <typeparam name="T">数据表</typeparam>
        /// <param name="where">where 后的Sql</param>
        /// <param name="par">Sql参数</param>
        /// <returns></returns>
        public static T GetModelByWhere<T>(string where, SqlParameter par) where T : new()
        {
            List<SqlParameter> parList = new List<SqlParameter>();
            parList.Add(par);
            return GetModelByWhere<T>(where, parList);
        }
        /// <summary>
        /// 获取单个实体 根据where 后的sql 及参数
        /// </summary>
        /// <typeparam name="T">数据表</typeparam>
        /// <param name="where">where 后的Sql</param>
        /// <param name="parList">Sql参数列表</param>
        /// <returns></returns>
        public static T GetModelByWhere<T>(string where, List<SqlParameter> parList) where T : new()
        {
            string sql = string.Format("select top 1* from [{0}] where {1}", new T().GetType().Name, where);
            return GetModel<T>(sql, parList);
        }
        /// <summary>
        /// 获取单个实体 根据完整的sql 语句及参数
        /// </summary>
        /// <typeparam name="T">数据表</typeparam>
        /// <param name="sql">完整的sql 语句</param>
        /// <param name="parList">Sql参数列表</param>
        /// <returns></returns>
        public static T GetModel<T>(string sql, List<SqlParameter> parList) where T : new()
        {
            DataTable dt = ExecuteDataTable(sql, parList);
            if (dt.Rows.Count > 0)
            {
                return DataTableToModelList<T>(dt)[0];
            }
            else
            {
                return default(T);
            }

        }
        /// <summary>
        /// 获取数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="parList"></param>
        /// <returns></returns>
        public static long GetCount<T>(string where, SqlParameter par) where T : new()
        {

            List<SqlParameter> parList = new List<SqlParameter>();
            parList.Add(par);
            return GetCount<T>(where,parList);

        }
        /// <summary>
        /// 获取数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="parList"></param>
        /// <returns></returns>
        public static long GetCount<T>(string where, List<SqlParameter> parList) where T : new()
        {
            string sql = string.Format("select count(1) from [{0}]", new T().GetType().Name);
            if (!string.IsNullOrEmpty(where))
            {
                sql += " where " + where;
            }
            DataTable dt = ExecuteDataTable(sql, parList);
            if (dt.Rows.Count > 0)
            {
                return long.Parse(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }


        }
        /// <summary>
        /// 新增一条数据 返回成功失败
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public static bool Add<T>(T model) where T : new()
        {

            SQLHelper sqlHelper = new SQLHelper();
            try
            {
                sqlHelper.Open();
                StringBuilder strSql = new StringBuilder();
                StringBuilder strColumns = new StringBuilder();
                StringBuilder strValues = new StringBuilder();
                List<SqlParameter> parameters = new List<SqlParameter>();
                Type t = model.GetType();
                MetaTable metaTable = GetMetas().Tables[t.Name];
                PropertyInfo[] properties = t.GetProperties();
                int i = 0;
                foreach (PropertyInfo p in properties)
                {
                    object val = p.GetValue(model, null);

                    //如果该属性在表结构中不存在，或者model里属性值为null，或者字段名称是id, 则跳过。
                    if (!metaTable.Columns.ContainsKey(p.Name) || null == val || p.Name.ToLower().Equals("id"))
                    {
                        continue;
                    }
                    if (i == 0)
                    {
                        strColumns.AppendFormat("{0} ", p.Name);
                        strValues.AppendFormat("@{0} ", p.Name);
                    }
                    else
                    {
                        strColumns.AppendFormat(", {0} ", p.Name);
                        strValues.AppendFormat(", @{0} ", p.Name);
                    }

                    parameters.Add(new SqlParameter(string.Format("@{0}", p.Name), metaTable.Columns[p.Name]));
                    parameters[i].Value = val;
                    i++;
                }
                strSql.AppendFormat("insert into [{0}] ", metaTable.Name);
                strSql.AppendFormat("({0}) values ({1}) ;", strColumns, strValues);
                return long.Parse(sqlHelper.ExecuteNonQuery(strSql.ToString(), parameters.ToArray()).ToString()) > 0;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                sqlHelper.Close();
            }


        }
        /// <summary>
        /// 新增一条数据 返回成功失败 使用事务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">实体</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public static bool Add<T>(T model, DalTransaction tran) where T : new()
        {
            SQLHelper sqlHelper = new SQLHelper(tran.Connection);

            try
            {
                sqlHelper.Open(tran.Connection);
                StringBuilder strSql = new StringBuilder();
                StringBuilder strColumns = new StringBuilder();
                StringBuilder strValues = new StringBuilder();
                List<SqlParameter> parameters = new List<SqlParameter>();
                Type t = model.GetType();
                MetaTable metaTable = GetMetas().Tables[t.Name];
                PropertyInfo[] properties = t.GetProperties();
                int i = 0;
                foreach (PropertyInfo p in properties)
                {
                    object val = p.GetValue(model, null);

                    //如果该属性在表结构中不存在，或者model里属性值为null，或者字段名称是id, 则跳过。
                    if (!metaTable.Columns.ContainsKey(p.Name) || null == val || p.Name.ToLower().Equals("id"))
                    {
                        continue;
                    }
                    if (i == 0)
                    {
                        strColumns.AppendFormat("{0} ", p.Name);
                        strValues.AppendFormat("@{0} ", p.Name);
                    }
                    else
                    {
                        strColumns.AppendFormat(", {0} ", p.Name);
                        strValues.AppendFormat(", @{0} ", p.Name);
                    }

                    parameters.Add(new SqlParameter(string.Format("@{0}", p.Name), metaTable.Columns[p.Name]));
                    parameters[i].Value = val;
                    i++;
                }
                strSql.AppendFormat("insert into [{0}] ", metaTable.Name);
                strSql.AppendFormat("({0}) values ({1}) ;", strColumns, strValues);
                return long.Parse(sqlHelper.ExecuteNonQuery(strSql.ToString(), parameters.ToArray(), tran.Transaction).ToString()) > 0;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
            }


        }
        /// <summary>
        /// 新增一条数据 并返回Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public static string AddReturnId<T>(T model) where T : new()
        {
            SQLHelper sqlHelper = new SQLHelper();
            try
            {
                sqlHelper.Open();
                StringBuilder strSql = new StringBuilder();
                StringBuilder strColumns = new StringBuilder();
                StringBuilder strValues = new StringBuilder();
                List<SqlParameter> parameters = new List<SqlParameter>();
                Type t = model.GetType();
                MetaTable metaTable = GetMetas().Tables[t.Name];
                PropertyInfo[] properties = t.GetProperties();
                int i = 0;
                foreach (PropertyInfo p in properties)
                {
                    object val = p.GetValue(model, null);

                    //如果该属性在表结构中不存在，或者model里属性值为null，或者字段名称是id, 则跳过。
                    if (!metaTable.Columns.ContainsKey(p.Name) || null == val || p.Name.ToLower().Equals("id"))
                    {
                        continue;
                    }
                    if (i == 0)
                    {
                        strColumns.AppendFormat("{0} ", p.Name);
                        strValues.AppendFormat("@{0} ", p.Name);
                    }
                    else
                    {
                        strColumns.AppendFormat(", {0} ", p.Name);
                        strValues.AppendFormat(", @{0} ", p.Name);
                    }

                    parameters.Add(new SqlParameter(string.Format("@{0}", p.Name), metaTable.Columns[p.Name]));
                    parameters[i].Value = val;
                    i++;
                }
                strSql.AppendFormat("insert into [{0}] ", metaTable.Name);
                strSql.AppendFormat("({0}) values ({1}) ;", strColumns, strValues);
                strSql.AppendFormat(" SELECT SCOPE_IDENTITY() ; ");
                return sqlHelper.ExecuteScalar(strSql.ToString(), parameters.ToArray()).ToString();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                sqlHelper.Close();
            }


        }
        /// <summary>
        /// 新增一条数据 并返回Id 使用事务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">实体</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public static string AddReturnId<T>(T model, DalTransaction tran) where T : new()
        {
            SQLHelper sqlHelper = new SQLHelper(tran.Connection);
            try
            {
                sqlHelper.Open(tran.Connection);
                StringBuilder strSql = new StringBuilder();
                StringBuilder strColumns = new StringBuilder();
                StringBuilder strValues = new StringBuilder();
                List<SqlParameter> parameters = new List<SqlParameter>();
                Type t = model.GetType();
                MetaTable metaTable = GetMetas().Tables[t.Name];
                PropertyInfo[] properties = t.GetProperties();
                int i = 0;
                foreach (PropertyInfo p in properties)
                {
                    object val = p.GetValue(model, null);

                    //如果该属性在表结构中不存在，或者model里属性值为null，或者字段名称是id, 则跳过。
                    if (!metaTable.Columns.ContainsKey(p.Name) || null == val || p.Name.ToLower().Equals("id"))
                    {
                        continue;
                    }
                    if (i == 0)
                    {
                        strColumns.AppendFormat("{0} ", p.Name);
                        strValues.AppendFormat("@{0} ", p.Name);
                    }
                    else
                    {
                        strColumns.AppendFormat(", {0} ", p.Name);
                        strValues.AppendFormat(", @{0} ", p.Name);
                    }

                    parameters.Add(new SqlParameter(string.Format("@{0}", p.Name), metaTable.Columns[p.Name]));
                    parameters[i].Value = val;
                    i++;
                }
                strSql.AppendFormat("insert into [{0}] ", metaTable.Name);
                strSql.AppendFormat("({0}) values ({1}) ;", strColumns, strValues);
                strSql.AppendFormat(" SELECT SCOPE_IDENTITY() ; ");
                return sqlHelper.ExecuteScalar(tran.Transaction, strSql.ToString(), parameters.ToArray()).ToString();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
            }


        }
        /// <summary>
        /// 批量新增数据 返回成功失败
        /// </summary>
        /// <param name="modelList">实体集合</param>
        /// <returns></returns>
        public static bool AddList<T>(List<T> modelList)
        {
            return AddBulkCopy(modelList);
        }
        /// <summary>
        /// 批量新增数据 返回成功失败 使用事务
        /// </summary>
        /// <param name="modelList">实体集合</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public static bool AddList<T>(List<T> modelList, DalTransaction tran)
        {
            return AddBulkCopy(modelList, tran);
        }
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelList">实体集合</param>
        /// <returns></returns>
        private static bool AddBulkCopy<T>(List<T> modelList)
        {
            try
            {
                Type t = modelList[0].GetType();
                DataTable dtCopy = new DataTable();
                dtCopy = ListToDataTable(modelList);
                return AddBulkCopy(t.Name, dtCopy);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        /// <summary>
        /// 批量插入 使用事务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelList">实体集合</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        private static bool AddBulkCopy<T>(List<T> modelList, DalTransaction tran)
        {
            try
            {
                Type t = modelList[0].GetType();
                DataTable dtCopy = new DataTable();
                dtCopy = ListToDataTable(modelList);
                return AddBulkCopy(t.Name, dtCopy, tran);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        /// <summary>
        /// 批量插入 
        /// </summary>
        /// <param name="tableName">数据库表名称</param>
        /// <param name="copyTable">数据表</param>
        /// <returns></returns>
        public static bool AddBulkCopy(string tableName, DataTable copyTable)
        {
            try
            {

                SqlBulkCopy bulkCopy = new SqlBulkCopy(DataBaseConfig.ConnectionString);
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.BatchSize = copyTable.Rows.Count;
                bulkCopy.BulkCopyTimeout = 600;
                if (copyTable.Rows.Count > 0)
                {
                    foreach (DataColumn col in copyTable.Columns)
                    {
                        bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                    }
                    bulkCopy.WriteToServer(copyTable);
                    bulkCopy.Close();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 批量插入 使用事务
        /// </summary>
        /// <param name="tableName">数据库表名称</param>
        /// <param name="copyTable">数据表</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public static bool AddBulkCopy(string tableName, DataTable copyTable, DalTransaction tran)
        {
            try
            {

                SqlBulkCopy bulkCopy = new SqlBulkCopy(tran.Connection, SqlBulkCopyOptions.Default, tran.Transaction);
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.BatchSize = copyTable.Rows.Count;
                bulkCopy.BulkCopyTimeout = 600;
                if (copyTable.Rows.Count > 0)
                {
                    foreach (DataColumn col in copyTable.Columns)
                    {
                        bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                    }
                    bulkCopy.WriteToServer(copyTable);
                    bulkCopy.Close();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public static bool Update<T>(T model) where T : new()
        {
            SQLHelper sqlHelper = new SQLHelper();
            try
            {
                sqlHelper.Open();
                List<SqlParameter> parameters = new List<SqlParameter>();
                Type t = model.GetType();
                PropertyInfo[] properties = t.GetProperties();
                MetaTable metaTable = GetMetas().Tables[t.Name];
                StringBuilder strSql = new StringBuilder();
                strSql.Append(string.Format("update [{0}] set ", metaTable.Name));
                int paramIndex = 0; //SqlParameter 参数下标
                if (metaTable.Keys.Count == 0 || metaTable.Keys[0].Count == 0)
                {
                    throw new Exception("没有主键，不能执行model修改");
                }
                foreach (string key in metaTable.Columns.Keys)
                {
                    if (key.ToLower().Equals("id")) continue;
                    if (metaTable.Keys.Count > 0 && metaTable.Keys[0].Contains(key)) continue;
                    PropertyInfo p = null;
                    foreach (PropertyInfo item in properties)
                    {
                        if (item.Name == key)
                        {
                            p = item;
                            break;
                        }
                    }

                    if (p == null) continue;

                    object val = p.GetValue(model, null);
                    if (paramIndex == 0)
                    {
                        strSql.AppendFormat("{0} = @{0}", key);
                    }
                    else
                    {
                        strSql.AppendFormat(", {0} = @{0}", key);
                    }
                    parameters.Add(new SqlParameter(string.Format("@{0}", key), metaTable.Columns[key]));
                    parameters[paramIndex++].Value = val;


                }



                int keyIndex = 0; //SqlParameter 参数下标
                foreach (string key in metaTable.Keys[0])
                {
                    if (keyIndex == 0)
                    {
                        strSql.AppendFormat(" where {0} = @{0}", key);
                    }
                    else
                    {
                        strSql.AppendFormat(" and {0} = @{0}", key);
                    }
                    keyIndex++;
                    parameters.Add(new SqlParameter(string.Format("@{0}", key), metaTable.Columns[key]));



                    PropertyInfo p = null;
                    foreach (PropertyInfo item in properties)
                    {
                        if (item.Name == key)
                        {
                            p = item;
                            break;
                        }
                    }

                    parameters[paramIndex++].Value = p.GetValue(model, null);
                }
                return sqlHelper.ExecuteNonQuery(strSql.ToString(), parameters.ToArray()) > 0;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                sqlHelper.Close();
            }


        }
        /// <summary>
        /// 更新一条数据 使用事务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">实体</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public static bool Update<T>(T model, DalTransaction tran) where T : new()
        {
            SQLHelper sqlHelper = new SQLHelper(tran.Connection);
            try
            {
                sqlHelper.Open(tran.Connection);
                List<SqlParameter> parameters = new List<SqlParameter>();
                Type t = model.GetType();
                PropertyInfo[] properties = t.GetProperties();
                MetaTable metaTable = GetMetas().Tables[t.Name];
                StringBuilder strSql = new StringBuilder();
                strSql.Append(string.Format("update [{0}] set ", metaTable.Name));
                int paramIndex = 0; //SqlParameter 参数下标
                if (metaTable.Keys.Count == 0 || metaTable.Keys[0].Count == 0)
                {
                    throw new Exception("没有主键，不能执行model修改");
                }
                foreach (string key in metaTable.Columns.Keys)
                {
                    if (key.ToLower().Equals("id")) continue;
                    if (metaTable.Keys.Count > 0 && metaTable.Keys[0].Contains(key)) continue;

                    PropertyInfo p = null;
                    foreach (PropertyInfo item in properties)
                    {
                        if (item.Name == key)
                        {
                            p = item;
                            break;
                        }
                    }
                    if (p == null) continue;

                    object val = p.GetValue(model, null);
                    if (paramIndex == 0)
                    {
                        strSql.AppendFormat("{0} = @{0}", key);
                    }
                    else
                    {
                        strSql.AppendFormat(", {0} = @{0}", key);
                    }
                    parameters.Add(new SqlParameter(string.Format("@{0}", key), metaTable.Columns[key]));
                    parameters[paramIndex++].Value = val;
                }


                int keyIndex = 0; //SqlParameter 参数下标
                foreach (string key in metaTable.Keys[0])
                {
                    if (keyIndex == 0)
                    {
                        strSql.AppendFormat(" where {0} = @{0}", key);
                    }
                    else
                    {
                        strSql.AppendFormat(" and {0} = @{0}", key);
                    }
                    keyIndex++;
                    parameters.Add(new SqlParameter(string.Format("@{0}", key), metaTable.Columns[key]));


                    PropertyInfo p = null;
                    foreach (PropertyInfo item in properties)
                    {
                        if (item.Name == key)
                        {
                            p = item;
                            break;
                        }
                    }
                    parameters[paramIndex++].Value = p.GetValue(model, null);
                }
                return sqlHelper.ExecuteNonQuery(strSql.ToString(), parameters.ToArray(), tran.Transaction) > 0;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
            }


        }
        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="modelList">实体列表</param>
        public static bool UpdateList<T>(List<T> modelList)
        {
            try
            {
                if (modelList.Count == 0)
                {
                    return false;
                }
                Type t = modelList[0].GetType();
                PropertyInfo[] properties = t.GetProperties();
                DataTable dataTable = new DataTable();
                dataTable = ListToDataTable(modelList);
                dataTable.TableName = t.Name;
                return Update(dataTable) == modelList.Count;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="table">数据表</param>
        public static int Update(DataTable table)
        {
            int count = 0;
            SqlConnection conn = new SqlConnection(DataBaseConfig.ConnectionString);
            try
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(string.Format("select * from [{0}] where 1=0", table.TableName), conn);
                SqlCommandBuilder command = new SqlCommandBuilder(adapter);
                command.ConflictOption = ConflictOption.OverwriteChanges;
                command.SetAllValues = true;
                table.AcceptChanges();
                foreach (DataRow row in table.Rows)
                {
                    row.SetModified();
                }
                count = adapter.Update(table);
                adapter.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }
        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="modelList">实体列表</param>
        /// <param name="fieds">要更新的字段列表 多个字段之间用英文逗号分隔</param>
        public static bool UpdateList<T>(List<T> modelList, string fieds)
        {
            try
            {

                if (modelList.Count == 0)
                {
                    return false;
                }
                Type t = modelList[0].GetType();
                PropertyInfo[] properties = t.GetProperties();
                DataTable dataTable = new DataTable();
                dataTable = ListToDataTable(modelList);
                dataTable.TableName = t.Name;
                return Update(dataTable, fieds) == modelList.Count;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="fieds">要更新的字段列表 多个字段之间用英文逗号分隔</param>
        public static int Update(DataTable table, string fieds)
        {
            int count = 0;
            MetaTable metaTable = GetMetas().Tables[table.TableName];
            SqlConnection conn = new SqlConnection(DataBaseConfig.ConnectionString);
            try
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(string.Format("select * from [{0}] where 1=0", table.TableName), conn);
                SqlCommandBuilder command = new SqlCommandBuilder(adapter);
                command.ConflictOption = ConflictOption.OverwriteChanges;
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("update [{0}] set ", table.TableName);
                string[] fiedArry = fieds.Split(',');
                for (int i = 0; i < fiedArry.Length; i++)
                {
                    strSql.AppendFormat(" {0}=@{0} ", fiedArry[i]);
                    if (i < fiedArry.Length - 1)
                    {
                        strSql.Append(",");
                    }
                }
                strSql.AppendFormat(" where  id=@id ");
                adapter.UpdateCommand = new SqlCommand(strSql.ToString(), conn);
                foreach (string item in fiedArry)
                {
                    adapter.UpdateCommand.Parameters.Add("@" + item, metaTable.Columns[item], 4000, item);
                }
                adapter.UpdateCommand.Parameters.Add("@id", SqlDbType.BigInt, 20, "id");
                table.AcceptChanges();
                foreach (DataRow row in table.Rows)
                {
                    row.SetModified();
                }
                count = adapter.Update(table);
                adapter.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public static bool Delete<T>(T model) where T : new()
        {
            SQLHelper sqlHelper = new SQLHelper();
            try
            {
                sqlHelper.Open();
                List<SqlParameter> parameters = new List<SqlParameter>();
                Type t = model.GetType();
                PropertyInfo[] properties = t.GetProperties();
                MetaTable metaTable = GetMetas().Tables[t.Name];
                StringBuilder strSql = new StringBuilder();
                strSql.Append(string.Format("delete from [{0}] ", metaTable.Name));
                int paramIndex = 0; //SqlParameter 参数下标
                if (metaTable.Keys.Count == 0 || metaTable.Keys[0].Count == 0)
                {
                    throw new Exception("没有主键，不能执行model删除");
                }
                int keyIndex = 0;
                foreach (string key in metaTable.Keys[0])
                {
                    if (keyIndex == 0)
                    {
                        strSql.AppendFormat(" where {0} = @{0}", key);
                    }
                    else
                    {
                        strSql.AppendFormat(" and {0} = @{0}", key);
                    }
                    keyIndex++;
                    parameters.Add(new SqlParameter(string.Format("@{0}", key), metaTable.Columns[key]));


                    PropertyInfo p = null;
                    foreach (PropertyInfo item in properties)
                    {
                        if (item.Name == key)
                        {
                            p = item;
                            break;
                        }
                    }
                    parameters[paramIndex++].Value = p.GetValue(model, null);
                }
                return sqlHelper.ExecuteNonQuery(strSql.ToString(), parameters.ToArray()) > 0;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                sqlHelper.Close();
            }
        }
        /// <summary>
        /// 删除一条数据 使用事务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">实体</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public static bool Delete<T>(T model, DalTransaction tran) where T : new()
        {
            SQLHelper sqlHelper = new SQLHelper(tran.Connection);
            try
            {
                sqlHelper.Open(tran.Connection);
                List<SqlParameter> parameters = new List<SqlParameter>();
                Type t = model.GetType();
                PropertyInfo[] properties = t.GetProperties();
                MetaTable metaTable = GetMetas().Tables[t.Name];
                StringBuilder strSql = new StringBuilder();
                strSql.Append(string.Format("delete from [{0}] ", metaTable.Name));
                int paramIndex = 0; //SqlParameter 参数下标
                if (metaTable.Keys.Count == 0 || metaTable.Keys[0].Count == 0)
                {
                    throw new Exception("没有主键，不能执行model删除");
                }
                int keyIndex = 0;
                foreach (string key in metaTable.Keys[0])
                {
                    if (keyIndex == 0)
                    {
                        strSql.AppendFormat(" where {0} = @{0}", key);
                    }
                    else
                    {
                        strSql.AppendFormat(" and {0} = @{0}", key);
                    }
                    keyIndex++;
                    parameters.Add(new SqlParameter(string.Format("@{0}", key), metaTable.Columns[key]));


                    PropertyInfo p = null;
                    foreach (PropertyInfo item in properties)
                    {
                        if (item.Name == key)
                        {
                            p = item;
                            break;
                        }
                    }
                    parameters[paramIndex++].Value = p.GetValue(model, null);
                }
                return sqlHelper.ExecuteNonQuery(strSql.ToString(), parameters.ToArray(), tran.Transaction) > 0;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="modelList">实体列表</param>
        public static bool DeleteList<T>(List<T> modelList)
        {
            try
            {
                if (modelList.Count == 0)
                {
                    return false;
                }
                Type t = modelList[0].GetType();
                PropertyInfo[] properties = t.GetProperties();
                DataTable dataTable = new DataTable();
                dataTable = ListToDataTable(modelList);
                dataTable.TableName = t.Name;
                return Delete(dataTable) == modelList.Count;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="table">数据表</param>
        public static int Delete(DataTable table)
        {
            int count = 0;
            SqlConnection conn = new SqlConnection(DataBaseConfig.ConnectionString);
            try
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(string.Format("select * from [{0}] where 1=0", table.TableName), conn);
                SqlCommandBuilder command = new SqlCommandBuilder(adapter);
                command.ConflictOption = ConflictOption.OverwriteChanges;
                command.SetAllValues = true;
                table.AcceptChanges();
                foreach (DataRow row in table.Rows)
                {
                    row.Delete();
                }
                count = adapter.Update(table);
                adapter.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }
        /// <summary>
        /// 获取表中所有记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetList<T>() where T : new()
        {
            return GetList<T>("", null);
        }
        /// <summary>
        /// 获取实体List 根据where 后的sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"> where后面的sql</param>
        /// <param name="parList">sql参数 </param>
        /// <returns></returns>
        public static List<T> GetList<T>(string where, List<SqlParameter> parList) where T : new()
        {

            T model = new T();
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendFormat("select*from [{0}]", model.GetType().Name);
            if (!string.IsNullOrEmpty(where))
            {
                sbSql.AppendFormat(" where {0}", where);
            }
            return GetListBySql<T>(sbSql.ToString(), parList);
        }
        /// <summary>
        /// 获取实体List 根据完整sql 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"> 完整的sql语句</param>
        /// <param name="parList">sql参数 </param>
        /// <returns></returns>
        public static List<T> GetListBySql<T>(string sql, List<SqlParameter> parList) where T : new()
        {

            DataTable dt = ExecuteDataTable(sql, parList);
            if (dt.Rows.Count > 0)
            {
                return DataTableToModelList<T>(dt);
            }
            else
            {
                return new List<T>();
            }

            //SQLHelper sqlHelper = new SQLHelper();
            //try
            //{
            //    sqlHelper.Open();
            //    if (parList == null)
            //    {
            //        parList = new List<SqlParameter>();
            //    }
            //    DataSet ds = sqlHelper.ExecuteDataSet(sql, parList.ToArray());
            //    return DataTableToModelList<T>(ds.Tables[0]);

            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}
            //finally
            //{
            //    sqlHelper.Close();

            //}
        }
        /// <summary>
        /// 获取实体List 获取前几行 
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="top">前几行</param>
        /// <param name="where">where 后的 sql 可为空</param>
        /// <param name="orderBy">order by 后的sql 可为空</param>
        /// <param name="parList">sql参数 可为空</param>
        /// <returns></returns>
        public static List<T> GetListTop<T>(int top, string where, string orderBy, List<SqlParameter> parList) where T : new()
        {

            T model = new T();
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendFormat("select top {0} * from [{1}]", top, model.GetType().Name);
            if (!string.IsNullOrEmpty(where))
            {
                sbSql.AppendFormat(" where {0}", where);
            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                sbSql.AppendFormat(" order by {0}", orderBy);
            }
            return GetListBySql<T>(sbSql.ToString(), parList);
        }
        /// <summary>
        /// 获取列表数据 分页
        /// </summary>
        /// <typeparam name="T">表或者视图</typeparam>
        /// <param name="where">where 后面的sql 可为空</param>
        /// <param name="orderBy">order by 后面的sql 不可为空</param>
        /// <param name="parList">Sql参数列表</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页取几条数据</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns></returns>
        public static List<T> GetListPage<T>(string where, string orderBy, List<SqlParameter> parList, int pageIndex, int pageSize, out int totalCount, out int pageCount) where T : new()
        {

            totalCount = 0;
            pageCount = 0;
            T model = new T();
            string tableName = model.GetType().Name;
            StringBuilder sbSql = new StringBuilder();
            if (string.IsNullOrEmpty(where))
            {
                where = " 1=1";
            }
            if (string.IsNullOrEmpty(orderBy))
            {
                //throw new Exception("orderBy 参数不能为空 orderBy参数格式为 字段 asc 或 字段 desc");
                //orderBy= GetMetas().Tables[tableName].Keys[0][0]+" desc";
                orderBy = model.GetType().GetProperties()[0].Name + " desc";
            }
            sbSql.AppendFormat("select top(select {0}) *", pageSize);
            sbSql.AppendFormat(" from(select row_number() over(order by {0}) as rownumber,(select count(1) from {1} where {2}) as total_count_page, *", orderBy, tableName, where);
            sbSql.AppendFormat(" from [{0}]) temp_row where {1}", tableName, where);
            sbSql.AppendFormat(" and rownumber> ({0} - 1) * {1};", pageIndex, pageSize);

            if (parList == null)
            {
                parList = new List<SqlParameter>();
            }
            DataTable dt = ExecuteDataTable(sbSql.ToString(), parList);

            if (dt != null && dt.Rows.Count > 0)
            {
                totalCount = (int)dt.Rows[0]["total_count_page"];
                pageCount = (int)Math.Ceiling((double)totalCount / (double)pageSize);
            }
            return DataTableToModelList<T>(dt);

            //SQLHelper sqlHelper = new SQLHelper();
            //try
            //{
            //    if (parList == null)
            //    {
            //        parList = new List<SqlParameter>();
            //    }
            //    sqlHelper.Open();
            //    DataSet ds = sqlHelper.ExecuteDataSet(sbSql.ToString(), parList.ToArray());
            //    if (ds!=null&& ds.Tables.Count>0&& ds.Tables[0].Rows.Count>0)
            //    {
            //        totalCount = (int)ds.Tables[0].Rows[0]["total_count_page"];
            //        pageCount = (int)Math.Ceiling((double)totalCount / (double)pageSize);
            //    }
            //    return DataSetToModelList<T>(ds);

            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}
            //finally
            //{
            //    sqlHelper.Close();

            //}

        }
        /// <summary>
        /// 执行Sql 返回受影响的行数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, List<SqlParameter> parList)
        {

            SQLHelper sqlHelper = new SQLHelper();
            try
            {
                sqlHelper.Open();
                if (parList == null)
                {
                    parList = new List<SqlParameter>();
                }
                // 执行SQL查询
                return sqlHelper.ExecuteNonQuery(sql, parList.ToArray());

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                sqlHelper.Close();

            }
        }
        /// <summary>
        /// 执行Sql 返回受影响的行数 使用事务
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parList">Sql参数列表</param>
        /// <returns></returns>
        public static int ExecuteNonQueryTransaction(string sql, List<SqlParameter> parList)
        {
            SQLHelper sqlHelper = new SQLHelper();
            try
            {
                sqlHelper.Open();
                sqlHelper.BeginTran();
                // 执行SQL查询
                if (parList == null)
                {
                    parList = new List<SqlParameter>();
                }
                int result = sqlHelper.ExecuteNonQuery(sql, parList.ToArray());
                sqlHelper.CommitTran();
                return result;
            }
            catch (Exception ex)
            {
                sqlHelper.RollbackTran();
                throw ex;
            }
            finally
            {
                sqlHelper.Close();

            }
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果的第一行第一列
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="parList">Sql参数列表</param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, List<SqlParameter> parList)
        {

            SQLHelper sqlHelper = new SQLHelper();
            try
            {
                sqlHelper.Open();
                if (parList == null)
                {
                    parList = new List<SqlParameter>();
                }
                // 执行SQL查询
                return sqlHelper.ExecuteScalar(sql, parList.ToArray());


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                sqlHelper.Close();

            }
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果的第一行第一列
        /// </summary>
        /// <param name="conn">数据库连接字符串</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="parList">Sql参数列表</param>
        /// <returns></returns>
        public static object ExecuteScalar(string conn,string sql, List<SqlParameter> parList)
        {

            SQLHelper sqlHelper = new SQLHelper(conn);
            try
            {
                sqlHelper.Open();
                if (parList == null)
                {
                    parList = new List<SqlParameter>();
                }
                // 执行SQL查询
                return sqlHelper.ExecuteScalar(sql, parList.ToArray());


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                sqlHelper.Close();

            }
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果的第一行第一列 使用事务
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="parList">Sql参数列表</param>
        /// <returns></returns>
        public static object ExecuteScalarTransaction(string sql, List<SqlParameter> parList)
        {

            SQLHelper sqlHelper = new SQLHelper();
            try
            {
                sqlHelper.Open();
                sqlHelper.BeginTran();
                if (parList == null)
                {
                    parList = new List<SqlParameter>();
                }
                // 执行SQL查询
                object result = sqlHelper.ExecuteScalar(sql, parList.ToArray());
                sqlHelper.CommitTran();
                return result;
            }
            catch (Exception ex)
            {
                sqlHelper.RollbackTran();
                throw ex;
            }
            finally
            {
                sqlHelper.Close();

            }
        }
        /// <summary>
        /// 执行查询,返回DataSet
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="parList">Sql参数列表</param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string sql, List<SqlParameter> parList)
        {

            SQLHelper sqlHelper = new SQLHelper();
            try
            {
                sqlHelper.Open();
                if (parList == null)
                {
                    parList = new List<SqlParameter>();
                }
                // 执行SQL查询
                return sqlHelper.ExecuteDataSet(sql, parList.ToArray());


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                sqlHelper.Close();

            }
        }
        /// <summary>
        /// 执行查询,返回DataSet
        /// </summary>
        /// <param name="conn">数据库连接字符串</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="parList">Sql参数列表</param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string conn,string sql, List<SqlParameter> parList)
        {

            SQLHelper sqlHelper = new SQLHelper(conn);
            try
            {
                sqlHelper.Open();
                if (parList == null)
                {
                    parList = new List<SqlParameter>();
                }
                // 执行SQL查询
                return sqlHelper.ExecuteDataSet(sql, parList.ToArray());


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                sqlHelper.Close();

            }
        }
        /// <summary>
        /// 执行查询,返回DataTable
        /// </summary>
        /// <param name="sql">完整sql 语句</param>
        /// <param name="parList">Sql参数列表</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql, List<SqlParameter> parList)
        {
            return ExecuteDataSet(sql, parList).Tables[0];
        }
        /// <summary>
        /// 执行查询,返回DataTable
        /// </summary>
        /// <param name="conn">数据库连接字符串</param>
        /// <param name="sql">完整sql 语句</param>
        /// <param name="parList">Sql参数列表</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string conn,string sql, List<SqlParameter> parList)
        {
            return ExecuteDataSet(conn,sql, parList).Tables[0];
        }
        /// <summary>
        ///查询数据是否存在
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parList">Sql参数列表</param>
        /// <returns></returns>
        public static bool Exists(string sql, List<SqlParameter> parList)
        {

            SQLHelper sqlHelper = new SQLHelper();
            try
            {
                if (parList == null)
                {
                    parList = new List<SqlParameter>();
                }
                sqlHelper.Open();
                // 执行SQL查询
                return sqlHelper.Exists(sql, parList.ToArray());


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                sqlHelper.Close();

            }
        }
        /// <summary>
        ///检查指定记录是否存在
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public static bool Exists(object model)
        {
            Type t = model.GetType();
            MetaTable metaTable = GetMetas().Tables[t.Name];
            PropertyInfo[] properties = t.GetProperties();
            Dictionary<string, string> columns = new Dictionary<string, string>();
            foreach (string keyCol in metaTable.Keys[0])
            {
                foreach (PropertyInfo p in properties)
                {
                    if (keyCol == p.Name)
                    {
                        columns.Add(keyCol, p.GetValue(model, null).ToString());
                        break;
                    }
                }
            }
            return Exists(metaTable.Name, columns);


        }
        /// <summary>
        /// 检查指定记录是否存在
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        private static bool Exists(string tableName, Dictionary<string, string> columns)
        {
            return Exists(tableName, columns, "and");
        }
        /// <summary>
        /// 判断组合字段对应的记录是否存在,可以选and， or操作
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <param name="orand"></param>
        /// <returns></returns>
        private static bool Exists(string tableName, Dictionary<string, string> columns, string orand)
        {
            SQLHelper sqlHelper = new SQLHelper();
            try
            {
                sqlHelper.Open();
                MetaTable metaTable = GetMetas().Tables[tableName];
                StringBuilder strSql = new StringBuilder();
                List<SqlParameter> parameters = new List<SqlParameter>();
                strSql.AppendFormat("select count(1) from [{0}]", metaTable.Name);
                int i = 0;
                foreach (KeyValuePair<string, string> kv in columns)
                {
                    if (i == 0)
                    {
                        strSql.AppendFormat(" where {0}=@{0} ", kv.Key);

                    }
                    else
                    {
                        strSql.AppendFormat(" {0} {1}=@{1} ", orand, kv.Key);
                    }
                    parameters.Add(new SqlParameter(string.Format("@{0}", kv.Key), metaTable.Columns[kv.Key]));
                    parameters[i++].Value = kv.Value;
                }
                return sqlHelper.Exists(strSql.ToString(), parameters.ToArray());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                sqlHelper.Close();
            }

        }
        /// <summary>
        /// 判断是否存在某表的某个列
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        public bool ColumnExists(string tableName, string columnName)
        {

            SQLHelper sqlHelper = new SQLHelper();
            try
            {
                sqlHelper.Open();
                // 执行SQL查询
                return sqlHelper.ColumnExists(tableName, columnName);


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                sqlHelper.Close();

            }
        }
        /// <summary>
        /// 锁定对象
        /// </summary>
        private static object lockObj = new object();
        /// <summary>
        /// 数据库结构
        /// </summary>
        private static MetaTables MetaTables = null;
        /// <summary>
        /// 清空数据库结构缓存
        /// </summary>
        public static void ClearMetas() 
        {
            MetaTables = null;
        }
        /// <summary>
        /// 获取数据库结构缓存
        /// </summary>
        /// <returns></returns>
        public static MetaTables GetMetas()
        {

            if (MetaTables == null)
            {
                lock (lockObj)
                {
                    if (MetaTables == null)
                    {
                        SQLHelper sqlHelper = new SQLHelper();
                        MetaTables = new MetaTables();
                        try
                        {
                            sqlHelper.Open();
                            DataTable dsTable = sqlHelper.ExecuteDataSet("select name from sysobjects where (xtype = 'U') order by name ").Tables[0];
                            DataTable dsFieldTable = sqlHelper.ExecuteDataSet(@"select T0.name as 'colname', T2.name as 'coltype' ,T1.name 'tablename' from syscolumns T0 
                    							inner join sysobjects T3 on T3.xtype = 'U'
                                                inner join sysobjects T1 on T0.id = T1.id and T1.name = T3.name
                                                inner join systypes T2 on T0.xtype = T2.xtype and T2.name <> 'sysname' 
                                                order by T1.name,T0.colid ").Tables[0];
                            DataTable dsKeyTable = sqlHelper.ExecuteDataSet(@"select T0.indid as 'keyindex', T2.name as 'colname',T1.name 'tablename' from sysindexkeys T0 
                                                join sysobjects T1 on T0.id = T1.id and T1.xtype = 'U'
                                                join sys.syscolumns T2 on T0.colid = T2.colid and T1.id = T2.id
                                                order by T1.name,T0.indid").Tables[0];
                            foreach (DataRow dsRow in dsTable.Rows)
                            {
                                MetaTable metaTable = new MetaTable(dsRow[0].ToString());
                                string tableName = dsRow[0].ToString();
                                DataRow[] rowFields = dsFieldTable.Select(string.Format(" tablename='{0}' ", tableName));
                                DataRow[] rowKeys = dsKeyTable.Select(string.Format(" tablename='{0}' ", tableName));
                                foreach (DataRow nowField in rowFields)
                                {
                                    metaTable.Columns.Add(nowField["colname"].ToString(), GetColType(nowField["coltype"].ToString()));
                                }
                                if (rowKeys.Length > 0)
                                {
                                    int indId = int.Parse(rowKeys[0]["keyindex"].ToString());
                                    List<string> key = new List<string>();
                                    foreach (DataRow nowKey in rowKeys)
                                    {
                                        int keyIndex = int.Parse(nowKey["keyindex"].ToString());
                                        if (keyIndex == indId)
                                        {
                                            key.Add(nowKey["colname"].ToString());
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    metaTable.Keys.Add(key);
                                }
                                MetaTables.Tables.Add(tableName, metaTable);
                            }


                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            sqlHelper.Close();
                        }

                    }

                }

            }
            return MetaTables;

        }
        /// <summary>
        /// 获取数据库字段对应的 SqlDbType
        /// </summary>
        /// <param name="colType">数据库字段类型</param>
        /// <returns></returns>
        private static SqlDbType GetColType(string colType)
        {
            switch (colType)
            {
                case "image":
                    return SqlDbType.Image;
                case "text":
                    return SqlDbType.Text;
                case "uniqueidentifier":
                    return SqlDbType.UniqueIdentifier;
                case "date":
                    return SqlDbType.Date;
                case "time":
                    return SqlDbType.Time;
                case "datetime2":
                    return SqlDbType.DateTime2;
                case "datetimeoffset":
                    return SqlDbType.DateTimeOffset;
                case "tinyint":
                    return SqlDbType.TinyInt;
                case "smallint":
                    return SqlDbType.SmallInt;
                case "int":
                    return SqlDbType.Int;
                case "smalldatetime":
                    return SqlDbType.SmallDateTime;
                case "real":
                    return SqlDbType.Real;
                case "money":
                    return SqlDbType.Money;
                case "datetime":
                    return SqlDbType.DateTime;
                case "float":
                    return SqlDbType.Float;
                case "sql_variant":
                    return SqlDbType.Variant;
                case "ntext":
                    return SqlDbType.NText;
                case "bit":
                    return SqlDbType.Bit;
                case "decimal":
                    return SqlDbType.Decimal;
                //case "numeric":
                //return ;
                case "smallmoney":
                    return SqlDbType.SmallMoney;
                case "bigint":
                    return SqlDbType.BigInt;
                //case "hierarchyid":
                //return ;
                //case "geometry":
                //return ;
                //case "geography":
                //return ;
                case "varbinary":
                    return SqlDbType.VarBinary;
                case "varchar":
                    return SqlDbType.VarChar;
                case "binary":
                    return SqlDbType.Binary;
                case "char":
                    return SqlDbType.Char;
                case "timestamp":
                    return SqlDbType.Timestamp;
                case "nvarchar":
                    return SqlDbType.NVarChar;
                case "nchar":
                    return SqlDbType.NChar;
                case "xml":
                    return SqlDbType.Xml;
                default:
                    return SqlDbType.NVarChar;
            }
        }
        /// <summary>
        /// DataSet转成List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ds">DataSet</param>
        /// <returns></returns>
        private static List<List<T>> DataSetToModelList<T>(DataSet ds) where T : new()
        {
            List<List<T>> result = new List<List<T>>();
            foreach (DataTable dt in ds.Tables)
            {
                result.Add(DataTableToModelList<T>(dt));
            }
            return result;
        }
        /// <summary>
        /// DataTable转成List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ds">DataTable</param>
        /// <returns></returns>
        private static List<T> DataTableToModelList<T>(DataTable dt) where T : new()
        {
            T m = new T();
            Type type = m.GetType();
            List<T> modelList = new List<T>();
            PropertyInfo[] properties = type.GetProperties();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                T model = new T();
                foreach (DataColumn dc in dt.Columns)
                {
                    PropertyInfo pi = null;
                    foreach (PropertyInfo item in properties)
                    {
                        if (item.Name.ToLower() == dc.ColumnName.ToLower())
                        {
                            pi = item;
                        }

                    }
                    if (pi != null && dt.Rows[i][pi.Name] != DBNull.Value)
                    {
                        if (!pi.CanWrite) continue;
                        try
                        {

                            pi.SetValue(model, ConvertTo(dt.Rows[i][pi.Name], pi.PropertyType), null);
                        }
                        catch (Exception ex)
                        {

                            throw ex;
                        }
                    }
                }
                modelList.Add(model);
            }
            return modelList;
        }
        /// <summary>
        ///List转换成DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">实体集合</param>
        /// <returns></returns>
        private static DataTable ListToDataTable<T>(IList<T> list)
        {
            List<string> propertyNameList = new List<string>();
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    if (propertyNameList.Count == 0)
                    {

                        Type colType = pi.PropertyType;
                        if (colType.IsGenericType && colType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        result.Columns.Add(pi.Name, colType);
                    }
                    else
                    {
                        if (propertyNameList.Contains(pi.Name))
                        {
                            result.Columns.Add(pi.Name, pi.PropertyType);
                        }
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    System.Collections.ArrayList tempList = new System.Collections.ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        if (propertyNameList.Count == 0)
                        {
                            object obj = pi.GetValue(list[i], null);
                            tempList.Add(obj);
                        }
                        else
                        {
                            if (propertyNameList.Contains(pi.Name))
                            {
                                object obj = pi.GetValue(list[i], null);
                                tempList.Add(obj);
                            }
                        }
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }
        /// <summary>
        /// 转换类型
        /// </summary>
        /// <param name="convertibleValue"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static object ConvertTo(object convertibleValue, Type type)
        {
            if (!type.IsGenericType)
            {
                return Convert.ChangeType(convertibleValue, type);
            }
            else
            {
                Type genericTypeDefinition = type.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(Nullable<>))
                {
                    return Convert.ChangeType(convertibleValue, Nullable.GetUnderlyingType(type));
                }
            }
            throw new InvalidCastException(string.Format("Invalid cast from type \"{0}\" to type \"{1}\".", convertibleValue.GetType().FullName, type.FullName));
        }
    }
}
