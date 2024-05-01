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
using System.Windows.Shapes;
using WpfClient.JewGymService;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private User currentUser;
        private WorkoutPlanList workPlans;
        private ServiceModelClient GymService;

        public UserWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            GymService = new ServiceModelClient();
            LoadTodayWorkout();
        }

        private void ButtonCloseApp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateProgram_Selected(object sender, RoutedEventArgs e)
        {
            Clear_Grid();
            MainView.Children.Add(new CreateWorkoutUC(currentUser, this));            
        }
        public void CreateProgramRefresh()
        {
            Clear_Grid();
            MainView.Children.Add(new CreateWorkoutUC(currentUser, this));
        }

        private void Clear_Grid()
        {
            MainView.Children.Clear();
            ProgramView.Children.Clear();
        }

        private void MyWorkout_Selected(object sender, RoutedEventArgs e)
        {
            Clear_Grid();
            MainView.Children.Add(new WeeKPlanUC(currentUser));
        }

        private void LoadTodayWorkout()
        {
            Clear_Grid();
            MainView.Children.Add(new WorkoutsUC(GymService.GetTodayWorkoutPlan(currentUser), true));
        }

        private void TodayWorkout_Selected(object sender, RoutedEventArgs e)
        {
            LoadTodayWorkout();
        }

        private void RecommendedWorkour_Selected(object sender, RoutedEventArgs e)
        {
            Clear_Grid();
            UserList users = GymService.GetAllPlanAdmins();
            foreach (User u in users)
            {
                EditWorkoutUC editWorkoutUC = new EditWorkoutUC(u);
                editWorkoutUC.MouseUp += ShowPlan;
                ProgramView.Children.Add(editWorkoutUC);
            }
        }

        private void ShowPlan(object sender, RoutedEventArgs e)
        {

        }
    }
}
