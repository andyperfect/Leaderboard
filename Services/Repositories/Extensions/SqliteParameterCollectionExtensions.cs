using System;
using Microsoft.Data.Sqlite;

namespace Services.Repositories.Extensions
{
    public static class SqliteParameterCollectionExtensions
    {
        public static SqliteParameter AddWithValue(this SqliteParameterCollection target, string parameterName, object value, object nullValue)
        {
            return value == null ? target.AddWithValue(parameterName, nullValue ?? DBNull.Value) : target.AddWithValue(parameterName, value);
        }
    }
}
