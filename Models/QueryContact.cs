﻿using LiveNiceApp;
using Npgsql;
using ProjectManagement.Database;
using ProjectManagement.Model;
using ProjectManagement.utlis;

namespace ProjectManagement.Models
{
    internal class QueryContact
    {
        private static readonly DatabaseConnection databaseConnection = new DatabaseConnection();

        public static Contact[] GetContactByID(int contact_id)
        {
            databaseConnection.OpenConnection();
            List<Contact> contacts = new();

            try
            {
                string commandText = $"SELECT * FROM CONTACT WHERE contact_id = @contact_id;";
                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());

                cmd.Parameters.AddWithValue("contact_id", contact_id);

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Contact contact = DatabaseReaders.ReadContact(reader);
                    databaseConnection.DisposeConnection();
                    contacts.Add(contact);
                    return contacts.ToArray();
                }
                Console.WriteLine($"Selected all contacts from the CONTACT Table");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not get all contacts");
            }

            databaseConnection.DisposeConnection();
            return contacts.ToArray();
        }

        public static Contact[] GetAllContacts()
        {
            databaseConnection.OpenConnection();
            List<Contact> contacts = new();

            try
            {
                string commandText = $"SELECT * FROM CONTACT;";
                using var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection());

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Contact contact = DatabaseReaders.ReadContact(reader);
                    contacts.Add(contact);
                }
                Console.WriteLine($"Selected all contacts from the CONTACT Table");
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not get all contacts");
            }

            databaseConnection.DisposeConnection();
            return contacts.ToArray();
        }

            public static Contact[] AddContact(int person_id, string cellphone, string email)
        {
            databaseConnection.OpenConnection();

            try
            {
                string commandText = $"INSERT INTO CONTACT (person_id, email, cellphone_number) VALUES(@person_id,@email, @cellphone_number);";
                using (var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("person_id", person_id);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("cellphone_number", cellphone);

                    Task<int> task = cmd.ExecuteNonQueryAsync();
                    Console.WriteLine("task result: "+task.Result);
                    Console.WriteLine($"Saved contact of person with ID {person_id} INTO CONTACT table");
                }
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not add contact details to the contact table for the person with the ID {person_id}");
            }

            databaseConnection.DisposeConnection();
            return GetAllContacts();
        }

        public static void DeleteContact(Contact contact) { }

        public static Contact[] UpdateContactEmail(int id, string email)
        {
            databaseConnection.OpenConnection();
            List<Contact> contacts = new();

            try
            {
                string commandText = $@"UPDATE CONTACT SET email = @email WHERE contact_id = @id;";

                using (var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("id", id);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"UPDATED CONTACT EMAIL WITH ID {id} IN CONTACT TABLE");
                    databaseConnection.DisposeConnection();
                    contacts.Add(GetContactByID(id)[0]);
                }
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not update CONTACT table with id {id}");
            }

            databaseConnection.DisposeConnection();
            return contacts.ToArray();
        }

        public static Contact[] UpdateContactCellphone(int id, string cellphone)
        {
            databaseConnection.OpenConnection();
            List<Contact> contacts = new();

            try
            {
                string commandText = $@"UPDATE CONTACT SET cellphone_number = @cellphone WHERE contact_id = @id;";

                using (var cmd = new NpgsqlCommand(commandText, databaseConnection.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("cellphone", cellphone);
                    cmd.Parameters.AddWithValue("id", id);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"UPDATED CONTACT Cellphone WITH ID {id} IN CONTACT TABLE");
                    databaseConnection.DisposeConnection();
                    contacts.Add(GetContactByID(id)[0]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"ERROR - Could not update CONTACT table with id {id}");
            }

            databaseConnection.DisposeConnection();
            return contacts.ToArray();
        }
    }
}
