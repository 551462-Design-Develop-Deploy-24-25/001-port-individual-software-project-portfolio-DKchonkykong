namespace DDD_report_program
{
    //got most of the program working although i need to fix some stuff + add error checking and change some stuff
    //need to do the file saving after they log out easy fix
    
    class Program : StudentSystems
    {
        private static List<User> users = new List<User>();
        private static User currentUser = null;
        private static List<SelfReport> selfReports = new List<SelfReport>();
        private static List<Booking> bookings = new List<Booking>();

        private static List<string> courses = new List<string>
        {
            "Programming Fundamentals",
            "Data Structures",
            "Algorithms",
            "Database Systems",
            "Web Development"
        };

        private static List<string> supervisors = new List<string>
        {
            "Sir. Simon the 3rd (PS)",
            "Dr. Nishikami (PS)",
            "Prof. Kowalski (ST)",
            "Dr. House (ST)"
        };

//all this does is just runs the program and checks bewteen two states of the program
//that being to show the login/register menu or the main menu
        static void Main(string[] args)
        {
            RunProgram();
        }

        private static void RunProgram()
        {
            while (true)
            {
                if (currentUser == null)
                {
                    ShowInitialMenu();
                }
                else
                {
                    ShowMainMenu();
                }
            }
        }

//menus work!!!!
        private static void ShowInitialMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Initial Menu ===");
                Console.WriteLine("1. Register a New Account");
                Console.WriteLine("2. Load Users from File");
                Console.WriteLine("3. Exit Program");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        RegisterNewAccount();
                        ShowMainMenu();
                        break;
                    case "2":
                        LoadUsersFromFile();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }
//this is where the user can register and is added to a new list of users and stuff
        private static void RegisterNewAccount()
        {
            Console.Clear();
            Console.WriteLine("=== Register a New Account ===");

            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            Console.Write("Enter your email: ");
            string email = Console.ReadLine();

            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            Console.Write("Enter your role (Student/PS): ");
            string role = Console.ReadLine();

            users.Add(new User { Email = email, Password = password, Role = role });

            Console.WriteLine("Account registered successfully! Press any key to continue...");
            Console.ReadKey();
        }

        //load users from a file, although you do need to add a file path but can't think of a better solution atm
        //maybe use regex? or a database
        private static void LoadUsersFromFile()
        {
            Console.Clear();
            Console.WriteLine("=== Load Users from File ===");

            Console.Write("Enter the file path: ");
            string filePath = Console.ReadLine();

            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 3)
                    {
                        users.Add(new User { Email = parts[0], Password = parts[1], Role = parts[2] });
                    }
                }

                Console.WriteLine("Users loaded successfully! Press any key to continue...");
            }
            else
            {
                Console.WriteLine("File not found. Press any key to try again...");
            }
            Console.ReadKey();
        }
        
//actual main menu where the user can make a self report etc.
        private static void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Main Menu - Welcome {currentUser?.Email} ===");
                Console.WriteLine("1. Create Self Report");
                Console.WriteLine("2. Book a Meeting");
                Console.WriteLine("3. Save Reports and Bookings");
                Console.WriteLine("4. Logout");
                Console.WriteLine("5. Exit Program");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        CreateSelfReport();
                        break;
                    case "2":
                        CreateBooking();
                        break;
                    case "3":
                        SaveData();
                        break;
                    case "4":
                        currentUser = null;
                        return;
                    case "5":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }
//self report system where the user can rate how they feel and how they feel about their courses
//currently it does do partially however i'd need have it so it's both the scale of 0-10 and the written feedback
        private static void CreateSelfReport()
        {
            Console.Clear();
            Console.WriteLine("=== Self Report ===");

            SelfReport report = new SelfReport
            {
                StudentEmail = currentUser?.Email
            };

            Console.Write("How are you feeling today? ");
            report.Feeling = Console.ReadLine();

            foreach (var course in courses)
            {
                while (true)
                {
                    Console.Write($"On a scale of 0-10, how do you feel about {course}? ");
                    if (int.TryParse(Console.ReadLine(), out int rating) && rating >= 0 && rating <= 10)
                    {
                        report.CourseRatings[course] = rating;
                        break;
                    }
                    Console.WriteLine("Please enter a valid number between 0 and 10.");
                }
            }

            selfReports.Add(report);
        }
//this seems fine enough however there's no error checking at the moment and it's a bit ehhh atm
//this is suppsoed to keep track of currently available supervisors which in theory would work but atm it doesn't
//maybe i should remove it since it wasn't in my original brief stuff
        private static void CreateBooking()
        {
            Console.Clear();
            Console.WriteLine("=== Book a Meeting ===");

            Booking booking = new Booking
            {
                StudentEmail = currentUser?.Email
            };

            Console.Write("Enter room number: ");
            booking.RoomNumber = Console.ReadLine();

            Console.WriteLine("\nAvailable Supervisors:");
            for (int i = 0; i < supervisors.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {supervisors[i]}");
            }

            while (true)
            {
                Console.Write("Select supervisor number: ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= supervisors.Count)
                {
                    booking.SupervisorName = supervisors[choice - 1];
                    break;
                }
                Console.WriteLine("Please enter a valid supervisor number.");
            }

            bookings.Add(booking);
        }

        //this is for saving reports to the file system and the correct format
        private static void SaveData()
        {
            Console.Clear();
            Console.WriteLine("=== Save Reports and Bookings ===");

            // Save Self Reports
            foreach (var report in selfReports)
            {
                string filename = $"self_report_{report.StudentEmail}_{report.ReportDate.ToString("yyyyMMdd_HHmmss")}.txt";
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.WriteLine($"Self Report - {report.ReportDate}");
                    writer.WriteLine($"Student: {report.StudentEmail}");
                    writer.WriteLine($"Feeling: {report.Feeling}");
                    writer.WriteLine("\nCourse Ratings:");
                    foreach (var rating in report.CourseRatings)
                    {
                        writer.WriteLine($"{rating.Key}: {rating.Value}/10");
                    }
                }
            }

            // Save Bookings same thing that i have done for the self reports
            foreach (var booking in bookings)
            {
                string filename = $"booking_{booking.StudentEmail}_{booking.BookingDate.ToString("yyyyMMdd_HHmmss")}.txt";
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.WriteLine($"Booking - {booking.BookingDate}");
                    writer.WriteLine($"Student: {booking.StudentEmail}");
                    writer.WriteLine($"Room: {booking.RoomNumber}");
                    writer.WriteLine($"Supervisor: {booking.SupervisorName}");
                }
            }

            Console.WriteLine("Data saved successfully! Press any key to continue...");
            Console.ReadKey();
        }
    }
}