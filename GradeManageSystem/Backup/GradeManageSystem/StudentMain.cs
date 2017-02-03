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

    public partial class StudentMain : Form
    {
        private string tp = "";
        private string num = "";
        public StudentMain(string tp, string num)
        {
            InitializeComponent();
            this.tp += tp;
            this.num += num;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void StudentMain_Load(object sender, EventArgs e)
        {
            label7.Text = this.num;
            this.Text += "(学生:" + this.num + ")";
            string sql = "select * from 学生视图hjh where 学生编号hjh = '"+this.num+"'";
            sqlConnect cc = new sqlConnect();
            try
            {
                DataSet ds = cc.GetDataSet(sql);
                label8.Text = ds.Tables[0].Rows[0]["姓名hjh"].ToString().Trim();
                label9.Text = ds.Tables[0].Rows[0]["专业名称hjh"].ToString().Trim()+"0"+ ds.Tables[0].Rows[0]["班级序号hjh"].ToString().Trim();

                string sql2 = "select * from 学院hjh";
                SqlDataAdapter da = new SqlDataAdapter(sql2, cc.conn);
                DataSet ds2 = new DataSet();
                da.Fill(ds2, "学院hjh");

                comboBox4.DisplayMember = "学院hjh.学院名称hjh";
                comboBox4.DataSource = ds2;                
                comboBox4.ValueMember = "学院hjh.学院编号hjh";
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.ToString());

            }finally
            {
                cc.closeConnect();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about frm = new about();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            glyInfo frm = new glyInfo(tp, num);
            frm.ShowDialog();
            frm.Dispose();
        }

        private void StudentMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ProfDevelopPlan frm = new ProfDevelopPlan(this.tp,this.num);
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlConnect cc = new sqlConnect();

            string sql = " select h.课程名称hjh,i.姓名hjh '授课教师hjh',h.学分hjh,h.考核方式hjh,e.开课学期hjh,g.成绩hjh " +
                        " from 学生hjh b,班级hjh c,开设hjh e,开课hjh f,选修hjh g,课程hjh h,教师hjh i " +
                        " where b.学生编号hjh = '" + this.num + "' " +
                        " and b.班级编号hjh = c.班级编号hjh and c.专业编号hjh = e.专业编号hjh " +
                        " and e.课程编号hjh = f.课程编号hjh and b.班级编号hjh = f.班级编号hjh  " +
                        " and g.开课编号hjh = f.开课编号hjh and g.学生编号hjh = b.学生编号hjh " +
                        " and f.课程编号hjh = h.课程编号hjh and f.教师编号hjh = i.教师编号hjh ";
            if(checkBox1.Checked)
            {
                sql+=" and g.成绩hjh >=0 ";
            }
            if(comboBox1.Text.ToString().Trim()!="*")
            {
                sql+=" and e.开课学期hjh = '"+comboBox1.Text.ToString().Trim()+"' ";
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

                double total = 0;
                int CourseNum =0;
                int totalCredit = 0;
                int excellent = 0;
                int unPassed = 0;
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    int tmp = int.Parse(dataGridView1.Rows[i].Cells["成绩hjh"].Value.ToString());
                    if (tmp <= 0)
                        continue;
                    int credit = int.Parse(dataGridView1.Rows[i].Cells["学分hjh"].Value.ToString());
                    CourseNum++;
                    if (tmp < 60)
                        unPassed++;
                    if (tmp >= 95)
                        excellent++;
                    total += (double)(tmp-50)*credit;
                    totalCredit += credit;
                }

                total = total / (totalCredit * 10);

                StudentGradeResult frm = new StudentGradeResult(this.num, total, unPassed, excellent, comboBox1.Text.ToString().Trim(), "学期");
                frm.ShowDialog();
                frm.Dispose();
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

        private void 退出ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sqlConnect cc = new sqlConnect();

            string sql = " select h.课程名称hjh,i.姓名hjh '授课教师hjh',h.学分hjh,h.考核方式hjh,e.开课学期hjh,g.成绩hjh " +
                        " from 学生hjh b,班级hjh c,开设hjh e,开课hjh f,选修hjh g,课程hjh h,教师hjh i " +
                        " where b.学生编号hjh = '" + this.num + "' " +
                        " and b.班级编号hjh = c.班级编号hjh and c.专业编号hjh = e.专业编号hjh " +
                        " and e.课程编号hjh = f.课程编号hjh and b.班级编号hjh = f.班级编号hjh  " +
                        " and g.开课编号hjh = f.开课编号hjh and g.学生编号hjh = b.学生编号hjh " +
                        " and f.课程编号hjh = h.课程编号hjh and f.教师编号hjh = i.教师编号hjh ";
            if(checkBox2.Checked)
            {
                sql+=" and g.成绩hjh >=0 ";
            }

            if(comboBox2.Text.ToString().Trim()!="*")
            {
                sql+=" and e.开课学期hjh like '"+comboBox2.Text.ToString().Trim()+"%' ";
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

                double total = 0;
                int CourseNum = 0;
                int totalCredit = 0;
                int excellent = 0;
                int unPassed = 0;
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    int tmp = int.Parse(dataGridView1.Rows[i].Cells["成绩hjh"].Value.ToString());
                    if (tmp <= 0)
                        continue;
                    int credit = int.Parse(dataGridView1.Rows[i].Cells["学分hjh"].Value.ToString());
                    CourseNum++;
                    if (tmp < 60)
                        unPassed++;
                    if (tmp >= 95)
                        excellent++;
                    total += (double)(tmp - 50) * credit;
                    totalCredit += credit;
                }

                total = total / (totalCredit * 10);

                StudentGradeResult frm = new StudentGradeResult(this.num, total, unPassed, excellent,comboBox2.Text.ToString().Trim(), "学年");
                frm.ShowDialog();
                frm.Dispose();


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

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
        }

        private void StudentMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确定要退出吗？", "高校成绩管理系统（学生）", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sqlConnect cc = new sqlConnect();
            string sql = "select d.开课编号hjh,e.课程名称hjh,f.姓名hjh '授课教师hjh',e.学分hjh,e.考核方式hjh,d.上课时间地点hjh,c.开课学期hjh " +
                        " from 学生hjh a, 班级hjh b,开设hjh c,开课hjh d,课程hjh e ,教师hjh f " +
                        " where   a.学生编号hjh ='" + this.num + "' " +
                        " and a.班级编号hjh = b.班级编号hjh  " +
                        " and b.专业编号hjh = c.专业编号hjh  " +
                        " and c.课程编号hjh = e.课程编号hjh " +
                        " and c.课程编号hjh = d.课程编号hjh " +
                        " and d.班级编号hjh = b.班级编号hjh " +
                        " and d.课程编号hjh = e.课程编号hjh " +
                        " and d.教师编号hjh = f.教师编号hjh  ";


            if (comboBox3.Text.ToString().Trim() != "*")
            {
                sql += " and c.开课学期hjh = '" +comboBox3.Text.ToString().Trim() + "' ";
            }
            else
            {
                sql += " order  by c.开课学期hjh ";
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
        
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

            comboBox5.Text = "";
            if (comboBox4.SelectedValue == null)
            {
                MessageBox.Show("该学院目前无教师");
                return;
            }

            sqlConnect cc = new sqlConnect();
            string sql = "select 教师编号hjh,姓名hjh from 教师hjh where 学院编号hjh = '" +
                comboBox4.SelectedValue.ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, cc.conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "教师hjh");

            comboBox5.ValueMember = "教师hjh.教师编号hjh";
            comboBox5.DataSource = ds;
            comboBox5.DisplayMember = "教师hjh.姓名hjh";
            cc.closeConnect();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            sqlConnect cc = new sqlConnect();
            if (comboBox5.Text=="")
            {
                MessageBox.Show("请选择一个教师！");
                return;
            }
            string sql = "select a.开课编号hjh,d.课程名称hjh,c.专业名称hjh,b.班级序号hjh,d.学分hjh,d.考核方式hjh,e.开课学期hjh,a.评定等级hjh " +
                        " from 开课hjh a ,班级hjh b,专业hjh c,课程hjh d ,开设hjh e " +
                        " where a.班级编号hjh = b.班级编号hjh and b.专业编号hjh = c.专业编号hjh " +
                        " and a.课程编号hjh = d.课程编号hjh and e.课程编号hjh = a.课程编号hjh and e.专业编号hjh = b.专业编号hjh "+
                        " and a.教师编号hjh = '" + comboBox5.SelectedValue.ToString().Trim() + "' ";
            if (comboBox7.Text != "*")
            {
                sql += " and e.开课学期hjh = '" + comboBox7.Text + "'";
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

        private void button7_Click(object sender, EventArgs e)
        {
            sqlConnect cc = new sqlConnect();

            string sql = " select g.开课编号hjh,e.开课学期hjh,h.课程名称hjh,i.姓名hjh '授课教师hjh',h.学分hjh,g.教学评定hjh " +
                        " from 学生hjh b,班级hjh c,开设hjh e,开课hjh f,选修hjh g,课程hjh h,教师hjh i " +
                        " where b.学生编号hjh = '" + this.num + "' " +
                        " and b.班级编号hjh = c.班级编号hjh and c.专业编号hjh = e.专业编号hjh " +
                        " and e.课程编号hjh = f.课程编号hjh and b.班级编号hjh = f.班级编号hjh  " +
                        " and g.开课编号hjh = f.开课编号hjh and g.学生编号hjh = b.学生编号hjh " +
                        " and f.课程编号hjh = h.课程编号hjh and f.教师编号hjh = i.教师编号hjh ";
            if (comboBox6.Text.ToString().Trim() != "*")
            {
                sql += " and e.开课学期hjh = '" + comboBox6.Text.ToString().Trim() + "' ";
            }
            try
            {
                cc.BindDataGridView(dataGridView1, sql);
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[3].ReadOnly = true;
                dataGridView1.Columns[4].ReadOnly = true;

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

        private void button8_Click(object sender, EventArgs e)
        {
            sqlConnect cc = new sqlConnect();
            if (dataGridView1.Rows.Count == 1)
            {
                MessageBox.Show("无可评教条目！");
                return;
            }
            string sql = "";
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells["教学评定hjh"].Value.ToString() != "")
                {
                    string sql2 = " update 选修hjh set 教学评定hjh = '" + dataGridView1.Rows[i].Cells["教学评定hjh"].Value.ToString()+"'" +
                        "  where  学生编号hjh = '" + this.num + "'" +
                        " and 开课编号hjh = '" + dataGridView1.Rows[i].Cells["开课编号hjh"].Value.ToString() + "' ";
                    sql += sql2;
                }
            }
            try
            {
                cc.ExecuteNonQuery(sql);
                MessageBox.Show("评教成功！");
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

        private void 个人详细信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentInfo frm = new StudentInfo(tp,num);
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sql = "select c.开课编号hjh,d.课程名称hjh,AVG(c.成绩hjh) '课程平均分hjh' "+
                        " from 学生hjh a,开课hjh b,选修hjh c,课程hjh d "+
                        " where a.班级编号hjh = b.班级编号hjh "+
	                    " and a.学生编号hjh = '"+this.num+"' "+
	                    " and c.开课编号hjh = b.开课编号hjh "+
	                    " and b.课程编号hjh = d.课程编号hjh "+
	                    " group by d.课程名称hjh ,c.开课编号hjh ";
            sqlConnect cc = new sqlConnect();
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
        
    }
}
