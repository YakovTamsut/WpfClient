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
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        private ServiceModelClient GymService;
        private UserList planlist;
        private User curentUser;
        public ManagerWindow(User user)
        {
            InitializeComponent();
            GymService = new ServiceModelClient();
            planlist = GymService.GetAllPlanAdmins();
            curentUser = user;
            foreach (User u in planlist) 
            { 

            }
        }

        private void CreateProgram_Selected(object sender, RoutedEventArgs e)
        {
            ProgramPlanUC program = new ProgramPlanUC(curentUser);
            MainView.Children.Add(program);

        }

        private void CreateProgramPlans_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonCloseApp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
