using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDSystemBusinessLogic
{
    public class Checking
    {
        


        //check if the id inputted is equal to id1, id2, and id3
        public static bool checkIDIfValid(string studentIdInput)
        {


            string[] validIds = { "2023-0001", "2024-0002", "2024-0003" };


            return validIds.Contains(studentIdInput);


        }
    }
}
