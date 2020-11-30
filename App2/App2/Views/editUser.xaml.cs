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

    public partial class editUser : ContentPage        
    {
        private static string url = "http://10.0.2.2:5000/editUser";
        private static string url2 = "http://10.0.2.2:5000/deleteUser";
        string auxId;
        public editUser()
        {
            InitializeComponent();
        }

        public editUser(int a) 
        {
            InitializeComponent();
            EntryNombres.Text = auxUser.fullName;

            auxId = auxUser.id;
        }
        public static HttpClient GetConection()
        {
            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Add("Accept", "application/json");
            return cliente;
        }
        public async Task Update(string id, string fullName, string CC, string phone, string password)
        {
            Users user = new Users()
            {
                id = id,
                fullName = fullName,
                CC = CC,
                phone = phone,
                password = password,
            };
            HttpClient cliente = GetConection();
            await cliente.PutAsync(url + "/" + id, new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {

            await Update(auxId, EntryNombres.Text, EntryCedula.Text, EntryCelular.Text, EntryContraseña.Text);
            await DisplayAlert("Success", "Producto actulizado con exito", "Ok");
            await Navigation.PushAsync(new UsersTable());
        }

        public async Task Delete(string id)
        {
            HttpClient cliente = GetConection();
            await cliente.DeleteAsync(url2 + "/" + id);
            await DisplayAlert("Mensaje", "Producto Eliminado con exito", "Ok");
        }

        private async void btnElimniar_Clicked(object sender, EventArgs e)
        {
            await Delete(auxId);
            await Navigation.PushAsync(new UsersTable());
        }
    }
}