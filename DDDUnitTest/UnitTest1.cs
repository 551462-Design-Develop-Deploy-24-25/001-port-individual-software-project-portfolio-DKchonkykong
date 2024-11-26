

//one way of doing it is to use ms test framework but i can't see the class in vs fsr
//another way is to use xunit framework but also is a bit finiky
//another way is to use nunit framework but also is finiky
//idk will do more tmrw managed to get some stuff done today
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;
using Assert = Xunit.Assert;
using  DDD_report_program;

namespace DDD_report_program.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void RegisterNewAccount_ValidInput_ShouldAddUser()
        {
            // Arrange
            // Reset static collections before each test
            StudentSystems.Students.Clear();

            var input = new StringBuilder();
            input.AppendLine("JohnDoe"); // Name
            input.AppendLine("john.doe@example.com"); // Email
            input.AppendLine("Password123!"); // Password
            input.AppendLine("1"); // Role (Student)
            Console.SetIn(new StringReader(input.ToString()));

            // Act
            Program.RegisterNewAccount();

            // Assert
            Assert.Single(StudentSystems.Students);
            var student = StudentSystems.Students[0];
            Assert.Equal("JohnDoe", student.Name);
            Assert.Equal("john.doe@example.com", student.Email);
            Assert.Equal("Password123!", student.Password);
            Assert.Equal("Student", student.Role);
        }

        [Fact]
        public void RegisterNewAccount_InvalidName_ShouldPromptAgain()
        {
            // Arrange
            StudentSystems.Students.Clear();

            var input = new StringBuilder();
            input.AppendLine("John123"); // Invalid Name
            input.AppendLine("JohnDoe"); // Valid Name
            input.AppendLine("john.doe@example.com"); // Email
            input.AppendLine("Password123!"); // Password
            input.AppendLine("1"); // Role (Student)
            Console.SetIn(new StringReader(input.ToString()));

            // Act
            Program.RegisterNewAccount();

            // Assert
            Assert.Single(StudentSystems.Students);
            var student = StudentSystems.Students[0];
            Assert.Equal("JohnDoe", student.Name);
        }
    }
}