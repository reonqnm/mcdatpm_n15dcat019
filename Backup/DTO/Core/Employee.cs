using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Core
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName  { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
