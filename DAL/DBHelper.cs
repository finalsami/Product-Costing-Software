using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DBHelper
    {
        public bool IfExistQuery(string sql)
        {
            this.TextCommand(sql);
            try
            {
                if (this.ExecuteScalar() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public long ReturnVal(string sql)
        {
            this.TextCommand(sql);
            return this.ExecuteScalar();
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        String strConn = String.Empty;

        SqlConnection _Connection;
        public SqlConnection Connection
        {
            get { return _Connection; }
            set { _Connection = value; }
        }

        SqlCommand _Command;
        public SqlCommand Command
        {
            get { return _Command; }
            set { _Command = value; }
        }

        SqlDataReader _DataReader;
        public SqlDataReader DataReader
        {
            get { return _DataReader; }
            set { _DataReader = value; }
        }

        SqlTransaction _Transaction;
        public SqlTransaction Transaction
        {
            get { return _Transaction; }
            set { _Transaction = value; }
        }
       
        public DBHelper()
        {
            strConn = ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ToString();
            Connection = new SqlConnection(strConn);
        }
        public void AddParameter(String ParameterName, object value)
        {
            SqlParameter sqlPara = new SqlParameter();
            Command.Parameters.AddWithValue(ParameterName, value);
        }

        public void TextCommand(String CommandText)
        {
            if (this._Connection == null)
            {
                Connection = new SqlConnection(strConn);
            }
            if (this._Command == null)
            {
                Command = new SqlCommand(CommandText, Connection, _Transaction);
                Command.CommandType = CommandType.Text;
            }
            else
            {
                _Command.Parameters.Clear();
                this._Command.CommandText = CommandText;
                this._Command.CommandType = CommandType.Text;
                if (_Transaction != null)
                {
                    _Command.Transaction = _Transaction;
                }
            }
        }

        public void SpCommand(String SpName)
        {
            if (this._Connection == null)
            {
                Connection = new SqlConnection(strConn);
            }
            if (this._Command == null)
            {
                Command = new SqlCommand(SpName, Connection, _Transaction);
                Command.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                _Command.Parameters.Clear();
                this._Command.CommandText = SpName;
                this._Command.CommandType = CommandType.StoredProcedure;
                if (_Transaction != null)
                {
                    _Command.Transaction = _Transaction;
                }
            }
        }

        public Int64 ExecuteNonQuery()
        {
            try
            {
                Int64 iReturnVal = 0;

                if (Command.Connection.State == ConnectionState.Closed)
                    Command.Connection.Open();

                iReturnVal = Command.ExecuteNonQuery();

                return iReturnVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Command.Connection.State == ConnectionState.Open)
                    Command.Connection.Close();
            }
        }

        public Int64 ExecuteScalar()
        {
            try
            {
                //Command = new SqlCommand(CommandText, Connection);
                //Command.CommandType = CommandType;

                Int64 iReturnVal = 0;

                if (Command.Connection.State == ConnectionState.Closed)
                    Command.Connection.Open();

                iReturnVal = Convert.ToInt64(Command.ExecuteScalar());

                return iReturnVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Command.Connection.State == ConnectionState.Open)
                    Command.Connection.Close();
            }
        }

        public DataSet GetDataSet()
        {
            try
            {
                //Command = new SqlCommand(CommandText, Connection);
                //Command.CommandType = CommandType;

                DataSet ds = new DataSet();
                SqlDataAdapter adpt = new SqlDataAdapter(Command);

                if (Command.Connection.State == ConnectionState.Closed)
                    Command.Connection.Open();
                adpt.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Command.Connection.State == ConnectionState.Open)
                    Command.Connection.Close();
            }
        }

        public DataTable GetDataTable()
        {
            try
            {
                //Command = new SqlCommand(CommandText, Connection);
                //Command.CommandType = CommandType;

                DataTable dt = new DataTable();
                SqlDataAdapter adpt = new SqlDataAdapter(Command);

                if (Command.Connection.State == ConnectionState.Closed)
                    Command.Connection.Open();
                adpt.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Command.Connection.State == ConnectionState.Open)
                    Command.Connection.Close();
            }
        }

        public SqlDataReader ExecuteReader()
        {
            try
            {
                SqlDataReader dr = null;

                if (Command.Connection.State == ConnectionState.Closed)
                    Command.Connection.Open();

                dr = Command.ExecuteReader();

                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Command.Connection.State == ConnectionState.Open)
                    Command.Connection.Close();
            }
        }

        public bool IfExist(string TableName, string FieldName, object FieldValue)
        {
            this.TextCommand(string.Format("select count(1) from {0} where {1}=@paramValue", TableName, FieldName));
            this.AddParameter("@paramValue", FieldValue);
            try
            {
                if (this.ExecuteScalar() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public string GetInfo(string colName, string TableName, string whereField, object FilterValue, string DefaultValue)
        {
            this.TextCommand(string.Format("select {0} from {1} where {2} = @Param", colName, TableName, whereField));
            this.AddParameter("@Param", FilterValue);

            try
            {
                DataTable dt = this.GetDataTable();
                if (dt.Rows.Count <= 0)
                {
                    return DefaultValue;
                }
                else
                {
                    DataRow dr = dt.Rows[0];
                    return Convert.ToString(dr[colName]);
                }
            }
            catch
            {
                return DefaultValue;
            }
        }       
        public void Dispose()
        {
            if (Connection != null)
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Dispose();
            }
            if (Command != null)
            {
                Command.Parameters.Clear();
                Command.Dispose();
            }
            if (DataReader != null)
            {
                if (!DataReader.IsClosed)
                    DataReader.Close();
                DataReader.Dispose();
            }
        }

       
    }
}
