// See https://aka.ms/new-console-template for more information
namespace codingExercise {
    class Program {
        static void Main(string[] args) {
            string dir = @"C:\Users\hayde\Desktop\Projects\coding-exercise\";
            // Need function to replace yyyyMMdd
            string admission = @"CombinedLetters\Input\Admission\20230808\";
            string scholar = @"CombinedLetters\Input\Scholarship\20230808\";
            string output = @"CombinedLetters\Output\20230808\";

            ILetterService service = new LetterService();
            
            string [] fileEntries = Directory.GetFiles(dir + admission);
            int numLetters = 0;
            List<string> letters = new List<string>();

            foreach(string fileName in fileEntries) {
                int n = fileName.Length;
                // minus 12 to account for the 8 length ID and 4 length extension
                // Assuming all files end in .txt
                string ID = fileName.Substring(n - 12);
                if(File.Exists(dir + scholar + "scholarship-" + ID)) {
                    string scholarFile = dir + scholar + "scholarship-" + ID;
                    service.CombineTwoLetters(fileName, scholarFile, dir + output + "combined-" + ID);
                    numLetters++;
                    letters.Add(ID.Substring(0,8));
                }
            }

            List<string> report = new List<string> {DateTime.Now.ToString("MM/dd/yyyy") + " Report", "-----------------------", ""};
            report.Add("Number of Combined Letters: " + numLetters);
            for (int i = 0; i < letters.Count; i++) {
                report.Add("    " + letters[i]);
            }
            File.WriteAllLines(dir + output + "report.txt", report);
        } 

        // Helper function to create files. Not needed for exercise
        static void createFiles() {
            int[] IDs = {00129080, 00129081, 00129082, 00129083, 00129084, 00129085, 00129086, 00129087, 00129088, 00129089, 00129090};
            // Even IDs are scholarships for ease of use
            int[] Scholars = {00129080, 00129084, 00129086, 00129088, 00129090, 00129092, 00129094, 00129096};

            string directory = @"C:\Users\hayde\Desktop\Projects\coding-exercise\";
            string folder = @"CombinedLetters\Input\Scholarship\20230808\";

            for (int i = 0; i < Scholars.Length; i++) {
                string filename = "admission-00" + Scholars[i].ToString() + ".txt";
                File.WriteAllText(directory + folder + filename, filename);
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
        void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile);
        
    }

    public class LetterService : ILetterService {
        // This is not recommended for large files
        // Assuming no large files are given, and no newline operator is needed
        public void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile) {
            File.WriteAllText(resultFile, File.ReadAllText(inputFile1) + File.ReadAllText(inputFile2));
        }
    }
}