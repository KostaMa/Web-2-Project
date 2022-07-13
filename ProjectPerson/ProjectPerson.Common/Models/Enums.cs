using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPerson.Common.Models
{
    public class Enums
    {
        public enum OrderStatus
        {
            Accept,
            OnHold,
            Done
        }

        public enum PersonType
        {
            Admin,
            Deliverer,
            Customer,
            None,
        }

        public enum VerificationStatus
        {
            Active,
            Denied,
            OnHold,
            None
        }
    }
}
