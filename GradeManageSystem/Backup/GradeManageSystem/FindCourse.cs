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
    public partial class FindCourse : Form
    {
       private DataGridView dgv ;
       public FindCourse(DataGridView dgv)
        {
            InitializeComponent();
            this.dgv = dgv;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入课程号！");
                return;
            }
            if (textBox1.Text.Length != 6)
            {
                textBox1.Text = "";
                MessageBox.Show("对不起，请输入6位数字！");
                return;
            }
            sqlConnect cc = new sqlConnect();
            string sql = "select * from 课程hjh where 课程编号hjh = '" + textBox1.Text + "'";
            try
            {
                cc.BindDataGridView(dgv, sql);
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
