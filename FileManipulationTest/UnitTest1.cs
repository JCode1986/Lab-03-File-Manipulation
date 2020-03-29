using System;
using Xunit;
using FileManipulation;

namespace FileManipulationTest
{
    public class UnitTest1
    {
        [Fact]
        public void CanReadAndWriteToAndFromAFile()
        {
            string[] expected = new string[3] { "banana", "apple", "orange" };

            Program.WriteToAFile(expected);
            string[] actual = Program.ReadAllLines();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CanAppendItems()
        {
            string[] array = new string[3] { "banana", "apple", "orange" };
            string[] addItems = new string[2] { "plum", "cup" };
            string[] expected = new string[5] { "banana", "apple", "orange", "plum", "cup" };
            Program.WriteToAFile(array);
            Program.FileAppendText(addItems);
            string[] actual = Program.ReadAllLines();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(new string[1] { "pillow" }, new string[4] 
        { 
            "banana", "apple", "orange", "pillow" 
        })]
        [InlineData(new string[2] { "pillow", "checkers" }, new string[5] 
        { 
            "banana", "apple", "orange", "pillow", "checkers" 
        })]
        [InlineData(new string[3] { "pillow", "checkers", "koolaid" }, new string[6]
        {
            "banana", "apple", "orange", "pillow", "checkers", "koolaid"
        })]
        public void CanAppendItemsTheory(string[] addedItems, string[] expected)
        {
            string[] array = new string[3] { "banana", "apple", "orange" };
            Program.WriteToAFile(array);
            Program.FileAppendText(addedItems);
            string[] actual = Program.ReadAllLines();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CanDeleteItems()
        {
            string[] array = new string[3] { "banana", "apple", "orange" };
            Program.WriteToAFile(array);
            string[] expected = new string[2] { "apple", "orange" };
            Program.DeleteItem(1);
            string[] actual = Program.ReadAllLines();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, new string[4] { "apple", "orange", "plum", "cup" })]
        [InlineData(2, new string[4] { "banana","orange", "plum", "cup" })]
        [InlineData(3, new string[4] { "banana", "apple", "plum", "cup" })]
        [InlineData(4, new string[4] { "banana", "apple", "orange", "cup" })]
        [InlineData(5, new string[4] { "banana", "apple", "orange", "plum" })]
        public void UpdatesLengthOfArrayWhenRemoved(int input, string[] expected)
        {
            string[] array = new string[5] { "banana", "apple", "orange", "plum", "cup" };
            Program.WriteToAFile(array);
            Program.DeleteItem(input);
            string[] actual = Program.ReadAllLines();
            Assert.Equal(expected, actual);
        }
    }
}
