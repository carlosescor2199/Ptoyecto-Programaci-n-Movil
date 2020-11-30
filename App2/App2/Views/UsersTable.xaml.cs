using App2.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersTable : ContentPage
    {
        private static string url = "http://10.0.2.2:5000/borrower/allborrowers";
        private static ObservableCollection<Borrower> observable_productosAPI;
        public UsersTable()
        {
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            InitializeComponent();
            cargar();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AgregarPrestatario());
        }

        public async void cargar()
        {
            HttpClient cl = new HttpClient();
            var content = await cl.GetStringAsync(url);
            List<Borrower> post = JsonConvert.DeserializeObject<List<Borrower>>(content);           
            observable_productosAPI = new ObservableCollection<Borrower>(post);
            UsersTable.BorrowerList.ItemsSource = observable_productosAPI;
        }

        private async void ContactosList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var auxBorrower = (Borrower)e.SelectedItem;
            Borrower borrower = new Borrower
            {
                id = auxBorrower.id,
                fullname = auxBorrower.fullname,
                CC = auxBorrower.CC,
                phone = auxBorrower.phone,
                amount = auxBorrower.amount,
                percentage = auxBorrower.percentage,
                months = auxBorrower.months,
            };
            await Navigation.PushAsync(new AgregarPrestatario(borrower));
        }
    }
}