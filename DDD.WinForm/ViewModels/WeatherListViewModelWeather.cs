using DDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.WinForm.ViewModels
{
    public sealed class WeatherListViewModelWeather
    {
        WeatherEntity _entity;
        public WeatherListViewModelWeather(WeatherEntity entiry)
        {
            _entity = entiry;
        }
        public string AreaId => _entity.AreaId.DisplayVaule;
        public string AreaName => _entity.AreaName;
        public string DataDate => _entity.DataDate.ToString();
        public string Condition => _entity.Condition.DisplayValue;
        public string Temperature => _entity.Temperature.DisplayValue;
    }
}
