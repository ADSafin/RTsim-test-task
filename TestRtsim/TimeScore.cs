using System;

namespace DockerServerRtsim.CalculationExercise.Score
{
    /// <summary>
    /// Параметры подсчета времени работы
    /// </summary>
    public class TimeScore
    {
        public double MaxScoreWorkTime; // максимальное количество баллов
        public double StepScore; // шаг отнимания баллов 
        public double MaxSecondsWorkTime; // максимальное время на работу
        public double Result;

        /// <summary>
        /// Время работы
        /// </summary>
        /// <param name="maxScoreWorkTime">максимальное количество баллов за время работы</param>
        /// <param name="stepScore">количество баллов, отнимаемых за каждое превышение времени</param>
        /// <param name="maxSecondsWorkTime">максимальное время на работу</param>
        /// <param name="startTime">время начала работы</param>
        /// <param name="endTime">время окончания работы</param>
        /// <param name="stepTime">шаг по времени</param>
        public TimeScore(double maxScoreWorkTime, double stepScore, double maxSecondsWorkTime, DateTime startTime, DateTime endTime, double stepTime)
        {
            MaxScoreWorkTime = maxScoreWorkTime;
            StepScore = stepScore;
            MaxSecondsWorkTime = maxSecondsWorkTime;
            Result = CalculateTimeScore(startTime, endTime, stepTime);
        }

        double CalculateTimeScore(DateTime startTime, DateTime endTime, double stepTime)
        {
            double totalTimeScore = MaxScoreWorkTime;

            TimeSpan valTime = endTime - startTime; 

            if (valTime.TotalSeconds > MaxSecondsWorkTime)
            {
                double dif = valTime.TotalSeconds - MaxSecondsWorkTime;
                double count = Math.Ceiling(dif / stepTime);
                var result = count * StepScore;
                totalTimeScore -= result;
            }
            
            return totalTimeScore;
        }
    }
}
