namespace DockerServerRtsim.CalculationExercise.Score
{
    /// <summary>
    /// параметр технологического режима
    /// </summary>
    public class ClassParamsTProwForLog
    {
        public int Id { set; get; }
        public string N { set; get; }       // Порядковый номер
        public string Position { set; get; }   // Позиция прибора
        public double Value { set; get; }      // Значение
        public double NormaL { set; get; }     // Норма мин.
        public double NormaH { set; get; }     // Норма макс.
      

    }
}
