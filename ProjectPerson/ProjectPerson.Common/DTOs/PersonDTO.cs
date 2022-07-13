
using System;
using static ProjectPerson.Common.Models.Enums;

namespace ProjectPerson.Common.DTOs
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Birth { get; set; }
        public PersonType PersonType { get; set; }
        public VerificationStatus Verification { get; set; }
    }
}