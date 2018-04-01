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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        public ResultWindow()
        {
            InitializeComponent();
        }














        List<double> ArriveTime = ((MainWindow)Application.
                Current.MainWindow).GetArriveTimeList();
        List<double> BurstTime = ((MainWindow)Application.
            Current.MainWindow).GetBurstTimeList();
        int NumberProcess = ((MainWindow)Application.
            Current.MainWindow).GetProcessNumber();
        List<double> Priority = ((MainWindow)Application.
        Current.MainWindow).GetPriorityList();

        int ScheduleTypeIndex = ((MainWindow)Application.
        Current.MainWindow).GetSchedulingTypeIndex();

        int timeQuantum = ((MainWindow)Application.
        Current.MainWindow).GetTimeQuntum();

        List <Process> processes = new List<Process>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            double total = BurstTime.Sum();
            for (var i = 0; i < NumberProcess; i++)
            {
                Process p = new Process(i + 1, ArriveTime[i], BurstTime[i]);
                processes.Add(p);
            }

            // Create the Grid
            Grid DynamicGrid = new Grid();

            DynamicGrid.Width = 1700;
            DynamicGrid.Height = 1000;

            //DynamicGrid.HorizontalAlignment = HorizontalAlignment.Left;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Top;

            DynamicGrid.ShowGridLines = true;
            Border border = new Border();
            border.BorderThickness = new Thickness(1);
            border.BorderBrush = new SolidColorBrush(Colors.Black);

            DynamicGrid.Background = new SolidColorBrush(Colors.Black);
            DynamicGrid.Children.Add(border);


            // Create Columns
            for (int i = 0; i <= ArriveTime[0] + total; i++)
            {
                string x = i.ToString();
                ColumnDefinition gridColi = new ColumnDefinition();
                gridColi.Width = new GridLength(30);

                DynamicGrid.ColumnDefinitions.Add(gridColi);
                //MessageBox.Show(x);
            }
            // Create Rows :
            RowDefinition gridRow1 = new RowDefinition();

            gridRow1.Height = new GridLength(15);

            RowDefinition gridRow2 = new RowDefinition();

            gridRow2.Height = new GridLength(45);

            DynamicGrid.RowDefinitions.Add(gridRow1);
            DynamicGrid.RowDefinitions.Add(gridRow2);

            for (int n = 1; n <= ArriveTime[0] + total; n++)
            {
                TextBlock txtBlockx = new TextBlock();
                txtBlockx.Text = n.ToString();
                txtBlockx.FontSize = 12;

                // txtBlockx.FontWeight = FontWeights.Bold;

                txtBlockx.Foreground = new SolidColorBrush(Colors.Red);

                txtBlockx.VerticalAlignment = VerticalAlignment.Top;
                // txtBlockc.HorizontalAlignment = HorizontalAlignment.Left;

                Grid.SetColumn(txtBlockx, n);
                Grid.SetRow(txtBlockx, 0);
                DynamicGrid.Children.Add(txtBlockx);


            }

/*
            // Add first column header
            List<string> processi = new List<string>();

            for (int t = 1; t <= NumberProcess; t++)
            {
                string z = t.ToString();
                processi.Add("P" + z);

            }

            */
            if (ScheduleTypeIndex == 0)
            {
                List<int> ProcessIDs = FCFS(ref processes);
                List<string> processi = new List<string>();
                for (int q = 0, i = 0, n = ProcessIDs.Count; q < total && i < n; q++, i++)
                {
                    processi.Add("P" + ProcessIDs[i].ToString());
                    int m = (int)ArriveTime[0] + q;
                    TextBlock txtBlockc = new TextBlock();
                    txtBlockc.Text = processi[q];
                    Grid.SetColumn(txtBlockc, m);
                    Grid.SetRow(txtBlockc, 1);
                    txtBlockc.FontSize = 14;
                    txtBlockc.FontWeight = FontWeights.Bold;
                    txtBlockc.Foreground = new SolidColorBrush(Colors.White);
                    txtBlockc.VerticalAlignment = VerticalAlignment.Top;
                    DynamicGrid.Children.Add(txtBlockc);
                }
            }

            if (ScheduleTypeIndex == 5)
            {

            }
            rootWindow.Content = DynamicGrid;
        } 










        private List<int> FCFS(ref List<Process> FCFS_Processes)
        {
            List<int> processIDinTime = new List<int>();
           
            for (var i = 0; i < NumberProcess; i++)
            {
                Process to_serve = FindNextProcess_ArriveTime(FCFS_Processes, NumberProcess);
                to_serve.MarkAssigned();

                for (int j = (int)to_serve.GetArriveTime(), n = (int)(to_serve.GetBurstTime() + 0.5) + (int)to_serve.GetArriveTime(); j < n; j++)
                    processIDinTime.Add(to_serve.GetID());

                to_serve.MarkFinished();
            }

            return processIDinTime;
        }

        private Process FindNextProcess_ArriveTime(List<Process> ProcessList,
            int numberProcesses)
        {
            Process current = ProcessList[0]; // first element
            for (var i = 1; i < numberProcesses; i++)
            {
                if (!current.IsAssigned() && !current.IsFinished())
                {
                    if (!ProcessList[i].IsAssigned() && !ProcessList[i].IsFinished())
                    {
                        if (current.CompareArriveTime(ProcessList[i]) == -1) /*means that current 
                        arrived before next*/
                        { }

                        else if (current.CompareArriveTime(ProcessList[i]) == -1) /* they both arrived
                        at the same time*/
                        {
                            // we need to compare burst time of each
                            if (current.CompareBurstTime(ProcessList[i]) == -1) /* means that current 
                            has burst time less than next*/
                            { }

                            else /* in case they both have the same burst time or if current 
                            has burst time larger than next*/
                            {
                                current = ProcessList[i];
                            }

                        }
                        else // current arrived after next
                        {
                            current = ProcessList[i];
                        }
                    }
                }
                else
                    current = ProcessList[i];
            }


            return current;
        }
        //Round Robin 
        private List<int> FCFS(ref List<Process> FCFS_Processes)
        {
            int burst_new = ((int)(to_serve.GetBurstTime() + 0.5));
            List<int> processIDinTime = new List<int>();

            for (var i = 0; i < NumberProcess; i++)
            {

                Process to_serve = FindNextProcess_ArriveTime(FCFS_Processes, NumberProcess);
                to_serve.MarkAssigned();

                if (burst_new > 4) {
                    for (int j = 0, n = 4 ; j < n; j++) { 

                    processIDinTime.Add(to_serve.GetID());

                    to_serve.MarkFinished();
                }
                burst_new = ((int)(to_serve.GetBurstTime() + 0.5)) - 4;
            }
                else
                {
                    for (int j = 0, n = burst_new; j < n; j++)
                    {

                        processIDinTime.Add(to_serve.GetID());

                        to_serve.MarkFinished();
                    }
            }




    }
}
