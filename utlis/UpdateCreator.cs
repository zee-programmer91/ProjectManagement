using Microsoft.AspNetCore.Http;
using ProjectManagement.Model;
using System.Text;

namespace ProjectManagement.utlis
{
    public static class UpdateCreator
    {
        private const string emptyString = "";

        /// <summary>
        /// Creates an SQL update query statement based on the values given
        /// </summary>
        /// <param name="tableName">The name of the table in the database</param>
        /// <param name="columAndValues">Column names and their values</param>
        /// <param name="whereColumn">The name of the column you want to update the entry with</param>
        /// <returns>An SQL executable string</returns>
        public static string CreateUpdateQuery(string tableName, Dictionary<string, object> columAndValues, string whereColumn)
        {
            StringBuilder updateQuery = new StringBuilder();
            updateQuery.Append($"UPDATE {tableName.ToUpper()}");
            int sizeOfUpdateQueryBefore = updateQuery.Length;
            int emptyFields = 0;

            foreach (var column in columAndValues)
            {
                switch (column.Value == emptyString)
                {
                    case true:
                        emptyFields++;
                        break;
                    case false:
                        updateQuery.Append($" SET {column.Key} = @{column.Key},");
                        break;
                }
            }

            if (updateQuery.Length == sizeOfUpdateQueryBefore)
            {
                return emptyString;
            }

            int lastIndex = updateQuery.Length - 1;
            int legnthToRemove = 1;
            updateQuery.Remove(lastIndex, legnthToRemove);
            updateQuery.Append($" WHERE {whereColumn} = @{whereColumn}");

            return updateQuery.ToString();
        }
    }
}
