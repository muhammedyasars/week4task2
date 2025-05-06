using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace StudentDataApp
{
    public class StudentRepository
    {
        private string connectionString;

        public StudentRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public DataSet GetStudents()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Students";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Students");

                return dataSet;
            }
        }

        public void UpdateStudentAge(DataSet dataSet, int studentId, int newAge)
        {
            DataTable table = dataSet.Tables["Students"];
            foreach (DataRow row in table.Rows)
            {
                if ((int)row["Id"] == studentId)
                {
                    row["Age"] = newAge;
                    break;
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Students";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                adapter.Update(dataSet, "Students");
            }
        }

        public void DisplayFilteredStudentsByAge(DataSet dataSet, int age)
        {
            DataView view = new DataView(dataSet.Tables["Students"]);
            view.RowFilter = $"Age = {age}";

            Console.WriteLine($"\nStudents with Age = {age}:");
            foreach (DataRowView row in view)
            {
                Console.WriteLine($"ID: {row["Id"]}, Name: {row["Name"]}, Age: {row["Age"]}");
            }
        }

        public void DisplayAllStudents(DataSet dataSet)
        {
            Console.WriteLine("All Students:");
            foreach (DataRow row in dataSet.Tables["Students"].Rows)
            {
                Console.WriteLine($"ID: {row["Id"]}, Name: {row["Name"]}, Age: {row["Age"]}");
            }
        }
    }
}
