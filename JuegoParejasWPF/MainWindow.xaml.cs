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

namespace JuegoParejasWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random random = new Random();

        private List<String> icons = new List<string>()
            {
                "!", "!", "N", "N", ",", ",", "k", "k",
                "b", "b", "v", "v", "w", "w", "z", "z"
            };
        private List<Label> labelList;
        private System.Windows.Threading.DispatcherTimer timer;

        private Label firstClicked = null;
        private Label secondClicked = null;
        private Run runClickedOne = null;
        private Run runSecondCLicked = null;
        public MainWindow()
        {
            InitializeComponent();
            timer = new System.Windows.Threading.DispatcherTimer();
            runClickedOne = new Run();
            runSecondCLicked = new Run();
            addLabelsToList();
            assignIconsToSquares();
        }

        private void addLabelsToList()
        {
            labelList = new List<Label>();
            labelList.Add(label1);
            labelList.Add(label2);
            labelList.Add(label3);
            labelList.Add(label4);
            labelList.Add(label5);
            labelList.Add(label6);
            labelList.Add(label7);
            labelList.Add(label8);
            labelList.Add(label9);
            labelList.Add(label11);
            labelList.Add(label12);
            labelList.Add(label13);
            labelList.Add(label14);
            labelList.Add(label15);
            labelList.Add(label16);
            labelList.Add(label0);
        }

        /// <summary>
        /// Assign each icon from the list of icons to a random square
        /// </summary>
        private void assignIconsToSquares()
        {
            
            
            foreach (Label control in labelList)
            {
                 
                if (control != null)
                {
                    Run run = new Run();
                    int randomNumber = random.Next(icons.Count);
                    run.Text = icons[randomNumber];
                    control.Content = run;
                    run.Foreground = Brushes.CornflowerBlue;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        private void label_Click(object sender, EventArgs e)
        {
            Label clickedLabel = (Label)sender;

            if (clickedLabel != null)
            {
                Run run = (Run) clickedLabel.Content;
                if (run.Foreground == Brushes.Black)
                    return;
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    runClickedOne = (Run)clickedLabel.Content;
                    runClickedOne.Foreground= Brushes.Black;

                    return;
                }



                if(secondClicked == null)
                {
                    secondClicked = clickedLabel;
                    runSecondCLicked = (Run)secondClicked.Content;
                    runSecondCLicked.Foreground = Brushes.Black;
                }

                checkForWinner();

                if (runClickedOne.Text == runSecondCLicked.Text)
                {
                    firstClicked = null;
                    runClickedOne = null;
                    runSecondCLicked = null;
                    secondClicked = null;
                    return;
                }

                
                timer.Interval = TimeSpan.FromMilliseconds(500);
                timer.Tick += Timer1_Tick;
                timer.Start();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            if(firstClicked != null && secondClicked != null)
            {
                Run run1 = (Run)firstClicked.Content;
                Run run2 = (Run)secondClicked.Content;
                run1.Foreground = Brushes.CornflowerBlue;
                run2.Foreground = Brushes.CornflowerBlue;
            }

            firstClicked = null;
            secondClicked = null;
        }

        private void checkForWinner()
        {
            int count = 0;
            // Go through all of the labels in the TableLayoutPanel, 
            // checking each one to see if its icon is matched
            foreach (Label control in labelList)
            {
                if (control != null)
                {
                    Run runWinner = (Run)control.Content;


                    if (runWinner.Foreground == Brushes.Black)
                    {
                        count++;
                    }
                }
            }

            // If the loop didn’t return, it didn't find
            // any unmatched icons
            // That means the user won. Show a message and close the form
            if(count == 16)
            {
                System.Windows.MessageBox.Show("¡¡Felicidades!!! ¡¡¡Has descubierto todos los íconos!!!", "¡Has ganado!");
                Close();
            }
        }


    }
}
