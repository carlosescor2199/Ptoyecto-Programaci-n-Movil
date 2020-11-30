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
    public partial class AgregarPrestatario : ContentPage
    {

        Borrower borrower;
        private string url = "http://10.0.2.2:5000/borrower/register";
        private string urlEdit = "http://10.0.2.2:5000/borrower/edit/";
        private string urlDelete = "http://10.0.2.2:5000/borrower/delete/";
        public AgregarPrestatario()
        {
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            InitializeComponent();
            buttonGuardar.IsVisible = true;
            buttonEditar.IsVisible = false;
            buttonCancelar.IsVisible = true;
            buttonEliminar.IsVisible = false;

        }

        public AgregarPrestatario(Borrower borro)
        {
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            InitializeComponent();
            buttonGuardar.IsVisible = false;
            buttonEditar.IsVisible = true;
            buttonCancelar.IsVisible = false;
            buttonEliminar.IsVisible = true;
            borrower = borro;
            EntryNombre.Text = borrower.fullname;
            EntryCC.Text = borrower.CC;
            EntryTelefono.Text = borrower.phone;
            EntryCantidad.Text = borrower.amount.ToString();
            EntryPorcentaje.Text = borrower.percentage.ToString();
            EntryMeses.Text = borrower.months.ToString();
        }
        public static HttpClient GetConection()
        {
            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Add("Accept", "application/json");
            return cliente;
        }

        public async Task<Borrower> signUpBorrower(string fullname, string CC, string phone, float amount, float percentage, int months)
        {
            Borrower borro = new Borrower()
            {
                id = 0,
                fullname = fullname,
                CC = CC,
                phone = phone,
                amount = amount,
                percentage = percentage,
                months = months

            };
            HttpClient cliente = GetConection();
            var response = await cliente.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(borro), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Borrower>(await response.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<Borrower> editBorrower(string fullname, string CC, string phone, float amount, float percentage, int months)
        {
            Borrower borro = new Borrower()
            {
                id = 0,
                fullname = fullname,
                CC = CC,
                phone = phone,
                amount = amount,
                percentage = percentage,
                months = months

            };
            HttpClient cliente = GetConection();
            var response = await cliente.PutAsync(urlEdit+borrower.id,
                new StringContent(JsonConvert.SerializeObject(borro), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Borrower>(await response.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<Borrower> deleteBorrower(int id)
        {
            HttpClient cliente = GetConection();
            var response = await cliente.DeleteAsync(urlDelete + borrower.id);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Borrower>(await response.Content.ReadAsStringAsync());
            }
            return null;
        }

        private async void EvtAgregarPrestatario(object sender, EventArgs e)
        {
            try
            {
                Borrower borro = await signUpBorrower(EntryNombre.Text, EntryCC.Text, EntryTelefono.Text, Convert.ToSingle(EntryCantidad.Text), Convert.ToSingle(EntryPorcentaje.Text), Int32.Parse(EntryMeses.Text));
                
                if (borro != null)
                {
                    await this.DisplayAlert("Exitoso", "Usuario guardado de manera satisfactoria", "Ok");
                    await Navigation.PushAsync(new UsersTable());
                }
                else
                    await DisplayAlert("Fallido", "Datos invalidos", "Ok");
            }
            catch
            {
                await DisplayAlert("Fallido", "Ingrese los campos", "Ok");
            }
    
        }

        private async void EvtEditarPrestatario(object sender, EventArgs e)
        {
            try
            {
                Borrower borro = await editBorrower(EntryNombre.Text, EntryCC.Text, EntryTelefono.Text, Convert.ToSingle(EntryCantidad.Text), Convert.ToSingle(EntryPorcentaje.Text), Int32.Parse(EntryMeses.Text));

                if (borro != null)
                {
                    await this.DisplayAlert("Exitoso", "Usuario actualizado de manera satisfactoria", "Ok");
                    await Navigation.PushAsync(new UsersTable());
                }
                else
                    await DisplayAlert("Fallido", "Datos invalidos", "Ok");
            }
            catch
            {
                await DisplayAlert("Fallido", "Ingrese los campos", "Ok");
            }

        }

        private async void EvtEliminarPrestatario(object sender, EventArgs e)
        {
            try
            {
                Borrower borro = await deleteBorrower(borrower.id);

                if (borro != null)
                {
                    await this.DisplayAlert("Exitoso", "Usuario eliminado de manera satisfactoria", "Ok");
                    await Navigation.PushAsync(new UsersTable());
                }
                else
                    await DisplayAlert("Fallido", "Datos invalidos", "Ok");
            }
            catch
            {
                await DisplayAlert("Fallido", "Ingrese los campos", "Ok");
            }

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UsersTable());
        }
    }
}