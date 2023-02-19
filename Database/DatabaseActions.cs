using System;

namespace ProjectManagement.Database
{

    public interface DatabaseActions

    {
        enum DatabaseActionsResponses
        {
            Success,
            Failed,
            FieldEmpty
        }
        /// <summary>
        /// Gets an Item from the database table based on the given ID number
        /// </summary>
        /// <param name="ID">The ID number of the item in the database</param>
        /// <returns>The item that belongs to the given ID</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static object GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all the items from the database table
        /// </summary>
        /// <returns>All the items from the database table</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static object GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Inserts an entry into the database table
        /// </summary>
        /// <param name="newEntry"></param>
        /// <returns>Returns a DatabaseActionsResponses response</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static DatabaseActionsResponses InsertEntry(object newEntry)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Udpates an entry in the database table where it matches the given ID number
        /// </summary>
        /// <param name="updateEntry"></param>
        /// <returns>Returns a DatabaseActionsResponses response</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static DatabaseActionsResponses UpdateEntryByID(object updateEntry)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes an entry from the database table based on the given ID number
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>Returns a DatabaseActionsResponses response</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static DatabaseActionsResponses DeleteEntryByID(int ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes an entry from the database table based on the given ID number
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>Returns a DatabaseActionsResponses response</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static DatabaseActionsResponses SoftDeleteEntryByID(int ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes all entries from the database table
        /// </summary>
        /// <returns>Returns a DatabaseActionsResponses response</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static DatabaseActionsResponses DeleteAll()
        {
            throw new NotImplementedException();
        }
    }
}
