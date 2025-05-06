using System;
using System.Data;
using StudentDataApp;


namespace StudentDataApp
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentRepository repo = new StudentRepository();

            // Get data
            DataSet dataSet = repo.GetStudents();

            // Display original
            repo.DisplayAllStudents(dataSet);

            // Modify Age of student with Id = 1 to Age = 25
            repo.UpdateStudentAge(dataSet, 1, 25);

            // Display updated data
            repo.DisplayAllStudents(dataSet);

            // Filter and show students of age 25
            repo.DisplayFilteredStudentsByAge(dataSet, 25);
        }
    }
}
