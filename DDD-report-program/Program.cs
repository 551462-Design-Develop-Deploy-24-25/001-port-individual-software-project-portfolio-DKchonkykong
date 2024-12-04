namespace DDD
{
    class Program : StudentSystems
    {
        /// <General Lists>
        /// This is where lists is called from and where you can change what each lists has e.,g courses or the supervisors
        /// </Lists>
        public static List<User> users = new List<User>();

        public static User currentUser = null;
        public static List<SelfReport> selfReports = new List<SelfReport>();
        public static List<Booking> bookings = new List<Booking>();

        public static List<string> courses = new List<string>
        {
            "Programming Fundamentals",
            "Introduction To Games Design",
            "Introduction To Games Design",
            "Algorithms",
            "Database Systems",
            "Web Development"
        };

        public static List<string> supervisors = new List<string>
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

        public static void RunProgram()
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

// it's what the user will first see can pick to register, load user and exit
        public static void ShowInitialMenu()
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
        public static void RegisterNewAccount()
        {
            Console.Clear();
            Console.WriteLine("=== Register a New Account ===");
            string
                name,
                email,
                password,
                role =
                    string.Empty; //implemented regex to check if the name is valid so if it only includes letters and no special characters
            while (true)
            {
                Console.Write("Enter your name: ");
                name = Console.ReadLine();
                if (System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                {
                    break;
                }

                Console.WriteLine("Invalid name. Please enter a name that only includes letters.");
            }
            //implemented regex to check if the email is valid

            while (true)
            {
                Console.Write("Enter your email: ");
                email = Console.ReadLine();
                if (System.Text.RegularExpressions.Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    break;
                }

                Console.WriteLine("Invalid email. Please enter a valid email.");
            }

            //implemented regex to check if password has the valid format of min 8 characters with a special character and a number

            while (true)
            {
                Console.Write("Enter your password: ");
                password = Console.ReadLine();
                if (System.Text.RegularExpressions.Regex.IsMatch(password,
                        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$"))
                {
                    break;
                }

                Console.WriteLine(
                    "Invalid password. Please enter a password that is more than 8 characters, contains at least one uppercase letter, one lowercase letter, one number, and one special character.");
            }

            //role selection
            Console.WriteLine("What is your role? Select from the following:");

            Console.WriteLine("1. Student");
            Console.WriteLine("2. Supervisor");
            Console.WriteLine("3. Senior Tutor");
            string RoleInput = Console.ReadLine();
            switch (RoleInput)
            {
                case "1":
                    role = "Student";
                    break;
                case "2":
                    role = "Supervisor";
                    break;
                case "3":
                    role = "Senior Tutor";
                    break;
                default:
                    Console.WriteLine("Invalid role. Please enter a valid role.");
                    break;
            }

            ;


            users.Add(new User { Name = name, Email = email, Password = password, Role = role });

            Console.WriteLine("Account registered successfully! Press any key to continue...");
            Console.ReadKey();
        }

        //load users from a file, although you do need to add a file path but can't think of a better solution atm
        public static void LoadUsersFromFile()
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
                    if (parts.Length == 4)
                    {
                        users.Add(new User { Name = parts[0], Email = parts[1], Password = parts[2], Role = parts[3] });
                    }
                }

                Console.WriteLine("Users loaded successfully! Press any key to continue...");
                Console.ReadKey();

                // Set the current user to the first user in the list and show the main menu
                currentUser = users.FirstOrDefault();
                ShowMainMenu();
            }
            else
            {
                Console.WriteLine("File not found. Press any key to try again...");
                Console.ReadKey();
            }
        }

        //this displays the users that are in the system 
        public static void DisplayUsers()
        {
            Console.Clear();
            Console.WriteLine("=== Users ===");
            foreach (var user in users)
            {
                string filename = $"user_data_{user.Email}.txt";
                if (File.Exists(filename))
                {
                    var lines = File.ReadAllLines(filename);
                    foreach (var line in lines)
                    {
                        Console.WriteLine(line);
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"Name: {user.Name}, Email: {user.Email}, Role: {user.Role}");
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
//actual main menu where the user can make a self report book a meeting etc.
        public static void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Main Menu - Welcome {currentUser?.Email} ===");
                Console.WriteLine("1. Create Self Report");
                Console.WriteLine("2. Book a Meeting");
                Console.WriteLine("3. Save Reports and Bookings as TXT Files");
                Console.WriteLine("4. Display Users");
                Console.WriteLine("5. Logout");
                Console.WriteLine("6. Exit Program");
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
                        DisplayUsers();
                        break;
                    case "5":
                        currentUser = null;
                        return;
                    case "6":
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
        public static void CreateSelfReport()
        {
            Console.Clear();
            Console.WriteLine("=== Self Report ===");

            SelfReport report = new SelfReport
            {
                StudentEmail = currentUser?.Email
            };


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

            Console.WriteLine("In general, if you could change one thing about these courses what would it be?");
            report.Feeling = Console.ReadLine();
            selfReports.Add(report);
            SaveData(); //saves it after creating the self report
        }

        //Makes it so the user can book a meeting with a supervisor
        public static void CreateBooking()
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
            SaveData(); //saves it after creating the booking
        }

        //this is for saving reports to the file system and the correct format
        public static void SaveData()
        {
            Console.Clear();
            Console.WriteLine("=== Save Reports and Bookings ===");

            foreach (var user in users)
            {
                string filename = $"user_data_{user.Email}.txt";
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.WriteLine($"Name: {user.Name}");
                    writer.WriteLine($"Email: {user.Email}");
                    writer.WriteLine($"Password: {user.Password}");
                    writer.WriteLine($"Role: {user.Role}");
                    writer.WriteLine();

                    var userReports = selfReports.Where(r => r.StudentEmail == user.Email).ToList();
                    if (userReports.Any())
                    {
                        writer.WriteLine("Self Report Answers:");
                        foreach (var report in userReports)
                        {
                            writer.WriteLine($"Report Date: {report.ReportDate}");
                            writer.WriteLine($"Feeling: {report.Feeling}");
                            writer.WriteLine("Course Ratings:");
                            foreach (var rating in report.CourseRatings)
                            {
                                writer.WriteLine($"{rating.Key}: {rating.Value}/10");
                            }

                            writer.WriteLine();
                        }
                    }

                    var userBookings = bookings.Where(b => b.StudentEmail == user.Email).ToList();
                    if (userBookings.Any())
                    {
                        writer.WriteLine("Booking Details:");
                        foreach (var booking in userBookings)
                        {
                            writer.WriteLine($"Booking Date: {booking.BookingDate}");
                            writer.WriteLine($"Room: {booking.RoomNumber}");
                            writer.WriteLine($"Supervisor: {booking.SupervisorName}");
                            writer.WriteLine();
                        }
                    }
                }
            }

            Console.WriteLine("Data saved successfully! Press any key to continue...");
            Console.ReadKey();
        }
    }
}