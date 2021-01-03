using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace NorthwindMobile.Models
{
    public class Customer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static IList<Customer> Customers;

        static Customer() {
            Customers = new ObservableCollection<Customer>();
        }

        private string customerID;
        private string companyName;
        private string contactName;
        private string city;
        private string country;
        private string phone;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string CustomerID {
            get => customerID;
            set {
                customerID = value;
                NotifyPropertyChanged();
            }
        }

        public string CompanyName {
            get => companyName;
            set {
                companyName = value;
                NotifyPropertyChanged();
            }
        }

        public string ContactName {
            get => contactName;
            set {
                contactName = value;
                NotifyPropertyChanged();
            }
        }

        public string City {
            get => city;
            set {
                city = value;
                NotifyPropertyChanged();
            }
        }

        public string Country {
            get => country;
            set {
                country = value;
                NotifyPropertyChanged();
            }
        }

        public string Phone {
            get => phone;
            set {
                phone = value;
                NotifyPropertyChanged();
            }
        }

        public string Location {
            get => $"{City}, {Country}";
        }

        public static void AddSampleData() {
            Customers.Add(new Customer {
                CustomerID = "ALFKI",
                CompanyName = "Alfreda Futterkiste",
                ContactName = "Maria Anders",
                City = "Berlin",
                Country = "Germany",
                Phone = "030-0074321"
            });

            Customers.Add(new Customer {
                CustomerID = "FRANK",
                CompanyName = "Frankversand",
                ContactName = "Peter Franken",
                City = "Munchen",
                Country = "Germany",
                Phone = "089-0877310"
            });

            Customers.Add(new Customer {
                CustomerID = "SEVES",
                CompanyName = "Seven Seas Imports",
                ContactName = "Hari Kumar",
                City = "London",
                Country = "UK",
                Phone = "(171) 555-1717"
            });
        }

    }
}
