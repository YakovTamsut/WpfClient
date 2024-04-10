using System;
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
    /// Interaction logic for MiniExerciseUC.xaml
    /// </summary>
    public partial class MiniExerciseUC : UserControl
    {
        public Exercise currentEx;
        private CreateWorkoutUC parent = null;
        public MiniExerciseUC(JewGymService.Exercise exercise)
        {
            InitializeComponent();
            currentEx = exercise;
            this.DataContext = exercise;
            tbInfo.Text = exercise.TargetMuscle;
            exNameTB.Text = exercise.ExerciseName.Replace('-', ' ');

            try
            {
                img.Source = new BitmapImage(new Uri(exercise.ExerciseUrl));
            }
            catch (Exception)
            {
                img.Source = new BitmapImage(new Uri($"pack://application:,,,/Sources/Logo.png"));
            }
        }

        public MiniExerciseUC(JewGymService.Exercise exercise, ExerciseInWorkOut exinwo, bool isActive, bool isCreate,CreateWorkoutUC createWorkoutUC)
        {
            InitializeComponent();
            parent = createWorkoutUC;
            this.DataContext = exercise;
            tbInfo.Text = exercise.TargetMuscle;
            setsTB.Text = " " + exinwo.Sets + "x" + exinwo.Reps;
            exNameTB.Text = exercise.ExerciseName.Replace('-', ' ');
            if (!isCreate)
            {
                if (!isActive)
                {
                    tbInfo.FontSize = 12;
                    setsTB.FontSize = 12;
                    setsTB.FontSize = 12;
                    exNameTB.FontSize = 12;
                    img.Height = 40;
                    img.Width = 40;
                }
                else
                {
                    tbInfo.FontSize = 20;
                    setsTB.FontSize = 20;
                    setsTB.FontSize = 20;
                    exNameTB.FontSize = 20;
                    img.Height = 60;
                    img.Width = 60;
                }
            }
           
            try
            {
                img.Source = new BitmapImage(new Uri(exercise.ExerciseUrl));
            }
            catch (Exception)
            {
                img.Source = new BitmapImage(new Uri($"pack://application:,,,/Sources/Logo.png"));
            }
        }
    }
}
