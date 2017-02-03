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
    public partial class FindStudent : Form
    {
       private DataGridView dgv ;
        public FindStudent(DataGridView dgv)
        {
            InitializeComponent();
            this.dgv = dgv;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入学号！");
                return;
            }
            if (textBox1.Text.Length != 12)
            {
                textBox1.Text = "";
                MessageBox.Show("对不起，请输入12位数字！");
                return;
            }
            sqlConnect cc = new sqlConnect();
            string sql = "select * from 学生hjh where 学生编号hjh = '" + textBox1.Text + "'";
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
