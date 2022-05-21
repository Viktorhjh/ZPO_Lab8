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
        int a, b, count = 0, countIteration, step;        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {            
            count = 0;            
            a = Int32.Parse(textBox1.Text);
            b = Int32.Parse(textBox2.Text);
            countIteration = Int32.Parse(textBox3.Text);
            step = (b - a) / countIteration;         
                
            label1.Text = "Primary numbers: ";            
            label2.Text = "Task doing somehing...";
            progressBar1.Value = 0;
            
            for (int i = 0; i < countIteration; i++)
            {
                await Task.Run(() => diapason(a, a + step));
                a += step;
                label1.Text = $"Primary numbers: {count}";               
                progressBar1.Value = (int)(((double)(i+1) / countIteration) * 100);

            }
            /*
            Parallel.For(0, countIteration, i =>
            {
                Task.Run(() => diapason(a, a + step));
                a += step;
                //label1.Text = $"Primary numbers: {count}";
                //progressBar1.Value = (int)(((double)(i + 1) / countIteration) * 100);
            });
            label1.Text += count;*/
            label2.Text = "Task is done!";
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

        public void diapason(int a, int b)
        {            
            for(int i = a; i < b; i++)
            {
                if (isPrimaryNumber(i)) 
                {
                    count++;
                }
            }
            
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
