//Chase Christiansen
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
using System.Windows.Threading;

namespace WpfPong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel view = new ViewModel();       //initializing ViewModel object that controls the changing elements in the window.
        private DispatcherTimer timer;
        private double dx = 1;                          //double variables dx and dy serve as the velocity values on the x-axis and y-axis respectively.
        private double dy = 1;
        private int PadSpeed = 16;                      //PadSpeed serves as the value the paddles for each player move in response to input on the keyboard.
        public MainWindow()
        {
            InitializeComponent();
            SetupTimer();
            DataContext = view;
        }
        private void SetupTimer()
        {
            this.timer = new DispatcherTimer();                     //creates a DispatcherTimer that updates in intervals of 10 milliseconds.
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            this.timer.Tick += TimerTick;
            this.timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            double gameBallTop = Canvas.GetTop(ball);               //the timer is used to check the location of the ball object every tick and
            double gameBallLeft = Canvas.GetLeft(ball);             //see if any of the following conditionals statements occur.

            if (gameBallTop <= 0)                                   //this method contains all of the balls collision and acceleration physics.
                dy *= -1;
            if (gameBallTop + ball.Height >= myCanvas.Height)
                dy *= -1;

            Canvas.SetTop(ball, gameBallTop + dy);
            Canvas.SetLeft(ball, gameBallLeft + dx);

            if (gameBallLeft >= myCanvas.ActualWidth - 10)          //if the ball goes past the right paddle horizontally (therefore out of bounds).
            {
                view.LeftResult += 1;                               //add 1 to left player score.
                GameResetBallPosition();                            //reset the position of the ball to the center.
            }
            if (gameBallLeft <= -10)                                //if the ball goes past the left paddle horizontally it is out of bounds.
            {                                                       
                view.RightResult += 1;                              //add 1 to right player score.
                GameResetBallPosition();                            //reset the position of the ball to the center.
            }
            if(gameBallLeft >= 760 && (gameBallTop > view.RightPadPosition - 20 && gameBallTop < view.RightPadPosition + 80))
            {
                dx *= -1.4;
                Canvas.SetTop(ball, gameBallTop + dy);              //accelerates the ball by 1.4x current velocity the other direction when contact is made with the paddle.
                Canvas.SetLeft(ball, gameBallLeft + dx);
            }

            if(gameBallLeft <= 20 && (gameBallTop > view.LeftPadPosition - 20 && gameBallTop < view.LeftPadPosition + 80))
            {
                dx *= -1.4;
                Canvas.SetTop(ball, gameBallTop + dy);              //accelerates the ball by 1.4x current velocity the other direction when contact is made with the paddle.
                Canvas.SetLeft(ball, gameBallLeft + dx);
            }
        }

        private void GameResetBallPosition()
        {
            Canvas.SetTop(ball, 210);                               //sets the ball object position to the center of the screen and sets the velocity to 1.
            Canvas.SetLeft(ball, 380);
            dx = 1;
            dy = 1;
        }
        
        private void MainWindowOnKeyDown(object sender, KeyboardEventArgs e)        //handles the controls that result in corresponding method calls.
        {
            if (Keyboard.IsKeyDown(Key.W)) view.LeftPadPosition = VerifyBounds(view.LeftPadPosition, -PadSpeed);        //player 1 controls. 
            if (Keyboard.IsKeyDown(Key.S)) view.LeftPadPosition = VerifyBounds(view.LeftPadPosition, PadSpeed);         //W moves paddle up. S moves paddle down.

            if (Keyboard.IsKeyDown(Key.Up)) view.RightPadPosition = VerifyBounds(view.RightPadPosition, -PadSpeed);     //player 2 controls.
            if (Keyboard.IsKeyDown(Key.Down)) view.RightPadPosition = VerifyBounds(view.RightPadPosition, PadSpeed);    //Up arrow moves paddle up. Down arrow moves down.
        }

        private int VerifyBounds(int position, int change)
        {
            position += change;

            if (position < 0)                                       //maintains the position of the paddles so they stay in bounds of the screen.
                position = 0;
            if (position > (int) myCanvas.ActualHeight - 90)
                position = (int) myCanvas.ActualHeight - 90;

            return position;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
