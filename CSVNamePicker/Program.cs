using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSVNamePicker
{
    public class Program
    {

        private static void ProcessLine(string date, string line)
        {
            // Store date and names read from current line
            string[] array;

            // Check if separator character is present and throw an exception if it isn't
            if (line.IndexOf(";") == -1)
            {
                throw new FormatException();
            }

            // Separate date and names by splitting by ";"
            array = line.Split(';');

            // Check if date read from file matches the date entered as a parameter
            if (array[0].Equals(date))
            {
                // If dates match, print the names
                System.Console.WriteLine(array[1]);
            }

        }

        private static void ProcessFile(string date, System.IO.StreamReader file)
        {
            string line;            // Store current line read from the file
            int rowNumber = 1;     // Current line number used to indicate which row has been read

            try
            {
                // Loop until we're at the end of the file
                while (file.Peek() > 0)
                {

                    // Read the next line trimming any whitespace from beginning and end
                    line = file.ReadLine().Trim();

                    // Process the read line
                    ProcessLine(date, line);

                    // Update number of the the current row
                    rowNumber++;
                }

            }
            catch (FormatException e)
            {
                //e.Message("Line number " + rowNumber + " doesn't contain separator character \";\";, aborting");
                //e.Data.Values
                throw new FormatException("Line number " + rowNumber + " doesn't contain separator character \";\", aborting");
            }
            // Catch and throw any other exceptions
            catch (Exception e)
            {
                throw e;
            }

        }

        // Main program takes up to two arguments.
        // First argument is required and is a date
        // Second argument is optional and specifies the CSV file to read
        // If CSV file is not specified, program uses nimet.csv residing in current directory
        static void Main(string[] args)
        {
            string date;                // Store the date to search for
            string filename = "nimet.csv";  // Store the default file name

            // If no arguments are given, print usage and exit
            if (args.Count() == 0)
            {
                System.Console.WriteLine("Usage: CSVNamePicker.exe date [file]");
                System.Console.WriteLine("File is optional argument. If none is given, names.csv in current directory is used by default");
            }
            else
            {
                // Read date given as argument
                date = args[0];

                // If two parameters were given, replace default file name with one given as argument
                if (args.Count() == 2)
                {
                    filename = args[1];
                }

                try
                {
                    // Open file for reading
                    System.IO.StreamReader file = new System.IO.StreamReader(filename);

                    // Call method used to process the file
                    ProcessFile(date, file);

                    // Close the file when we're done
                    file.Close();
                }
                catch (FileNotFoundException e)
                {
                    System.Console.WriteLine("File {0} was not found, aborting ", e.FileName);
                }
                catch (FormatException e)
                {
                    System.Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("Error occured while processing file:\n{0}", e.ToString());
                }
            }
        }
    }
}
