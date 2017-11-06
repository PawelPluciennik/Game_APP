using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesApp
{
    class Program
    {
        static MySqlCommand cmd;
        static MySqlConnection connection = new MySqlConnection();
        static MySqlDataReader reader;
        static string strLoginAdd = "1";
        static string strPassAdd = "1";

        static void Main(string[] args)
        {


            Initialization();


            if (LogUserProcess())
            {

                AddNewGame();
            }
            else
            {

            }
         

            //Console.WriteLine(String.Compare(login, ));
            //  if ('login'=='poprawny login')
            //  {
            //      Console.Write("Haslo: ");
            //      string password = Console.ReadLine();
            //        if ('haslo'=='haslodanego uzytkownika')
            //    {
            //         Console.Write("Zalogowano.");
            //    }
            //        else
            //    {
            //          Console.Write("Wprowadzono bledne haslo!");
            //          break;
            //    }
            //  }
            //  else
            //  {
            //          Console.Write("Wprowadzono bledny login!");
            //  }





            Console.ReadKey();

        }

        private static bool LogUserProcess()
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Połączony.");
                    List<string> names = new List<string>();

                    cmd.CommandText = "SELECT id, name, password FROM Users";
                    reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetInt32(0).ToString() + " " + reader.GetString(1) + " " + reader.GetString(2));
                        names.Add(reader.GetString(1));
                    }

                    reader.Close();

                    names.Contains("Andrzej");
                }
                else
                {
                    Console.WriteLine("Niepołączony");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Niepołączony: " + ex.Message);
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Niepołączony: " + ex.Message);
            }

            Console.Write("Login: ");
            strLoginAdd = Console.ReadLine();
            Console.Write("Haslo: ");
            strPassAdd = Console.ReadLine();


           // cmd.CommandText = string.Format("SELECT password FROM users WHERE name LIKE @userName;");
           // cmd.Parameters.Add(new MySqlParameter("@userName", login));


            reader = cmd.ExecuteReader();

            string password;
            bool userExists = reader.Read();

            if (userExists)
            {
                int position = reader.GetOrdinal("password");
                if (position < 0)
                {

                }

                password = reader.IsDBNull(position) ? "" : reader.GetString(position);
                reader.Close();

                return true;
            }
            else
            {
                Console.WriteLine("Uzytkownik nie istenieje.");
                return false;
            }
        }

        private static void Initialization()
        {
            connection.ConnectionString = "Server=localhost;Database=GamesAppDB;Uid=root;Pwd=1234;";
            cmd = connection.CreateCommand();


        }

        

        private static void AddNewGame()
        {
            cmd.CommandText = string.Format("INSERT INTO users (name,password) VALUES ('[0]', '[1]');", strLoginAdd, strPassAdd);

            int records  = cmd.ExecuteNonQuery();
        }
    }
}