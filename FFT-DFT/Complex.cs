using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFT_DFT {
    enum ComplexType {
        Real, Imaginary, UpperRight, BottomRight, UpperLeft, BottomLeft
    }
    class Complex : IComparable<Complex>, ICloneable {
        private double x;
        private double y;
        private double absVal;
        private ComplexType numType;

        public Complex(double a, double b, bool cartesian = true) {
            if (cartesian) {
                x = a;
                y = b;
                GetAbsVal();
            }
            else {
                //double rad = b % (2 * Math.PI);
                x = Math.Round(a * Math.Cos(b), 6);
                y = Math.Round(a * Math.Sin(b), 6);
                absVal = a;
            }
            GetLocation();
        }
        
        public double X {
            get { return x; }
            set { 
                x = value;
                GetAbsVal();
                GetLocation();
            }
        }
        public double Y {
            get { return y; }
            set { 
                y = value;
                GetAbsVal();
                GetLocation();
            }
        }
        public double AbsVal {
            get { return Math.Round(absVal,6); }
        }
        public double this[int index] { get {
                switch (index) {
                    case 0: return x;
                    case 1: return y;
                    case 2: return absVal;
                    default:
                        throw new IndexOutOfRangeException("Index out of range...");
                }
            } 
        }

        public string Location {
            get {
                switch (this.numType) {
                    case ComplexType.Real:
                        return "Real";
                    case ComplexType.Imaginary:
                        return "Imaginary";
                    case ComplexType.UpperRight:
                        return "Upper right";
                    case ComplexType.BottomRight:
                        return "Bottom right";
                    case ComplexType.UpperLeft:
                        return "Upper left";
                    case ComplexType.BottomLeft:
                        return "Bottom left";
                    default:
                        return "undefined";
                }
            }
        }

        private void GetLocation() {
            if (y == 0) numType = ComplexType.Real;
            else if (x == 0) numType = ComplexType.Imaginary;
            else if (x > 0 && y > 0) numType = ComplexType.UpperRight;
            else if (x < 0 && y > 0) numType = ComplexType.UpperLeft;
            else if (x < 0 && y < 0) numType = ComplexType.BottomLeft;
            else numType = ComplexType.BottomRight;
        }

        private void GetAbsVal() {
            absVal = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }

        public int CompareTo(Complex other) {
            return absVal.CompareTo(other.absVal);
        }

        public object Clone() {
            return new Complex(x, y);
        }

        public static Complex operator+(Complex z1, Complex z2) => new Complex(z1.X + z2.X, z1.Y + z2.Y);

        public static Complex operator-(Complex z1, Complex z2) => new Complex(z1.X - z2.X, z1.Y - z2.Y);

        public static Complex operator*(Complex z1, Complex z2) {
            double z = z1.absVal * z2.absVal;
            double phi = Math.Atan2(z1.Y, z1.X) + Math.Atan2(z2.Y, z2.X);
            return new Complex(Math.Round(z * Math.Cos(phi), 6), Math.Round(z * Math.Sin(phi), 6));
        }

        public static Complex operator/(Complex z1, Complex z2) {
            double z = z1.absVal / z2.absVal;
            double phi = Math.Atan2(z1.Y, z1.X) - Math.Atan2(z2.Y, z2.X);
            return new Complex(Math.Round(z * Math.Cos(phi), 6), Math.Round(z * Math.Sin(phi), 6));
        }

        public override string ToString() {
            switch (this.numType) {
                case ComplexType.Real:
                    return String.Format("{0}", Math.Round(X, 6));
                case ComplexType.Imaginary:
                    return String.Format("{0}j", Math.Round(Y, 6));
                case ComplexType.UpperRight:
                case ComplexType.BottomRight:
                case ComplexType.UpperLeft:
                case ComplexType.BottomLeft:
                    if (Y > 0) {
                        return String.Format("{0} + {1}j", Math.Round(X, 6), Math.Round(Y, 6));
                    } else {
                        return String.Format("{0} - {1}j", Math.Round(X, 6), Math.Abs(Math.Round(Y, 6)));
                    }
                    
                default:
                    return "";
            }
        }
    }
}
