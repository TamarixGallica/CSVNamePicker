using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSVNamePicker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVNamePicker.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void ProcessLine_DateMatch()
        {
            string date = "2.10.";
            string line = "2.10.;Kalle";
            bool result;
            result = Program.ProcessLine(date, line);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void ProcessLine_DateNotMatch()
        {
            string date = "1.10.";
            string line = "4.10.;Ville";
            bool result;
            result = Program.ProcessLine(date, line);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void ProcessLine_NoSeparator()
        {
            string date = "5.10.";
            string line = "1.10.Ville";
            bool result;
            result = Program.ProcessLine(date, line);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void ProcessLine_WrongSeparator()
        {
            string date = "5.10.";
            string line = "1.10.:Ville";
            bool result;
            result = Program.ProcessLine(date, line);
        }

        [TestMethod()]
        public void ProcessFile_OneMatch()
        {
            string filename = "..\\..\\TestFiles\\OneMatch.csv";
            string date = "2.9.";
            System.IO.StreamReader file = new System.IO.StreamReader(filename);
            int result;
            result = Program.ProcessFile(date, file);
            Assert.AreEqual(1, result);
        }

        [TestMethod()]
        public void ProcessFile_TwoMatches()
        {
            string filename = "..\\..\\TestFiles\\TwoMatches.csv";
            string date = "1.3.";
            System.IO.StreamReader file = new System.IO.StreamReader(filename);
            int result;
            result = Program.ProcessFile(date, file);
            Assert.AreEqual(2, result);
        }

        [TestMethod()]
        public void ProcessFile_NoMatches()
        {
            string filename = "..\\..\\TestFiles\\NoMatches.csv";
            string date = "2.10.";
            System.IO.StreamReader file = new System.IO.StreamReader(filename);
            int result;
            result = Program.ProcessFile(date, file);
            Assert.AreEqual(0, result);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void ProcessFile_NoSeparator()
        {
            string filename = "..\\..\\TestFiles\\NoSeparator.csv";
            string date = "1.9.";
            System.IO.StreamReader file = new System.IO.StreamReader(filename);
            int result;
            result = Program.ProcessFile(date, file);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void ProcessFile_WrongSeparator()
        {
            string filename = "..\\..\\TestFiles\\WrongSeparator.csv";
            string date = "2.12.";
            System.IO.StreamReader file = new System.IO.StreamReader(filename);
            int result;
            result = Program.ProcessFile(date, file);
        }

    }
}