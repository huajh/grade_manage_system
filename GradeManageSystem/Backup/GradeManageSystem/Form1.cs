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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tp = comboBox1.Text;
            string num = textBox1.Text;
            string pwd = textBox2.Text;
            if(tp != "教师" && tp !="学生" && tp !="系统管理员")
            {
                MessageBox.Show("请选择登录身份！");
                return;                  
            }
            if(num == "")
            {
                MessageBox.Show("用户名不能为空！");
                return;                  
            }
            if(pwd == "")
            {
                MessageBox.Show("请输入密码！");
                return;
            }
            string strSQL = "";
            if (tp == "系统管理员")
            {
                strSQL += "select * from 系统管理员hjh where '" + num + "' = 用户名hjh and 登录密码hjh = '" + pwd + "'";
            }
            else 
            {
                strSQL += "select * from " + tp + "hjh where '" + num + "' = " + tp + "编号hjh and 登录密码hjh = '" + pwd + "'";

            }
            sqlConnect cc = new sqlConnect();
            DataSet dataset = new DataSet();
            try
            {
                dataset = cc.GetDataSet(strSQL);
                if (dataset.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("密码错误，请重新登录！");
                    return;
                }
                DataRow dr = dataset.Tables[0].Rows[0];
                if(dr["权限级别hjh"].ToString() == "1")
                {
                    StudentMain frm = new StudentMain(tp, num);
                    frm.Show();
                }
                else if (dr["权限级别hjh"].ToString() == "2")
                {
                    TeacherMain frm2 = new TeacherMain(tp,num);
                    frm2.Show();
                }
                else if (dr["权限级别hjh"].ToString() == "5")
                {
                    SystemManage frm3 = new SystemManage(tp,num);
                    frm3.Show();
                }
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                cc.closeConnect();
                dataset.Dispose();
            }
        }

    }
}
