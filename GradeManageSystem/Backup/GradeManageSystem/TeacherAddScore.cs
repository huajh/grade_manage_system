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
    public partial class TeacherAddScore : Form
    {
        private string tp="";
        private string num="";
        public TeacherAddScore(string tp, string num)
        {
            InitializeComponent();
            this.tp += tp;
            this.num += num;
        }

        private void TeacherAddScore_Load(object sender, EventArgs e)
        {
            label2.Text = this.num;
            this.Text += "(教师:" +this.num+")";
            sqlConnect cc = new sqlConnect();
            string sql = "select distinct b.课程名称hjh ,b.课程编号hjh from 开课hjh a,课程hjh b " +
                        " where a.课程编号hjh = b.课程编号hjh and a.教师编号hjh = '"+this.num+"'";
            SqlDataAdapter da = new SqlDataAdapter(sql, cc.conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "课程hjh");

            comboBox2.ValueMember = "课程hjh.课程编号hjh";
            comboBox2.DataSource = ds;
            comboBox2.DisplayMember = "课程hjh.课程名称hjh";
            
         
            cc.closeConnect();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlConnect cc = new sqlConnect();

            /*
             * select b.班级编号hjh,c.专业名称hjh,b.班级序号hjh
                from 班级hjh b,专业hjh c
                where b.专业编号hjh = c.专业编号hjh and b.班级编号hjh in(
	                select  a.班级编号hjh
	                from 开课hjh a
	                where a.教师编号hjh = '010101' and a.课程编号hjh = '010001' 
	                )
             * 
             * */

            string sql = " select b.班级编号hjh,c.专业名称hjh,b.班级序号hjh " +
                         " from 班级hjh b,专业hjh c " +
                         " where b.专业编号hjh = c.专业编号hjh and b.班级编号hjh in( " +
                         "  select  a.班级编号hjh " +
                         "  from 开课hjh a " +
                         "  where a.课程编号hjh = '" + comboBox2.SelectedValue.ToString().Trim()+
                         "' and a.教师编号hjh = '" + this.num + "')";

            SqlDataAdapter da = new SqlDataAdapter(sql, cc.conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "班级hjh");

            comboBox4.ValueMember = "班级hjh.班级编号hjh";
            comboBox4.DataSource = ds;
            comboBox4.DisplayMember = "班级hjh.专业名称hjh+班级hjh.班级序号hjh";

            cc.closeConnect();



    



















        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(comboBox2.Text =="" || comboBox4.Text == "")
            {
                MessageBox.Show("请选择一个班级！");
                return;
            }
            sqlConnect cc = new sqlConnect();
            /*
             * 
             * select a.开课编号hjh,c.姓名hjh,b.成绩hjh
             *  from 开课hjh a,选修hjh b,学生hjh c
             *  where  a.开课编号hjh = b.开课编号hjh and b.学生编号hjh = c.学生编号hjh
             *  and a.班级编号hjh='' and a.课程编号hjh ='' and a.教师编号hjh ='' 
             *  
             * */
            string sql = "select a.开课编号hjh,c.学生编号hjh,c.姓名hjh,b.成绩hjh " +
                        " from 开课hjh a,选修hjh b,学生hjh c" +
                        "  where  a.开课编号hjh = b.开课编号hjh and b.学生编号hjh = c.学生编号hjh " +
                        " and a.班级编号hjh='" + comboBox4 .SelectedValue.ToString().Trim()+ 
                        "' and a.课程编号hjh ='"+comboBox2.SelectedValue.ToString().Trim()+
                        "' and a.教师编号hjh ='"+this.num+"' ";
            try
            {
                cc.BindDataGridView(dataGridView1, sql);
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].ReadOnly = true;
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入开课编号！");
                return;
            }
            sqlConnect cc = new sqlConnect();
            string sql2 = "select * from 开课hjh where 开课编号hjh = '" + textBox1.Text 
                +"' and 教师编号hjh = '"+this.num+"'";
            DataSet ds = cc.GetDataSet(sql2);
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("对不起，该课程不存在或不是您的课！");
                return;
            }
            string sql = "select b.开课编号hjh,c.学生编号hjh,c.姓名hjh,b.成绩hjh " +
                        " from 选修hjh b,学生hjh c" +
                        "  where  b.开课编号hjh = '" + textBox1.Text + "'and b.学生编号hjh = c.学生编号hjh ";
            try
            {
                cc.BindDataGridView(dataGridView1, sql);
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].ReadOnly = true;

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
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlConnect cc = new sqlConnect();
            if (dataGridView1.Rows.Count == 1)
            {
                Close();
                return;
            }
            string sql = "";
        //    int count = 0;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells["成绩hjh"].Value.ToString() != "")
                {
         //           count++;
                    string sql2 = " update 选修hjh set 成绩hjh = " + dataGridView1.Rows[i].Cells["成绩hjh"].Value.ToString() +
                        "  where  学生编号hjh = '" + dataGridView1.Rows[i].Cells["学生编号hjh"].Value.ToString() + "'" +
                        " and 开课编号hjh = '" + dataGridView1.Rows[i].Cells["开课编号hjh"].Value.ToString() + "' ";
                    sql += sql2;
                }
            }
            try
            {
                cc.ExecuteNonQuery(sql);
           //     MessageBox.Show("共添加"+count+"个成绩！");
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
