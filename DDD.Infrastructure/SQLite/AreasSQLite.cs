using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace DDD.Infrastructure.SQLite
{
    public sealed class AreasSQLite : IAreaRepository
    {
        public IReadOnlyList<AreaEntity> GetData()
        {
            var sql = @"SELECT AreaId,
                               AreaName
                          FROM Areas";

            return SQLiteHelper.Query<AreaEntity>(sql, reader =>{
                return new AreaEntity(
                    Convert.ToInt32(reader["AreaId"]), Convert.ToString(reader["AreaName"]));
            });
        }
    }
}
