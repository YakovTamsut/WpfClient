using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for CreateWorkoutUC.xaml
    /// </summary>
    public partial class CreateWorkoutUC : UserControl
    {
        private ServiceModelClient GymService;
        private ExerciseInWorkOutList exInWoList;
        private Exercise exercise;
        private Workout workout;
        private int countCompoundSets = 0;
        private int countSets = 0;
        ExerciseUC exerciseUC;
        SetRepEXUC setRepEXUC;
        private UserWindow window;
        private WorkoutPlan wop;

        public CreateWorkoutUC(User user, UserWindow window)
        {
            InitializeComponent();
            exInWoList=new ExerciseInWorkOutList(); 
            DataContext = this;
            exercise=new Exercise();
            workout=new Workout();                          
            workout.User = user;
            wop = new WorkoutPlan();
            wop.User = user;
            wop.Day = 0;
            GymService = new JewGymService.ServiceModelClient();
            this.window = window;
            
        }
        public void CloseEXhost()
        {
            exercise = exerciseUC.SelectedEx;
            if (exercise != null)
            {
                Clear();
                setRepEXUC = new SetRepEXUC(exercise, this);
                main.Height = 230;
                main.Width = 280;
                main.ShowDialog(setRepEXUC);
            }
        }

        private void AddEx_Click(object sender, RoutedEventArgs e)
        {
            exerciseUC = new ExerciseUC(this);
            main.Height = 800;
            main.Width = 480;
            main.IsOpen = false;
            main.ShowDialog(exerciseUC);
        }

        internal void AddEx(Exercise exercise, string sets, string reps)
        {
            main.Visibility = Visibility.Visible;
            main.IsOpen = true;
            if (sets == "" || reps == "")
            {
                MessageBox.Show("need to enter sets and reps");
                return;
            }
            ExerciseInWorkOut exInWo = new ExerciseInWorkOut { Exercise = exercise, Sets=int.Parse(sets), Reps=int.Parse(reps)};
            exInWoList.Add(exInWo);
            MiniExerciseUC uc = new MiniExerciseUC(exercise, exInWo, true, true,this);
            uc.MouseDoubleClick += RemoveUc_MouseDoubleClick;
            ExLB.Items.Add(uc);
            ExLB.ScrollIntoView(ExLB.Items[ExLB.Items.Count - 1]);
            countSets += int.Parse(sets);
            if (exercise.IsCompound)
            {
                countCompoundSets += int.Parse(sets);
            }
            else
            {
                countSets += int.Parse(sets);
            }
            Clear();
        }

        private void RemoveUc_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ExLB.Items.Remove(sender as MiniExerciseUC);
        }

        public void Clear()
        {
            main.Content = null;
            main.IsOpen = false;
        }

        private void finish_click(object sender, RoutedEventArgs e)
        {
            if (typeTB.Text.Length == 0)
            {
                MessageBox.Show("workout name required");
                return;
            }
            if (ExLB.HasItems)
            {
                workout.Type = typeTB.Text;
                workout.Duration = countSets * 2 + countCompoundSets * 4;
                workout.ExInWorkout = exInWoList;
                workout.ID=GymService.InsertWorkout(workout);
                wop.Workout = workout;
                GymService.InsertWorkoutPlan(wop);
                MessageBox.Show($"added {workout.Type} to your workouts");
                window.CreateProgramRefresh();
            }
            else
            {
                MessageBox.Show("must add exercises");
                return;
            }
        }
    }
}
