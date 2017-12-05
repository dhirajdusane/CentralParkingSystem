using CentralParkingSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralParkingSystem
{
    /// <summary>
    /// Responsible to simulate parking system
    /// </summary>
    public class CPSTestCases
    {
        /// <summary>
        /// Runs predefined test files.
        /// </summary>
        public void RunInputFiles()
        {
            CPS.CPSBuildDirector director = new CPS.CPSBuildDirector();
            ICPSMain mainInstance = director.ConstructUsing(new CPS.TestCaseBuilder());

            string[] testInputFiles = new string[] { @".\TestFiles\TestCase1.txt", @".\TestFiles\TestCase2.txt" };
            Task[] tasks = new Task[testInputFiles.Length];
            foreach (var item in Enumerable.Range(0, testInputFiles.Length))            
            {
                CPSClientClass clinet = new CPSClientClass(item, mainInstance);
                clinet.ParseInputFile(testInputFiles[item]);
            }

            Task.WaitAll(tasks);
            Console.WriteLine("Test completed");
            Console.ReadLine();
        }

        /// <summary>
        /// Runs random parking entry from possible inputs
        /// </summary>
        public void RunRandomTest()
        {
            CPS.CPSBuildDirector director = new CPS.CPSBuildDirector();
            ICPSMain mainInstance = director.ConstructUsing(new CPS.TestCaseBuilder());

            string[] testInputs = File.ReadAllLines(@".\TestFiles\RandomTestCase.txt");
            int length = testInputs.Length, width = 10;
            Task[] tasks = new Task[width];

            foreach (var item in Enumerable.Range(0, width))
            {
                tasks[item] = Task.Run(() =>
                {
                    Random rand = new Random();

                    CPSClientClass clinet = new CPSClientClass(item, mainInstance);
                    for (int i = 0; i < width; i++)
                    {
                        string entryString = testInputs[rand.Next(length)];
                        clinet.ParseEntry(entryString);
                    }
                });
            }

            Task.WaitAll(tasks);
            Console.WriteLine("Test completed");
            Console.ReadLine();
        }
    }
}