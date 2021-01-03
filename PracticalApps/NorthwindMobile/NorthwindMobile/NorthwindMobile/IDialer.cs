using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindMobile
{
    public interface IDialer
    {
        bool Dial(string number);
    }
}
