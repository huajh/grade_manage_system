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
    public partial class glyInfo : Form
    {
        private string tp = "";
        private string num = "";

        public glyInfo(string tp,string num)
        {
            InitializeComponent();
            this.tp =tp;
            this.num = num;
        }

        private void glyInfo_Load(object sender, EventArgs e)
        {
            sqlConnect cc = new sqlConnect();
            string sql = "";
            if (this.tp == "系统管理员")
            {
                sql = "select * from "+this.tp+"hjh"+" where 用户名hjh = '"+this.num+"'";
            }else
            {
                sql = "select * from "+ this.tp+"hjh"+ " where "+this.tp+"编号hjh = '"+this.num+"'";
            }
                DataSet dataset = cc.GetDataSet(sql);
                DataRow dr = dataset.Tables[0].Rows[0];
            if(this.tp == "系统管理员")
            {
                label6.Text = dr["用户名hjh"].ToString().Trim();
            }
            else 
            {
                label6.Text = dr[this.tp + "编号hjh"].ToString().Trim();
            }  
            textBox2.Text = dr["登录密码hjh"].ToString().Trim();
            cc.closeConnect();
            dataset.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pwd1 = textBox3.Text.Trim();
            string pwd2 = textBox4.Text.Trim();
            if(pwd1=="" || pwd2 == "")
            {
                Close();
                return;
            }
            if (!pwd1.Equals(pwd2))
            {
                textBox3.Text = "";
                textBox4.Text = "";
                MessageBox.Show("密码不一致，请重新输入！");
                return;
            }
            sqlConnect cc = new sqlConnect();
            string sql = "";
            if(this.tp =="系统管理员")
            {
                sql = "update "+this.tp+"hjh set 登录密码hjh = '"+pwd1+"' where 用户名hjh = '"+this.num+"'";
            }
            else
            {
                sql = "update " + this.tp + "hjh set 登录密码hjh = '" + pwd1 + "' where "+this.tp+"编号hjh = '" + this.num + "'";
            }

            cc.ExecuteNonQuery(sql);
            cc.closeConnect();
            MessageBox.Show("修改成功！");
            textBox3.Text = "";
            textBox4.Text = "";
            Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
