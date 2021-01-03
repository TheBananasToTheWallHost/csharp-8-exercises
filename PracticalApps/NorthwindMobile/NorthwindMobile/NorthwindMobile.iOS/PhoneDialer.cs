using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Foundation;
using NorthwindMobile.iOS;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(PhoneDialer))]
namespace NorthwindMobile.iOS
{
    public class PhoneDialer : IDialer
    {
        public bool Dial(string number) {
            return UIApplication.SharedApplication.OpenUrl(new NSUrl("tel:" + number));
        }
    }
}