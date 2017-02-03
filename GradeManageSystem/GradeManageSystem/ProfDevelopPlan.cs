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
    public partial class ProfDevelopPlan : Form
    {
        private string tp = "";
        private string num = "";
        public ProfDevelopPlan(string tp, string num)
        {
            InitializeComponent();
            this.tp += tp;
            this.num += num;
        }
        private void ProfDevelopPlan_Load(object sender, EventArgs e)
        {
            label4.Text = this.num;
            string sql = " select a.姓名hjh,c.专业名称hjh,b.班级序号hjh,a.已修学分hjh,a.平均绩点hjh,c.毕业最少学分hjh "+
                         " from 学生hjh a, 班级hjh b,专业hjh c "+
                         " where a.学生编号hjh =  '" + this.num + "' and a.班级编号hjh = b.班级编号hjh " +
                         " and b.专业编号hjh = c.专业编号hjh ";

            string sql2 = "	select h.课程名称hjh,h.学分hjh,h.学时hjh,h.考核方式hjh,e.开课学期hjh,h.课程介绍hjh,g.成绩hjh,g.教学评定hjh " +
                            " from 学生hjh b,班级hjh c,开设hjh e,课程hjh h,选修hjh g,开课hjh k " +
                            " where b.学生编号hjh = '" + this.num + "' " +
                            " and b.班级编号hjh = c.班级编号hjh " +
                            " and c.专业编号hjh = e.专业编号hjh " +
                            " and e.课程编号hjh = h.课程编号hjh " +
                            " and g.学生编号hjh = b.学生编号hjh " +
                            " and k.班级编号hjh = c.班级编号hjh " +
                            " and k.课程编号hjh = h.课程编号hjh " +
                            " and k.开课编号hjh = g.开课编号hjh " +
                            " order by e.开课学期hjh ";
            sqlConnect cc = new sqlConnect();
            try
            {
                DataSet ds = cc.GetDataSet(sql);
                label6.Text = ds.Tables[0].Rows[0]["姓名hjh"].ToString().Trim();
                label5.Text = ds.Tables[0].Rows[0]["专业名称hjh"].ToString().Trim() + "0" + ds.Tables[0].Rows[0]["班级序号hjh"].ToString().Trim();
                label23.Text = ds.Tables[0].Rows[0]["已修学分hjh"].ToString().Trim();
                label24.Text = ds.Tables[0].Rows[0]["平均绩点hjh"].ToString().Trim();
                label7.Text = "(毕业最少学分: " + ds.Tables[0].Rows[0]["毕业最少学分hjh"].ToString().Trim() + ")";

                this.Text = ds.Tables[0].Rows[0]["专业名称hjh"].ToString().Trim()+"专业(学生:" + this.num + ")";

                cc.BindDataGridView(dataGridView1,sql2);
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[3].ReadOnly = true;
                dataGridView1.Columns[4].ReadOnly = true;
                dataGridView1.Columns[5].ReadOnly = true;
                dataGridView1.Columns[6].ReadOnly = true;
                dataGridView1.Columns[7].ReadOnly = true;
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
