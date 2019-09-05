using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComparer.UnitTests
{
    public static class AssertExtensions
    {
        public static void IsOfType<T>(this Assert assert, object value)
        {
            Assert.IsInstanceOfType(value, typeof(T));
        }

        public static void IsOfType<T>(this Assert assert, object value, string message)
        {
            Assert.IsInstanceOfType(value, typeof(T), message);
        }
        public static void IsOfType<T>(this Assert assert, object value, string message, params object[] parameters)
        {
            Assert.IsInstanceOfType(value, typeof(T), message, parameters);
        }


    }
}
