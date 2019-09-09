using System;
using System.Collections.Generic;
using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using DDD.WinForm.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DDDTest.Tests
{
    [TestClass]
    public class WeatherListViewModelTest
    {
        [TestMethod]
        public void 天気一覧画面シナリオ()
        {
            var weatherMock = new Mock<IWeatherRepository>();

            var entities = new List<WeatherEntity>();
            entities.Add(new WeatherEntity(1, "東京", Convert.ToDateTime("2019/01/01 12:34:56"), 2, 12.3f));
            entities.Add(new WeatherEntity(2, "神戸", Convert.ToDateTime("2019/02/02 12:34:56"), 1, 22.4f));

            weatherMock.Setup(x => x.GetData()).Returns(entities);

            var viewModel = new WeatherListViewModel(weatherMock.Object);
            Assert.AreEqual(2, viewModel.Weathers.Count);
            Assert.AreEqual("0001", viewModel.Weathers[0].AreaId);
            Assert.AreEqual("東京", actual: viewModel.Weathers[0].AreaName);
            Assert.AreEqual("2019/01/01 12:34:56", viewModel.Weathers[0].DataDate);
            Assert.AreEqual("曇り", viewModel.Weathers[0].Condition);
            Assert.AreEqual("12.30℃", viewModel.Weathers[0].Temperature);
        }
    }
}
