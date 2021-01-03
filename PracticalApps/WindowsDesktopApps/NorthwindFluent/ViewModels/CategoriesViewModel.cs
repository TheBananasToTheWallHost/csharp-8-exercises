using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packt.Shared;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Data.SqlClient;

namespace NorthwindFluent.ViewModels
{
    public class CategoriesViewModel
    {
        public class CategoryJson
        {
            public int categoryID;
            public string categoryName;
            public string description;
        }

        public ObservableCollection<Category> Categories { get; set; }

        public CategoriesViewModel() {
            using (var http = new HttpClient()) {
                http.BaseAddress = new Uri("https://localhost:5001/");

                var serializer = new DataContractJsonSerializer(typeof(List<CategoryJson>));
                var stream = http.GetStreamAsync("api/categories").Result;

                var apiCategories = serializer.ReadObject(stream) as List<CategoryJson>;

                var categories = apiCategories.Select(category =>
                    new Category {
                        CategoryID = category.categoryID,
                        CategoryName = category.categoryName,
                        Description = category.description
                    });

                Categories = new ObservableCollection<Category>(categories);
            }
        }
    }
}
