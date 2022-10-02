using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsAgentTests
{
    public class HddMetricsControllerTests
    {
        private HddMetricsController _hddMetricsController;

        public HddMetricsControllerTests()
        {
            _hddMetricsController = new HddMetricsController();
        }

        [Fact]
        public void GetMetrics_ReturnOk()
        {
            //подготовка данных для тестирования
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            //исполнение тестируемого кода
            var result = _hddMetricsController.GetHddMetrics(fromTime, toTime);

            //подготовка эталонного результата, проверка результата
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
