using System;

namespace Packt.Shared
{
    public class Thing
    {
        public object Data = default;

        public string Process(object input)
        {
            if (Data == input)
            {
                return "Data and input are the same";
            }
            else
            {
                return "Data and input are NOT the same";
            }
        }
    }

    public class GenericThing<T> where T : IComparable
    {
        public T Data = default(T);

        public string Process(T input){
            if(Data.CompareTo(input) == 0){
                return "Data and input are the same";
            }
            else{
                return "Data and input are NOT the same";
            }
        }
    }
}