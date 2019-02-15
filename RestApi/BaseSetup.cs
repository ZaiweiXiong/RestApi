using NUnit.Framework;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace RestApi
{
    [TestFixture]
    class BaseSetup
    {

        [SetUp]

        public static void testSetUp()
        {

            Console.WriteLine(" set up!");
        }

        [TearDown]
        public static void testTeardown() {

            Console.WriteLine(" testTeardown ");
        }
    }
}
