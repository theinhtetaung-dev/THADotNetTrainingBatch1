using MySql.Data.MySqlClient;

string connectionString = "server=localhost;port=3306;database=studentdb;user=root;password=sasa@123;";

MySqlConnection connection = new MySqlConnection(connectionString);

try
{
    connection.Open();
    string selectQuery = "SELECT * FROM Tbl_Students";
    MySqlCommand selectCmd = new MySqlCommand(selectQuery, connection);
    MySqlDataReader reader = selectCmd.ExecuteReader();

    Console.WriteLine("\nStudent List");
    while (reader.Read())
    {
        Console.WriteLine("ID: " + reader["Id"] + " | Name: " + reader["Name"] + " | Major: " + reader["Major"]);
    }
    reader.Close();
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}
finally
{
    connection.Close();
}