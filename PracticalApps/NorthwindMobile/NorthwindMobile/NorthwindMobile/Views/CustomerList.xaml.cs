using NorthwindMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NorthwindMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerList : ContentPage
    {
        public CustomerList() {
            InitializeComponent();

            Customer.Customers.Clear();

            var client = new HttpClient {
                BaseAddress = new Uri("https://localhost:5003/")
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/customers").Result;

            response.EnsureSuccessStatusCode();

            string content = response.Content.ReadAsStringAsync().Result;

            var customersFromService = JsonConvert.DeserializeObject<IEnumerable<Customer>>(content);

            foreach(Customer customer in customersFromService) {
                Customer.Customers.Add(customer);
            }

            //Customer.AddSampleData();
            BindingContext = Customer.Customers;
        }

        async void Customer_Tapped(object sender, ItemTappedEventArgs e) {

            if(e.Item is Customer customer){
                await Navigation.PushAsync(new CustomerDetails(customer));
            }
        }

        async void Customers_Refreshing(object sender, EventArgs e) {
            var listView = sender as ListView;
            listView.IsRefreshing = true;

            await Task.Delay(1500);

            listView.IsRefreshing = false;
        }

        void Customer_Deleted(object sender, EventArgs e) {
            var menuItem = sender as MenuItem;
            Customer customer = menuItem.BindingContext as Customer;
            Customer.Customers.Remove(customer);
        }

        async void Customer_Phoned(object sender, EventArgs e) {
            var menuItem = sender as MenuItem;
            var customer = menuItem.BindingContext as Customer;

            if (await DisplayAlert("Dial a Number", "Would you like to call " + customer.Phone + "?",
                "Yes", "No")) {
                var dialer = DependencyService.Get<IDialer>();
                
                dialer?.Dial(customer.Phone);
            }
        }

        async void Add_Activated(object sender, EventArgs e) {
            await Navigation.PushAsync(new CustomerDetails());
        }
    }
}