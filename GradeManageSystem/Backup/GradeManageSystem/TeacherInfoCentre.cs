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
    public partial class TeacherInfoCentre : Form
    {
        private string tp="";
        private string num="";
        public TeacherInfoCentre(string tp, string num)
        {
            InitializeComponent();
            this.tp += tp;
            this.num += num;
        }

        private void TeacherInfoCentre_Load(object sender, EventArgs e)
        {
            sqlConnect cc = new sqlConnect();
            this.Text += "(教师:" + this.num + ")";

            /*
             * 
             *  select a.教师编号hjh,a.姓名hjh,a.性别hjh,a.出生年月hjh,a.联系电话hjh,
		            a.学历hjh,a.职称hjh,b.学院名称hjh,a.电子邮箱hjh
                from 教师hjh a,学院hjh b 
                where 教师编号hjh = '' and a.学院编号hjh = b.学院编号hjh
             * 
             * 
             * */
            string sql = "select a.教师编号hjh,a.姓名hjh,a.性别hjh,a.出生年月hjh,a.联系电话hjh,"+
                " a.学历hjh,a.职称hjh,b.学院名称hjh,a.电子邮箱hjh "+
                " from 教师hjh a,学院hjh b "+
                " where 教师编号hjh = '"+ this.num+"' and a.学院编号hjh = b.学院编号hjh ";
            DataSet ds = cc.GetDataSet(sql);
            DataRow dr = ds.Tables[0].Rows[0];

            label10.Text = dr.ItemArray[0].ToString();
            label11.Text = dr.ItemArray[1].ToString();
            if (dr.ItemArray[2].ToString().Trim() == "男")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
            textBox1.Text = dr.ItemArray[3].ToString().Trim();
            textBox2.Text = dr.ItemArray[4].ToString().Trim();
            label12.Text = dr.ItemArray[5].ToString().Trim();
            label13.Text = dr.ItemArray[6].ToString().Trim();
            label14.Text = dr.ItemArray[7].ToString().Trim();
            textBox3.Text = dr.ItemArray[8].ToString().Trim();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlConnect cc = new sqlConnect();
            string sql = "update 教师hjh set ";
            if (radioButton1.Checked)
            {
                sql += "性别hjh = '男'";
            }
            else
            {
                sql += "性别hjh = '女'";
            }
            if(textBox1.Text !="")
            {
                sql += ",出生年月hjh = '"+textBox1.Text+"'";
            }
            if (textBox2.Text != "")
            {
                sql += ",联系电话hjh = '" + textBox2.Text + "'";
            }
            if (textBox3.Text != "")
            {
                sql += ",电子邮箱hjh = '" + textBox3.Text + "'";
            }
            sql += " where 教师编号hjh = '" + this.num + "'";
            try
            {
                cc.ExecuteNonQuery(sql);
        //        MessageBox.Show("修改成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                cc.closeConnect();
                this.Close();
            }
        }
    }
}
