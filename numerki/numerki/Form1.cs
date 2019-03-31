using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace numerki
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        double f(double x, double y)
        {
            return 1/y;
        }
        double f2(double x)
        {
            return (2*x)+5;
        }
        double f3(double x, double y)
        {
            return y;
        }
        double f4(double x)
        {
            return (-1 *x *x * x * x)+ (2*x*x*x)+(x*x)+(10*x) - 10;
        }
        double fp(double x)
        {
            return (-4*x*x*x)+(6*x*x)+(2*x)+10;
        }
        void heun(double x0, double y0, double h, double N, int i)
        {
            if (i == N)
            {
                textBox3.Text = yend;
                textBox4.Text = xend;
            }
            else
            {
                double tempy = y0 + ((h / 2) * (f(x0, y0) + f((x0 + h), (y0 + (h * f(x0, y0))))));
                double tempx = x0 + h;
                tempy = Math.Round(tempy, 4);
                tempx = Math.Round(tempx, 4);
                i++;
                yend += "y" + i.ToString() + ": " + tempy.ToString() + " ";
                xend += "x" + i.ToString() + ": " + tempx.ToString() + " ";
                heun(tempx, tempy, h, N, i);
            }
        }
        string yend, xend;
        void rk4(double x0, double y0, double h, double N, int i)
        {
            
            if (i == N)
            {
                textBox1.Text = yend;
                textBox2.Text = xend;
            }
            else
            {
                double k1 = h * f(x0, y0);
                double k2 = h * f(x0 + (h / 2), y0 + (k1 / 2));
                double k3 = h * f(x0 + (h / 2), y0 + (k2 / 2));
                double k4 = h * f((x0 + h), (y0 + k3));
                double tempy = y0 + ((k1 + (2 * k2) + (2 * k3) + k4) / 6);
                double tempx = x0 + h;
                tempy = Math.Round(tempy, 4);
                tempx = Math.Round(tempx, 4);
                i++;
                yend += "y"+i.ToString()+": "+ tempy.ToString()+" ";
                xend += "x" + i.ToString() + ": " + tempx.ToString() + " ";

                rk4(tempx, tempy, h, N, i);
            }
        }
        void rk1(double x0, double y0, double h, double N, int i)
        {

            if (i == N)
            {
                textBox6.Text = yend;
                textBox5.Text = xend;
            }
            else
            {
                double tempy = y0 + (h * f(x0, y0));
                double tempx = x0 + h;
                tempy = Math.Round(tempy, 4);
                tempx = Math.Round(tempx, 4);
                i++;
                yend += "y" + i.ToString() + ": " + tempy.ToString() + " ";
                xend += "x" + i.ToString() + ": " + tempx.ToString() + " ";

                rk1(tempx, tempy, h, N, i);
            }
        }
        void bisekcja(double a, double b, double dok)
        {
            double x1 = (a + b) / 2;
            if (Math.Abs(a-b)<dok)
            {
                x1 = Math.Round(x1, 4);
                textBox7.Text = x1.ToString();
            }
            else
            {
                if (f4(x1) * f4(a) < 0) bisekcja(a, x1, dok);
                else if (f4(x1) * f4(b) < 0) bisekcja(x1, b, dok);
            }
        }
        void rn(double x, double dok)
        {
            double tempx = x - (f4(x) / fp(x));
            if(Math.Abs(f4(x))<=dok || Math.Abs(tempx-x)<=dok)
            {
                tempx = Math.Round(tempx, 4);
                textBox8.Text = tempx.ToString();
            }
            else
            {
                rn(tempx, dok);
            }
        }
        void prostokaty(int stopien, int[] tab, int n, int xp, int xk)
        {
            double suma = 0;
            double dx = (xk - xp)*1.0 / n;
            
            double j = xp + dx;
            
            while(j<=xk)
            {
                for (int i = 0; i <= stopien; i++)
                {
                    suma = suma + (tab[i] * Math.Pow(j, i));
                    
                    
                    

                }
                j += dx;
            }
            textBox9.Text = (suma * dx).ToString();
        }
        void trapezy(int stopien, int[] tab, int n, int xp, int xk)
        {
            double suma = 0;
            double dx = (xk - xp)*1.0 / n;
            double[] F = new double[n+1];
            for (int i = 0; i < n; i++)
            {
                F[i] = 0;
            }
            int z = 0;
            double j = xp;
            while (j <= xk)
            {
                for (int i = 0; i <= stopien; i++)
                {
                    F[z] = F[z] + (tab[i] * Math.Pow(j, i));

                }

                j += dx;
                
                z++;
            }
            for (int i = 1; i <= n; i++)
            {
                suma = suma + (((F[i - 1] + F[i]) * dx)*1.0 / 2);
            }
            textBox10.Text = suma.ToString();
        }
        void simpson(int stopien, int[] tab, int n, int xp, int xk)
        {
            if (n % 2 == 1)
                textBox11.Text = "Wrong n value";
            else
            {
                double h = (xk - xp)*1.0 / n;
                double[] I = new double[n/2];
                for(int i=0;i<n/2;i++)
                {
                    I[i] = 0;
                }
                double suma = 0;
                double[] F = new double[n+1];
                for (int i = 0; i <= n; i++)
                {
                    F[i] = 0;
                }
                int z = 0;
                double j = xp;
                while (j<=xk)
                {
                    for (int i = 0; i <= stopien; i++)
                    {
                        F[z] = F[z] + (tab[i] * Math.Pow(j, i));

                    }
                    z++;
                    j += h;
                }
                for (int i = 2; i <= n; i +=2)
                {
                    suma = suma + (((F[i - 2] + (4 * F[i - 1]) + F[i]) * h) * 1.0) / 3;
                }
                textBox11.Text = suma.ToString();
            }
        }
        void monteCarlo(int stopien, int[] tab, int n, int xp, int xk)
        {
            Random rnd = new Random();
            double fs = 0;
            double[] tab1 = new double[n];
            int liczba = (xk - xp) * 100;
            for (int i = 0; i < n; i++)
            {
                tab1[i] = rnd.Next((100 * xp), (100 * xk));
                tab1[i] = tab1[i] / 100;
            }
            /*double[] tab2=new double[4];
            tab2[0] = 1.5;
            tab2[1] = 2.6;
            tab2[2] = 3.8;
            tab2[3] = 4.5;*/

            double suma = 0;
            double[] F = new double[n];
            for (int i = 0; i < n; i++)
            {
                F[i] = 0;
            }
            int z = 0;
            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i <= stopien; i++)
                {
                    F[z] = F[z] + (tab[i] * Math.Pow(tab1[j], i));

                }
                z++;
            }
            for (int i = 0; i < n; i++)
            {
                fs = fs + (F[i] / n);
            }
            textBox12.Text = (fs * Math.Abs(xk - xp)).ToString();
        }
        void interN(double x, int iloscpkt, double[] xi, double[] yi)
        {
            double[] tab = new double[iloscpkt - 1];
            double[] tab2 = new double[iloscpkt - 1];
            double buff = 1;
            double W = yi[0];

            for (int i = 0; i < iloscpkt - 1; i++)
            {
                tab[i] = (yi[i + 1] - yi[i]) / (xi[i + 1] - xi[i]);
            }
            for (int i = 1; i < iloscpkt - 1; i++)
            {
                int j = iloscpkt - 2;

                while (j >= i)
                {
                    tab[j] = (tab[j] - tab[j - 1]) / (xi[j + 1] - xi[j - i]);
                    j--;
                }
            }
            for (int i = 0; i < iloscpkt - 1; i++)
            {
                buff = buff * (x - xi[i]);
                tab2[i] = buff;
            }
            for (int i = 0; i < iloscpkt - 1; i++)
            {
                W = W + (tab[i] * tab2[i]);
            }
            textBox13.Text = W.ToString();
        }
        void rownanie(double X, int stopien, double[] xi, double[] yi, double[] zi, double[] w)
        {
            double W;
            double Wx, Wy, Wz;
            double x, y, z;
            double[,] tab = new double[stopien, stopien + 1];
            for (int i = 0; i < stopien; i++)
                tab[i,0] = xi[i];
            for (int i = 0; i < stopien; i++)
                tab[i,1] = yi[i];
            for (int i = 0; i < stopien; i++)
                tab[i,2] = zi[i];
            for (int i = 0; i < stopien; i++)
                tab[i,3] = w[i];
            W = (tab[0,0] * tab[1,1] * tab[2,2]) + (tab[0,1] * tab[1,2] * tab[2,0]) + (tab[0,2] * tab[1,0] * tab[2,1]) - (tab[0,2] * tab[1,1] * tab[2,0]) - (tab[1,2] * tab[2,1] * tab[0,0]) - (tab[2,2] * tab[0,1] * tab[1,0]);
            Wx = (tab[0,3] * tab[1,1] * tab[2,2]) + (tab[0,1] * tab[1,2] * tab[2,3]) + (tab[0,2] * tab[1,3] * tab[2,1]) - (tab[0,2] * tab[1,1] * tab[2,3]) - (tab[1,2] * tab[2,1] * tab[0,3]) - (tab[2,2] * tab[0,1] * tab[1,3]);
            Wy = (tab[0,0] * tab[1,3] * tab[2,2]) + (tab[0,3] * tab[1,2] * tab[2,0]) + (tab[0,2] * tab[1,0] * tab[2,3]) - (tab[0,2] * tab[1,3] * tab[2,0]) - (tab[1,2] * tab[2,3] * tab[0,0]) - (tab[2,2] * tab[0,3] * tab[1,0]);
            Wz = (tab[0,0] * tab[1,1] * tab[2,3]) + (tab[0,1] * tab[1,3] * tab[2,0]) + (tab[0,3] * tab[1,0] * tab[2,1]) - (tab[0,3] * tab[1,1] * tab[2,0]) - (tab[1,3] * tab[2,1] * tab[0,0]) - (tab[2,3] * tab[0,1] * tab[1,0]);
            x = Wx / W;
            y = Wy / W;
            z = Wz / W;
            textBox16.Text = ((x * X * X) + (y * X) + (z)).ToString();
        }
        void interR(double x, int stopien, double[] Xi, double[] Yi)
        {
            double[] xi = new double[stopien];
            double[] yi = new double[stopien];
            double[] zi = new double[stopien];
            double[] w = new double[stopien];
            for (int i = 0; i < stopien; i++)
            {
                xi[i] = Xi[i] * Xi[i];
                yi[i] = Xi[i];
                zi[i] = 1;
                w[i] = Yi[i];

            }
            rownanie(x, 3, xi, yi, zi, w);
        }
        void interL(double x, int ilosc, double[] xi, double[] yi)
        {

            double[] l = new double[ilosc];
            for (int i = 0; i < ilosc; i++)
            {
                l[i] = 1;
            }
            for (int i = 0; i < ilosc; i++)
            {
                for (int j = 0; j < ilosc; j++)
                {
                    if (i == j)
                        continue;
                    else
                    {
                        l[i] *= (x - xi[j]) / (xi[i] - xi[j]);
                    }

                }

            }
            double L = 0;
            for (int i = 0; i < ilosc; i++)
            {
                L = L + (yi[i] * l[i]);
            }
            textBox18.Text = L.ToString();
        }
        void pochodna(double x0, double h)
        {
            double prawo = (f2(x0 + h) - f2(x0)) / h;
            double lewo = (f2(x0) - f2(x0 - h)) / h;
            double centr = (f2(x0 + h) - f2(x0 - h)) / (2 * h);
            textBox19.Text = prawo.ToString();
            textBox20.Text = lewo.ToString();
            textBox21.Text = centr.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            StreamReader plik = new StreamReader("heun.txt");
            double x0 = Double.Parse(plik.ReadLine());
            double b = Double.Parse(plik.ReadLine());
            double y0 = Double.Parse(plik.ReadLine());
            double h = Double.Parse(plik.ReadLine());
            plik.Close();
            double N = (b - x0) / h;
            heun(x0, y0, h, N, 0);
            yend = "";
            xend = "";
        }


        private void button3_Click(object sender, EventArgs e)
        {
            StreamReader plik = new StreamReader("rk1.txt");
            double x0 = Double.Parse(plik.ReadLine());
            double b = Double.Parse(plik.ReadLine());
            double y0 = Double.Parse(plik.ReadLine());
            double h = Double.Parse(plik.ReadLine());
            double N = (b - x0) / h;
            rk1(x0, y0, h, N, 0);
            yend = "";
            xend = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StreamReader plik = new StreamReader("C:\\Users\\Rafał\\Documents\\Visual Studio 2015\\Projects\\numerki\\numerki\\bisekcja.txt");
            string[] temp = new string[3];
            for (int i = 0; i < 3; i++)
            {
                temp[i] = plik.ReadLine();
                Console.WriteLine(temp[i]);
            }
            
            plik.Close();
            double a = Double.Parse(temp[0]);
            double b = Double.Parse(temp[1]);
            double dok = Convert.ToDouble(temp[2]);
            if (f4(a) * f4(b) < 0)
            {
                double x1 = (a + b) / 2;
                if (f4(x1) == 0)
                {
                    x1 = Math.Round(x1, 4);
                    textBox7.Text = x1.ToString();
                }
                else
                {
                    bisekcja(a,b,dok);
                }
            }
            else
            {
                textBox7.Text = "NONE";
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            StreamReader plik = new StreamReader("NR.txt");

            double a = Double.Parse(plik.ReadLine());
            double b = Double.Parse(plik.ReadLine());
            double dok = Double.Parse(plik.ReadLine());
            plik.Close();
            double x0 = 0;
            if (f4(a) * f4(b) < 0)
            {
                rn(x0, dok);
            }
            else
            {
                textBox8.Text = "NONE";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader plik = new StreamReader("prostokaty.txt");
                int stopien, n, xp, xk;
                string temp;
                string[] tab = new string[200];
                string fx;
                int licznik= 0;
                while (!plik.EndOfStream)
                {
                    tab[licznik] = plik.ReadLine();
                    licznik++;
                }
                stopien = Int32.Parse(tab[0]);
                n = Int32.Parse(tab[1]);
                xp = Int32.Parse(tab[2]);
                xk = Int32.Parse(tab[3]);
                int[] tab1 = new int[stopien+1];
                for(int i=0;i<=stopien;i++)
                {
                    tab1[i] = Int32.Parse(tab[i+4]);
                }

                prostokaty(stopien, tab1, n, xp, xk);
            }
            catch (Exception except)
            {
                Console.WriteLine("Exception: " + except.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader plik = new StreamReader("trapezy.txt");
                int stopien, n, xp, xk;
                string temp;
                string[] tab = new string[200];
                string fx;
                int licznik = 0;
                while (!plik.EndOfStream)
                {
                    tab[licznik] = plik.ReadLine();
                    licznik++;
                }
                stopien = Int32.Parse(tab[0]);
                n = Int32.Parse(tab[1]);
                xp = Int32.Parse(tab[2]);
                xk = Int32.Parse(tab[3]);
                int[] tab1 = new int[stopien + 1];
                for (int i = 0; i <= stopien; i++)
                {
                    tab1[i] = Int32.Parse(tab[i + 4]);
                }

                trapezy(stopien, tab1, n, xp, xk);
            }
            catch (Exception except)
            {
                Console.WriteLine("Exception: " + except.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader plik = new StreamReader("simpson.txt");
                int stopien, n, xp, xk;
                string temp;
                string[] tab = new string[200];
                string fx;
                int licznik = 0;
                while (!plik.EndOfStream)
                {
                    tab[licznik] = plik.ReadLine();
                    licznik++;
                }
                stopien = Int32.Parse(tab[0]);
                n = Int32.Parse(tab[1]);
                xp = Int32.Parse(tab[2]);
                xk = Int32.Parse(tab[3]);
                int[] tab1 = new int[stopien + 1];
                for (int i = 0; i <= stopien; i++)
                {
                    tab1[i] = Int32.Parse(tab[i + 4]);
                }

                simpson(stopien, tab1, n, xp, xk);
            }
            catch (Exception except)
            {
                Console.WriteLine("Exception: " + except.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader plik = new StreamReader("montecarlo.txt");
                int stopien, n, xp, xk;
                string temp;
                string[] tab = new string[200];
                string fx;
                int licznik = 0;
                while (!plik.EndOfStream)
                {
                    tab[licznik] = plik.ReadLine();
                    licznik++;
                }
                stopien = Int32.Parse(tab[0]);
                n = Int32.Parse(tab[1]);
                xp = Int32.Parse(tab[2]);
                xk = Int32.Parse(tab[3]);
                int[] tab1 = new int[stopien + 1];
                for (int i = 0; i <= stopien; i++)
                {
                    tab1[i] = Int32.Parse(tab[i + 4]);
                }

                monteCarlo(stopien, tab1, n, xp, xk);
            }
            catch (Exception except)
            {
                Console.WriteLine("Exception: " + except.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader plik = new StreamReader("interN.txt");
                int licznik = 0;
                string linia;
                while (!plik.EndOfStream)
                {
                    linia = plik.ReadLine();
                    licznik++;
                }
                plik.Close();
                StreamReader plik2 = new StreamReader("interN.txt");
                int iloscpkt = licznik / 2;
                double[] xi = new double[iloscpkt];
                double[] yi = new double[iloscpkt];
                string[] tab = new string[licznik];
                for (int i = 0; i < licznik; i++)
                {
                    tab[i] = plik2.ReadLine();
                    Console.WriteLine(tab[i]);
                    if (i % 2 == 0)
                        xi[i / 2] = Int32.Parse(tab[i]);
                    else
                        yi[i / 2] = Int32.Parse(tab[i]);
                }
                double x = Double.Parse(textBox14.Text);
                interN(x, iloscpkt, xi, yi);
            }
            catch (Exception except)
            {
                Console.WriteLine("Exception: " + except.Message);
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader plik = new StreamReader("interR.txt");
                int licznik = 0;
                string linia;
                while (!plik.EndOfStream)
                {
                    linia = plik.ReadLine();
                    licznik++;
                }
                plik.Close();
                StreamReader plik2 = new StreamReader("interR.txt");
                int iloscpkt = licznik / 2;
                double[] xi = new double[iloscpkt];
                double[] yi = new double[iloscpkt];
                string[] tab = new string[licznik];
                for (int i = 0; i < licznik; i++)
                {
                    tab[i] = plik2.ReadLine();
                    Console.WriteLine(tab[i]);
                    if (i % 2 == 0)
                        xi[i / 2] = Int32.Parse(tab[i]);
                    else
                        yi[i / 2] = Int32.Parse(tab[i]);
                }
                double x = Double.Parse(textBox15.Text);
                interR(x, iloscpkt, xi, yi);
            }
            catch (Exception except)
            {
                Console.WriteLine("Exception: " + except.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader plik = new StreamReader("interL.txt");
                int licznik = 0;
                string linia;
                while (!plik.EndOfStream)
                {
                    linia = plik.ReadLine();
                    licznik++;
                }
                plik.Close();
                StreamReader plik2 = new StreamReader("interL.txt");
                int iloscpkt = licznik / 2;
                double[] xi = new double[iloscpkt];
                double[] yi = new double[iloscpkt];
                string[] tab = new string[licznik];
                for (int i = 0; i < licznik; i++)
                {
                    tab[i] = plik2.ReadLine();
                    Console.WriteLine(tab[i]);
                    if (i % 2 == 0)
                        xi[i / 2] = Int32.Parse(tab[i]);
                    else
                        yi[i / 2] = Int32.Parse(tab[i]);
                }
                double x = Double.Parse(textBox17.Text);
                interL(x, iloscpkt, xi, yi);
            }
            catch (Exception except)
            {
                Console.WriteLine("Exception: " + except.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            StreamReader plik = new StreamReader("pochodna.txt");
            double x0 = Double.Parse(plik.ReadLine());
            double h = Double.Parse(plik.ReadLine());
            pochodna(x0, h);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            StreamReader plik = new StreamReader("rk4.txt");
            double x0 = Double.Parse(plik.ReadLine());
            double b = Double.Parse(plik.ReadLine());
            double y0 = Double.Parse(plik.ReadLine());
            double h = Double.Parse(plik.ReadLine());
            double N = (b - x0) / h;
            rk4(x0, y0, h, N, 0);
            yend = "";
            xend = "";

        }
    }
}
