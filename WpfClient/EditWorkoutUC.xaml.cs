﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfClient.JewGymService;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for EditWorkoutUC.xaml
    /// </summary>
    public partial class EditWorkoutUC : UserControl
    {
        public WorkoutPlan workPlan;
        public User user;
        public EditWorkoutUC(WorkoutPlan wp)
        {
            InitializeComponent();
            textTB.Text = wp.Workout.Type;
            this.workPlan = wp;

        }
        public EditWorkoutUC(User user)
        {
            InitializeComponent();
            textTB.Text = user.Firstname;
            this.user = user;
        }
        public EditWorkoutUC()
        {
            InitializeComponent();
            textTB.Text = "Rest";
        }

        public void ShowWorkout()
        {
            return;
        }

    }
}
