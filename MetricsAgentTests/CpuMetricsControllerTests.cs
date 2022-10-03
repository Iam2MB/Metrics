using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerTests
    {
        private CpuMetricsController _cpuMetricsController;

        public CpuMetricsControllerTests()
        {
            _cpuMetricsController = new CpuMetricsController();
        }

        [Fact]
        public void GetMetrics_ReturnOk()
        {
            //подготовка данных для тестирования
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            //исполнение тестируемого кода
            var result = _cpuMetricsController.GetCpuMetrics(fromTime, toTime);

            //подготовка эталонного результата, проверка результата
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
