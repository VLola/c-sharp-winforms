using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_42
{
    public partial class Task4 : Form
    {
        public Task4()
        {
            InitializeComponent();
        }
        public Coordinates A { get; set; }
        public Coordinates B { get; set; }

        // Внося в список координаты прямоугольника, отпадает нужда в порядковом номере прямоугольника,
        // так как в заголовке окна будет выводиться информация о последнем прямоугольнике который был внесён в список
        // (при условии что под курсором несколько прямоугольников)
        // Економия ресурсов на поиски одинаковых прямоугольников и сравнение их по Id

        public List<NewRectangle> list = new List<NewRectangle>();
        public struct NewRectangle
        {
            public NewRectangle(Coordinates A, Coordinates B)
            {
                this.A = A;
                this.B = B;
                minX = Math.Min(A.X, B.X);
                maxX = Math.Max(A.X, B.X);
                minY = Math.Min(A.Y, B.Y);
                maxY = Math.Max(A.Y, B.Y);
                S = (maxX - minX) * (maxY - minY);
            }

            public int minX { get; }
            public int maxX { get; }
            public int minY { get; }
            public int maxY { get; }
            public int S { get; }
            public Coordinates A { get; }
            public Coordinates B { get; }
        }
        public struct Coordinates
        {
            public Coordinates(int x, int y)
            {
                X = x;
                Y = y;
            }
            public int X { get; }
            public int Y { get; }
        }

        private void Task4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                A = new Coordinates(e.X, e.Y);
            }
        }

        private void Task4_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Y < Size.Height && e.X < Size.Width)
            {
                B = new Coordinates(e.X, e.Y);
                NewRectangle rectangle = new NewRectangle(A, B);
                if (rectangle.maxX - rectangle.minX < 10 || rectangle.maxY - rectangle.minY < 10) MessageBox.Show("Small rectangle!");
                else list.Add(rectangle);
            }
        }

        private void Task4_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                foreach (var it in list)
                {
                    if (it.minX < e.X && it.maxX > e.X && it.minY < e.Y && it.maxY > e.Y)
                    {
                        Text = $"S = {it.S} , coordinates - A({it.A.X},{it.A.Y}) , B({it.B.X},{it.B.Y})";
                    }
                }
            }
        }
    }
}
