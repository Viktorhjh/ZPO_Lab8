﻿using System;
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
        int a, b, amount, count = 0;
        List<Thread> list = new List<Thread>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            count = 0;
            label1.Text = "0";            
            a = Int32.Parse(textBox1.Text);
            b = Int32.Parse(textBox2.Text);    
                          
                Task.Run(() =>
                {
                    diapason(a, b);
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

        public void diapason(int a, int b)
        {            
            ParallelLoopResult result = Parallel.For (a, b+1, i =>
            {
                if (isPrimaryNumber(i))
                {
                    count++;
                    setText(count);                    
                }                   
            });
            
        }

        private delegate void SafeCallDelegate(int data);

        public void setText(int data)
        {            
            if (label1.InvokeRequired)
            {
                var d = new SafeCallDelegate(setText);                
                label1.Invoke(d, new object[] {data});
            }
            else
            {
                label1.Text = data.ToString();
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
