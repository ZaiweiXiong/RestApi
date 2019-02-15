

namespace RestApi
{
    class Emps
    {
        public string name { get; set; }
        public string salary { get; set; }
        public string age { get; set; }

        public Emps(string name,string salary,string age) {
            this.name = name;
            this.salary = salary;
            this.age = age;
        }
    }
}
