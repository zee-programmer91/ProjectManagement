using System.Text;

namespace ProjectManagement.utlis
{
    public static class UpdateCreator
    {
        private const string emptyString = "";
        private const string zeroIndex = "0";
        private static readonly DateTime defaultDateTime = new DateTime(1,1,1);
        private const bool defaultAccess = false;

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

            Console.WriteLine($"columAndValues: {columAndValues["has_room_access"]}");

            foreach (var column in columAndValues)
            {
                switch (column.Value as string == emptyString || column.Value.ToString() == zeroIndex ||
                    column.Value.ToString() == defaultDateTime.ToString() || column.Value.ToString() == defaultAccess.ToString().ToUpper())
                {
                    case true:
                        emptyFields++;
                        break;
                    case false:
                        if (column.Value.ToString() == "True")
                        {
                            Console.WriteLine($" SET {column.Key} = CAST(1 AS bit),");
                            updateQuery.Append($" SET {column.Key} = CAST(1 AS bit),");
                        }
                        else if (column.Value.ToString() == "False")
                        {
                            Console.WriteLine($" SET {column.Key} = CAST(0 AS bit),");
                            updateQuery.Append($" SET {column.Key} = CAST(0 AS bit),");
                        }
                        else
                        {
                            Console.WriteLine($" SET {column.Key} = @{column.Key},");
                            updateQuery.Append($" SET {column.Key} = @{column.Key},");
                        }
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
