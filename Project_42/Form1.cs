using System;
using System.Windows.Forms;

namespace Project_42
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void task1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] message = { "Age: 34 years old", "Working hours: free work schedule, remote work, part-time employment", "Headings: IT, WEB specialists" };
            Task1.Show(message);
        }

        private void task2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task2.Secret();
        }

        private void task3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task3 task3 = new Task3();
            task3.ShowDialog();
        }

        private void task4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task4 task4 = new Task4();
            task4.ShowDialog();
        }

        private void task1ToolStripMenuItem_MouseMove(object sender, MouseEventArgs e)
        {
            textBox1.Text = "Task 1:  Вывести на экран свое(краткое!) резюме с помощью последовательности MessageBox(числом не менее трех).Причем на заголовке последнего должно отобразиться среднее число символов на странице(общее количество символов в резюме / количество MessageBox).";
        }

        private void task2ToolStripMenuItem_MouseMove(object sender, MouseEventArgs e)
        {
            textBox1.Text = "Task 2:  Написать функцию, которая «угадывает» задуманное пользователем число от 1 до 2000.Для запроса к пользователю использовать МеззавеВох. После того, как число отгадано, необходимо вывести количество запросов, потребовавшихся для этого, и предоставить пользователю возможность сыграть еще раз, не выходя из программы (MessageBox оформляются кнопками и значками соответственно ситуации).";
        }

        private void task3ToolStripMenuItem_MouseMove(object sender, MouseEventArgs e)
        {
            textBox1.Text = "Task 3:  Представьте, что у вас на форме есть прямоугольник, границы которого на 10 пикселей отстоят от границ рабочей области формы. Необходимо создать следующие обработчики: Обработчик нажатия левой кнопки мыши, который выводит сообщение о том, где находится текущая точка: внутри прямоугольника, снаружи, на границе прямоугольника.Если при нажатии левой кнопки мыши была нажата кнопка Control (Ctrl), то приложение должно закрываться; Обработчик нажатия правой кнопки мыши, который выводит в заголовок окна информацию о размере клиентской(рабочей) области окна в виде: Ширина = х, Высота у - соответствующие параметры вашего окна; Обработчик перемещения указателя мыши в пределах рабочей области, который должен выводить в заголовок окна текущие координаты мыши х и y.";
        }

        private void task4ToolStripMenuItem_MouseMove(object sender, MouseEventArgs e)
        {
            textBox1.Text = "Task 4:  Разработать приложение, созданное на основе форме.Пользователь «щелкает» левой кнопкой мыши по форме и, не отпуская кнопку, ведет по ней мышку, а в момент отпускания кнопки по полученным координатам прямоугольника (вам, конечно, известно, что двух точек на плоскости достаточно для создания прямоугольника) необходимо создать «статик», который содержит свой порядковый номер (имеется в виду порядок появления на форме) Минимальный размер «статика» составляет 10х10, при попытке создания элемента меньших размеров пользователь должен увидеть соответствующее предупреждение.При щелчке правой кнопкой мыши над поверхностью статика» в заголовке окна должна появиться информация о его площади и координатах (относительно формы). В случае если в точке щелчка находится несколько «статиков», то предпочтение отдается «статику» с порядковым номером.";
        }
    }
}
