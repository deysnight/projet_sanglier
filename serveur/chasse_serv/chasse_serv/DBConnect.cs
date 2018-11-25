using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace chasse_serv
{
    public class DBConnect
    {
        protected static readonly string database = "test.db";

        public static Object my_select(string table, string value)
        {
            Object json;
            using (var connection = new SQLiteConnection("" + new SQLiteConnectionStringBuilder { DataSource = database }))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var selectCommand = connection.CreateCommand();
                    selectCommand.Transaction = transaction;
                    selectCommand.CommandText = "SELECT " + value + " FROM " + table; /// QUERY HERE
                    using (var reader = selectCommand.ExecuteReader())
                    {
                        var items = new List<Dictionary<string, object>>();
                        while (reader.Read())
                        {
                            /*var item = new Dictionary<string, object>(reader.FieldCount - 1);
                            for (var i = 1; i < reader.FieldCount; i++)
                            {
                                item[reader.GetName(i)] = reader.GetValue(i);
                            }
                            items[reader.GetValue(0)] = item;*/


                            var item = new Dictionary<string, object>(reader.FieldCount - 1);
                            for (var i = 0; i < reader.FieldCount; i++)
                            {
                                item[reader.GetName(i)] = reader.GetValue(i);
                            }
                            items.Add(item);
                        }
                        var jsonstr = JsonConvert.SerializeObject(items, Formatting.Indented);
                        json = JsonConvert.DeserializeObject(jsonstr);
                    }
                    transaction.Commit();
                }
            }
            return (json);
        }

        public static void my_insert(string table, string champs, string values)
        {
            using (var connection = new SQLiteConnection("" + new SQLiteConnectionStringBuilder { DataSource = database }))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCommand = connection.CreateCommand();
                    insertCommand.Transaction = transaction;
                    insertCommand.CommandText = "INSERT INTO '" + table + "' (" + champs + ") VALUES (" + values + ");"; /// QUERY HERE
                    insertCommand.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
        }

    }
}
