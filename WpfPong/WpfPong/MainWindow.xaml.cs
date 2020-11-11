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
        private ViewModel view = new ViewModel();
        private DispatcherTimer timer;
        private double dx = 1;
        private double dy = 1;
        private int PadSpeed = 16;
        public MainWindow()
        {
            InitializeComponent();
            SetupTimer();
            DataContext = view;
        }
        private void SetupTimer()
        {
            this.timer = new DispatcherTimer();
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            this.timer.Tick += TimerTick;
            this.timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            double gameBallTop = Canvas.GetTop(ball);
            double gameBallLeft = Canvas.GetLeft(ball);

            if (gameBallTop <= 0)
                dy *= -1;
            if (gameBallTop + ball.Height >= myCanvas.Height)
                dy *= -1;

            Canvas.SetTop(ball, gameBallTop + dy);
            Canvas.SetLeft(ball, gameBallLeft + dx);

            if (gameBallLeft >= myCanvas.ActualWidth - 10)
            {
                view.LeftResult += 1;
                GameResetBallPosition();
            }
            if (gameBallLeft <= -10)
            {
                view.RightResult += 1;
                GameResetBallPosition();
            }
            if(gameBallLeft >= 760 && (gameBallTop > view.RightPadPosition - 20 && gameBallTop < view.RightPadPosition + 80))
            {
                dx *= -1.4;
                Canvas.SetTop(ball, gameBallTop + dy);
                Canvas.SetLeft(ball, gameBallLeft + dx);
            }

            if(gameBallLeft <= 20 && (gameBallTop > view.LeftPadPosition - 20 && gameBallTop < view.LeftPadPosition + 80))
            {
                dx *= -1.4;
                Canvas.SetTop(ball, gameBallTop + dy);
                Canvas.SetLeft(ball, gameBallLeft + dx);
            }
        }

        private void GameResetBallPosition()
        {
            Canvas.SetTop(ball, 210);
            Canvas.SetLeft(ball, 380);
            dx = 1;
            dy = 1;
        }
        
        private void MainWindowOnKeyDown(object sender, KeyboardEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.W)) view.LeftPadPosition = VerifyBounds(view.LeftPadPosition, -PadSpeed);
            if (Keyboard.IsKeyDown(Key.S)) view.LeftPadPosition = VerifyBounds(view.LeftPadPosition, PadSpeed);

            if (Keyboard.IsKeyDown(Key.Up)) view.RightPadPosition = VerifyBounds(view.RightPadPosition, -PadSpeed);
            if (Keyboard.IsKeyDown(Key.Down)) view.RightPadPosition = VerifyBounds(view.RightPadPosition, PadSpeed);
        }

        private int VerifyBounds(int position, int change)
        {
            position += change;

            if (position < 0)
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
