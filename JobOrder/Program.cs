using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobOrder
{
    class Program
    {
        static string JobOrder(string input)
        {
            string jobSequence = "";
            List<string> job = new List<string>();
            char[,] inputArray = new char[6,2];

            int j = 0;
            int k = 0;
            // build up 2d string array, which pairs up the dependencies
            for(int i =0; i < input.Length; i++)
            {
                if (input[i] == ' ')
                {
                    j++;
                    if (k % 2 == 0) // so k mod give us 0 for first element in the next item in inputArray
                        k++;
                }
                else
                {
                    inputArray[j, k % 2] = input[i];    // add to input string array
                    if (k % 2 == 0 && inputArray[j, (k + 1) % 2] == inputArray[j, k % 2])
                        throw new Exception("Jobs cannot depend on themselves");
                }
                k++;
            }

            if (inputArray[0, 1] != '\0' || inputArray[j, 1] != '\0')
                throw new Exception("Circular dependencies are not allowed");

            // now build list of jobs to do depending on the input string array, which contains the dependencies
            for (int i = 0; i < inputArray.Length/2; i++)
            {
                // if there is no dependency then simply add to the job list. Although make a check to see if the job is in the list first. if it is then we dont need to add it again
                if (inputArray[i, 1] == '\0')
                {
                    int pos = job.IndexOf(inputArray[i, 0].ToString());
                    if (pos < 0)
                        job.Add(inputArray[i, 0].ToString());
                }
                else
                {
                    // if there is a dependency then we need to add the dependent item after its parent item
                    int pos = job.IndexOf(inputArray[i, 0].ToString());
                    // find position of the child item, see if it exists in the job list.
                    if (pos > 0)    // if it does then simply add its parent item in its place using insert, which moves the child item along the list
                        job.Insert(pos, inputArray[i, 1].ToString());
                    else
                    {
                        // if the child item does not exist then we just need to add it onto the end of the list.
                        // first we try and find out if the parent item exists, if it doesnt then we add that first and then the dependent child item after
                        pos = job.IndexOf(inputArray[i, 1].ToString());
                        if (pos < 0)
                            job.Add(inputArray[i, 1].ToString());
                        job.Add(inputArray[i, 0].ToString());
                    }
                }
            }
            foreach (var item in job)
            {
                string test = item.ToString();
                jobSequence = jobSequence + test;
            }
            jobSequence = "Your job list order is " + jobSequence;
            return jobSequence;
        }

        static void Main(string[] args)
        {
            string userInput;
        startQueryProcess:
            Console.WriteLine("Please enter your job structure below (press escape after last character). There is a limit of 6 job items.");
        waitForUserInput:
            userInput = Console.ReadLine();
            if (userInput.Length > 17)
            {
                Console.WriteLine("Please enter a max of 6 jobs");
                goto waitForUserInput;
            }
            
            try
            {
                string order = JobOrder(userInput);
                if (order.Length > 0)
                    Console.WriteLine("'{0}'", order);
            }
            catch (Exception ex) // returns exceptions as described in the pdf
            {
                if (string.Equals(ex.Message, "Index was outside the bounds of the array."))
                {
                    Console.WriteLine("You have entered too many jobs, please try again");
                    goto waitForUserInput;
                }
                else
                    Console.WriteLine(ex.Message.ToString());
            }
            Console.WriteLine("Press Q to close application or enter to continue"); // gives user option to close the application or run it again
            userInput = Console.ReadLine();
            if (userInput.ToString() == "Q" || userInput.ToString() == "q")
                Environment.Exit(0);
            else
                goto startQueryProcess;
            Console.ReadKey();
        }
    }
}
