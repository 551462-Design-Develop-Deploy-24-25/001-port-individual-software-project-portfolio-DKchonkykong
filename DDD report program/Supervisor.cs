namespace DDD_report_program;
//user details class has get set methods for supervisor and student
//inherits from base class user
public class UserDetails
{
    public class Supervisor : User
    {
        public List<string> SupervisedStudents { get; set; }

        public Supervisor()
        {
            SupervisedStudents = new List<string>();
        }
    }


    public class Student : User
    {
        public List<string> Courses { get; set; }

        public Student()
        {
            Courses = new List<string>();
        }
    }    
}
