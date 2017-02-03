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
    public partial class TeacherQueryGrade : Form
    {
        private string tp="";
        private string num="";

        public TeacherQueryGrade(string tp, string num)
        {
            InitializeComponent();
            this.tp += tp;
            this.num += num;
        }

        private void TeacherQueryGrade_Load(object sender, EventArgs e)
        {
            label2.Text = this.num;
            this.Text += "(教师:" + this.num + ")";
            sqlConnect cc = new sqlConnect();
            string sql = "select distinct b.课程名称hjh ,b.课程编号hjh from 开课hjh a,课程hjh b " +
                        " where a.课程编号hjh = b.课程编号hjh and a.教师编号hjh = '" + this.num + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, cc.conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "课程hjh");
            comboBox1.ValueMember = "课程hjh.课程编号hjh";
            comboBox1.DataSource = ds;
            comboBox1.DisplayMember = "课程hjh.课程名称hjh";
           
            cc.closeConnect();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlConnect cc = new sqlConnect();
            try
            {
                string sql = " select b.班级编号hjh,c.专业名称hjh,b.班级序号hjh " +
                             " from 班级hjh b,专业hjh c " +
                             " where b.专业编号hjh = c.专业编号hjh and b.班级编号hjh in( " +
                             "  select  a.班级编号hjh " +
                             "  from 开课hjh a " +
                             "  where a.课程编号hjh = '" + comboBox1.SelectedValue.ToString().Trim() +
                             "' and a.教师编号hjh = '" + this.num + "')";

                SqlDataAdapter da = new SqlDataAdapter(sql, cc.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "班级hjh");
                comboBox2.ValueMember = "班级hjh.班级编号hjh";
                comboBox2.DataSource = ds;
                comboBox2.DisplayMember = "班级hjh.专业名称hjh" + "班级hjh.班级序号hjh";
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {   cc.closeConnect();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("请选择一个班级！");
                return;
            }
            sqlConnect cc = new sqlConnect();

            string sql = "select a.开课编号hjh,c.学生编号hjh,c.姓名hjh,b.成绩hjh " +
                        " from 开课hjh a,选修hjh b,学生hjh c" +
                        "  where  a.开课编号hjh = b.开课编号hjh and b.学生编号hjh = c.学生编号hjh " +
                        " and a.班级编号hjh='" + comboBox2.SelectedValue.ToString().Trim() +
                        "' and a.课程编号hjh ='" + comboBox1.SelectedValue.ToString().Trim() +
                        "' and a.教师编号hjh ='" + this.num + "' ";
            try
            {
                cc.BindDataGridView(dataGridView1, sql);
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[3].ReadOnly = true;
                double total = 0;
                int stuNum = dataGridView1.RowCount-1;
                int unPassed = 0;
                for (int i = 0; i < stuNum; i++)
                {
                    int tmp = int.Parse(dataGridView1.Rows[i].Cells["成绩hjh"].Value.ToString());
                    if (tmp<60)
                        unPassed++;
                    total+=(double)tmp;
                }
                
                total /= stuNum;

                label8.Text = total.ToString();
                label9.Text = stuNum.ToString();
                label10.Text = unPassed.ToString();


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
            TeacherUnPassedTable frm = new TeacherUnPassedTable(tp, num, comboBox2.SelectedValue.ToString().Trim(),
                comboBox1.SelectedValue.ToString().Trim());
            frm.Show();
        }
    }
}
