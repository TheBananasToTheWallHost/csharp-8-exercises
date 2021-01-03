using System;

namespace WorkingWithReflection
{
    [System.AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class CoderAttribute : System.Attribute
    {
        // See the attribute guidelines at
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        public string Coder{get; set;}
        public DateTime LastModified{get; set;}

        public CoderAttribute(string coder, string lastModified){
            Coder = coder;
            LastModified = DateTime.Parse(lastModified);
        }

    }
}