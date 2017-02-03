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
    public partial class TeacherUnPassedTable : Form
    {
        private string tp="";
        private string num="";
        private string aN = "";
        private string bN = "";
        public TeacherUnPassedTable(string tp, string num,string aN,string bN)
        {
            InitializeComponent();
            this.tp += tp;
            this.num += num;
            this.aN = aN;
            this.bN = bN;
        }

        private void TeacherUnPassedTable_Load(object sender, EventArgs e)
        {
            this.Text += "(教师:" + this.num + ")";

            if (this.aN == "" || this.bN== "")
            {
                MessageBox.Show("请选择一个班级！");
                return;
            }
            sqlConnect cc = new sqlConnect();

            string sql = "select a.开课编号hjh,c.学生编号hjh,c.姓名hjh,b.成绩hjh " +
                        " from 开课hjh a,选修hjh b,学生hjh c" +
                        "  where  a.开课编号hjh = b.开课编号hjh and b.学生编号hjh = c.学生编号hjh  " +
                        " and b.成绩hjh<60 "+
                        " and a.班级编号hjh='" + this.aN +
                        "' and a.课程编号hjh ='" + this.bN +
                        "' and a.教师编号hjh ='" + this.num + "' ";
            try
            {
                cc.BindDataGridView(dataGridView1, sql);
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[3].ReadOnly = true;
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

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
