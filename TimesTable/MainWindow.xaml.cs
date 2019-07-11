using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TimesTable.Properties;

namespace TimesTable
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly Ellipse[] Positions;
        private readonly Line[] Lines;

        public MainWindow()
        {
            InitializeComponent();
            MyCanvas.Children.Clear();

            Lines = new Line[200];
            for (int i = 0; i < 200; i++)
            {
                Lines[i] = new Line
                {
                    Stroke = Brushes.DodgerBlue,
                    StrokeThickness = 1,
                };
                MyCanvas.Children.Add(Lines[i]);
            }

            MyCanvas.Children.Add(Circle);

            Positions = new Ellipse[200];
            for (int i = 0; i < 200; i++)
            {
                Positions[i] = new Ellipse
                {
                    Fill = Brushes.DarkRed,
                    Width = 6,
                    Height = 6,
                };
                MyCanvas.Children.Add(Positions[i]);
            }

            Slider.Focus();
        }

        public double Number { get; set; }

        // ReSharper disable once UnusedMember.Global
        public void OnNumberChanged(object oldValue, object newValue)
        {
            Redraw();
        }

        private double CenterX;
        private double CenterY;
        private double RadiusX;
        private double RadiusY;


        private void Circle_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            CenterX = (Circle.ActualWidth - 6.0) / 2.0;
            CenterY = (Circle.ActualHeight - 6.0) / 2.0;
            RadiusX = CenterX + 1.5;
            RadiusY = CenterY + 1.5;
            for (int i = 0; i < 200; ++i)
            {
                double angle = Math.PI - i * (2 * Math.PI / 200);
                Canvas.SetLeft(Positions[i], CenterX + RadiusX * Math.Cos(angle));
                Canvas.SetTop(Positions[i], CenterY + RadiusY * Math.Sin(angle));
            }
            Redraw();
        }

        private void Redraw()
        {
            for (int i = 1; i < 200; i++)
            {
                double j = (i * Number) % 200;
                double angle = Math.PI - j * (2 * Math.PI / 200);
                Lines[i].X1 = Canvas.GetLeft(Positions[i]) + 3;
                Lines[i].Y1 = Canvas.GetTop(Positions[i]) + 3;
                Lines[i].X2 = CenterX + RadiusX * Math.Cos(angle) + 3;
                Lines[i].Y2 = CenterY + RadiusY * Math.Sin(angle) + 3;
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
