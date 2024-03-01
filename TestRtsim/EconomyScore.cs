using System;
using System.Collections.Generic;
using System.Linq;

namespace DockerServerRtsim.CalculationExercise.Score
{
    /// <summary>
    /// параметры подсчета экономики
    /// </summary>
    public class EconomyScore
    {
        public double EfficiencyMax;//максимальное количество баллов
        public double EfficiencyStep;//шаг отнимания баллов за каждый не соответсвующий параметр
        public double Result;

        public List<ProfitEconomy> profitEconomies;//параметры профита
        /// <summary>
        /// Экономика
        /// </summary>
        /// <param name="efficiencyMax">максимальное балл за эффективность</param>
        /// <param name="efficiencyStep">кол-во баллов смимаемых за каждое не соответствие</param>
        /// <param name="exercise">упражнение</param>
        /// <param name="profitEconomies">список экономических показателей</param>
        /// <param name="paramname">список параметров эффективности</param>
        /// <param name="info"></param>
        public EconomyScore(double efficiencyMax, double efficiencyStep, List<ClassParamsTProwForLog> lptProtocols, List<ProfitEconomy> profitEconomies, string[] paramname)
        {
            EfficiencyMax = efficiencyMax;
            EfficiencyStep = efficiencyStep;
            this.profitEconomies = profitEconomies;
            Result = Economy(lptProtocols,paramname);
        }

        /// <summary>
        /// Экономика
        /// </summary>
        /// <param name="classParamsTProwForLogs">журнал параметров технологического режима</param>
        /// <param name="paramname">параметры посчета экономики</param>
        /// <returns>результат</returns>
        double Economy(List<ClassParamsTProwForLog> classParamsTProwForLogs, string[] paramname)
        {
            var efficiency = Efficiency(classParamsTProwForLogs, paramname);
            double profit = 0;
            // выгода по зарплате, премиии и ВПР не менее MoneyMin (-Step за каждые StepMoney руб)
            // если значение параметра меньше MoneyMin то за каждое StepMoney отнимаем Step
            // пример: значение параметра =10 MoneyMin=14 StepMoney=2 Step=1 Максимальный балл=7 
            // Значит из MoneyMin мы вычитаем StepMoney, пока результат не будет равен значению параметра (то есть вычитаем дважды)
            // и так же дважды вычитаем из максимального балла Ste. Конечный результат = 5 баллов
            foreach (var p in profitEconomies)
            {
                var scoreEconomy = p.Max;
                var parametr = classParamsTProwForLogs.FirstOrDefault(b => b.Position == p.ParamName);
                
                if (parametr != null)
                {
                    var value = parametr.Value;
                    if (value < p.MoneyMin)
                    {
                        var dif = p.MoneyMin - value;
                        var count = Math.Truncate(dif / p.StepMoney);
                        var result = count * p.Step;
                        scoreEconomy -= result;
                    }
                }
                profit += scoreEconomy;
            }
            var score = efficiency + profit;
            return score;
        }

        /// <summary>
        /// Показатели экономической эффективности (nameParams) находятся в зеленой зоне (-EfficiencyStep за каждый прибор не в зеленой зоне)
        /// зеленая зона - это диапазон от NormaL(нижняя граница) до NormaH(верхняя граница)
        /// </summary>
        /// <param name="classParamsTProwForLogs">журнал параметров технологического режима</param>
        /// <param name="nameParams">параметры посчета экономики</param>
        /// <returns>результат</returns>
        double Efficiency(List<ClassParamsTProwForLog> classParamsTProwForLogs, string[] nameParams)
        {
            var score = EfficiencyMax;
            
            foreach (var nameparam in nameParams)
            {
                var param = classParamsTProwForLogs.FirstOrDefault(p => p.Position == nameparam);
                if (param != null)
                {
                    double value = param.Value;
                    if (!(value >= param.NormaL && value <= param.NormaH))
                    {
                        score -= EfficiencyStep;
                    }
                }
            }
            return score < 0 ? 0 : score;

        }
        
    }

}
