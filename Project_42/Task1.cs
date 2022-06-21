using System.Collections.Generic;
using System.Windows.Forms;

namespace Project_42
{
    class Task1
    {
        public static void Show(string[] message)
        {
            int size = message.Length;
            int count_sumbols = 0;
            foreach(string it in message) count_sumbols += it.Length;
            for (int count = 0; count < size; count++)
            {
                if (count != size - 1) MessageBox.Show(message[count]);
                else MessageBox.Show(message[count], "Average number of characters: " + (count_sumbols / size).ToString());
            }

        }
    }
}
