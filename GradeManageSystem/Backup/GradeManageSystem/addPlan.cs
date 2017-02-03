using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GradeManageSystem
{
    public partial class addplan : Form
    {
        public addplan()
        {
            InitializeComponent();
        }

        private void addPlan_Load(object sender, EventArgs e)
        {

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col1.Name = "专业编号hjh";
            col1.HeaderText = "专业编号hjh";
            col2.Name = "课程编号hjh";
            col2.HeaderText = "课程编号hjh";
            col3.Name = "开课学期hjh";
            col3.HeaderText = "开课学期hjh";

            dataGridView1.Columns.Add(col1);
            dataGridView1.Columns.Add(col2);
            dataGridView1.Columns.Add(col3);

         sqlConnect cc = new sqlConnect();
            try
            {              
                string sql = "select * from 学院hjh";
                SqlDataAdapter da = new SqlDataAdapter(sql, cc.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "学院hjh");
                comboBox1.DataSource = ds;
                comboBox1.DisplayMember = "学院hjh.学院名称hjh";
                comboBox1.ValueMember = "学院hjh.学院编号hjh";
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                cc.closeConnect();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Text = "";
            if(comboBox1.SelectedValue == null)
            {
                MessageBox.Show("无可选专业！");
                return;
            }
            sqlConnect cc = new sqlConnect();
            string sql = "select * from 专业hjh where 学院编号hjh = '"+ comboBox1.SelectedValue.ToString()+"'";
            SqlDataAdapter da = new SqlDataAdapter(sql, cc.conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "专业hjh");
            comboBox2.DataSource = ds;
            comboBox2.DisplayMember = "专业hjh.专业名称hjh";
            comboBox2.ValueMember = "专业hjh.专业编号hjh";
            
            cc.closeConnect();
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Text = "";
            if (comboBox2.SelectedValue == null)
            {
                MessageBox.Show("无可选课程！");
                return;
            }
            sqlConnect cc = new sqlConnect();
            try
            {
                string sql = "select * from 课程hjh where 课程编号hjh not in( select 课程编号hjh from 开设hjh where 专业编号hjh = '" + comboBox2.SelectedValue.ToString().Trim() + "')";

                SqlDataAdapter da = new SqlDataAdapter(sql, cc.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "课程hjh");
                comboBox3.DataSource = ds;
                comboBox3.DisplayMember = "课程hjh.课程名称hjh";
                comboBox3.ValueMember = "课程hjh.课程编号hjh";
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                cc.closeConnect();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox2.SelectedValue == null || comboBox3.SelectedValue ==null)
            {
                MessageBox.Show("信息不足，不可添加！");
                return;
            }
            string[] row = {comboBox2.SelectedValue.ToString(),comboBox3.SelectedValue.ToString(),comboBox4.Text};
             dataGridView1.Rows.Add(row);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sqlConnect cc = new sqlConnect();
            if (dataGridView1.Rows.Count == 1)
            {
                Close();
                return;
            }
            string sql = "";
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                string sql2 = "insert into 开设hjh values( ";
                for (int j = 0; j < dataGridView1.Columns.Count - 1; j++) //dataGridView1.Columns.Count-1
                {
                    sql2 += "'" + dataGridView1.Rows[i].Cells[j].Value.ToString().Trim() + "',";
                }
                sql2 += "'" + dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 1].Value.ToString().Trim() + "');";
                sql += sql2;

            }
            try
            {
                cc.ExecuteNonQuery(sql);
                MessageBox.Show("添加成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                cc.closeConnect();
                Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选中要删除的行！");
                return;
            }
            if (dataGridView1.CurrentRow.Index < 0 || dataGridView1.CurrentRow.Index >= dataGridView1.Rows.Count - 1)
            {
                MessageBox.Show("请选择有效行进行删除！");
                return;
            }
            if (this.dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString() == "")
            {
                MessageBox.Show("请选择一项进行删除！");
                return;
            }
            else
            {
                if (MessageBox.Show("确认删除这一行？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dataGridView1.Rows.Remove(dataGridView1.Rows[dataGridView1.CurrentRow.Index]);
                }
            }
        }
    }
}
