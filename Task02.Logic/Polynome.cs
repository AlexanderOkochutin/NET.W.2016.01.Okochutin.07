using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02.Logic
{
    /// <summary>
    /// class for polynome functionality, you can set acuracy epsilon in the app.config 
    /// </summary>
    public sealed class Polynome : IEquatable<Polynome>, ICloneable
    {
        private double[] Coefficients { get; }

        private int Length => Coefficients.Length;

        private static double epsilon;

        public static double Epsilon => epsilon;

        public int Degree
        {
            get
            {
                if (Length == 1 || Length ==0) return 0;
                int i = 0;
                for (i = Length - 1; i >= 0; i--)
                {
                    if (Math.Abs(this[i]) >= epsilon) break;
                }
                return i;
            }
        }

        public Polynome(params double[] coefficients)
        {
            Coefficients = new double[coefficients.Length];
            Array.Copy(coefficients, Coefficients, coefficients.Length);
        }

        static Polynome()
        {
                epsilon = double.Parse(ConfigurationManager.AppSettings["epsilon"], CultureInfo.InvariantCulture);
        }

        public double this[int index]
        {
            get
            {
                if (index >= Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                return Coefficients[index];
            }
        }

        /// <summary>
        /// calculate the value of polynome in point x
        /// </summary>
        /// <param name="x"></param>
        /// <returns>the value of polynome in point x</returns>
        public double Calculate(float x)
        {
            return Coefficients.Select((t, i) => t * Math.Pow(x, i)).Sum();
        }

        /// <summary>
        /// represent polynome in next format
        /// </summary>
        /// <returns>C0*(X^0)+C1*(X^1) ...</returns>
        public override string ToString()
        {
            StringBuilder polynome = new StringBuilder();
            for (int i = 0; i < Coefficients.Length; i++)
            {
                if (Math.Abs(this[i]) >= epsilon)
                {
                    if ((i != 0) && Coefficients[i] > 0)
                    {
                        polynome.Append("+");
                    }
                    polynome.Append(Coefficients[i] + "*" + "(X^" + i + ")");
                }
            }
            return polynome.ToString();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Polynome)obj);
        }

        public override int GetHashCode()
        {
            return Coefficients.Aggregate(Coefficients.Length, (current, t) => unchecked(current * 31459 + (int)t));
        }

        public bool Equals(Polynome other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (Length != other.Length) return false;
            for (int i = 0; i < Length; i++)
            {
                if (Math.Abs(this[i] - other[i]) > epsilon) return false;
            }
            return true;
        }

        public Polynome Clone()
        {
            return new Polynome(Coefficients);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public static Polynome operator +(Polynome pol1, Polynome pol2)
        {
            double[] temp = new double[Math.Max(pol1.Coefficients.Length, pol2.Coefficients.Length)];
            for (int i = 0; i < pol1.Length; i++)
            {
                temp[i] += pol1[i];
            }
            for (int i = 0; i < pol2.Length; i++)
            {
                temp[i] += pol2[i];
            }
            return new Polynome(temp);
        }

        public static Polynome Add(Polynome pol1, Polynome pol2)
        {
            return pol1 + pol2;
        }

        public static Polynome operator -(Polynome pol)
        {
            return pol*(-1f);
        }

        public static Polynome operator -(Polynome pol1, Polynome pol2)
        {
            return pol1 + (-pol2);
        }

        public static Polynome Substract(Polynome pol1, Polynome pol2)
        {
            return pol1 - pol2;
        }

        public static Polynome operator *(Polynome pol1, float x)
        {
            double[] temp = new double[pol1.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = pol1[i] * x;
            }
            return new Polynome(temp);
        }

        public static Polynome operator *(Polynome pol1, Polynome pol2)
        {
            double[] temp = new double[pol1.Length + pol2.Length - 1];
            for (int i = 0; i < pol1.Length; i++)
            {
                for (int j = 0; j < pol2.Length; j++)
                {
                    temp[j + i] += pol1[i] * pol2[j];
                }
            }
            return new Polynome(temp);
        }

        public static Polynome Multiply(Polynome pol1, Polynome pol2)
        {
            return pol1*pol2;
        }

    }
}
