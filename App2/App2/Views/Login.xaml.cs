using App2.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {

        private string url = "http://10.0.2.2:5000/login";

        public Login()
        {
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            InitializeComponent();
            
        }

        private async void EventButtonRegister(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp());
        }

        public static HttpClient GetConection()
        {
            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Add("Accept", "application/json");
            return cliente;
        }

        public async Task<Users> LoginUsers(string CC, string password)
        {
            Users contact = new Users()
            {
                CC = CC,
                password = password

            };
            HttpClient cliente = GetConection();
            var response = await cliente.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Users>(await response.Content.ReadAsStringAsync());
            }
            return null;

        }

        private async void EventButtonLogin(object sender, EventArgs e)
        {
            Users user = await LoginUsers(EntryUser.Text, EntryPassword.Text);

            if (user != null)
            {
                await this.DisplayAlert("Exitoso", "Usuario Encontrado", "Ok");
                await Navigation.PushAsync(new UsersTable());
            }
            else
                await DisplayAlert("Fallido", "Datos invalidos", "Ok"); 
        }
    }
}