using System;
using System.Collections.Generic;
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

using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Odbc;


namespace Lab7
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString = @"Data Source=DESKTOP-5KSEELV; Initial Catalog=OOP_Lab7; Integrated Security=True";
        public MainWindow()
        {
            CHeckDB();
            InitializeComponent();
        }

        void CHeckDB()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    connect.Open();
                    connect.Close();
                }
            }
            catch
            {
                MessageBox.Show("Базы данных нет");
                CreateDB();
                CreateTable();
            }
        }

        void CreateDB()
        {
            MessageBox.Show("Попытка создания");
            using (SqlConnection myConn = new SqlConnection("Server=DESKTOP-5KSEELV; Integrated security=True; database=master"))
            {
                try
                {
                    MessageBox.Show("1");
                    myConn.Open();
                    //SqlCommand t = new SqlCommand("Create database OOP_Lab7", myConn);
                    SqlCommand t = new SqlCommand("CREATE DATABASE OOP_Lab7 ON PRIMARY " +
                                                    "(NAME = OOP_Lab7_Data, " +
                                                    "FILENAME = 'C:\\Users\\Sergey Sukharevich\\Documents\\SQL Server Management Studio\\OOP_Lab7.mdf', " +
                                                     "SIZE = 10MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
                                                     "LOG ON (NAME = OOP_Lab7_Log, " +
                                                     "FILENAME = 'C:\\Users\\Sergey Sukharevich\\Documents\\SQL Server Management Studio\\OOP_Lab7Log.ldf', " +
                                                    "SIZE = 1MB, " +
                                                "MAXSIZE = 5MB, " +
                                            "FILEGROWTH = 10%)", myConn);

                    int number = t.ExecuteNonQuery();
                    MessageBox.Show("База данных создана: " + number);
                    MessageBox.Show("22");
                    myConn.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        void CreateTable()
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                try
                {
                    connect.Open();
                    SqlTransaction transaction = connect.BeginTransaction();
                    SqlCommand command = connect.CreateCommand();
                    command.Transaction = transaction;
                    MessageBox.Show("2");

                    command.CommandText = "create TABLE [PULPIT] (PULPIT nvarchar(20) not null primary key," +
                        " PULPIT_NAME nvarchar(100) null," +
                        " FACULTY nvarchar(10) not null)";
                    command.ExecuteNonQuery();
                    
                    MessageBox.Show("3");
                    
                    command.CommandText = "Create Table [SUBJECT] ([SUBJECT] nvarchar(20) not null primary key," +
                        " [SUBJECT_NAME] nvarchar(50) not null," +
                        " [CONTROL] nvarchar(20) check([CONTROL] in ('Экзамен','Зачет'))," +
                        " COUNT_STUDENT int null)";
                    command.ExecuteNonQuery();

                    MessageBox.Show("4");

                    command.CommandText = "Create TABLE [TEACHER] (TEACHER_ID int not null constraint TEACHER_PK primary key," +
                        " TEACHER_NAME nvarchar(100) null," +
                        " GENDER  nvarchar(1) check(GENDER in ('м', 'ж'))," +
                        " PULPIT nvarchar(20) not null foreign key references PULPIT(PULPIT)," +
                        " [SUBJECT] nvarchar(20) not null foreign key references[SUBJECT]([SUBJECT]))";
                    command.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Finally");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }



        private void Read_Click(object sender, RoutedEventArgs e)
        {
            // Создание подключения
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Открываем подключение
                    connection.Open();

                    SqlCommand sql = new SqlCommand("Select * from TEACHER ORDER BY TEACHER_NAME", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(sql);
                    DataTable TEACHER = new DataTable("TEACHER");
                    adapter.Fill(TEACHER);
                    resultTeacher.ItemsSource = TEACHER.DefaultView;

                    SqlCommand sql1 = new SqlCommand("Select * from SUBJECT", connection);
                    SqlDataAdapter adapter1 = new SqlDataAdapter(sql1);
                    DataTable SUBJECT = new DataTable("SUBJECT");
                    adapter1.Fill(SUBJECT);
                    resultSubject.ItemsSource = SUBJECT.DefaultView;

                    SqlCommand sql2 = new SqlCommand("Select * from PULPIT", connection);
                    SqlDataAdapter adapter2 = new SqlDataAdapter(sql2);
                    DataTable PULPIT = new DataTable("PULPIT");
                    adapter2.Fill(PULPIT);
                    resultPulpit.ItemsSource = PULPIT.DefaultView;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InsertPulpit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Открываем подключение
                    connection.Open();

                    SqlCommand sql = new SqlCommand("Insert into PULPIT values( '" + PulpitTextBox.Text + "', '" + PulpitNameTextBox.Text + "', '" + FacultyTextBox.Text + "' );", connection);
                    int number = sql.ExecuteNonQuery();
                    MessageBox.Show("Добавлено объектов: " + number);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InsertTeacher_Click(object sender, RoutedEventArgs e)
        {
            // Создание подключения
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Открываем подключение
                    connection.Open();

                    SqlCommand sql = new SqlCommand("Insert into TEACHER values( '" + Convert.ToInt32(TeacherTextBox.Text) + "', '" + TeacherNameTextBox.Text + "', '" + GenderTextBox.Text + "', '" + PulpitTTextBox.Text + "', '" + SubjectTTextBox.Text + "' );", connection);
                    int number = sql.ExecuteNonQuery();
                    MessageBox.Show("Добавлено объектов: " + number);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InsertSubject_Click(object sender, RoutedEventArgs e)
        {
            // Создание подключения
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Открываем подключение
                    connection.Open();

                    SqlCommand sql = new SqlCommand("Insert into SUBJECT values( '" + SubjectTextBox.Text + "', '" + SubjectNameTextBox.Text + "', '" + ControlTextBox.Text + "', '" + Convert.ToInt32(CountStudentTextBox.Text) + "' );", connection);
                    int number = sql.ExecuteNonQuery();
                    MessageBox.Show("Добавлено объектов: " + number);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string str = "";
            try
            {
                if (RadioT.IsChecked == true)
                    str = "Delete From TEACHER Where TEACHER_NAME Like '%";
                if (RadioS.IsChecked == true)
                    str = "Delete From SUBJECT Where SUBJECT Like '%";
                if (RadioP.IsChecked == true)
                    str = "Delete From PULPIT Where PULPIT Like '%";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand sql = new SqlCommand(str + DeleteTextBox.Text + "%'", connection);
                    int number = sql.ExecuteNonQuery();
                    MessageBox.Show("Удалена " + number + " строка");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Открываем подключение
                    connection.Open();

                    SqlCommand sql = new SqlCommand("Update TEACHER set TEACHER_ID=" + Convert.ToInt32(ID_Change.Text) + ", TEACHER_NAME='" + TeacherNameTxtBox.Text + "', Gender='" + GenderBox.Text + "' , PULPIT='" + PulpitTTxtBox.Text + "', SUBJECT='" + SubjectTTxtBox.Text + "' Where TEACHER_ID=" + Convert.ToInt32(ID_Change.Text), connection);
                    int number = sql.ExecuteNonQuery();
                    MessageBox.Show("Изменено объектов: " + number);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Query_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand sql = new SqlCommand(Query.Text, connection);
                    int number = sql.ExecuteNonQuery();
                    MessageBox.Show("Обработано: " + number);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
