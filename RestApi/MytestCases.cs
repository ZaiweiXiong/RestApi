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
    class MytestCases :BaseSetup
    {

        private static String URL = ConfigurationManager.AppSettings.Get("URL");
        private static String amount = ConfigurationManager.AppSettings.Get("amount");

        private static String employee_name = ConfigurationManager.AppSettings.Get("employee_name");
        private static String employee_salary = ConfigurationManager.AppSettings.Get("employee_age");
        private static String employee_age = ConfigurationManager.AppSettings.Get("URL");
        private static String profile_image = ConfigurationManager.AppSettings.Get("profile_image");
        private static String name = ConfigurationManager.AppSettings.Get("name");
        private static String salary = ConfigurationManager.AppSettings.Get("salary");
        private static String age = ConfigurationManager.AppSettings.Get("age");
      


        static List<employee> employees = new List<employee>();

        [Test]
        [Category("QuickTests")]
        public static async Task testAGetEmployeesAsync()
        {

            //this is positive test case  and list all emoplyees
            using (HttpClient client = new HttpClient())
            {

                using (HttpResponseMessage response = await client.GetAsync(URL+ "/employees"))
                {
                    using (HttpContent content = response.Content)
                    {

                        string mycontent = await content.ReadAsStringAsync();
                        JArray a = JArray.Parse(mycontent);
                        for (int i = 0; i < a.Count; i++)
                        {

                            string emp = a[i].ToString();
                            employee e = JsonConvert.DeserializeObject<employee>(emp);
                            employees.Add(e);
                            Console.WriteLine(i + " current employee id " + e.id);

                        }

                            Console.WriteLine("Current employee's amount =>" + employees.Count);
                            Assert.IsNotNull(mycontent);
                            Assert.AreEqual(a.Count, employees.Count);

                    }
                }

            }
                  
        }


        [Test]
        public static async Task testGetEmployee()
        {
                //this is positive test case and find employee 
                Console.WriteLine("Current employees amount are =>" + employees.Count);
                int length = Int32.Parse(amount);
                employee[] strs = employees.ToArray();

                int i = 1;

                while (i<length) {

                employee e = strs[i];

                int id_ = Int32.Parse(e.id);

                using (HttpClient client = new HttpClient())
                {

                    using (HttpResponseMessage response = await client.GetAsync(URL + "/employee/" + id_))
                    {
                        using (HttpContent content = response.Content)
                        {

                            string mycontent = await content.ReadAsStringAsync();

                            Assert.AreNotEqual(mycontent, false);

                            if (mycontent != "false")
                            {
                                Console.WriteLine("id is existed"+id_);
                            }
                            else
                            {
                                Console.WriteLine("id is not existed"+id_);
                            }

                        }
                    }

                }

                        i++;
            }
                
        }

        
        [Test]
        public static async Task testNegativeGetEmployee() {

            // this is Negative test case and failed to get employee
            int id_ = employee.RandomNum(100000);

            using (HttpClient client = new HttpClient()) {

                using (HttpResponseMessage response = await client.GetAsync(URL + "/employee/" + id_)) {

                    using (HttpContent content = response.Content) {

                        string mycontent = await content.ReadAsStringAsync();
                       
                        StringAssert.Contains("false", mycontent);

                    }
                }
            }
        }

        [Test]
        public static async Task testCreateEmployee()
        {
            //this is positive test case to crete employee
            var obj = new Emps(name,salary,age);
            var json = JsonConvert.SerializeObject(obj);

            using (HttpClient client = new HttpClient()) {

                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(URL + "/create", content);
                string mycontent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("content is  =>" + mycontent);
                Assert.IsNotEmpty(mycontent);
               
            }
                

        }

        [Test]
        public static async Task testNegativeCreateEmployee()
        {
            //this is Negative test case and failed to creating Employee
            
            var obj = new employee(name, salary, age);
            var json = JsonConvert.SerializeObject(obj);
            Console.WriteLine(json);
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(URL + "/create", content);
                string mycontent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("content is  =>" + mycontent);
                Assert.IsNotEmpty(mycontent);
                StringAssert.Contains("error", mycontent);

            }


        }


        [Test]
        public static async Task testUpdateEmployee()
        {

            //this is positive test case 
            
            var obj = new Emps(name, salary, age);
            var json = JsonConvert.SerializeObject(obj);

            employee[] strs = employees.ToArray();
            employee e = strs[3];
            int id_ = Int32.Parse(e.id);
            Console.WriteLine("updated employee id "+id_);

            using (var client = new HttpClient())

            {
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(URL + "/update/" + id_, content);
                response.EnsureSuccessStatusCode();
                string mycontent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("content is  =>" + mycontent);
                Assert.IsNotEmpty(mycontent);
            }
        }

        [Test]
        public static async Task testNegativeUpdateEmployee() {

            //this is Negative test case to update employee

            var jsonString = "{\"name\":,\"salary\":1,\"age\":3}";
            int id_ = employee.RandomNum(100000);
            Console.WriteLine("updated employee id " + id_);

            using (var client = new HttpClient())

            {
                var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(URL + "/update/" + id_, content);
                response.EnsureSuccessStatusCode();
                string mycontent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("content is  =>" + mycontent);
                StringAssert.Contains("null", mycontent);
            }
        }

        [Test]
        public static async Task testdelteEmployee()
        {
            //this is positive test case 
            employee[] strs = employees.ToArray();
            employee e = strs[employee.randomID()];
            int id = Int32.Parse(e.id);
            Console.WriteLine("deleted employee's id is " + id);


            using (HttpClient client = new HttpClient())
            {

              using (HttpResponseMessage response = await client.DeleteAsync(URL+"/delete/" + id))

                   
                {
                    using (HttpContent content = response.Content)
                    {

                        string mycontent = await content.ReadAsStringAsync();
                        Console.WriteLine("content is  =>" + mycontent);
                        StringAssert.Contains("success", mycontent);
                       
                    }
                }
            }           
        }



        [Test]
        public static async Task testNegativedelteEmployee() {

            //this is Negative test case

            int id_ = employee.RandomNum(6);
            string id = employee.RandomString(6,false);
            id += id_.ToString();

            Console.WriteLine("deleted employee's id is " + id);

            using (HttpClient client = new HttpClient())
            {

                using (HttpResponseMessage response = await client.DeleteAsync(URL + "/delete/" + id))

                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();
                        Console.WriteLine("content is  =>" + mycontent);
                        if (mycontent.Contains("success")) {

                            Console.WriteLine("the id is as string so it should not be supported by deleting " + id);
                        }
                        
                            StringAssert.Contains("OK", response.StatusCode.ToString());
                        
                    }
                }

            }
        }

    }
}
