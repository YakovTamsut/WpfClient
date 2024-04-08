﻿using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Expando;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfClient.JewGymService;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for WeeKPlanUC.xaml
    /// </summary>
    public partial class WeeKPlanUC : UserControl
    {
        private ServiceModelClient GymService;
        private WorkoutPlanList workPlans;
        //https://www.youtube.com/watch?v=THV5BW5WW_o&t=130s
        public WeeKPlanUC(User user)
        {
            InitializeComponent();
            GymService = new ServiceModelClient();
            workPlans = GymService.GetUserWorkoutPlans(user);
            bool[] empty = new bool[7];
            WorkoutsUC trash = new WorkoutsUC(true);
            trash.isTrash = true;
            trash.MouseMove += Uc_MouseMove;
            trash.MouseUp += Uc_MouseUp;
            trash.Drop += Uc_Drop;
            MoveToTrash.Children.Add(trash);
            foreach (WorkoutPlan wp in workPlans)
            {
                WorkoutsUC uc = new WorkoutsUC(wp, false); 
                uc.MouseMove += Uc_MouseMove;
                uc.MouseUp += Uc_MouseUp;
                uc.Drop += Uc_Drop;
                uc.Tag = wp;
                if (wp.Day != 0)
                {
                    //ExWP.Children.Add(uc);
                    DayUC duc = new DayUC(uc);

                    switch (wp.Day)
                    {
                        case 1:
                            SundayST.Children.Add(duc);
                            empty[0] = true;
                            break;
                        case 2:
                            MondayST.Children.Add(duc);
                            empty[1] = true;
                            break;
                        case 3:
                            TuesdayST.Children.Add(duc);
                            empty[2] = true;
                            break;
                        case 4:
                            WednesdayST.Children.Add(duc);
                            empty[3] = true;
                            break;
                        case 5:
                            ThursdayST.Children.Add(duc);
                            empty[4] = true;
                            break;
                        case 6:
                            FridayST.Children.Add(duc);
                            empty[5] = true;
                            break;
                        case 7:
                            SaturdayST.Children.Add(duc);
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
                            SundayST.Children.Add(duc);
                            empty[i] = true;
                            break;
                        case 1:
                            MondayST.Children.Add(duc);
                            empty[i] = true;
                            break;
                        case 2:
                            TuesdayST.Children.Add(duc);
                            empty[i] = true;
                            break;
                        case 3:
                            WednesdayST.Children.Add(duc);
                            empty[i] = true;
                            break;
                        case 4:
                            ThursdayST.Children.Add(duc);
                            empty[i] = true;
                            break;
                        case 5:
                            FridayST.Children.Add(duc);
                            empty[i] = true;
                            break;
                        case 6:
                            SaturdayST.Children.Add(duc);
                            empty[i] = true;
                            break;
                        default:
                            break;

                    }
                }

            }
        }

        private void Uc_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WorkoutsUC uc = sender as WorkoutsUC;
            WorkoutPlan plan = uc.Tag as WorkoutPlan;
            if (plan.Day == 0)
            {
                uc.SetIsUp(!uc.GetIsUp());
                uc.AnimateSize();
                ListBoxItem item = (ListBoxItem)ExLB.ItemContainerGenerator.ContainerFromItem(uc);

                if (item != null)
                {
                    // Get the user control from the ListBoxItem's content
                    WorkoutsUC usercontrol = item.Content as WorkoutsUC;

                    if (usercontrol != null)
                    {
                        // Modify the properties of the user control
                        // For example, if your UserControl has a property called Text, you can do:
                        AnimateItemSize(usercontrol, uc.GetIsUp());
                    }
                }
            }
        }
        internal void AnimateItemSize(WorkoutsUC uc, bool expand)
        {
            double targetWidth = expand ? 340 : 300;
            double targetHeight = expand ? 440 : 150;
            uc.HorizontalAlignment = HorizontalAlignment.Center;
            // Create the animation for width
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                To = targetWidth,
                Duration = TimeSpan.FromSeconds(0.2),
                FillBehavior = FillBehavior.HoldEnd
            };

            // Create the animation for height
            DoubleAnimation heightAnimation = new DoubleAnimation
            {
                To = targetHeight,
                Duration = TimeSpan.FromSeconds(0.2),
                FillBehavior = FillBehavior.HoldEnd
            };

            // Apply the animations to the stackPanel's width and height properties
            uc.BeginAnimation(WidthProperty, widthAnimation);
            uc.BeginAnimation(HeightProperty, heightAnimation);
        }

        private void Uc_Drop(object sender, DragEventArgs e)
        {
            WorkoutsUC drag = e.Data.GetData("Object") as WorkoutsUC;
            WorkoutsUC drop = sender as WorkoutsUC;
            WorkoutPlan planDrag = drag.Tag as WorkoutPlan;
            WorkoutPlan planDrop = drop.Tag as WorkoutPlan;

            //Error?
            if (planDrop!=null && planDrag!=null && planDrop.Day == planDrag.Day) return;
            //Delete workout?
            if (drop.isTrash)
            {
                if (planDrag.Workout != null)
                {
                    MessageBoxResult remove = MessageBox.Show($"Do you want to delete {planDrag.Workout.Type}? permanently", "🏋️‍", MessageBoxButton.YesNo);
                    if (remove == MessageBoxResult.Yes)
                    {
                        GymService.DeleteWorkout(planDrag.Workout);
                        workPlans.RemoveAll(w => w.Workout.ID == planDrag.ID);
                        if (drag.Parent == ExLB)
                        {
                            ExLB.Items.Remove(drag);
                        }
                        drag.ChangePlan(planDrop, true);
                    }
                }
                return;
            }
            else
            {
                MessageBoxResult answer;
                string from = planDrag.Workout != null ? planDrag.Workout.Type : "rest day";
                string to = planDrop.Workout != null ? planDrop.Workout.Type : "rest day";
                if (planDrop.Workout == null)
                {
                    answer = MessageBox.Show($"Do you want to place {from} in {planDrop.Day}?", "🏋️‍", MessageBoxButton.YesNo);
                }
                else
                {
                    answer = MessageBox.Show($"Do you want to change {from} with {to}?", "🏋️‍", MessageBoxButton.YesNo);
                }

                if (answer == MessageBoxResult.Yes)
                {
                    //from side view?
                    bool sideDrag = planDrag.Day == 0;
                    //switch days
                    int num = planDrag.Day;
                    planDrag.Day = planDrop.Day;
                    planDrop.Day = num;

                    drop.ChangePlan(planDrag, true);
                    drag.ChangePlan(planDrop, sideDrag);

                    //update change in service
                    if (planDrag.Workout != null)
                        GymService.UpdateWorkoutPlan(planDrag);
                    if (planDrop.Workout != null)
                        GymService.UpdateWorkoutPlan(planDrop);
                }
            }

            
        }

        private void Uc_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            WorkoutsUC uc=sender as WorkoutsUC;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Package the data.
                DataObject data = new DataObject();
                data.SetData("Object", uc);

                // Initiate the drag-and-drop operation.
                DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
            }
        }

        private void ExLB_Drop(object sender, DragEventArgs e)
        {
            WorkoutsUC uc = sender as WorkoutsUC;
            if(uc==null) return;
            WorkoutPlan plan = uc.Tag as WorkoutPlan;
            if (plan.Day == 0)
            {
                //ShowWorkout.IsOpen = true;
                //WorkoutsUC info = new WorkoutsUC(plan, true);
                //info.Width = info.Height = 150;
                //ShowWorkout.ShowDialog(info);
            }
        }
    }
}
