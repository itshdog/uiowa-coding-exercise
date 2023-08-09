// See https://aka.ms/new-console-template for more information
namespace codingExercise {
    class Program {
        static void Main(string[] args) {
            int[] IDs = {00129080, 00129081, 00129082, 00129083, 00129084, 00129085, 00129086, 00129087, 00129088, 00129089, 00129090};
            int[] Scholars = {00129080, 00129082, 00129084, 00129086, 00129088, 00129090};
            string folder = @"C:\Users\hayde\Desktop\Projects\coding-exercise\CombinedLetters\Input\Scholarship\20230808\";
            for (int i = 0; i < Scholars.Length; i++) {
                string filename = "admission-00" + Scholars[i].ToString() + ".txt";
                File.WriteAllText(folder + filename, filename);
            }
            ILetterService test = new LetterService();
            test.CombineTwoLetters(); 
        } 
    }

    public interface ILetterService {
        /// <summary>
        /// Combine two letter files into one file.
        /// </summary>
        /// <param name="inputFile1">File path for the first letter.</param>
        /// <param name="inputFile2">File path for the second letter.</param>
        /// <param name="resultFile">File path for the combined letter.</param>
        void CombineTwoLetters();
        
    }

    public class LetterService : ILetterService {
        public void CombineTwoLetters() {
            Console.WriteLine("Hello, World!");
            /// Implementation
        }
    }
}