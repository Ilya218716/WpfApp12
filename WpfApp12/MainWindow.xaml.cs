using IdentityModel.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
using WpfApp12.Models;
using System.IdentityModel.Tokens.Jwt;





namespace WpfApp12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();
        public MainWindow()
        {
            client.BaseAddress = new Uri("https://localhost:7188/api");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders
                .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
        }


        private void btnLoadStudents_Click(object sender, RoutedEventArgs e)
        {
            this.GetStudents();
        }
        private async void GetStudents()
        {

            var response = await client.GetStringAsync("student");
            var students = JsonConvert.DeserializeObject<List<Student>>(response);
            dgStudent.DataContext = students;
        }
        private async void SaveStudent(Student student)

        {
            await client.PutAsJsonAsync("student", student);

        }
        private async void UpdateStudent(Student student)

        {
            await client.PutAsJsonAsync("student" + student.StudetnId, student);

        }
        public async void DeleteStudent(int studentId)
        {
            await client.DeleteAsync("students/" + studentId);
        }

        private void btnSaveStudent_Click(object sender, RoutedEventArgs e)
        {
            var student = new Student()
            {
                StudetnId = Convert.ToInt32(txtSudentId.Text),
                Name = txtName.Text,
                Roll = txtRoll.Text
            };
            if (student.StudetnId == 0)
            {
                this.SaveStudent(student);
            }
            else
            {
                this.UpdateStudent(student);
            }
            txtSudentId.Text = 0.ToString();
            txtName.Text = "";
            txtRoll.Text = "";
        }
        void btnEditStudent(object sender, RoutedEventArgs e)
        {
            Student student = ((FrameworkElement)sender).DataContext as Student;
            txtSudentId.Text = student.StudetnId.ToString();
            txtName.Text = student.Name;
            txtRoll.Text = student.Roll;

        }
        void btnDeleteStudent(object sender, RoutedEventArgs e)
        {
            Student student = ((FrameworkElement)sender).DataContext as Student;
            this.DeleteStudent(student.StudetnId);

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = Login.Text;
            string password = Password.Text;
            
            var loginData = new { Username = username, Password = password };
            using (var httpClient = new HttpClient())
            {
                
                var response = await httpClient.PostAsJsonAsync("api/auth/login", loginData);
                if (response.IsSuccessStatusCode)
                {
                    
                    var tokenResponse = await response.Content.ReadAsAsync<TokenResponse>();
                    string token = tokenResponse.RefreshToken;
                   
                }
                else
                {
                   
                    MessageBox.Show("Ошибка аутентификации");
                }
            }

        }

        
    }
}
