// See https://aka.ms/new-console-template for more information
namespace codingExercise {
    class Program {
        static void Main(string[] args) {
            // User input to help run the exercise
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Running Coding Exercise by Hayden Westphal");
            string? input = "0";
            while(input != "3") {
                Console.WriteLine("-------------------------------------------");
                Console.Write("Press 1 to create files. Press 2 to run the exercise. Press 3 to exit. ");
                input = Console.ReadLine();
            // Create files
            if (input == "1") {
                createFiles();
                Console.WriteLine("Finished creating files!");
            }
            // Main Exercise
            else if (input == "2") {
                Console.WriteLine("Running exercise...");
                string dir = @"C:\Users\hayde\Desktop\Projects\coding-exercise\";
                string date = DateTime.Now.ToString("yyyyMMdd");

                string admission = dir + @"CombinedLetters\Input\Admission\" + date + @"\";
                string scholar = dir + @"CombinedLetters\Input\Scholarship\"  + date + @"\";
                string output = dir + @"CombinedLetters\Output\"  + date + @"\";
                string archive = dir + @"CombinedLetters\Archive\";

                // Initialize service
                ILetterService service = new LetterService();
                
                // Variables
                string [] fileEntries = Directory.GetFiles(admission);
                int numLetters = 0;
                List<string> letters = new List<string>();

                // Iterate through each admission letter, checking for scholarship matches
                Console.WriteLine("Checking for new entries...");
                foreach(string admissionFile in fileEntries) {
                    int n = admissionFile.Length;
                    // minus 12 to account for the 8 length ID and 4 length extension
                    // Assuming all files end in .txt
                    string ID = admissionFile.Substring(n - 12); // ########.txt 
                    if(File.Exists(scholar + "scholarship-" + ID)) {
                        // Match found, send matching paths for combination
                        string scholarFile = scholar + "scholarship-" + ID;
                        service.CombineTwoLetters(admissionFile, scholarFile, output, "combined-" + ID);
                        // Report stats
                        numLetters++;
                        letters.Add(ID.Substring(0,8));
                    }
                }
                if (numLetters > 0) {
                    Console.WriteLine("Combination letters created...");
                }

                Console.WriteLine("Archiving Admission folder for " + date);
                moveFolder(admission, archive + @"Admission\" + date + @"\");
                Console.WriteLine("Archiving Scholarship folder for " + date);
                moveFolder(scholar, archive + @"Scholarship\" + date + @"\");

                // Create Report
                Console.WriteLine("Creating Report...");
                List<string> report = new List<string> {DateTime.Now.ToString("MM/dd/yyyy") + " Report", "-----------------------", ""};
                report.Add("Number of Combined Letters: " + numLetters);
                for (int i = 0; i < letters.Count; i++) {
                    report.Add("    " + letters[i]);
                }
                File.WriteAllLines(output + "report.txt", report);
                Console.WriteLine("All finished! :)");
            }
            else if (input == "3") {
                Console.WriteLine("Exiting exercise...");
            }
            else {
                Console.WriteLine("Invalid input");
            }
            }
        } 

        // Helper function to create files. Not needed for exercise
        static void createFiles() {
            int[] IDs = {00129080, 00129081, 00129082, 00129083, 00129084, 00129085, 00129086, 00129087, 00129088, 00129089, 00129090};
            int[] Scholars = {00129080, 00129084, 00129086, 00129088, 00129090, 00129092, 00129094, 00129096, 00129085};

            string dir = @"C:\Users\hayde\Desktop\Projects\coding-exercise\";
            string date = DateTime.Now.ToString("yyyyMMdd");

            string admission = dir + @"CombinedLetters\Input\Admission\"  + date + @"\";
            string scholar = dir + @"CombinedLetters\Input\Scholarship\"  + date + @"\";

            // Create admission files
            Console.WriteLine("Creating admission files...");
            for (int i = 0; i < IDs.Length; i++) {
                string filename = "admission-00" + IDs[i].ToString() + ".txt";
                if(!Directory.Exists(admission)) {
                    Directory.CreateDirectory(admission);
                }
                File.WriteAllText(admission + filename, filename);
            }
            // Create scholarship files
            Console.WriteLine("Creating scholarship files...");
            for (int i = 0; i < Scholars.Length; i++) {
                string filename = "scholarship-00" + Scholars[i].ToString() + ".txt";
                if(!Directory.Exists(scholar)) {
                    Directory.CreateDirectory(scholar);
                }
                File.WriteAllText(scholar + filename, filename);
            }
        }

        static void moveFolder(string origin, string destination) {
            // Move directory if this is the first time running
            if (!Directory.Exists(destination)) {
                Console.WriteLine("Moving directory " + origin + " to " + destination);
                Directory.Move(origin, destination);
            } else {
                // Check all files for new entries
                string [] fileEntries = Directory.GetFiles(origin);
                foreach(string f in fileEntries) {
                    string file = f.Split(@"\")[^1];
                    // Delete duplicate or move any new entries
                    if(File.Exists(destination + file)) {
                        File.Delete(origin + file);
                    } else {
                        Console.WriteLine("Moving " + file + " to " + destination);
                        File.Move(origin + file, destination + file);
                    }
                }
            }
        }
    }

    public interface ILetterService {
        /// <summary>
        /// Combine two letter files into one file.
        /// </summary>
        /// <param name="inputFile1">File path for the first letter.</param>
        /// <param name="inputFile2">File path for the second letter.</param>
        /// <param name="resultFile">File path for the combined letter.</param>
        void CombineTwoLetters(string inputFile1, string inputFile2, string resultPath, string resultFile);
        
    }

    public class LetterService : ILetterService {
        // This is not recommended for large files
        // Assuming no large files are given, and no newline operator is needed
        public void CombineTwoLetters(string inputFile1, string inputFile2, string resultPath, string resultFile) {
            if(!Directory.Exists(resultPath)) {
                Directory.CreateDirectory(resultPath);
            }
            File.WriteAllText(resultPath + resultFile, File.ReadAllText(inputFile1) + File.ReadAllText(inputFile2));
        }
    }
}