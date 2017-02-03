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
    public partial class addTeacherSub : Form
    {
        private DataGridView dr;
        public addTeacherSub(DataGridView dr)
        {
            this.dr = dr;
            InitializeComponent();
        }

        private void addTeacherSub_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("教工号不能为空！");
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("姓名不能为空！");
                return;
            }
            sqlConnect cc = new sqlConnect();
            string sql = "select * from 教师hjh where 教师编号hjh = '" + textBox1.Text + "'";
            DataSet ds = cc.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
            {
                MessageBox.Show("对不起，教工号已经存在！");
                return;
            }
            if (textBox7.Text != "")
            {
                string sql2 = "select * from 学院hjh where 学院编号hjh = '" + textBox4.Text + "'";
                ds = cc.GetDataSet(sql2);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("对不起，学院编号不存在！");
                    return;
                }

            }
            string[] row = { textBox1.Text, textBox2.Text,textBox3.Text, textBox4.Text,
                           textBox5.Text,textBox6.Text,textBox7.Text,textBox8.Text,textBox9.Text};
            dr.Rows.Add(row);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
