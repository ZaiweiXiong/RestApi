using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi
{
    class employee
    {
        public string id { get; set; }
        public string employee_name { get; set; }
        public string employee_salary { get; set; }
        public string employee_age { get; set; }
        public string profile_image { get; set; }

        public employee(
            string employee_name, 
            string employee_salary, 
            string employee_age)
        {
           
            this.employee_name = employee_name;
            this.employee_salary = employee_salary;
            this.employee_age = employee_age;
        }
        public static int randomID() {

            Random rnd = new Random();
            int id = rnd.Next(60);
            return id;
        }

        public static int RandomNum(int size)
        {
            Random rand = new Random(100);
            int id = rand.Next(000000000, 999999999);
            return id;
        }

        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
                return builder.ToString();
        }

    }
}
