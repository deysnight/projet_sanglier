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

        public static bool tableExist()
        {
            bool isExist = false;
            using (var connection = new SQLiteConnection("" + new SQLiteConnectionStringBuilder { DataSource = database }))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var selectCommand = connection.CreateCommand();
                    selectCommand.Transaction = transaction;
                    selectCommand.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table' AND name = 'index'"; /// QUERY HERE
                    using (var reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            isExist = true;
                        }
                    }
                    transaction.Commit();
                }
            }
            return (isExist);
        }

        public Object my_select()
        {
            Object json;
            using (var connection = new SQLiteConnection("" + new SQLiteConnectionStringBuilder { DataSource = database }))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var selectCommand = connection.CreateCommand();
                    selectCommand.Transaction = transaction;
                    selectCommand.CommandText = "SELECT * FROM \"index\""; /// QUERY HERE
                    using (var reader = selectCommand.ExecuteReader())
                    {
                        var items = new Dictionary<object, Dictionary<string, object>>();
                        while (reader.Read())
                        {
                            var item = new Dictionary<string, object>(reader.FieldCount - 1);
                            for (var i = 1; i < reader.FieldCount; i++)
                            {
                                item[reader.GetName(i)] = reader.GetValue(i);
                            }
                            items[reader.GetValue(0)] = item;
                        }
                        var jsonstr = JsonConvert.SerializeObject(items, Formatting.Indented);
                        json = JsonConvert.DeserializeObject(jsonstr);
                    }
                    transaction.Commit();
                }
            }
            return (json);
        }

        public void my_insert()
        {
            using (var connection = new SQLiteConnection("" + new SQLiteConnectionStringBuilder { DataSource = database }))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCommand = connection.CreateCommand();
                    insertCommand.Transaction = transaction;
                    insertCommand.CommandText = "INSERT INTO \"index\" (nom) VALUES (\"bite2\"),(\"bite\");"; /// QUERY HERE
                    insertCommand.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
        }

    }
}
