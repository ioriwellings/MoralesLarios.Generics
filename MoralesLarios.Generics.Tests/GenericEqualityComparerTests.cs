using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoralesLarios.Generics.Tests.Types;
using System.Collections.Generic;
using System.Linq;

namespace MoralesLarios.Generics.Tests
{
    [TestClass]
    public class GenericEqualityComparerTests
    {

        private GenericEqualityComparer<Customer> instance;


        private List<Customer> customers;


        [TestInitialize]
        public void TestInitialize()
        {
            customers = Customer.GetData().ToList();

        }


        [TestMethod]
        public void ForField_ID()
        {
            Func<Customer, object> fieldComparator = a => a.ID;

            instance = new GenericEqualityComparer<Customer>(fieldComparator);

            var result = customers.Distinct(instance).Count();

            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void ForField_AllCityMadrid_OK()
        {
            Func<Customer, object> fieldComparator = a => a.City;

            customers.ForEach(a => a.City = "Madrid");

            instance = new GenericEqualityComparer<Customer>(fieldComparator);

            var result = customers.Distinct(instance).Count();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ForField_NullArgument_ThrowException()
        {
            instance = new GenericEqualityComparer<Customer>(null);
        }


        [TestMethod]
        public void ForExpression_FirstNameChar_OK()
        {
            Func<Customer, Customer, bool> ExpressionComparator = (a,b) => a.Name.FirstOrDefault() == b.Name.FirstOrDefault();

            instance = new GenericEqualityComparer<Customer>(ExpressionComparator);

            var result = customers.Distinct(instance).Count();

            Assert.AreEqual(4, result);
        }

        


    }
}
