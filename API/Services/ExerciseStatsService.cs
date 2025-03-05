using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Dto;

namespace API.Services
{
    public class ExerciseStatsService
    {
        private readonly string _filePath;

        public ExerciseStatsService(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<List<ExerciseStatDto>> GetAverageOneRepMaxByWeightCategory(string exerciseName)
        {
            var data = new List<WorkoutSetDto>();

            using (var reader = new StreamReader(_filePath))
            {
                string headerLine = await reader.ReadLineAsync(); // Pomijamy nagłówek

                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    var values = line.Split(',');

                    if (values.Length < 7) continue;

                    if (double.TryParse(values[4], NumberStyles.Any, CultureInfo.InvariantCulture, out double weight) &&
                        int.TryParse(values[5], out int reps) &&
                        double.TryParse(values[10], NumberStyles.Any, CultureInfo.InvariantCulture, out double bodyWeight) &&
                        !string.IsNullOrEmpty(values[2]) &&
                        weight > 0 && reps > 0 && bodyWeight > 0)
                    {
                        weight = weight * 0.453592; // Zamiana funtów na kilogramy
                        data.Add(new WorkoutSetDto
                        {
                            ExerciseName = values[2].Trim(),
                            Weight = weight,
                            Reps = reps,
                            BodyWeight = bodyWeight
                        });
                    }
                }
            }

            var groupedData = data
                .Where(ws => ws.ExerciseName.Equals(exerciseName, StringComparison.OrdinalIgnoreCase))
                .GroupBy(ws => (int)(ws.BodyWeight / 10) * 10) // Kategoryzowanie wag co 10 kg
                .Select(g => new ExerciseStatDto
                {
                    WeightCategory = g.Key,
                    AverageOneRepMax = g.Average(ws => ws.Weight * (1 + (ws.Reps / 30.0)))
                })
                .OrderBy(g => g.WeightCategory)
                .ToList();

            return groupedData;
        }
    }
}

