using System;
using System.Collections.Generic;
using DDD.Domain.Entities;
using DDD.Domain.Exceptions;
using DDD.Domain.Repositories;
using DDD.WinForm.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DDDTest.Tests
{
    [TestClass]
    public class WeatherSaveViewModelTest
    {
        [TestMethod]
        public void 天気登録シナリオ()
        {
            var weatherMock = new Mock<IWeatherRepository>();
            var areasMock = new Mock<IAreaRepository>();

            var areas = new List<AreaEntity>();
            areas.Add(new AreaEntity(1, "東京"));
            areas.Add(new AreaEntity(2, "神戸"));
            areasMock.Setup(x => x.GetData()).Returns(areas);

            var viewModelMock = new Mock<WeatherSaveViewModel>(weatherMock.Object, areasMock.Object);
            viewModelMock.Setup(x => x.GetDateTime())
                .Returns(Convert.ToDateTime("2018/01/01 12:34:56"));

            var viewModel = viewModelMock.Object;
            Assert.IsNull(viewModel.SelectedAreaId);
            Assert.AreEqual(Convert.ToDateTime("2018/01/01 12:34:56"), viewModel.DataDateValue);
            Assert.AreEqual(1, viewModel.SelectedCondition);
            Assert.AreEqual("", viewModel.TemperatureText);
            Assert.AreEqual("℃", viewModel.TemperatureUnitName);

            Assert.AreEqual(2, viewModel.Areas.Count);
            Assert.AreEqual(4, viewModel.Conditions.Count);

            var ex = Assert.ThrowsException<InputException>(()=> viewModel.Save());
            Assert.AreEqual("エリアを選択してください", ex.Message);

            viewModel.SelectedAreaId = 2;
            ex = Assert.ThrowsException<InputException>(() => viewModel.Save());
            Assert.AreEqual("温度の選択に誤りがあります", ex.Message);

            viewModel.TemperatureText= "12.345";

            weatherMock.Setup(x => x.Save(It.IsAny<WeatherEntity>()))
                .Callback<WeatherEntity>(saveValue => {
                    Assert.AreEqual(2, saveValue.AreaId.Value);
                    Assert.AreEqual(Convert.ToDateTime("2018/01/01 12:34:56"), saveValue.DataDate);
                    Assert.AreEqual(1, saveValue.Condition.Value);
                    Assert.AreEqual(12.345f, saveValue.Temperature.Value);
                });

            viewModel.Save();
            weatherMock.VerifyAll();
        }
    }
}
