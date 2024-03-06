using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Record
    {

        public int Id { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
        public int Emp_Id { get; set; }
    }
}
