using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Calculator;
using System.Collections.Generic;

namespace TestCalculator
{
    [TestClass]
    public class UnitTest1
    {
        private Calculation calculation;
        public TestContext TestContext { get; set; }
        [TestInitialize]
        public void Setup()
        {
            calculation = new Calculation(10, 5);
        }

        [TestMethod]
        public void TestAddOperator()
        {
            int expected = 15;
            int actual = calculation.Execute("+");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMinusOperator()
        {
            int expected = 5;
            int actual = calculation.Execute("-");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMultiOperator()
        {
            int expected = 50;
            int actual = calculation.Execute("*");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDivOperator()
        {
            int expected = 2;
            int actual = calculation.Execute("/");

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void TestDivideByZero()
        {
            calculation = new Calculation(10, 0);
            calculation.Execute("/");
        }   

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @".\Data\TestExecute.csv", "TestExecute#csv", DataAccessMethod.Sequential)]
        public void TestExecuteData()
        {
            int a = int.Parse(TestContext.DataRow[0].ToString());
            int b = int.Parse(TestContext.DataRow[1].ToString());
            String operation = TestContext.DataRow[2].ToString();
            operation = operation.Substring(1);
            int expected = int.Parse(TestContext.DataRow[3].ToString());
            calculation = new Calculation(a, b);

            Assert.AreEqual(expected, calculation.Execute(operation));
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @".\Data\TestPower.csv", "TestPower#csv", DataAccessMethod.Sequential)]
        public void TestPower()
        {
            double x = double.Parse(TestContext.DataRow[0].ToString());
            int n = int.Parse(TestContext.DataRow[1].ToString());

            String result = TestContext.DataRow[2].ToString();
            double expected = double.Parse(result);

            Assert.AreEqual(result, Calculation.Power(x, n));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPolynomialException()
        {
            int n = 2;
            List<int> arr = new List<int>();
            arr.Add(1);
            arr.Add(2);


            Polynomial polynomial = new Polynomial(n, arr);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @".\Data\TestPolynomial.csv", "TestPolynomial#csv", DataAccessMethod.Sequential)]
        public void TestPolynomial()
        {
            int n = int.Parse(TestContext.DataRow[0].ToString());
            int x = int.Parse(TestContext.DataRow[1].ToString());
            int expected = int.Parse(TestContext.DataRow[2].ToString());
            List<int> arr = new List<int>();
            for (int i = 3; i < TestContext.DataRow.ItemArray.Length; i++)
            {
                if(TestContext.DataRow[i].ToString().Length > 0)
                    arr.Add(int.Parse(TestContext.DataRow[i].ToString()));
            }
            Polynomial polynomial = new Polynomial(n, arr);

            int actual = polynomial.Cal(x);
            Assert.AreEqual(expected, actual);
        }
    }
}
