using System;
using System.Data;
using System.Data.SqlClient;

namespace StudentDataApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string connectionString = "Server=YAAZOOZ\\SQLEXPRESS;Database=schooldb;Trusted_Connection=True;";

                string query = "SELECT * FROM Students";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connectionString);

                DataSet studentDataSet = new DataSet();

                adapter.Fill(studentDataSet, "Students");

                DataTable studentsTable = studentDataSet.Tables["Students"];

                Console.WriteLine("Original Data:");
                foreach (DataRow row in studentsTable.Rows)
                {
                    Console.WriteLine($"ID: {row["StudentID"]}, Name: {row["Name"]}, Age: {row["Age"]}, Grade: {row["Grade"]}");
                }

                foreach (DataRow row in studentsTable.Rows)
                {
                    if (row["Name"].ToString() == "Anoop")
                    {
                        row["Age"] = 21;
                    }
                    if (row["Name"].ToString() == "Meera")
                    {
                        row["Age"] = 23;
                    }
                }

                Console.WriteLine("\nModified Data in Dataset:");
                foreach (DataRow row in studentsTable.Rows)
                {
                    Console.WriteLine($"ID: {row["StudentID"]}, Name: {row["Name"]}, Age: {row["Age"]}, Grade: {row["Grade"]}");
                }

                SqlCommand updateCommand = new SqlCommand(
                    "UPDATE Students SET Name = @Name, Age = @Age, Grade = @Grade WHERE StudentID = @StudentID",
                    new SqlConnection(connectionString)
                );
                updateCommand.Parameters.Add("@Name", SqlDbType.VarChar, 50, "Name");
                updateCommand.Parameters.Add("@Age", SqlDbType.Int, 0, "Age");
                updateCommand.Parameters.Add("@Grade", SqlDbType.VarChar, 10, "Grade");
                updateCommand.Parameters.Add("@StudentID", SqlDbType.Int, 0, "StudentID");
                adapter.UpdateCommand = updateCommand;

                adapter.Update(studentDataSet, "Students");
                Console.WriteLine("\nChanges saved to the database!");

                DataView studentView = new DataView(studentsTable);

                studentView.RowFilter = "Age = 21";

                Console.WriteLine("\nFiltered Data (Age = 21):");
                foreach (DataRowView rowView in studentView)
                {
                    Console.WriteLine($"ID: {rowView["StudentID"]}, Name: {rowView["Name"]}, Age: {rowView["Age"]}, Grade: {rowView["Grade"]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
