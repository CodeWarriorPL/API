using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using API.Models;
using API.Data;
using Microsoft.EntityFrameworkCore;

public class ExerciseImporter
{
    private readonly DataContext _context;

    public ExerciseImporter(DataContext context)
    {
        _context = context;
    }

    public void ImportExercises(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Plik nie istnieje!");
            return;
        }

        var exercises = new HashSet<string>(); // Unikalne nazwy ćwiczeń
        using (var reader = new StreamReader(filePath))
        {
            reader.ReadLine(); // Pomijamy nagłówek
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                if (values.Length < 3) continue; // Upewniamy się, że mamy poprawną linię

                string exerciseName = values[2].Trim(); // Nazwa ćwiczenia
                if (!string.IsNullOrEmpty(exerciseName))
                {
                    exercises.Add(exerciseName);
                }
            }
        }

        foreach (var name in exercises)
        {
            if (!_context.Exercises.Any(e => e.ExerciseName == name))
            {
                _context.Exercises.Add(new Exercise
                {
                    ExerciseName = name,
                    BodyPart = CategorizeExercise(name),
                    imageUrl = "" // Możesz dodać URL do obrazka
                });
            }
        }

        _context.SaveChanges();
        Console.WriteLine("Import zakończony!");
    }

    private BodyPart CategorizeExercise(string name)
    {
        name = name.ToLower().Trim();

        if (name.Contains("squat") || name.Contains("leg press") || name.Contains("calf") || name.Contains("hack squat") || name.Contains("leg curl") || name.Contains("leg extension"))
            return BodyPart.Legs;

        if (name.Contains("bench") || name.Contains("chest") || name.Contains("fly") || name.Contains("incline press") || name.Contains("decline press"))
            return BodyPart.Chest;

        if (name.Contains("deadlift") || name.Contains("row") || name.Contains("lat pulldown") || name.Contains("pull up") || name.Contains("chin up") || name.Contains("good morning") || name.Contains("t-bar row"))
            return BodyPart.Back;

        if (name.Contains("bicep") || name.Contains("tricep") || name.Contains("curl") || name.Contains("skullcrusher") || name.Contains("hammer"))
            return BodyPart.Arms;

        if (name.Contains("shoulder") || name.Contains("press") || name.Contains("military press") || name.Contains("lateral raise") || name.Contains("front raise") || name.Contains("shrug"))
            return BodyPart.Shoulders;

        if (name.Contains("core") || name.Contains("abs") || name.Contains("plank"))
            return BodyPart.Core;

        return BodyPart.Other;
    }

}
