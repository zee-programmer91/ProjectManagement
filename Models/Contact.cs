using Npgsql;
using System;

namespace LiveNiceApp
{
    internal class Contact
    {
        public int contact_id { get; set; }
        public int person_id { get; set; }
        public string Email { get; set; }
        public int cellphone_number { get; set; }
    }
}
