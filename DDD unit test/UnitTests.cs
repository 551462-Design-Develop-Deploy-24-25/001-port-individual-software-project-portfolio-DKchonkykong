using Xunit;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;
using Assert = Xunit.Assert;

//still not working

namespace DDD_report_program.Tests
{
    public class UserRegistrationTests
    {
        [Fact]
        public void ValidNameShouldPassRegexCheck()
        {
            // Arrange
            string validName = "JohnDoe";
            string invalidName1 = "John Doe";
            string invalidName2 = "John123";

            // Assert
            Assert.True(Regex.IsMatch(validName, @"^[a-zA-Z]+$"));
            Assert.False(Regex.IsMatch(invalidName1, @"^[a-zA-Z]+$"));
            Assert.False(Regex.IsMatch(invalidName2, @"^[a-zA-Z]+$"));
        }

        [Fact]
        public void ValidEmailShouldPassRegexCheck()
        {
            // Arrange
            string validEmail = "john.doe@example.com";
            string invalidEmail1 = "invalid-email";
            string invalidEmail2 = "john@doe";

            // Assert
            Assert.True(Regex.IsMatch(validEmail, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"));
            Assert.False(Regex.IsMatch(invalidEmail1, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"));
            Assert.False(Regex.IsMatch(invalidEmail2, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"));
        }

        [Fact]
        public void ValidPasswordShouldPassRegexCheck()
        {
            // Arrange
            string validPassword = "StrongP@ss123";
            string invalidPassword1 = "weak";
            string invalidPassword2 = "onlyletters";

            // Assert
            Assert.True(Regex.IsMatch(validPassword, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$"));
            Assert.False(Regex.IsMatch(invalidPassword1, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$"));
            Assert.False(Regex.IsMatch(invalidPassword2, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$"));
        }
    }
     public class BookingSystemTests
    {
        [Fact]
        public void ValidBookingShouldBeAccepted()
        {
            // Arrange
            var booking = new Booking
            {
                StudentEmail = "student@example.com",
                RoomNumber = "A101",
                SupervisorName = "Dr. House (ST)"
            };

            // Act
            bool isValid = !string.IsNullOrWhiteSpace(booking.StudentEmail) &&
                           !string.IsNullOrWhiteSpace(booking.RoomNumber) &&
                           !string.IsNullOrWhiteSpace(booking.SupervisorName);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void InvalidBookingShouldBeRejected()
        {
            // Arrange
            var booking = new Booking
            {
                StudentEmail = "", // Empty student email
                RoomNumber = "A101",
                SupervisorName = "Dr. House (ST)"
            };

            // Act
            bool isValid = !string.IsNullOrWhiteSpace(booking.StudentEmail) &&
                           !string.IsNullOrWhiteSpace(booking.RoomNumber) &&
                           !string.IsNullOrWhiteSpace(booking.SupervisorName);

            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void BookingShouldOnlyAllowValidSupervisors()
        {
            // Arrange
            var validSupervisors = new List<string>
            {
                "Sir. Simon the 3rd (PS)",
                "Dr. Nishikami (PS)",
                "Prof. Kowalski (ST)",
                "Dr. House (ST)"
            };

            string supervisor = "Dr. House (ST)";

            // Act
            bool isSupervisorValid = validSupervisors.Contains(supervisor);

            // Assert
            Assert.True(isSupervisorValid);
        }
    }

    public class RoleValidationTests
    {
        [Fact]
        public void ValidRolesShouldPass()
        {
            // Arrange
            string validRole1 = "Student";
            string validRole2 = "Supervisor";

            // Act
            bool isValidRole1 = validRole1 == "Student" || validRole1 == "Supervisor";
            bool isValidRole2 = validRole2 == "Student" || validRole2 == "Supervisor";

            // Assert
            Assert.True(isValidRole1);
            Assert.True(isValidRole2);
        }

        [Fact]
        public void InvalidRolesShouldFail()
        {
            // Arrange
            string invalidRole1 = "Teacher";
            string invalidRole2 = "Admin";

            // Act
            bool isValidRole1 = invalidRole1 == "Student" || invalidRole1 == "Supervisor";
            bool isValidRole2 = invalidRole2 == "Student" || invalidRole2 == "Supervisor";

            // Assert
            Assert.False(isValidRole1);
            Assert.False(isValidRole2);
        }
    }

    public class SelfReportTests
    {
        [Fact]
        public void RatingShouldBeWithinValidRange()
        {
            // Arrange
            int validRating = 7;
            int invalidRating1 = -1;
            int invalidRating2 = 11;

            // Act
            bool isValidRating1 = validRating >= 0 && validRating <= 10;
            bool isValidRating2 = invalidRating1 >= 0 && invalidRating1 <= 10;
            bool isValidRating3 = invalidRating2 >= 0 && invalidRating2 <= 10;

            // Assert
            Assert.True(isValidRating1);
            Assert.False(isValidRating2);
            Assert.False(isValidRating3);
        }

        [Fact]
        public void FeedbackShouldNotBeEmpty()
        {
            // Arrange
            string validFeedback = "Need more practical exercises";
            string invalidFeedback = "";

            // Act
            bool isValidFeedback1 = !string.IsNullOrWhiteSpace(validFeedback);
            bool isValidFeedback2 = !string.IsNullOrWhiteSpace(invalidFeedback);

            // Assert
            Assert.True(isValidFeedback1);
            Assert.False(isValidFeedback2);
        }
    }

    // Mock classes used in tests
    internal class Booking
    {
        public string StudentEmail { get; set; }
        public string RoomNumber { get; set; }
        public string SupervisorName { get; set; }
    }
}
    
    
    


