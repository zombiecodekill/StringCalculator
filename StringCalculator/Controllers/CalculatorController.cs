using System;
using Microsoft.AspNetCore.Mvc;
using StringCalculator.Models;

namespace StringCalculator.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly Services.ICalculator _calculator;
        public CalculatorController(Services.ICalculator calculator)
        {
            _calculator = calculator;
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Index(CalculatorModel model)
        {
            try
            {
                model.Number = _calculator.Calculate(model.Input);
            }
            catch (Exception e)
            {
                model.Error = e.Message;
            }

            return View(model);
        }


    }
}
