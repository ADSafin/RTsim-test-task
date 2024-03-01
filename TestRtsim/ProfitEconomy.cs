namespace DockerServerRtsim.CalculationExercise.Score
{
    /// <summary>
    /// параметры посчета профита экономики
    /// </summary>
    public class ProfitEconomy
    {
        public double Max;//максимальный балл
        public double Step;//шаг отнимания баллов за каждый не соответсвующий параметр
        public double StepMoney;//если реальный значение меньше MoneyMin, за каждое StepMoney вычитается баллы в размере Step
        public double MoneyMin;//минимально допустимое значение параметра 
        public string ParamName;//название параметра
    }
}
