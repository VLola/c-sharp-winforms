using System;
using System.Windows.Forms;

namespace Project_42
{
    class Task2
    {
        public static void Secret()
        {
            for (int i = 1; ; i++)
            {
                var answer = MessageBox.Show($"Conceived number: {RandomNumber()} ?", "Game the secret number", MessageBoxButtons.YesNo);
                if (answer == DialogResult.Yes)
                {
                    var newGame = MessageBox.Show($"Number of attempts: {i}. Play again?", "Game the secret number", MessageBoxButtons.YesNo);
                    if (newGame == DialogResult.No) break;
                    else i = 0;
                }
            }
        }
        public static int RandomNumber() { return new Random().Next(1, 2001); }
    }
}
