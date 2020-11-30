using App2.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        private string url = "http://10.0.2.2:5000/signup";

        public SignUp()
        {
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            InitializeComponent();
        }

        public static HttpClient GetConection()
        {
            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Add("Accept", "application/json");
            return cliente;
        }

        public async Task<Users> signUpUsers(string fullname, string CC, string phone, string password, string confirm_password)
        {
            newUser user = new newUser()
            {
                id = "",
                fullname = fullname,
                CC = CC,
                phone = phone,
                password = password,
                confirm_password = confirm_password

            };
            HttpClient cliente = GetConection();
            var response = await cliente.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Users>(await response.Content.ReadAsStringAsync());
            }
            return null;
        }

        private async void Registrar(object sender, EventArgs e)
        {
            Users user = await signUpUsers(EntryNombres.Text, EntryCedula.Text, EntryCelular.Text, EntryContraseña.Text, EntryNuevaContraseña.Text);

            if (user != null)
            {
                await this.DisplayAlert("Exitoso", "Usuario guardado de manera satisfactoria", "Ok");
                await Navigation.PushAsync(new Login());
            } else
                await DisplayAlert("Fallido", "Datos invalidos", "Ok");
        }
    }
}