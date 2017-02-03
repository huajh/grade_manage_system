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
    public partial class NewClassCourse : Form
    {
        public NewClassCourse()
        {
            InitializeComponent();
        }

        private void NewClassCourse_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
            col1.Name = "开课编号hjh";
            col1.HeaderText = "开课编号hjh";
            col2.Name = "课程编号hjh";
            col2.HeaderText = "课程编号hjh";
            col3.Name = "班级编号hjh";
            col3.HeaderText = "班级编号hjh";
            col4.Name = "教师编号hjh";
            col4.HeaderText = "教师编号hjh";
            col5.Name = "上课时间地点hjh";
            col5.HeaderText = "上课时间地点hjh";
            dataGridView1.Columns.Add(col1);
            dataGridView1.Columns.Add(col2);
            dataGridView1.Columns.Add(col3);
            dataGridView1.Columns.Add(col4);
            dataGridView1.Columns.Add(col5);

            sqlConnect cc = new sqlConnect();
            try
            {
                string sql = "select * from 学院hjh";
                SqlDataAdapter da = new SqlDataAdapter(sql, cc.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "学院hjh");

                comboBox1.ValueMember = "学院hjh.学院编号hjh";
                comboBox1.DataSource = ds;
                comboBox1.DisplayMember = "学院hjh.学院名称hjh";
                
                

                sql = "select * from 课程hjh ";
                SqlDataAdapter da2 = new SqlDataAdapter(sql,cc.conn);
                da2.Fill(ds,"课程hjh");

                comboBox4.ValueMember = "课程hjh.课程编号hjh";
                comboBox4.DataSource = ds;
                comboBox4.DisplayMember = "课程hjh.课程名称hjh";
                
               

                sql = "select * from 教师hjh ";
                SqlDataAdapter da3 = new SqlDataAdapter(sql, cc.conn);
                da3.Fill(ds, "教师hjh");

                comboBox5.ValueMember = "教师hjh.教师编号hjh";
                comboBox5.DataSource = ds;
                comboBox5.DisplayMember = "教师hjh.姓名hjh";
                
                

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";


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
            comboBox3.Text = "";
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("无可选专业！");
                return;
            }
            sqlConnect cc = new sqlConnect();
            try
            {
                
                string sql = "select * from 专业hjh where 学院编号hjh = '" + comboBox1.SelectedValue.ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(sql, cc.conn);

                DataSet ds = new DataSet();
                da.Fill(ds, "专业hjh");
                comboBox2.ValueMember = "专业hjh.专业编号hjh";
                comboBox2.DataSource = ds;
                comboBox2.DisplayMember = "专业hjh.专业名称hjh";

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
        
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Text = "";
            if (comboBox2.SelectedValue == null)
            {
                MessageBox.Show("无可选班级！");
                return;
            }
            sqlConnect cc = new sqlConnect();
            string sql = "select 班级序号hjh,班级编号hjh from 班级hjh where 专业编号hjh = '" + comboBox2.SelectedValue.ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, cc.conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "班级hjh");
            comboBox3.ValueMember = "班级hjh.班级编号hjh";
            comboBox3.DataSource = ds;
            comboBox3.DisplayMember = "班级hjh.班级序号hjh";
            cc.closeConnect();
        }
        /*
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedValue != null)
            {
                textBox2.Text = comboBox3.SelectedValue.ToString().Trim();
            }
        }
         * */


        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedValue != null)
            {
                textBox1.Text = comboBox4.SelectedValue.ToString().Trim();
            }
        }
        
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedValue != null)
            {
                textBox3.Text = comboBox5.SelectedValue.ToString().Trim();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                MessageBox.Show("开课编号不能为空！");
                return;
            }
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text =="")
            {
                MessageBox.Show("信息不足，不可添加！");
                return;
            }
            string sql = "select * from 开课hjh where 开课编号hjh = '"+textBox5.Text+"'";
            sqlConnect cc = new sqlConnect();
            DataSet ds = cc.GetDataSet(sql);

            if (ds.Tables[0].Rows.Count != 0)
            {
                MessageBox.Show("已经存在该开课编号！");
                return;
            }

            ds.Clear();

            string sql2 = "select * from 开课hjh where 班级编号hjh = '"+textBox2.Text+"' "+
                " and 课程编号hjh = '"+textBox1.Text+"'";
            ds = cc.GetDataSet(sql2);
            if(ds.Tables[0].Rows.Count != 0)
            {
                MessageBox.Show("发现该班级已经开设此课程！不能重复添加！");
                return;
            }
            string[] row = {textBox5.Text,textBox1.Text,textBox2.Text,textBox3.Text,textBox4.Text};
            dataGridView1.Rows.Add(row);
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
                string sql2 = "insert into 开课hjh(开课编号hjh,课程编号hjh,班级编号hjh,教师编号hjh,上课时间地点hjh) values( ";
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
    }
}
