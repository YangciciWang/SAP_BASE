using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SAP_BASE
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();           
        }

        MySQLConn con = new MySQLConn();            //连接数据库
        string sql_teacher = "select * from sap.teacher";//数据库的名字，根据你设定的名称修改
        string sql_examiner = "select * from sap.examiner";//数据库的名字，根据你设定的名称修改

        //加载所有数据
        private void loadData_all()
        {
            //string sql_teacher = "select * from sap.teacher";//数据库的名字，根据你设定的名称修改
            //string sql_examiner = "select * from sap.examiner";//数据库的名字，根据你设定的名称修改
            DataTable dt_teacher = new DataTable();         //新建DataTable类存放数据库输出结果  
            dt_teacher = con.ExecuteQuery(sql_teacher);
            dataGrid1.ItemsSource = dt_teacher.DefaultView;

            DataTable dt_examiner = new DataTable();
            dt_examiner = con.ExecuteQuery(sql_examiner);
            dataGrid2.ItemsSource = dt_examiner.DefaultView;
        }//end loadData

        private void Button_Click_reflesh_datagrid1(object sender, RoutedEventArgs e)
        {
            DataTable dt_teacher = new DataTable();
            dt_teacher = con.ExecuteQuery(sql_teacher);
            dataGrid1.ItemsSource = dt_teacher.DefaultView;
            MessageBox.Show("数据已刷新！", "提示");
        }

        private void Button_Click_reflesh_datagrid2(object sender, RoutedEventArgs e)
        {
            DataTable dt_examiner = new DataTable();
            dt_examiner = con.ExecuteQuery(sql_examiner);
            dataGrid2.ItemsSource = dt_examiner.DefaultView;
            MessageBox.Show("数据已刷新！", "提示");
        }

        private void Button_Click_reflesh(object sender, RoutedEventArgs e)
        {

            if (con.ExecuteQuery(sql_teacher).Rows.Count > 0)
            {
                MessageBox.Show("加载成功！");
                loadData_all();
                
            }               
            else
                MessageBox.Show("加载失败！");
            
        }

        //将datagrid1中选中一行的数据赋值到下面的textbox中
        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView rowSelected = dg.SelectedItem as DataRowView;
            if(rowSelected  != null )
            {
                teacher_num.Text  = rowSelected["序号"].ToString();
                teacher_name.Text = rowSelected["姓名"].ToString();
                teacher_section.Text = rowSelected["教研室"].ToString();
                teacher_sign.Text = rowSelected["签署"].ToString();
                teacher_nurturance.Text = rowSelected["上课项目（养成）"].ToString();
                teacher_147.Text = rowSelected["上课项目（147）"].ToString();
                teacher_66me.Text = rowSelected["上课项目（66ME）"].ToString();
                teacher_66av.Text = rowSelected["上课项目（66AV）"].ToString();
            }
        }

        //将datagrid2中选中一行的数据赋值到下面的textbox中
        private void dataGrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView rowSelected = dg.SelectedItem as DataRowView;
            if (rowSelected != null)
            {
                examiner_num.Text = rowSelected["序号"].ToString();
                examiner_name.Text = rowSelected["姓名"].ToString();
                examiner_workplace.Text = rowSelected["单位"].ToString();
                examiner_sign.Text = rowSelected["签署代码"].ToString();                
            }
        }
        
        //改变修改教官信息textbox的输入模式
        private void textbox_teacher_state(bool state, string color)
        {
            //this.teacher_num.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            //this.teacher_num.IsReadOnly = state;
            this.teacher_name.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            this.teacher_name.IsReadOnly = state;
            this.teacher_section.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            this.teacher_section.IsReadOnly = state;
            this.teacher_sign.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            this.teacher_sign.IsReadOnly = state;
            this.teacher_nurturance.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            this.teacher_nurturance.IsReadOnly = state;
            this.teacher_147.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            this.teacher_147.IsReadOnly = state;
            this.teacher_66me.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            this.teacher_66me.IsReadOnly = state;
            this.teacher_66av.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            this.teacher_66av.IsReadOnly = state;
        }

        //将修改教官信息textbox改为可输入
        int i_applychange_teaher = 1;
        private void Button_Click_applychange_teaher(object sender, RoutedEventArgs e)
        {
            i_applychange_teaher++;
            if (i_applychange_teaher % 2 == 0)
            {
                textbox_teacher_state(false, "#FFFFFFFF");
            }
            else
            {
                textbox_teacher_state(true, "#FFF0F0F0");
            }
            
        }

        //确认修改教官信息并将textbox改为不可输入（只读），并将数据写入数据库中保存
        private void Button_Click_confirmchange_teaher(object sender, RoutedEventArgs e)
        {
            //int count;
            //string updateQuery = "UPDATE `sap`.`teacher` SET `教研室` = '金工' WHERE(`序号` = '2')";
            //UPDATE `sap`.`teacher` SET `教研室` = '理论', `签署` = '是' WHERE(`序号` = '2');
            //count = con.ExecuteUpdate(updateQuery);
            //MessageBox.Show("刷新成功！" + count);

            MessageBoxResult dr = MessageBox.Show("是否确认修改？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            
            if (dr == MessageBoxResult.OK)
            {
                //点确定的代码
                int count;
                textbox_teacher_state(true , "#FFF0F0F0");
                string updateQuery = "UPDATE `sap`.`teacher` SET " +
                    "`姓名` = '" + this.teacher_name.Text +"', " +
                    "`教研室` = '" + this.teacher_section.Text  +"', " +
                    "`签署` = '" + this.teacher_sign.Text  + "', " +
                    "`上课项目（养成）` = '" + this.teacher_nurturance.Text +"', " +
                    "`上课项目（147）` = '" + this.teacher_147.Text +"', " +
                    "`上课项目（66ME）` = '" + this.teacher_66me.Text +"', " +
                    "`上课项目（66AV）` = '" +this.teacher_66av.Text +"' " +
                    "WHERE(`序号` = '" + this.teacher_num.Text  +"')";
                count = con.ExecuteUpdate(updateQuery);
                MessageBox.Show("已成功修改" + count +"行内容");
                textbox_teacher_state(true , "#FFF0F0F0");
                return;
            }
            else
            {
                //点取消的代码  
                //MessageBox.Show("取消修改");
                return;
            }           
        }

        private void Button_Click_delete_teacher(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dr = MessageBox.Show("是否确定删除？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (dr == MessageBoxResult.OK)
            {
                //点确定的代码

            }
            else
            {
                //点取消的代码        
            }
        }

        //改变增加教官信息textbox的输入模式
        private void textbox_add_teacher_state(bool state, string color)
        {
            this.add_teacher_num.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            this.add_teacher_num.IsReadOnly = state;
            this.add_teacher_name.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            this.add_teacher_name.IsReadOnly = state;
            this.add_teacher_section.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            this.add_teacher_section.IsReadOnly = state;
            this.add_teacher_sign.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            this.add_teacher_sign.IsReadOnly = state;
            this.add_teacher_nurturance.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            this.add_teacher_nurturance.IsReadOnly = state;
            this.add_teacher_147.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            this.add_teacher_147.IsReadOnly = state;
            this.add_teacher_66me.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            this.add_teacher_66me.IsReadOnly = state;
            this.add_teacher_66av.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            this.add_teacher_66av.IsReadOnly = state;
        }

        //将增加教官信息textbox改为可输入
        int i_add_data_teacher = 1;
        private void Button_Click_add_data_teacher(object sender, RoutedEventArgs e)
        {
            i_add_data_teacher++;
            if (i_add_data_teacher % 2 == 0)
            {
                textbox_add_teacher_state(false, "#FFFFFFFF");
                this.buttom_confirm_add_teacher.Visibility  = Visibility.Visible;
            }
            else
            {
                textbox_add_teacher_state(true, "#FFF0F0F0");
                this.buttom_confirm_add_teacher.Visibility = Visibility.Hidden;
            }            
        }

        //确认增加教官信息并将textbox改为不可输入（只读），并将数据写入数据库中保存
        private void Button_Click_confirm_add_teacher(object sender, RoutedEventArgs e)
        {
            //INSERT INTO `sap`.`teacher` (`序号`, `姓名`, `教研室`, `签署`, `上课项目（养成）`, `上课项目（147）`) VALUES ('7', '孙俊刚', '实训', '否', '保险', '保险')

            MessageBoxResult dr = MessageBox.Show("是否确认添加？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (this.add_teacher_num.Text != null)
            {
                MessageBox.Show("无效输入");
                return;
            }
            if ((dr == MessageBoxResult.OK) & (int.Parse(this.add_teacher_num.Text) > dataGrid1.Items.Count ))
            {
                int count;
                textbox_teacher_state(true, "#FFF0F0F0");
                string updateQuery = "INSERT INTO `sap`.`teacher` (`序号`, `姓名`, `教研室`, `签署`, `上课项目（养成）`, `上课项目（147）`, `上课项目（66ME）`, `上课项目（66AV）`) VALUES ('" +
                    this.add_teacher_num.Text  + "', '" +
                    this.add_teacher_name.Text  + "', '" +
                    this.add_teacher_section.Text  + "', '" +
                    this.add_teacher_sign.Text  + "', '" +
                    this.add_teacher_nurturance.Text  + "', '" +
                    this.add_teacher_147.Text  + "', '" +
                    this.add_teacher_66me.Text + "', '" +
                    this.add_teacher_66av.Text  + "')";
                count = con.ExecuteUpdate(updateQuery);
                MessageBox.Show("已成功添加" + count + "行内容");
                textbox_add_teacher_state(true, "#FFF0F0F0");
                return;
            }
            else
            {
                MessageBox.Show("请检查序号是否正确");
                return;
            }
        }

        private void Button_Click_applychange_examiner(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_confirmchange_examiner(object sender, RoutedEventArgs e)
        {

        }
        
    }

    class MySQLConn
    {
        private string MysqlCon = "Database=sap; Data Source=127.0.0.1;User Id=root;"
            + "Password=123456;pooling = false; CharSet = utf8 ;port =3306";
        public DataTable ExecuteQuery(string sqlStr)
        {
            MySqlCommand cmd;
            MySqlConnection con;
            MySqlDataAdapter msda;//数据适配器，DataAdapter对象在DataSet与数据之间起桥梁作用  
            con = new MySqlConnection(MysqlCon);
            //con.Open();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
            }
            //catch (Exception ee)
            catch 
            {
                //MessageBox.Show("数据库连接失败！\n" + ee.Message.ToString(), "提示");
                //DataTable dt_none = new DataTable();
                con.Close();
                return dt;
            }
            
            cmd = new MySqlCommand(sqlStr, con);
            cmd.CommandType = CommandType.Text;
            //DataTable dt = new DataTable();
            msda = new MySqlDataAdapter(cmd);
            msda.Fill(dt);
            con.Close();           
            return dt;
        }

        public int ExecuteUpdate(string sqlStr)
        {
            int iud = 0;
            MySqlCommand cmd;
            MySqlConnection con;
            con = new MySqlConnection(MysqlCon);
            con.Open();
            cmd = new MySqlCommand(sqlStr, con);
            cmd.CommandType = CommandType.Text;
            iud = cmd.ExecuteNonQuery();
            con.Close();
            return iud;
        }

    }
}
