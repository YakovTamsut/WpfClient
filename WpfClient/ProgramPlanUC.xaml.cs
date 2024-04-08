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
    /// Interaction logic for ProgramPlanUC.xaml
    /// </summary>
    
    public partial class ProgramPlanUC : UserControl
    {

        private ServiceModelClient GymService;
        public ProgramPlanUC(User user)
        {
            InitializeComponent();
            foreach (WorkoutPlan wp in GymService.GetUserWorkoutPlans(user))
            {
                if (wp.Day != 0)
                {
                }
                //uc.MouseMove += Uc_MouseMove;
                //uc.MouseUp += Uc_MouseUp;
                //uc.Drop += Uc_Drop;
                //uc.Tag = wp;
                if (wp.Day != 0)
                {
                    //ExWP.Children.Add(uc);
                    DayUC duc = new DayUC(uc);
                    EditWorkoutUC uc = new EditWorkoutUC(user, wp);

                    switch (wp.Day)
                    {
                        case 1:
                            SundaySP.Children.Add(duc);
                            empty[0] = true;
                            break;
                        case 2:
                            MondaySP.Children.Add(duc);
                            empty[1] = true;
                            break;
                        case 3:
                            TuesdaySP.Children.Add(duc);
                            empty[2] = true;
                            break;
                        case 4:
                            WednesdaySP.Children.Add(duc);
                            empty[3] = true;
                            break;
                        case 5:
                            ThursdaySP.Children.Add(duc);
                            empty[4] = true;
                            break;
                        case 6:
                            FridaySP.Children.Add(duc);
                            empty[5] = true;
                            break;
                        case 7:
                            SaturdaySP.Children.Add(duc);
                            empty[6] = true;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    ExLB.Items.Add(uc);
                }

            }

            for (int i = 0; i < empty.Length; i++)
            {
                WorkoutPlan wop = new WorkoutPlan();
                WorkoutsUC uc = new WorkoutsUC(false);
                wop.Day = i + 1;
                uc.workPlan = wop;
                uc.Tag = wop;
                if (empty[i] == false)
                {
                    uc.MouseMove += Uc_MouseMove;
                    uc.MouseUp += Uc_MouseUp;
                    uc.Drop += Uc_Drop;
                    DayUC duc = new DayUC(uc);
                    switch (i)
                    {
                        case 0:
                            SundaySP.Children.Add(duc);
                            empty[i] = true;
                            break;
                        case 1:
                            MondaySP.Children.Add(duc);
                            empty[i] = true;
                            break;
                        case 2:
                            TuesdaySP.Children.Add(duc);
                            empty[i] = true;
                            break;
                        case 3:
                            WednesdaySP.Children.Add(duc);
                            empty[i] = true;
                            break;
                        case 4:
                            ThursdaySP.Children.Add(duc);
                            empty[i] = true;
                            break;
                        case 5:
                            FridaySP.Children.Add(duc);
                            empty[i] = true;
                            break;
                        case 6:
                            SaturdaySP.Children.Add(duc);
                            empty[i] = true;
                            break;
                        default:
                            break;

                    }
                }
            }
    }
}
