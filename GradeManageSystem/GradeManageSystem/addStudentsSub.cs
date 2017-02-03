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
    public partial class addStudentsSub : Form
    {
        private DataGridView dr;
        public addStudentsSub(DataGridView dr)
        {
            this.dr = dr;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="")
            {
                MessageBox.Show("学号不能为空！");
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("姓名不能为空！");
                return;
            }
            sqlConnect cc = new sqlConnect();
            string sql = "select * from 学生hjh where 学生编号hjh = '"+textBox1.Text+"'";
            DataSet ds = cc.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count!=0)
            {
                MessageBox.Show("对不起，学号已经存在！");
                return;
            }
            if (textBox7.Text != "")
            {
                string sql2 = "select * from 班级hjh where 班级编号hjh = '" + textBox4.Text+"'";
                ds = cc.GetDataSet(sql2);
                if(ds.Tables[0].Rows.Count==0)
                {
                    MessageBox.Show("对不起，班级编号不存在！");
                    return;
                }

            }
            string[] row = { textBox1.Text, textBox2.Text,textBox3.Text, textBox4.Text,
                           textBox5.Text,textBox6.Text,textBox7.Text};
            dr.Rows.Add(row);
            Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void addStudentsSub_Load(object sender, EventArgs e)
        {

        }
    }
}
