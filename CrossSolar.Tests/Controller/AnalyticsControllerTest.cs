using CrossSolar.Controllers;
using CrossSolar.Models;
using CrossSolar.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CrossSolar.Tests.Controller
{
    public class AnalyticsControllerTest
    {

        private readonly IPanelRepository _panelRepositoryMock;
        private readonly IAnalyticsRepository _analyticsRepository;
        private readonly IDayAnalyticsRepository _dayAnalyticsRepository;

        private AnalyticsController _controller;


        public AnalyticsControllerTest(IPanelRepository panelRepositoryMock, IAnalyticsRepository analyticsRepository, IDayAnalyticsRepository dayAnalyticsRepository)
        {
            _panelRepositoryMock = panelRepositoryMock;
            _analyticsRepository = analyticsRepository;
            _dayAnalyticsRepository = dayAnalyticsRepository;
            _controller = new AnalyticsController(_analyticsRepository, _panelRepositoryMock, _dayAnalyticsRepository);
        }

        [Fact]
        public void GetTest()
        {
            var result = _controller.Get(1).Result;
            var objectResult = result as ObjectResult;
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void DayResultsTest()
        {
            var result = _controller.DayResults(1).Result;
            var objectResult = result as ObjectResult;
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public void PostTest()
        {

            var result = _controller.Post(1,
                    new OneHourElectricityModel
                    {
                        DateTime = DateTime.Now,
                        KiloWatt = 678
                    })
                .Result;
            var createdResult = result as CreatedResult;
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }



    }
}
