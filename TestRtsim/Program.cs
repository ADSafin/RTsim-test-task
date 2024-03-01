using DockerServerRtsim.CalculationExercise.Score;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRtsim
{
    internal class Program
    {
        static void Main(string[] args)
        {

                var param0 = new ClassParamsTProwForLog() { Id = 0, N = "0", NormaH = 76.99, NormaL=24.27, Value=87.43, Position = "Норма расхода сырья" };
                var param1 = new ClassParamsTProwForLog() { Id = 1, N = "1", NormaL  = 7.55, NormaH = 520.19, Value= 100.17, Position = "Норма расхода теплоносителя" };
                var param2 = new ClassParamsTProwForLog() { Id = 2, N = "2", NormaL  = 17.57, NormaH = 65.7, Value= 68.34, Position = "Норма расхода пара СД" };
                var param3 = new ClassParamsTProwForLog() { Id = 3, N = "3", NormaL  = 27.1, NormaH = 80.3, Value= 75.89, Position = "Норма расхода воды" };
                var param4 = new ClassParamsTProwForLog() { Id = 4, N = "4", NormaL  = 65.35, NormaH = 90.6, Value= 84.60, Position = "Себестоимость " };
                var param5 = new ClassParamsTProwForLog() { Id = 5, N = "5", NormaL  = 18.29, NormaH = 63.49, Value= 94.27, Position = "Param5" };
                var param6 = new ClassParamsTProwForLog() { Id = 6, N = "6", NormaL  = 10.1, NormaH = 30.18, Value= 94.43, Position = "Param6" };
                var param7 = new ClassParamsTProwForLog() { Id = 7, N = "7", NormaL  = 22.11, NormaH = 67.62, Value= 948065, Position = "Прибыль" };
                var param8 = new ClassParamsTProwForLog() { Id = 8, N = "8", NormaL  = 26.87, NormaH = 77.50, Value= 86815, Position = "Премия участника" };
                var param9 = new ClassParamsTProwForLog() { Id = 9, N = "9", NormaL  = 28.33, NormaH = 100.28, Value= 30610543, Position = "ВПР" };

            var listParams = new List<ClassParamsTProwForLog>() { param0, param1, param2, param3, param4, param5, param6, param7, param8, param9 };


            string[] nameParams = new string[] { "Норма расхода сырья", "Норма расхода теплоносителя", "Норма расхода пара СД", "Норма расхода воды", "Себестоимость " };

            const double efficiencyMax = 10;
            const double efficiencyStep = 2;
            var profitEconomies = new List<ProfitEconomy>(){
            new ProfitEconomy() { Max = 10, MoneyMin = 5500000, Step = 1, StepMoney = 500000, ParamName = "ВПР" },
            new ProfitEconomy() { Max = 10, MoneyMin = 150000, Step = 1, StepMoney = 20000, ParamName = "Премия участника" },
            new ProfitEconomy() { Max = 10, MoneyMin = 1200000, Step = 1, StepMoney = 100000, ParamName = "Прибыль" }

        };
            //Задание 1
            //внутри этого класса есть функции подсчета экономики. Но она считает не верно. Нужно найти проблемные места и исправить их. Сделать рефакторинг, если есть необходимость
            var economy = new EconomyScore(efficiencyMax, efficiencyStep, listParams, profitEconomies, nameParams);
            const double res = 31;
            if (res != economy.Result)
                Console.WriteLine("Не правильно считает экономику!");
            else Console.WriteLine("Подсчет экономики верный!");

            //Задание 2
            //Сделать аналогичную фукцию для расчета времени работы
            // общее время работы за каждые шаг по времени превышающие максимально допустимое время на работу вычитается шаг по баллам из максимального балла
            // WorkTime за каждые StepTime превышающие MaxMinutesWorkTime вычитается StepScore из MaxScoreWorkTime
            // пример: WorkTime =16 секунд, StepTime=2 секунды, MaxMinutesWorkTime= 10 секунд, StepScore = 1 балл, MaxScoreWorkTime = 5 баллов. Результат = 2 балла
            //входные значения: время начала работы, время окончания работы, максимально допустимое время на работу, максимальный балл, шаг по времени, шаг по баллам
            //выходное значение: полученое количество баллов
            
            //Входные данные для проверки работоспособности функции для подсчета времени работы
            const double maxScoreWorkTime = 10;
            const double stepScore = 2;
            const double maxSecondsWorkTime = 1000;
            const double stepTime = 150;
            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();
            date1 = DateTime.Now;
            date2 = date1.AddSeconds(1200);
            
            // Вызов самой функции и вывод кол-ва баллов на консоль
            var time = new TimeScore(maxScoreWorkTime, stepScore, maxSecondsWorkTime, date1, date2, stepTime); 
            Console.WriteLine(time.Result);
        }
    }
}
