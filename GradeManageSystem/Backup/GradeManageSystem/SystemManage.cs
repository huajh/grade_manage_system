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
    public partial class SystemManage : Form
    {
        private string tp="";
        private string num="";
        private string InfoType;
        public SystemManage()
        {
            InitializeComponent();
        }
        public SystemManage(string tp, string num)
        {
            InitializeComponent();
            this.tp += tp;
            this.num += num;
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode treenode = e.Node;
            if (treenode == null) return;
            if (管理.SelectedTab == tabPage1)
            {
                if (treenode.Parent == null)
                {
                    comboBox1.Text = treenode.Text;
                    textBox2.Text = "*";
                    textBox3.Text = "*";
                }
                else if (treenode.Parent.Parent == null)
                {
                    comboBox1.Text = treenode.Parent.Text;
                    textBox2.Text = treenode.Text;
                    textBox3.Text = "*";
                }
                else
                {
                    comboBox1.Text = treenode.Parent.Parent.Text;
                    textBox2.Text = treenode.Parent.Text;
                    textBox3.Text = treenode.Text;
                }
            }
            else if (管理.SelectedTab == tabPage2)
            {
                if (treenode.Parent == null)
                {
                    comboBox2.Text = treenode.Text;
                }
                else if (treenode.Parent.Parent == null)
                {
                    comboBox2.Text = treenode.Parent.Text;
                }
                else
                {
                    comboBox2.Text = treenode.Parent.Parent.Text;
                }
            }
            else if(管理.SelectedTab == tabPage3)
            {
                if (treenode.Parent == null)
                {
                    comboBox5.Text = treenode.Text;
                }
                else if (treenode.Parent.Parent == null)
                {
                    comboBox5.Text = treenode.Parent.Text;
                }
                else
                {
                    comboBox5.Text = treenode.Parent.Parent.Text;
                }

            }
            else if (管理.SelectedTab == tabPage4)
            {
                if (treenode.Parent == null)
                {
                    return;
                }
                else if (treenode.Parent.Parent == null)
                {
                    textBox7.Text = treenode.Text;
                }
                else
                {
                    textBox7.Text = treenode.Parent.Text;
                }
            }


        }

        private void 系统管理员_Load(object sender, EventArgs e)
        {
            label6.Text = this.num;

            sqlConnect cc = new sqlConnect();
            try
            {
                string sql = "select * from 学院hjh";
                SqlDataAdapter da = new SqlDataAdapter(sql, cc.conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "学院hjh");
               comboBox7.DataSource = ds;
                comboBox7.DisplayMember = "学院hjh.学院名称hjh";
                comboBox7.ValueMember = "学院hjh.学院编号hjh";
                

                comboBox12.DataSource = ds;
                comboBox12.DisplayMember = "学院hjh.学院名称hjh";
                comboBox12.ValueMember = "学院hjh.学院编号hjh";
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                cc.closeConnect();
            }
            comboBox8.Text = "";
            comboBox9.Text = "";
            comboBox10.Text = "";
            comboBox11.Text = "";



        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (管理.SelectedTab != tabPage1) return;           
            if(textBox1.Text=="")
            {
                MessageBox.Show("请输入学号！");
                return;
            }
            if(textBox1.Text.Length!=12)
            {
                textBox1.Text = "";
                MessageBox.Show("对不起，请输入12位数字！");
                return;
            }
            sqlConnect cc = new sqlConnect();
            string sql = "select * from 学生视图hjh where 学生编号hjh = '" + textBox1.Text + "'";
            try
            {
                InfoType = "students";
                cc.BindDataGridView(dataGridView1, sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                textBox1.Text = "";
                cc.closeConnect();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (管理.SelectedTab != tabPage1) return;
            sqlConnect cc = new sqlConnect();
            string sql = "select * "+
                          " from 学生视图hjh ";
            bool flag = false;
            if(comboBox1.Text != "*")
            {
                flag = true;
                sql += "where 学院名称hjh = '" + comboBox1.Text + "'";
            }            
            if(textBox2.Text != "*")
            {
                if (!flag) sql += " where "; else sql += " and ";
                sql += " 专业名称hjh = '" + textBox2.Text + "'";
                flag = true;
            }
            if(textBox3.Text !="*")
            {
                if (!flag) sql += " where "; else sql += " and ";
                sql += " 班级序号hjh = " + textBox3.Text;
            }
            try
            {
                InfoType = "students";
                cc.BindDataGridView(dataGridView1, sql);
                dataGridView1.Columns[0].ReadOnly = true;
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

        private void 系统管理员_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void 系统管理员_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确定要退出吗？", "高校成绩管理系统(系统管理员)", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            glyInfo frm = new glyInfo(this.tp,this.num);
            frm.ShowDialog();
            frm.Dispose();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dgvr = dataGridView1.Rows[e.RowIndex];
            if (InfoType == "students")
            {
                EditInfo edI = new EditInfo(dgvr);
                edI.ShowDialog();
            }
            else if (InfoType == "teachers")
            {
                EditTeacherInfo edI = new EditTeacherInfo(dgvr);
                edI.ShowDialog();
            }
            else if (InfoType == "courses")
            {

            }
            else if (InfoType == "专业培养计划")
            {

            }
            else if (InfoType == "开课")
            {

            }
        }

        private void 退出ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addStudent frm = new addStudent();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选中要删除的行！");
                return;
            }
            if (dataGridView1.CurrentRow.Index < 0 || dataGridView1.CurrentRow.Index >= dataGridView1.Rows.Count - 1)
            {
                MessageBox.Show("无效的行！");
                return;
            }
            if (MessageBox.Show("确认删除这一行？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string id = row.Cells[0].Value.ToString().Trim();
            string sql = "delete from 学生hjh  where 学生编号hjh = '"+id+"'" ;
            sqlConnect cc = new sqlConnect();
            try
            {
                cc.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
            finally {
                cc.closeConnect();
            }
        }

        private void 新建学生ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addStudent frm = new addStudent();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void 学生查询ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FindStudent frm = new FindStudent(dataGridView1);
            frm.Show();
        }

        private void 查找教师ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindTeacher frm = new FindTeacher(dataGridView1);
            frm.Show();
        }

        private void 查找课程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindCourse frm = new FindCourse(dataGridView1);
            frm.Show();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            about frm = new about();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            addTeacher frm = new addTeacher();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (管理.SelectedTab != tabPage2) return;
            if (textBox4.Text == "")
            {
                MessageBox.Show("请输入教工号！");
                return;
            }
            if (textBox4.Text.Length != 6)
            {
                textBox4.Text = "";
                MessageBox.Show("对不起，请输入6位数字！");
                return;
            }
            sqlConnect cc = new sqlConnect();
            string sql = "select  a.教师编号hjh ,a.姓名hjh,a.性别hjh,b.学院名称hjh,"+
                " a.出生年月hjh,a.学历hjh,a.职称hjh,a.电子邮箱hjh,a.联系电话hjh,a.权限级别hjh "+
                " from 教师hjh a ,学院hjh b where a.学院编号hjh = b.学院编号hjh "+
                " and a.教师编号hjh = '" + textBox4.Text + "'";
            try
            {
                InfoType = "teachers";
                cc.BindDataGridView(dataGridView1, sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                textBox4.Text = "";
                cc.closeConnect();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (管理.SelectedTab != tabPage2) return;
            sqlConnect cc = new sqlConnect();
/*
             select a.教师编号hjh ,a.姓名hjh,a.性别hjh,b.学院名称hjh,a.出生年月hjh,a.学历hjh,a.职称hjh,a.电子邮箱hjh,a.联系电话hjh,a.权限级别hjh
                from 教师hjh a ,学院hjh b
            where a.学院编号hjh = b.学院编号hjh
 */
            string sql = "select  a.教师编号hjh ,a.姓名hjh,a.性别hjh,b.学院名称hjh,a.出生年月hjh,a.学历hjh,a.职称hjh,a.电子邮箱hjh,a.联系电话hjh,a.权限级别hjh " +
                          " from 教师hjh a, 学院hjh b    where a.学院编号hjh = b.学院编号hjh ";
            if(comboBox2.Text !="*")
            {
                sql+=" and b.学院名称hjh = '" + comboBox2.Text + "'";
            }
            if (comboBox3.Text != "*")
            {
                sql += " and a.职称hjh = '" + comboBox3.Text + "'";
            }
            if (comboBox4.Text != "*")
            {
                sql += " and a.学历hjh = '" + comboBox4.Text + "'";
            }
            try
            {
                InfoType = "teachers";
                cc.BindDataGridView(dataGridView1, sql);
                dataGridView1.Columns[0].ReadOnly = true;
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

        private void 新建教师ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addTeacher frm = new addTeacher();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选中要删除的行！");
                return;
            }
            if (dataGridView1.CurrentRow.Index < 0 || dataGridView1.CurrentRow.Index >= dataGridView1.Rows.Count - 1)
            {
                MessageBox.Show("无效的行！");
                return;
            }
            if (MessageBox.Show("确认删除这一行？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string id = row.Cells[0].Value.ToString().Trim();
            string sql = "delete from 教师hjh  where 教师编号hjh = '" + id + "'";
            sqlConnect cc = new sqlConnect();
            try
            {
                cc.ExecuteNonQuery(sql);
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

        private void button9_Click(object sender, EventArgs e)
        {
            addCourse frm = new addCourse();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (管理.SelectedTab != tabPage3) return;
            sqlConnect cc = new sqlConnect();

            /*
             * 
             * select a.课程编号hjh,a.课程名称hjh,b.学院名称hjh ,a.考核方式hjh,a.学时hjh,a.学分hjh,a.课程介绍hjh
                from 课程hjh a ,学院hjh b
                where a.学院编号hjh = b.学院编号hjh
             * 
             * 
             */

            string sql = "select a.课程编号hjh,a.课程名称hjh,b.学院名称hjh ,a.考核方式hjh,a.学时hjh,a.学分hjh,a.课程介绍hjh "+
                          " from 课程hjh a ,学院hjh b "+
                          " where a.学院编号hjh = b.学院编号hjh ";
            if (comboBox5.Text != "*")
            {
                sql += " and b.学院名称hjh = '" + comboBox5.Text + "'";
            }
            if (textBox5.Text != "*")
            {
                sql += " and a.课程名称hjh = '" + textBox5.Text + "'";
            }
            try
            {
                InfoType = "courses";
                cc.BindDataGridView(dataGridView1, sql);
                dataGridView1.Columns[0].ReadOnly = true;
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

        private void button11_Click(object sender, EventArgs e)
        {
            if (管理.SelectedTab != tabPage3) return;
            if (textBox6.Text == "")
            {
                MessageBox.Show("请输入课程号！");
                return;
            }
            if (textBox6.Text.Length != 6)
            {
                textBox6.Text = "";
                MessageBox.Show("对不起，请输入6位数字！");
                return;
            }
            sqlConnect cc = new sqlConnect();
            string sql = "select * from 课程hjh where 课程编号hjh = '" + textBox6.Text + "'";
            try
            {
                InfoType = "courses";
                cc.BindDataGridView(dataGridView1, sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                textBox6.Text = "";
                cc.closeConnect();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选中要删除的行！");
                return;
            }
            if (dataGridView1.CurrentRow.Index < 0 || dataGridView1.CurrentRow.Index >= dataGridView1.Rows.Count - 1)
            {
                MessageBox.Show("无效的行！");
                return;
            }
            if (MessageBox.Show("确认删除这一行？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string id = row.Cells[0].Value.ToString().Trim();
            string sql = "delete from 课程hjh  where 课程编号hjh = '" + id + "'";
            sqlConnect cc = new sqlConnect();
            try
            {
                cc.ExecuteNonQuery(sql);
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

        private void button14_Click(object sender, EventArgs e)
        {
            if (管理.SelectedTab != tabPage4) return;
            sqlConnect cc = new sqlConnect();

            string sql = "select b.专业名称hjh,c.课程名称hjh,a.开课学期hjh from 开设hjh a,专业hjh b ,课程hjh c "+
                        " where a.专业编号hjh = b.专业编号hjh and a.课程编号hjh = c.课程编号hjh ";     
            if (textBox7.Text != "*")
            {
                sql += " and b.专业名称hjh = '" + textBox7.Text + "'";
            }
            if (comboBox6.Text != "*")
            {
                sql += " and a.开课学期hjh = '" + comboBox6.Text + "' ";
            }
            sql += " order by a.开课学期hjh ";
            try
            {
                InfoType = "专业培养计划";
                cc.BindDataGridView(dataGridView1, sql);
                dataGridView1.Columns[0].ReadOnly = true;
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

        private void button15_Click(object sender, EventArgs e)
        {
            if (管理.SelectedTab != tabPage4) return;
            if (textBox8.Text == "")
            {
                MessageBox.Show("请输入专业编号！");
                return;
            }
            sqlConnect cc = new sqlConnect();
            string sql = " select b.专业名称hjh,c.课程名称hjh,a.开课学期hjh from 开设hjh a,专业hjh b ,课程hjh c "+
                         " where a.专业编号hjh = b.专业编号hjh and a.课程编号hjh = c.课程编号hjh and a.专业编号hjh = '"+ textBox8.Text + "'"+
                         " order by a.开课学期hjh ";
            try
            {
                InfoType = "专业培养计划";
                cc.BindDataGridView(dataGridView1, sql);
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

        private void button13_Click(object sender, EventArgs e)
        {
            addplan frm = new addplan();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            NewClassCourse frm = new NewClassCourse();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox8.Text = "";
            comboBox9.Text = "";
            if (comboBox7.SelectedValue == null)
            {
                MessageBox.Show("无可选专业！");
                return;
            }
            sqlConnect cc = new sqlConnect();
            string sql = "select * from 专业hjh where 学院编号hjh = '" + comboBox7.SelectedValue.ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, cc.conn);

            DataSet ds = new DataSet();
            da.Fill(ds, "专业hjh");
            comboBox8.ValueMember = "专业hjh.专业编号hjh";
            comboBox8.DataSource = ds;
            comboBox8.DisplayMember = "专业hjh.专业名称hjh";
            
           
            cc.closeConnect();
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox9.Text = "";
            if (comboBox8.SelectedValue == null)
            {
                MessageBox.Show("无可选班级！");
                return;
            }
            sqlConnect cc = new sqlConnect();
            string sql = "select 班级序号hjh,班级编号hjh from 班级hjh where 专业编号hjh = '" + comboBox8.SelectedValue.ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, cc.conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "班级hjh");
            comboBox9.ValueMember = "班级hjh.班级编号hjh";
            comboBox9.DataSource = ds;
            comboBox9.DisplayMember = "班级hjh.班级序号hjh";   
            cc.closeConnect();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (管理.SelectedTab != tabPage5) return;

            MessageBox.Show(comboBox9.SelectedValue.ToString() + "  " + comboBox9.Text );

            sqlConnect cc = new sqlConnect();
            if(comboBox7.Text == "" ||comboBox8.Text == "" ||comboBox9.Text == ""  )
            {
                MessageBox.Show("请指定一个班级！");
                return;
            }
            string sql = "select a.开课编号hjh ,d.课程名称hjh,b.姓名hjh '授课教师hjh',f.学院名称hjh,e.专业名称hjh,c.班级序号hjh,a.上课时间地点hjh,a.评定等级hjh "+
                        " from 开课hjh a, 教师hjh b,班级hjh c,课程hjh d ,专业hjh e,学院hjh f "+
                        " where   a.教师编号hjh = b.教师编号hjh "+
	                    " and a.班级编号hjh = c.班级编号hjh and a.课程编号hjh = d.课程编号hjh "+
	                    " and c.专业编号hjh = e.专业编号hjh and e.学院编号hjh = f.学院编号hjh "+
                        " and a.班级编号hjh = '" + comboBox9.SelectedValue.ToString() + "'";
            try
            {
                InfoType = "开课";
                cc.BindDataGridView(dataGridView1, sql);
                dataGridView1.Columns[0].ReadOnly = true;
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

        private void button16_Click(object sender, EventArgs e)
        {
            if (管理.SelectedTab != tabPage5) return;
            if (textBox9.Text == "")
            {
                MessageBox.Show("请输入开课号！");
                return;
            }
            sqlConnect cc = new sqlConnect();
            string sql = "select * from 开课hjh where 开课编号hjh = '" + textBox9.Text + "'";
            try
            {
                InfoType = "开课";
                cc.BindDataGridView(dataGridView1, sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                textBox9.Text = "";
                cc.closeConnect();
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选中要删除的行！");
                return;
            }
            if (dataGridView1.CurrentRow.Index < 0 || dataGridView1.CurrentRow.Index >= dataGridView1.Rows.Count - 1)
            {
                MessageBox.Show("无效的行！");
                return;
            }
            if (MessageBox.Show("确认删除这一行？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string id = row.Cells[0].Value.ToString().Trim();
            string sql = "delete from 开课hjh  where 开课编号hjh = '" + id + "'";
            sqlConnect cc = new sqlConnect();
            try
            {
                cc.ExecuteNonQuery(sql);
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

        private void 新建课程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addCourse frm = new addCourse();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (管理.SelectedTab != tabPage6) return;
            if (textBox10.Text == "")
            {
                MessageBox.Show("请输入学号！");
                return;
            }
            if (textBox10.Text.Length != 12)
            {
                textBox10.Text = "";
                MessageBox.Show("对不起，请输入12位数字！");
                return;
            }
            sqlConnect cc = new sqlConnect();
            /*
             * 
             * select  a.学生编号hjh,d.姓名hjh '学生姓名hjh',e.课程名称hjh,c.姓名hjh '授课教师hjh',e.学分hjh,a.成绩hjh
                    from 选修hjh a,开课hjh b ,教师hjh c ,学生hjh d,课程hjh e
                    where a.学生编号hjh = '200901010101' and a.开课编号hjh = b.开课编号hjh 
	                    and b.教师编号hjh = c.教师编号hjh and a.学生编号hjh = d.学生编号hjh
                    	and b.课程编号hjh = e.课程编号hjh
             * 
             * 
             * */
            string sql = "select  a.学生编号hjh,d.姓名hjh '学生姓名hjh',e.课程名称hjh,c.姓名hjh '授课教师hjh',e.学分hjh,a.成绩hjh "+
                          " from 选修hjh a,开课hjh b ,教师hjh c ,学生hjh d,课程hjh e "+
                          " where a.学生编号hjh = '" + textBox10.Text + "' and a.开课编号hjh = b.开课编号hjh "+
	                      " and b.教师编号hjh = c.教师编号hjh and a.学生编号hjh = d.学生编号hjh "+
                    	  " and b.课程编号hjh = e.课程编号hjh";
            try
            {
                InfoType = "成绩管理";
                cc.BindDataGridView(dataGridView1, sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                textBox10.Text = "";
                cc.closeConnect();
            }
        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox10.Text = "";
            comboBox11.Text = "";
            if (comboBox12.SelectedValue == null)
            {
                MessageBox.Show("无可选专业！");
                return;
            }
            sqlConnect cc = new sqlConnect();
            string sql = "select * from 专业hjh where 学院编号hjh = '" + comboBox12.SelectedValue.ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, cc.conn);

            DataSet ds = new DataSet();
            da.Fill(ds, "专业hjh");
             comboBox11.DataSource = ds;
            comboBox11.DisplayMember = "专业hjh.专业名称hjh";
            comboBox11.ValueMember = "专业hjh.专业编号hjh";
           
            cc.closeConnect();
        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox10.Text = "";
            if (comboBox11.SelectedValue == null)
            {
                MessageBox.Show("无可选班级！");
                return;
            }
            sqlConnect cc = new sqlConnect();
            string sql = "select * from 班级hjh where 专业编号hjh = '" + comboBox11.SelectedValue.ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, cc.conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "班级hjh");
             comboBox10.DataSource = ds;
            comboBox10.DisplayMember = "班级jh.班级序号hjh";
            comboBox10.ValueMember = "班级hjh.班级编号hjh";
          
            cc.closeConnect();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            sqlConnect cc = new sqlConnect();
            if (comboBox10.SelectedValue == null || comboBox10.DisplayMember == null)
            {
                MessageBox.Show("请选择一个班级！");
                return;
            }
            string sql = "select 开课编号hjh from 开课hjh  where 班级编号hjh = '" + comboBox10.SelectedValue.ToString().Trim() + "'";
            DataSet ds = cc.GetDataSet(sql);
            sql = "select 学生编号hjh from 学生hjh where 班级编号hjh = '" + comboBox10.SelectedValue.ToString().Trim() + "'";
            DataSet ds2 = cc.GetDataSet(sql);
            int synNum = 0;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                foreach (DataRow dr2 in ds2.Tables[0].Rows)
                {
                    string sql2 = "select * from 选修hjh where 开课编号hjh = '" + dr[0].ToString() + "' and " +
                                  "学生编号hjh = '" + dr2[0].ToString() + "'";
                    DataSet ds3 = cc.GetDataSet(sql2);
                    if (ds3.Tables[0].Rows.Count == 0)
                    {
                        synNum++;
                        if (synNum == 1)
                        {
                            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
                            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
                            col1.Name = "开课编号hjh";
                            col1.HeaderText = "开课编号hjh";
                            col2.Name = "学生编号hjh";
                            col2.HeaderText = "学生编号hjh";
                            dataGridView1.Columns.Clear();
                            dataGridView1.DataSource = null;
                            dataGridView1.Columns.Add(col1);
                            dataGridView1.Columns.Add(col2);
                        }
                        sql += " insert into 选修hjh(开课编号hjh,学生编号hjh) values( '" + dr[0].ToString() + "','" + dr2[0].ToString() + "' );";
                        string[] row = { dr[0].ToString(), dr2[0].ToString() };
                        dataGridView1.Rows.Add(row);
                    }
                    ds3.Dispose();
                }
            }
            try
            {
                if (synNum != 0)
                {
                    cc.ExecuteNonQuery(sql);
                    MessageBox.Show("共同步" + synNum.ToString() + "条记录！");
                }
                else
                {
                    MessageBox.Show("无需再次同步，该班级同学选修情况已经全部与班级开课同步！");
                }
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

        private void button22_Click(object sender, EventArgs e)
        {
            sqlConnect cc = new sqlConnect();

            string sql = "select b.开课编号hjh, a.学生编号hjh from 学生hjh a,开课hjh b where a.班级编号hjh = b.班级编号hjh";
            DataSet ds = cc.GetDataSet(sql);
            int synNum = 0;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                string sql2 = "select * from 选修hjh where 开课编号hjh = '" + dr[0].ToString() + "' " +
                        " and 学生编号hjh = '" + dr[1].ToString() + "'";

                DataSet ds2 = cc.GetDataSet(sql2);
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    synNum++;
                    if (synNum == 1)
                    {
                        DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
                        DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
                        col1.Name = "开课编号hjh";
                        col1.HeaderText = "开课编号hjh";
                        col2.Name = "学生编号hjh";
                        col2.HeaderText = "学生编号hjh";
                        dataGridView1.Columns.Clear();
                        dataGridView1.DataSource = null;
                        dataGridView1.Columns.Add(col1);
                        dataGridView1.Columns.Add(col2);
                    }
                    sql += " insert into 选修hjh(开课编号hjh,学生编号hjh) values( '" + dr[0].ToString() + "','" + dr[1].ToString() + "' );";
                    string[] row = { dr[0].ToString(), dr[1].ToString() };
                    dataGridView1.Rows.Add(row);
                }
                ds2.Dispose();

            }
            try
            {
                if (synNum != 0)
                {
                    cc.ExecuteNonQuery(sql);
                    MessageBox.Show("共同步" + synNum.ToString() + "条记录！");
                }
                else
                {
                    MessageBox.Show("无可同步课程！");
                }
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

        private void 管理_Selected(object sender, TabControlEventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (comboBox10.SelectedValue == null || comboBox10.DisplayMember == null)
            {
                MessageBox.Show("请选择一个班级！");
                return;
            }
            if (MessageBox.Show("确定要取消该班同步课程吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            else
            {
                string sql = " delete " +
                            " from 选修hjh " +
                            " where 选修hjh.学生编号hjh in " +
                            "( select 学生编号hjh " +
                            " from 学生hjh a " +
                            " where a.班级编号hjh = '" + comboBox10.SelectedValue.ToString().Trim() + "')";
                sqlConnect cc = new sqlConnect();
                try
                {
                    cc.ExecuteNonQuery(sql);
                    MessageBox.Show("成功取消同步记录！");
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

        private void button24_Click(object sender, EventArgs e)
        {
            sqlConnect cc = new sqlConnect();

            if (comboBox10.SelectedValue == null || comboBox10.DisplayMember == null)
            {
                MessageBox.Show("请选择一个班级！");
                return;
            }

            string sql = "select a.开课编号hjh ,b.姓名hjh,e.课程名称hjh ,f.专业名称hjh,c.班级序号hjh "+
                " from 选修hjh a ,学生hjh b,班级hjh c,开课hjh d,课程hjh e,专业hjh f "+
                " where a.学生编号hjh = b.学生编号hjh and b.班级编号hjh = c.班级编号hjh " +
	            " and a.开课编号hjh = d.开课编号hjh and d.课程编号hjh = e.课程编号hjh "+
	            " and c.专业编号hjh = f.专业编号hjh "+
	            " and b.班级编号hjh = '"+comboBox10.SelectedValue.ToString().Trim()+"' "+
                " order by b.姓名hjh ";
            try
            {
                InfoType = "课程同步";
                dataGridView1.Columns.Clear();
                cc.BindDataGridView(dataGridView1, sql);
                dataGridView1.Columns[0].ReadOnly = true;
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
