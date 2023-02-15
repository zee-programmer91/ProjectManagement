
namespace ProjectManagement.Model
{
    public class Person
    {
        public int person_id { get; set; }
        public string person_name { get; set; }
        public string person_surname { get; set; }
        public string identity_code { get; set; }

        public Person() {}

        public Person(string value) {
            person_id = 0;
            person_name = "";
            person_surname = "";
        }
    }
}
