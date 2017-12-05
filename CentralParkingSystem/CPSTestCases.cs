using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralParkingSystem
{
    public class CPSTestCases
    {
        public void RunInputFiles()
        {
            string[] testInputFiles = new string[] {@".\TestFiles\TestCase1.txt", @".\TestFiles\TestCase2.txt" };

            int gateCount = 1;
            var tasks = testInputFiles.Select(x => Task.Run(() =>
            {
                CPSClientClass clinet = new CPSClientClass("Gate" + gateCount++);
                clinet.ParseInputFile(x);
            })).ToArray();

            Task.WaitAll(tasks);
            Console.WriteLine("Test completed");
            Console.ReadLine();
        }
    }
}
