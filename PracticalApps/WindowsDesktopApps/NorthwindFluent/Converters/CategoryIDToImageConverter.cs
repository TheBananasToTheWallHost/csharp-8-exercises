﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace NorthwindFluent.Converters
{
    class CategoryIDToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) {
            int categoryID = (int)value;
            string path = string.Format(
                format: "{0}/Assets/CategoryImages/category{1}-small.jpeg",
                arg0: Environment.CurrentDirectory,
                arg1: categoryID);

            var image = new BitmapImage(new Uri(path));
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
