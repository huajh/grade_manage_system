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
    public partial class TeacherQueryPost : Form
    {
        private string tp="";
        private string num="";

        public TeacherQueryPost(string tp, string num)
        {
            InitializeComponent();
            this.tp += tp;
            this.num += num;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TeacherQueryPost_Load(object sender, EventArgs e)
        {
            this.Text += "(教师:" + this.num + ")";
            label2.Text = this.num;

            /*
             * 
             * select a.开课编号hjh,d.课程名称hjh,c.专业名称hjh,b.班级序号hjh,d.学分hjh,d.考核方式hjh,e.开课学期hjh,a.评定等级hjh
from 开课hjh a ,班级hjh b,专业hjh c,课程hjh d ,开设hjh e
where a.教师编号hjh = '010101' and a.班级编号hjh = b.班级编号hjh and b.专业编号hjh = c.专业编号hjh
and a.课程编号hjh = d.课程编号hjh and e.课程编号hjh = a.课程编号hjh and e.专业编号hjh = b.专业编号hjh
             * 
             * 
             * 
             * */
            sqlConnect cc = new sqlConnect();
            string sql = "select a.开课编号hjh,d.课程名称hjh,c.专业名称hjh,b.班级序号hjh,d.学分hjh,d.考核方式hjh,e.开课学期hjh,a.评定等级hjh " +
                        " from 开课hjh a ,班级hjh b,专业hjh c,课程hjh d ,开设hjh e " +
                        " where a.教师编号hjh = '"+this.num+"' and a.班级编号hjh = b.班级编号hjh and b.专业编号hjh = c.专业编号hjh " +
                        " and a.课程编号hjh = d.课程编号hjh and e.课程编号hjh = a.课程编号hjh and e.专业编号hjh = b.专业编号hjh ";
            try
            {
                cc.BindDataGridView(dataGridView1, sql);
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[3].ReadOnly = true;
                dataGridView1.Columns[4].ReadOnly = true;
                dataGridView1.Columns[5].ReadOnly = true;
                dataGridView1.Columns[6].ReadOnly = true;
                dataGridView1.Columns[7].ReadOnly = true;


                string sql2 = "select * from 学院hjh";
                SqlDataAdapter da = new SqlDataAdapter(sql2, cc.conn);
                DataSet ds2 = new DataSet();
                da.Fill(ds2, "学院hjh");

                comboBox2.DisplayMember = "学院hjh.学院名称hjh";
                comboBox2.DataSource = ds2;
                comboBox2.ValueMember = "学院hjh.学院编号hjh";


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
            sqlConnect cc = new sqlConnect();
            string sql = "select a.开课编号hjh,d.课程名称hjh,c.专业名称hjh,b.班级序号hjh,d.学分hjh,d.考核方式hjh,e.开课学期hjh,a.评定等级hjh " +
                        " from 开课hjh a ,班级hjh b,专业hjh c,课程hjh d ,开设hjh e " +
                        " where a.教师编号hjh = '" + this.num + "' and a.班级编号hjh = b.班级编号hjh and b.专业编号hjh = c.专业编号hjh " +
                        " and a.课程编号hjh = d.课程编号hjh and e.课程编号hjh = a.课程编号hjh and e.专业编号hjh = b.专业编号hjh ";
            if(comboBox1.Text !="*")
            {
                sql += " and e.开课学期hjh = '"+comboBox1.Text+"'";
            }
            try
            {
                cc.BindDataGridView(dataGridView1, sql);
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[3].ReadOnly = true;
                dataGridView1.Columns[4].ReadOnly = true;
                dataGridView1.Columns[5].ReadOnly = true;
                dataGridView1.Columns[6].ReadOnly = true;
                dataGridView1.Columns[7].ReadOnly = true;
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

        private void button2_Click(object sender, EventArgs e)
        {
            sqlConnect cc = new sqlConnect();
            if (comboBox3.Text == "")
            {
                MessageBox.Show("请选择一个教师！");
                return;
            }
            string sql = "select a.开课编号hjh,d.课程名称hjh,c.专业名称hjh,b.班级序号hjh,d.学分hjh,d.考核方式hjh,e.开课学期hjh,a.评定等级hjh " +
                        " from 开课hjh a ,班级hjh b,专业hjh c,课程hjh d ,开设hjh e " +
                        " where a.班级编号hjh = b.班级编号hjh and b.专业编号hjh = c.专业编号hjh " +
                        " and a.课程编号hjh = d.课程编号hjh and e.课程编号hjh = a.课程编号hjh and e.专业编号hjh = b.专业编号hjh ";
            if (comboBox3.Text != "*")
            {
                sql += " and a.教师编号hjh = '" + comboBox3.SelectedValue.ToString().Trim() + "' ";
            }
            if (comboBox4.Text != "*")
            {
                sql += " and e.开课学期hjh = '" + comboBox4.Text + "'";
            }
            try
            {
                cc.BindDataGridView(dataGridView2, sql);
                dataGridView2.Columns[0].ReadOnly = true;
                dataGridView2.Columns[1].ReadOnly = true;
                dataGridView2.Columns[2].ReadOnly = true;
                dataGridView2.Columns[3].ReadOnly = true;
                dataGridView2.Columns[4].ReadOnly = true;
                dataGridView2.Columns[5].ReadOnly = true;
                dataGridView2.Columns[6].ReadOnly = true;
                dataGridView2.Columns[7].ReadOnly = true;
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
                MessageBox.Show("该学院目前无教师");
                return;
            }

            sqlConnect cc = new sqlConnect();
            string sql = "select 教师编号hjh,姓名hjh from 教师hjh where 学院编号hjh = '" +
                comboBox2.SelectedValue.ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, cc.conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "教师hjh");

            comboBox3.ValueMember = "教师hjh.教师编号hjh";
            comboBox3.DataSource = ds;
            comboBox3.DisplayMember = "教师hjh.姓名hjh";
            cc.closeConnect();
        }
    }
}
