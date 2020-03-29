using System;
using System.IO;

namespace FileManipulation
{
    public class Program
    {
        static void Main(string[] args)
        {
            Start();
        }
        /// <summary>
        /// Main start method that calls Menu
        /// </summary>
        static void Start()
        {
            char input = Menu();
            try
            {
                if (input == '1')
                {
                    ViewList();
                    Start();
                }
                else if (input == '2')
                {
                    Console.Write("\nHow many items would you like to add?: ");
                    string inputString = Console.ReadLine();
                    int convertedInputed = Convert.ToInt32(inputString);
                    string[] array = new string[convertedInputed];
                    WordsArray(array);
                    Start();
                }
                else if (input == '3')
                {
                    Console.WriteLine("Survival List");
                    ViewList();
                    Console.Write("\nWhich item would you like to delete?: ");
                    int inputDelete = Convert.ToInt32(Console.ReadLine());
                    DeleteItem(inputDelete);
                    Start();
                }
                else if (input == '4')
                {
                    Console.Write("\nWhich item would you like up update?: ");
                    ViewList();
                    string inputString = Console.ReadLine();
                    int convertedInputed = Convert.ToInt32(inputString);
                    UpdateItem(convertedInputed);
                    Start();
                }

                else if (input == '5')
                {
                    Console.Write("\nHow many items for you new survival: ");
                    string inputString = Console.ReadLine();
                    int convertedInputed = Convert.ToInt32(inputString);
                    string[] array = new string[convertedInputed];
                    WriteLinesToAFile(array);
                    Start();
                }

                else if (input == 'x')
                {
                    Environment.Exit(0);
                }

                else
                {
                    Start();
                }

            }
            catch (FormatException e)
            {
                string path = "../../../error.txt";
                Console.WriteLine($"{DateTime.Now}. {e.Message}");
                File.AppendAllText(path, $"{DateTime.Now}. {e.Message}");
                Start();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static char Menu()
        {
            Console.WriteLine("\nSURVIVAL LIST\n");
            Console.WriteLine("'1' - Check survival list");
            Console.WriteLine("'2' - Add item to survival list");
            Console.WriteLine("'3' - Remove item from survival list");
            Console.WriteLine("'4' - Update an item");
            Console.WriteLine("'5' - Delete list, and create a new one");
            Console.WriteLine("'x' - Exit");
            Console.Write("\nWhat would you like to do? ");
            string input = Console.ReadLine();
            char character = Convert.ToChar(input);
            return character;

        }

        /// <summary>
        /// Input array for items to add to the array
        /// </summary>
        /// <param name="arr">array to hold items</param>
        static void WordsArray(string[] arr)
        {
            string input;
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"\nEnter an item: ");
                input = Console.ReadLine();
                arr[i] = input;
            }
            FileAppendText(arr);
        }

        /// <summary>
        /// Writing items to a file
        /// </summary>
        /// <param name="contents">contents to be written to a file</param>
        public static void WriteLinesToAFile(string[] contents)
        {
            WordsArray(contents);
            string path = "../../../list.txt";
            File.WriteAllLines(path, contents);
        }

        public static void WriteToAFile(string[] contents)
        {
            string path = "../../../list.txt";
            File.WriteAllLines(path, contents);
        }
        public static void FileAppendText(string[] contents)
        {
            string path = "../../../list.txt";
            File.AppendAllLines(path, contents);
        }

        /// <summary>
        /// Returns array of items from file and returns value
        /// </summary>
        /// <returns>myText array</returns>
        public static string[] ReadAllLines()
        {
            string path = "../../../list.txt";
            string[] myText = File.ReadAllLines(path);
            return myText;
        }

        /// <summary>
        /// View List from file
        /// </summary>
        public static string ViewList()
        {
            string[] textString = new string[10];
            try
            {
                int counter = 1;
                string text = " ";
                string path = "../../../list.txt";
                using (StreamReader sr = File.OpenText(path))
                {
                    int i = 0;

                    while ((text = sr.ReadLine()) != null)
                    {
                        Console.WriteLine($"{counter++}: {text}");
                        textString[i] = text;
                        i++;
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Your file is not available", e.Message);
            }

            finally
            {
                Console.WriteLine("Process Complete");
            }
            return GetWordsInArray(textString);

        }

        /// <summary>
        /// Delete item from list
        /// </summary>
        /// <param name="textToRemove">name of item to remove</param>
        public static void DeleteItem(int textToRemove)
        {
            try
            {
                string[] words = ReadAllLines();
                string[] newWords = new string[words.Length - 1];
                int j = 0;
                for (int i = 0; i < words.Length; i++)
                {
                    if (i != textToRemove - 1)
                    {
                        newWords[j] = words[i];
                        j++;
                    }
                }
                WriteToAFile(newWords);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Updates a specific item in list
        /// </summary>
        /// <param name="textToRemove"></param>
        public static void UpdateItem(int textToUpdate)
        {
            try
            {
                string[] words = ReadAllLines();
                string[] newWords = new string[words.Length];
                int j = 0;
                if (textToUpdate > newWords.Length)
                {
                    throw new Exception("Item not in list");
                }
                for (int i = 0; i < words.Length; i++)
                {
                    if (i == textToUpdate - 1)
                    {
                        Console.Write("\nEnter the item");
                        string input = Console.ReadLine();
                        newWords[j] = input;
                        j++;
                    }
                    if (i != textToUpdate - 1)
                    {
                        newWords[j] = words[i];
                        j++;
                    }
                }
                WriteToAFile(newWords);
                Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static string GetWordsInArray(string[] arr)
        {
            string str = "{ ";
            for (int i = 0; i < arr.Length; i++)
            {
                str += $"{arr[i]}, ";
            }
            return $"{str}}}";
        }
    }
}

