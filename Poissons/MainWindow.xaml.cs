
using Fishs.Agent;
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

namespace Fishs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Ocean ocean = null;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            OceanCanvas.MouseDown += OceanCanvas_MouseDown;
            ocean = new Ocean(250, OceanCanvas.ActualWidth, OceanCanvas.ActualHeight);
            ocean.OceanUpdatedEvent += Ocean_OceanUpdatedEvent;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0,0,15);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ocean.UpdateOcean();
        }

        private void Ocean_OceanUpdatedEvent(Agent.Fish[] fishs, List<Agent.Enemy> enemies)
        {
            OceanCanvas.Children.Clear();
            foreach (var f in fishs)
            {
                DrawFish(f);
            }

            enemies.ForEach(e => DrawEnemny(e));

            OceanCanvas.UpdateLayout();
        }

        private void OceanCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ocean.AddEnemmy(new Point( e.GetPosition(OceanCanvas).X, e.GetPosition(OceanCanvas).Y), 10);
        }

        private void DrawFish(Fish fish)
        {
            Line lineBody = new Line();
            lineBody.Stroke = Brushes.Black;
            lineBody.X1 = fish.Pos.X;
            lineBody.Y1 = fish.Pos.Y;
            lineBody.X2 = fish.Pos.X - 10 * fish.Speed.X;
            lineBody.Y2 = fish.Pos.Y - 10 * fish.Speed.Y;
            OceanCanvas.Children.Add(lineBody);
        }

        private void DrawEnemny(Enemy enemy)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Stroke = Brushes.Red;
            ellipse.Width = enemy.Radius * 2;
            ellipse.Height = enemy.Radius * 2;
            ellipse.Margin = new Thickness(enemy.Pos.X - enemy.Radius, enemy.Pos.Y - enemy.Radius,0,0);
            OceanCanvas.Children.Add(ellipse);
        }

    }
}
