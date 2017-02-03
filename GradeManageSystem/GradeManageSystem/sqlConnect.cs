using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Linq;
using System.Text;

namespace GradeManageSystem
{
    class sqlConnect
    {
        public SqlConnection conn = null;
        public sqlConnect()
        {
            try
            {

                conn = new SqlConnection("server=localhost;database=C0901huajunhao;integrated security=SSPI");
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }
        }
        public void closeConnect()
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            conn.Dispose();
        }
        public DataSet GetDataSet(string sql)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            DataSet dataset = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql,conn);
            da.Fill(dataset);
            return dataset;           
        }
        public DataSet BindDataGridView(DataGridView dgv,string sql)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds); 
            dgv.DataSource = ds.Tables[0];
            return ds;
        }
        public void ExecuteNonQuery(string sql)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand Command = new SqlCommand(sql,this.conn);
            Command.ExecuteNonQuery();
        }
    }
}
