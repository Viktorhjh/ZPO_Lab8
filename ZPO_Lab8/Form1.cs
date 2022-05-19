using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZPO_Lab8
{
    public partial class Form1 : Form
    {
        int a, b, amount;
        List<Thread> threads = new List<Thread>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = 0;
            a = Int32.Parse(textBox1.Text);
            b = Int32.Parse(textBox2.Text);    
            amount = Int32.Parse(textBox3.Text);
            Parallel.Invoke(() =>
            {
                for (int i = 0; i < amount; i++)
                {
                    count += Task.Run(() =>
                    {
                        return diapason(a, b);
                    }).Result;

                    label1.Text = count.ToString();
                }
            });
            
        }

        bool isPrimaryNumber(int a)
        {
            bool res = true;            
            if (a > 1)
            {
                for (int i = 2; i < a; i++)
                {
                    if (a % i == 0)
                    {
                        res = false;
                        break;
                    }                    
                }
            }

            else
                res = false;

            return res;
        }

        public int diapason(int a, int b)
        {
            int count = 0;
            for (int i = a; i <= b; i++)
            {
                if (isPrimaryNumber(i))
                    count++;                
            }
            return count;
        }
    }
}

/*
Rozdzielamy przeszukiwany zakres na podaną liczbę podzakresów (Tasków)
Startujemy wszystkie Taski z 5 "równolegle"
Wyniki (ilość liczb pierwszych) aktualizujemy po każdym zakończonym Tasku
Pokazujemy paskiem postępu, ile Tasków się już zakończyło
Użycie Tasków dzieci, sprawdzających, czy dana liczba jest liczbą pierwszą
*/
