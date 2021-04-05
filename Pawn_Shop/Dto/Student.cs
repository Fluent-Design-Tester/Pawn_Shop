using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Dto
{
    public class Student
    {
        [DisplayName("No")]
        public int student_id { get; set; }

        [DisplayName("Name")]
        public String name { get; set; }

        [DisplayName("Gender")]
        public int gender { get; set; }

        [DisplayName("Nrc")]
        public String nrc { get; set; }

        [DisplayName("Birthday")]
        public String birthday { get; set; }

        [DisplayName("Phone")]
        public String phone { get; set; }

        [DisplayName("Address")]
        public String address { get; set; }

        [DisplayName("Hostel Address")]
        public String hostel_address { get; set; }

        public Student(int student_id, String name, int gender, String nrc, String birthday, String phone, String address, String hostel_address)
        {
            this.student_id = student_id;
            this.name = name;
            this.gender = gender;
            this.nrc = nrc;
            this.birthday = birthday;
            this.phone = phone;
            this.address = address;
            this.hostel_address = hostel_address;
        }
    }
}
