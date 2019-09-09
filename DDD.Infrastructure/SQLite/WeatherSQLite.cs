using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DDD.Infrastructure.SQLite
{
    public class WeatherSQLite : IWeatherRepository
    {

        public WeatherEntity GetLatest(int latest)
        {
            var sql = @"
select DataDate,
        Condition,
        Temperature
from Weather
where AreaId = @AreaId
order by DataDate desc
LIMIT 1";

            return SQLiteHelper.QuerySingle<WeatherEntity>(sql,
                reader =>
                {
                    return new WeatherEntity(
                                latest,
                                Convert.ToDateTime(reader["DataDate"]),
                                Convert.ToInt32(reader["Condition"]),
                                Convert.ToSingle(reader["temperature"])
                                );
                },
                null,
                new SQLiteParameter[] {
                    new SQLiteParameter("@AreaId", latest)
                }
                );

        }

        public IReadOnlyList<WeatherEntity> GetData()
        {
            var sql = @"
select a.AreaId, 
        b.AreaName, 
        a.DataDate,
        a.Condition,
        a.Temperature
from Weather a
left join Areas b on a.AreaId = b.AreaId";

            return SQLiteHelper.Query<WeatherEntity>(sql, reader =>
            {
                return new WeatherEntity(
                                Convert.ToInt32(reader["AreaId"]),
                                Convert.ToString(reader["AreaName"]),
                                Convert.ToDateTime(reader["DataDate"]),
                                Convert.ToInt32(reader["Condition"]),
                                Convert.ToSingle(reader["temperature"])
                                );
            });

        }

        public void Save(WeatherEntity weather)
        {
            var insert = @"
insert into Weather 
(AreaId,DataDate,Condition,Temperature)
values 
(@AreaId,@DataDate,@Condition,@Temperature)";

            var update = @"
update Weather 
set Condition = @Condition,
    Temperature = @Temperature
where AreaId = @AreaId
  and DataDate = @DataDate";

            var args = new SQLiteParameter[]
            {
                new SQLiteParameter("",weather.AreaId.Value),
                new SQLiteParameter("",weather.DataDate),
                new SQLiteParameter("",weather.Condition.Value),
                new SQLiteParameter("",weather.Temperature.Value),
            };

            SQLiteHelper.Excecute(insert, update, args);
        }


    }
}
