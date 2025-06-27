using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDSystemData
{

    public static class InMemory
    {


        //data for ID and Name
        public static readonly Dictionary<string, string> validIds = new Dictionary<string, string>
        {


            { "2023-0001", "Yves" },
            { "2024-0002", "JM" },
            { "2024-0003", "Alfred" }


        };


        //data for schef
        public static readonly Dictionary<string, string> attendances = new Dictionary<string, string>
        {


            { "2023-0001", "\n--- Schedule ---\n" +
                "Monday - Friday: 8:00 AM - 12:00 PM\n" +
                "Saturday: 10:00 AM - 1:00 PM\n" +
                "Sunday: No Classes\n" },


            { "2024-0002", "\n--- Schedule ---\n" +
                "Monday - Friday: 1:00 PM - 5:00 PM\n" +
                "Saturday: 9:00 AM - 12:00 PM\n" +
                "Sunday: No Classes\n" },


            { "2024-0003", "\n--- Schedule ---\n" +
                "Monday - Friday: 9:00 AM - 3:00 PM\n" +
                "Saturday: 8:00 AM - 11:00 AM\n" +
                "Sunday: No Classes\n" }


        };


    }
}
