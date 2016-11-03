using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02.Logic
{
    /// <summary>
    /// class for polynome functionality
    /// </summary>
    public class Polynome: IEnumerable
    {
        private float[] Coefficients { get; }

        private int Length => Coefficients.Length;

        public Polynome(params float[] coefficients)
        {
            Coefficients = new float[coefficients.Length];
            Array.Copy(coefficients, Coefficients, coefficients.Length);

        }

        public float this[int index] => Coefficients[index];

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
                if (Coefficients[i] != 0)
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
            if (!(obj is Polynome))
            {
                return false;
            }
            else if (this == obj)
            {
                return true;
            }else if (obj == null)
            {
                return false;
            }
            else
            {
                return Coefficients.SequenceEqual(((Polynome) obj).Coefficients);
            }
        }

        public override int GetHashCode()
        {
            return Coefficients.Aggregate(Coefficients.Length, (current, t) => unchecked(current * 31459 + (int)t));
        }

        public IEnumerator GetEnumerator()
        {
           return Coefficients.GetEnumerator();
        }

        public static Polynome operator +(Polynome pol1, Polynome pol2)
        {
            float[] temp = new float[Math.Max(pol1.Coefficients.Length, pol2.Coefficients.Length)];
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

        public static Polynome operator -(Polynome pol1, Polynome pol2)
        {
            float[] temp = new float[Math.Max(pol1.Coefficients.Length, pol2.Coefficients.Length)];
            for (int i = 0; i < pol1.Length; i++)
            {
                temp[i] -= pol1[i];
            }
            for (int i = 0; i < pol2.Length; i++)
            {
                temp[i] -= pol2[i];
            }
            return new Polynome(temp);
        }

        public static Polynome operator *(Polynome pol1, float x)
        {
            float[] temp = new float[pol1.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = pol1[i] * x;
            }
            return new Polynome(temp);
        }

        public static Polynome operator *(Polynome pol1, Polynome pol2)
        {
            float[] temp = new float[pol1.Length + pol2.Length - 1];
            for (int i = 0; i < pol1.Length; i++)
            {
                for (int j = 0; j < pol2.Length; j++)
                {
                    temp[j + i] += pol1[i] * pol2[j];
                }
            }
            return new Polynome(temp);
        }

    }
}
