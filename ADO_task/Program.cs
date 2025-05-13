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

    
            DataSet dataSet = repo.GetStudents();

            
            repo.DisplayAllStudents(dataSet);

       
            repo.UpdateStudentAge(dataSet, 1, 25);

    
            repo.DisplayAllStudents(dataSet);

  
            repo.DisplayFilteredStudentsByAge(dataSet, 25);
        }
    }
}
