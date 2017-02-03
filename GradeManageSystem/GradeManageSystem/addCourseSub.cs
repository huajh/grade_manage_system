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
    public partial class addCourseSub : Form
    {
        private DataGridView dr;
        public addCourseSub(DataGridView dr)
        {
            this.dr = dr;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("课程号不能为空！");
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("课程名称不能为空！");
                return;
            }

            sqlConnect cc = new sqlConnect();

            string sql = "select * from 课程hjh where 课程编号hjh = '" + textBox1.Text + "'";
            DataSet ds = cc.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
            {
                MessageBox.Show("对不起，课程号已经存在！");
                return;
            } 
            ds.Dispose();
            if (comboBox1.Text == "  ------选择------")
            {
                MessageBox.Show("请选择开课学院！");
                return;
            }

            if (comboBox2.Text == "  ----选择----") comboBox2.Text = "";
            if (comboBox3.Text == "  ----选择----") comboBox3.Text = "";
           
            string sql2 = "select * from 学院hjh where 学院名称hjh = '" + comboBox1.Text + "'";
            ds = cc.GetDataSet(sql2);
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("未能到该学院名称的学院编号，学院信息有误！");
                return;
            }
            comboBox1.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString().Trim();
            ds.Dispose();

            string str="";
            if (radioButton1.Checked) { str = radioButton1.Text; }
            else { str = radioButton2.Text; }

            string[] row = { textBox1.Text, textBox2.Text,comboBox1.Text,str,
                           comboBox2.Text,comboBox3.Text,textBox4.Text};

            dr.Rows.Add(row);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
