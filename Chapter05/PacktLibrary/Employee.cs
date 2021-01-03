using System;

namespace Packt.Shared
{
    public class Employee : Person
    {
        public string EmployeeCode{get; set;}
        public DateTime HireDate{get; set;}

        public override void WriteToConsole(){
            Console.WriteLine(
                format: "{0} was born on {1:dd/MM/yy} and hired on {2:dd/MM/yy}",
                Name,
                DateOfBirth,
                HireDate
            );
        }
    }

    public sealed class ScroogeMcDuck{

    }
}