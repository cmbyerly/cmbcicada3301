using LiberPrimusAnalysisTool.Analyzers;
using LiberPrimusAnalysisTool.Utility;

namespace LiberPrimusAnalysisTool
{
    /// <summary>
    /// Program
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            FileUtilities.RemovePreviousRuns();

            var dontExit = true;

            while (dontExit)
            {
                Console.Clear();
                Console.WriteLine("THIS TOOL REMOVES OUTPUT BETWEEN RUNS!!!");
                Console.WriteLine("Running Liber Primus Analysis Tool");
                Console.WriteLine("Enter the test number you want to perform.");
                Console.WriteLine("0: Exit Program");
                Console.WriteLine("1: Round 1 Test");
                Console.WriteLine("2: Round 2 Test");
                Console.WriteLine("3: Color Report");
                Console.WriteLine("9999: All Tests");

                Console.Write("Choice: ");
                var choice = Console.ReadLine();

                switch (choice.Trim())
                {
                    case "0":
                        dontExit = false;
                        break;
                    case "1":
                        ColorCountText.RunMe();
                        break;
                    case "2":
                        ColorBreakDownText.RunMe();
                        break;
                    case "3":
                        ColorReport.RunMe();
                        break;
                    case "9999":
                        ColorReport.RunMe();
                        ColorCountText.RunMe();
                        ColorBreakDownText.RunMe();
                        break;
                    default:
                        Console.WriteLine("Not a valid choice.");
                        break;
                }
            }
        }

        
    }
}
