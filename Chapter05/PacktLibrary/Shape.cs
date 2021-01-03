using System;
using System.Xml.Serialization;
using static System.Math;

namespace Packt.Shared
{
    [XmlInclude(typeof(Rectangle))]
    [XmlInclude(typeof(Circle))]
    public abstract class Shape
    {

        public virtual string Color{
            get; set;
        }

        public abstract double Area{get;}
    }

    public class Rectangle : Shape
    {
        public double Height{get; set;}
        public double Width{get;set;}

        public override double Area{
            get{
                return Width * Height;
            }
        }

        // public Rectangle(){

        // }

        // public Rectangle(double width, double height)
        // {
        //     _width = width;
        //     _height = height;
        // }
    }

    public class Square : Shape
    {
        // public override double Height{
        //     set{
        //         _height = value;
        //         _width = value;
        //     }
        // }

        // public override double Width{
        //     set{
        //         _height = value;
        //         _width = value;
        //     }
        // }

        public override double Area{
            get{
                return 0;
            }
        }

        // public Square(double sideLength)
        // {
        //     _width = sideLength;
        //     _height = sideLength;
        // }

        // public Square(){

        // }
    }

    public class Circle : Shape
    {

        public double Radius{
            get; set;
        }

        public override double Area{
            get{
                return Radius * Radius * PI;
            }
        }

        // public Circle(){

        // }

        // public Circle(double radius)
        // {
        //     _radius = radius;
        //     _width = 2 * radius;
        //     _height = 2 * radius;
        // }
    }
}