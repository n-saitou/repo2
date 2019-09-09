using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DDD.Infrastructure.SQLite
{
    internal static class SQLiteHelper
    {
        internal const string ConnectionString = 
            @"Data Source=C:\Users\n_sai\OneDrive\デスクトップ\ddd.db;Version=3;";

        internal static IReadOnlyList<T> Query<T>(string sql,
            Func<SQLiteDataReader, T> createEntity,
            SQLiteParameter[] parameters = null)
        {
            var result = new List<T>();
            using (var connection = new SQLiteConnection(SQLiteHelper.ConnectionString))
            using (var command = new SQLiteCommand(sql, connection))
            {
                connection.Open();
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(createEntity(reader));
                    }
                }
            }
            return result;
        }


        internal static T QuerySingle<T>(string sql,
            Func<SQLiteDataReader, T> createEntity, 
            T nullEntity,
            SQLiteParameter[] parameters = null)
        {
            using (var connection = new SQLiteConnection(SQLiteHelper.ConnectionString))
            using (var command = new SQLiteCommand(sql, connection))
            {
                connection.Open();
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return createEntity(reader);
                    }
                }
            }
            return nullEntity;
        }

        internal static void Excecute(string insert,
            string update,
            SQLiteParameter[] parameters)
        {
            using (var connection = new SQLiteConnection(SQLiteHelper.ConnectionString))
            using (var command = new SQLiteCommand(update, connection))
            {
                connection.Open();
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                if (command.ExecuteNonQuery() < 1)
                {
                    command.CommandText = insert;
                    command.ExecuteNonQuery();
                }

            }
        }

    }
}
