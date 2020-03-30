﻿using MongoDB.Bson;
using MongoDB.Driver;
using PodcastApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using WorkoutApp.Model;

namespace WorkoutApp.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        private ObservableCollection<Exercise> _exercises;
        public ObservableCollection<Exercise> Exercises
        {
            get { return _exercises; }
            set
            {
                if (_exercises == value) return;
                _exercises = value;
                OnPropertyChanged("Exercises");
            }
        }

        private ObservableCollection<Workout> _workouts;
        public ObservableCollection<Workout> Workouts
        {
            get { return _workouts; }
            set
            {
                if (_workouts == value) return;
                _workouts = value;
                OnPropertyChanged("Workouts");
            }
        }

        private Exercise _selectedExercise;
        public Exercise SelectedExercise
        {
            get { return _selectedExercise; }
            set
            {
                if (_selectedExercise == value) return;
                _selectedExercise = value;
                OnPropertyChanged("SelectedExercise");
            }
        }

        private Workout _selectedWorkout;
        public Workout SelectedWorkout
        {
            get { return _selectedWorkout; }
            set
            {
                if (_selectedWorkout == value) return;
                _selectedWorkout = value;
                SaveWorkoutCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("SelectedWorkout");
            }
        }
        public Random Rng { get; set; }
        public ICommand RandomWorkoutCommand { get; set; }
        public BaseCommand SaveWorkoutCommand { get; set; }
        public MainVM()
        {
            Exercises = new ObservableCollection<Exercise>();
            Workouts = new ObservableCollection<Workout>();
            Rng = new Random();

            InstantiateCommands();

            ReadExercises();
            ReadWorkouts();
        }
        public void InstantiateCommands()
        {
            // x = nothing
            RandomWorkoutCommand = new BaseCommand(x => true, x => GenerateRandomWorkout());
            SaveWorkoutCommand = new BaseCommand(w => w != null, x => SaveWorkout());
        }
        public void ReadExercises()
        {
            // Summary
            // 
            // Read exercises from Database with DBHelper class. Clear existing exercises first.

            var exercises = DatabaseHelper.GetExercises();

            Exercises.Clear();

            foreach (Exercise exercise in exercises)
            {
                Exercises.Add(exercise);
            }
        }
        public void ReadWorkouts()
        {
            // Summary
            //
            // Read workouts from Database with DbHelper class. Clear existing workouts first.

            var workouts = MongoHelper.GetWorkoutsAsync();

            Workouts.Clear();

            foreach(var workout in workouts)
            {
                Workouts.Add(workout);
            }
        }
        public void GenerateRandomWorkout(int numStations = 4, int numExercises = 3, int repSeconds = 35, int restSeconds = 10)
        {
            // Summary
            //
            // Generates new randomized workout

            List<Exercise> randomizedExercises = new List<Exercise>();

            var exercises = (List<Exercise>)DatabaseHelper.GetExercises();
            
            int randIndex;

            // Get list of distinct randomized exercises of necessary size
            while (randomizedExercises.Count != numStations * numExercises)
            {
                randIndex = Rng.Next(exercises.Count);

                if (!randomizedExercises.Contains(exercises[randIndex]))
                {
                    randomizedExercises.Add(exercises[randIndex]);
                }
            }

            Workout workout = new Workout{ Name = "New Workout", 
                Description = "Randomly Generated Workout",
                RepSeconds = repSeconds,
                RestSeconds = restSeconds};

            // Fill workout
            for (int i=0; i < numStations; i++)
            {
                workout.Stations.Add(new Station());
                workout.Stations[i].StationName = ("Station " + (i+1).ToString());
                workout.Stations[i].Exercises = new List<Exercise>();

                for (int j=0; j<numExercises; j++)
                {
                    workout.Stations[i].Exercises.Add(new Exercise());
                    workout.Stations[i].Exercises[j] = randomizedExercises[numExercises * i + j];
                }
            }

            SelectedWorkout = workout;
        }
        public void SaveWorkout()
        {
            // Summary
            //
            // Saves selected workout to the DB then refreshes the list

            MongoHelper.AddWorkoutAsync(SelectedWorkout);

            ReadWorkouts();
        }
        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
