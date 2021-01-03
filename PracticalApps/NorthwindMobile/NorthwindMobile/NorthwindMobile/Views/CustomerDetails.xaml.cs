using NorthwindMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NorthwindMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerDetails : ContentPage
    {
        public CustomerDetails() {
            InitializeComponent();
            BindingContext = new Customer();
            Title = "Add Customer";
        }

        public CustomerDetails(Customer customer) {
            InitializeComponent();
            BindingContext = customer;
            InsertButton.IsVisible = false;
        }

        private async void InsertButton_Clicked(object sender, EventArgs e) {
            Customer.Customers.Add((Customer)BindingContext);
            await Navigation.PopAsync(animated: true);
        }
    }
}