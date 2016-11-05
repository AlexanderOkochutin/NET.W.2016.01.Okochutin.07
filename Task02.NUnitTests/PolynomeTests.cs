using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Task02.Logic;
using System.Collections;

namespace Task02.NUnitTests
{
    [TestFixture]
    public class PolynomeTests
    {
        public class MyDataClass
        {
            public static IEnumerable MultiplyTestCases
            {
                get
                {
                    yield return new TestCaseData(new Polynome(1,-1), new Polynome(1,1)).Returns(new Polynome(1,0,-1));
                    yield return new TestCaseData(new Polynome(1, 1), new Polynome(1,1)).Returns(new Polynome(1, 2,1));
                    yield return new TestCaseData(new Polynome(1, 1), new Polynome(1,-1,1)).Returns(new Polynome(1, 0, 0,1));
                }
            }
            public static IEnumerable SumTestCases
            {
                get
                {
                    yield return new TestCaseData(new Polynome(1, -1), new Polynome(1, 1)).Returns(new Polynome(2, 0));
                    yield return new TestCaseData(new Polynome(1, 1), new Polynome(1, 1)).Returns(new Polynome(2, 2));
                    yield return new TestCaseData(new Polynome(1, 1), new Polynome(1, -1, 1)).Returns(new Polynome(2, 0, 1));
                }
            }
            public static IEnumerable ConstMultiplyTestCases
            {
                get
                {
                    yield return new TestCaseData(new Polynome(1, -1), 5).Returns(new Polynome(5, -5));
                    yield return new TestCaseData(new Polynome(1, 1), -6).Returns(new Polynome(-6, -6));
                    yield return new TestCaseData(new Polynome(1, 1),12).Returns(new Polynome(12, 12));
                }
            }
            public static IEnumerable GetHashCodeTest
            {
                get
                {
                    yield return new TestCaseData(new Polynome(1, -1), new Polynome(1, -1)).Returns((new Polynome(2, -2)).GetHashCode());
                    yield return new TestCaseData(new Polynome(1, 1), new Polynome(1, 2,5)).Returns((new Polynome(2,3,5)).GetHashCode());
                    yield return new TestCaseData(new Polynome(1, 1), new Polynome(10, -1)).Returns((new Polynome(11, 0)).GetHashCode());
                }
            }
            public static IEnumerable PolyEqualTest
            {
                get
                {
                    yield return new TestCaseData(new Polynome(1 + Polynome.Epsilon/2, -1,5.0,6.0,8.0), new Polynome(1, -1 + Polynome.Epsilon/2, 5,6 + Polynome.Epsilon, 8)).Returns(true);
                    yield return new TestCaseData(new Polynome(1, 1,5,3), new Polynome(1, 1, 5 +Polynome.Epsilon/2,3-Polynome.Epsilon/2)).Returns(true);
                    yield return new TestCaseData(new Polynome(1, 1,2), new Polynome(1, 1,2.1)).Returns(false);
                }
            }

        }


        [Test,TestCaseSource(typeof(MyDataClass),nameof(MyDataClass.MultiplyTestCases))]
        public Polynome Polynome_Multiply_tests(Polynome pol1, Polynome pol2)
        {
            return pol1*pol2;
        }

        [Test, TestCaseSource(typeof(MyDataClass), nameof(MyDataClass.SumTestCases))]
        public Polynome Polynome_Sum_tests(Polynome pol1, Polynome pol2)
        {
            return pol1 + pol2;
        }
        [Test, TestCaseSource(typeof(MyDataClass), nameof(MyDataClass.ConstMultiplyTestCases))]
        public Polynome Polynome_ConstMul_tests(Polynome pol1, float num)
        {
            return pol1*num;
        }
        [Test, TestCaseSource(typeof(MyDataClass), nameof(MyDataClass.GetHashCodeTest))]
        public int Polynome_GetHash_tests(Polynome pol1, Polynome pol2)
        {
            return (pol1 + pol2).GetHashCode();
        }

        [Test, TestCaseSource(typeof(MyDataClass), nameof(MyDataClass.PolyEqualTest))]
        public bool Polynome_Equals_tests(Polynome pol1, Polynome pol2)
        {
            return pol1.Equals(pol2);
        }
    }
}
