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
    public partial class StudentGradeResult : Form
    {
        private string num = "";
        private double jidian = 0;
        private int unpassed = 0;
        private int excellent = 0;
        private string kk = "";
        private string type;
        public StudentGradeResult(string num,double jidian,int unpassed,int excellent,string kk,string type)
        {
            InitializeComponent();
            this.num = num;
            this.jidian = jidian;
            this.unpassed = unpassed;
            this.excellent = excellent;
            this.kk = kk;
            this.type = type;
        }

        private void StudentGradeResult_Load(object sender, EventArgs e)
        {
            label18.Text = this.jidian.ToString();
            label19.Text = this.unpassed.ToString();
            label20.Text = this.excellent.ToString();

            string sql =
                "select b.学生编号hjh,sum((g.成绩hjh-50)*h.学分hjh)/(SUM(h.学分hjh)*10) '绩点hjh'" +
                " from 学生hjh b,班级hjh c,开设hjh e,开课hjh f,选修hjh g,课程hjh h,教师hjh i " +
                " where " +
                " b.班级编号hjh = c.班级编号hjh and c.专业编号hjh = e.专业编号hjh ";
            if(this.type == "学期")
            {
                sql+= " and e.开课学期hjh = '"+kk+"' ";
            }
            else if (this.type == "学年")
            {
                sql += " and e.开课学期hjh like '" + kk + "%' ";
            }
	            sql+=" and e.课程编号hjh = f.课程编号hjh and b.班级编号hjh = f.班级编号hjh  "+
	            " and g.开课编号hjh = f.开课编号hjh and g.学生编号hjh = b.学生编号hjh "+
	            " and f.课程编号hjh = h.课程编号hjh and f.教师编号hjh = i.教师编号hjh "+
	            " and b.班级编号hjh = ( "+
			        " select b.班级编号hjh "+
			        " from 学生hjh a,班级hjh b "+
			        " where a.学生编号hjh = '"+this.num+"' and a.班级编号hjh = b.班级编号hjh ) "+
                " group by b.学生编号hjh,g.成绩hjh "+
                " having (g.成绩hjh >=60 and sum((g.成绩hjh-50)*h.学分hjh)/(SUM(h.学分hjh)*10) >"+this.jidian+") ";

                string sql2 =
                    "select b.学生编号hjh,sum((g.成绩hjh-50)*h.学分hjh)/(SUM(h.学分hjh)*10) '绩点hjh'" +
                    " from 学生hjh b,班级hjh c,开设hjh e,开课hjh f,选修hjh g,课程hjh h,教师hjh i " +
                    " where " +
                    " b.班级编号hjh = c.班级编号hjh and c.专业编号hjh = e.专业编号hjh ";
                if (this.type == "学期")
                {
                    sql2 += " and e.开课学期hjh = '" + kk + "' ";
                }
                else if (this.type == "学年")
                {
                    sql2 += " and e.开课学期hjh like '" + kk + "%' ";
                }
                sql2+=" and e.课程编号hjh = f.课程编号hjh and b.班级编号hjh = f.班级编号hjh  " +
                " and g.开课编号hjh = f.开课编号hjh and g.学生编号hjh = b.学生编号hjh " +
                " and f.课程编号hjh = h.课程编号hjh and f.教师编号hjh = i.教师编号hjh " +
                " and c.专业编号hjh = ( " +
                    " select b.专业编号hjh " +
                    " from 学生hjh a,班级hjh b " +
                    " where a.学生编号hjh = '" + this.num + "' and a.班级编号hjh = b.班级编号hjh ) " +
                " group by b.学生编号hjh,g.成绩hjh " +
                " having (g.成绩hjh >=60 and sum((g.成绩hjh-50)*h.学分hjh)/(SUM(h.学分hjh)*10) >" + this.jidian + ") ";

                sqlConnect cc = new sqlConnect();
                try
                {
                    DataSet ds = cc.GetDataSet(sql);
                    DataSet ds2 = cc.GetDataSet(sql2);

                    MessageBox.Show(ds.Tables[0].Rows.Count.ToString());

                    label21.Text = (ds.Tables[0].Rows.Count + 1).ToString();
                    label22.Text = (ds2.Tables[0].Rows.Count + 1).ToString();
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
