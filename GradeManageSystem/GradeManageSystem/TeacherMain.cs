using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GradeManageSystem
{
    public partial class TeacherMain : Form
    {
        private string tp="";
        private string num="";

        public TeacherMain(string tp, string num)
        {
            InitializeComponent();
            this.tp += tp;
            this.num += num;
        }

        private void 教师界面_Load(object sender, EventArgs e)
        {
            label3.Text = num;
            this.Text += "(教师:" + this.num + ")";
        }

        private void 文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 教师界面_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            glyInfo frm = new glyInfo(tp,num);
            frm.ShowDialog();
            frm.Dispose();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about frm = new about();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TeacherAddScore frm = new TeacherAddScore(tp,num);
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TeacherQueryGrade frm = new TeacherQueryGrade(tp, num);
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TeacherQueryPost frm = new TeacherQueryPost(tp, num);
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TeacherInfoCentre frm = new TeacherInfoCentre(tp, num);
            frm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TeacherMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确定要退出吗？", "高校成绩管理系统（教师)", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
