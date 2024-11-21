using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//one way of doing it is to use ms test framework but i can't see the class in vs fsr
//another way is to use xunit framework but also is a bit finiky
//another way is to use nunit framework but also is finiky
//idk will do more tmrw managed to get some stuff done today


namespace DDD_report_program.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void TestRegisterNewAccount_ValidInput()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                using (var sr = new StringReader("John\njohn@example.com\nPassword1!\n1\n"))
                {
                    Console.SetIn(sr);
                    Program.RegisterNewAccount();
                }

                var output = sw.ToString();
                Assert.IsTrue(output.Contains("Account registered successfully!"));
            }
        }

        [TestMethod]
        public void TestRegisterNewAccount_InvalidName()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                using (var sr = new StringReader("John123\nJohn\njohn@example.com\nPassword1!\n1\n"))
                {
                    Console.SetIn(sr);
                    Program.RegisterNewAccount();
                }

                var output = sw.ToString();
                Assert.IsTrue(output.Contains("Invalid name. Please enter a name that only includes letters."));
            }
        }

        [TestMethod]
        public void TestRegisterNewAccount_InvalidEmail()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                using (var sr = new StringReader("John\njohnexample.com\njohn@example.com\nPassword1!\n1\n"))
                {
                    Console.SetIn(sr);
                    Program.RegisterNewAccount();
                }

                var output = sw.ToString();
                Assert.IsTrue(output.Contains("Invalid email. Please enter a valid email."));
            }
        }

        [TestMethod]
        public void TestRegisterNewAccount_InvalidPassword()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                using (var sr = new StringReader("John\njohn@example.com\npassword\nPassword1!\n1\n"))
                {
                    Console.SetIn(sr);
                    Program.RegisterNewAccount();
                }

                var output = sw.ToString();
                Assert.IsTrue(output.Contains("Invalid password. Please enter a password that is more than 8 characters, contains at least one uppercase letter, one lowercase letter, one number, and one special character."));
            }
        }

        [TestMethod]
        public void TestLoadUsersFromFile_FileExists()
        {
            var filePath = "test_users.txt";
            File.WriteAllLines(filePath, new[] { "John,john@example.com,Password1!,Student" });

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                using (var sr = new StringReader(filePath))
                {
                    Console.SetIn(sr);
                    Program.LoadUsersFromFile();
                }

                var output = sw.ToString();
                Assert.IsTrue(output.Contains("Users loaded successfully!"));
            }

            File.Delete(filePath);
        }

        [TestMethod]
        public void TestLoadUsersFromFile_FileNotFound()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                using (var sr = new StringReader("non_existent_file.txt"))
                {
                    Console.SetIn(sr);
                    Program.LoadUsersFromFile();
                }

                var output = sw.ToString();
                Assert.IsTrue(output.Contains("File not found. Press any key to try again..."));
            }
        }

        [TestMethod]
        public void TestCreateSelfReport_ValidInput()
        {
            Program.currentUser = new User { Email = "john@example.com" };
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                using (var sr = new StringReader("8\n7\n9\n6\n5\nI would like more practical examples.\n"))
                {
                    Console.SetIn(sr);
                    Program.CreateSelfReport();
                }

                var output = sw.ToString();
                Assert.IsTrue(output.Contains("Data saved successfully!"));
            }
        }

        [TestMethod]
        public void TestCreateBooking_ValidInput()
        {
            Program.currentUser = new User { Email = "john@example.com" };
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                using (var sr = new StringReader("101\n1\n"))
                {
                    Console.SetIn(sr);
                    Program.CreateBooking();
                }

                var output = sw.ToString();
                Assert.IsTrue(output.Contains("Data saved successfully!"));
            }
        }
    }
}