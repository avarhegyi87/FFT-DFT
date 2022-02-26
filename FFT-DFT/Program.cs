using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFT_DFT {
    class Program {
        private const char Separator = ' ';

        static void Main(string[] args) {
            using (StreamReader sr = File.OpenText(@"C:\Users\Adam\OneDrive\UNI\Jelek es rendszerek\feladatok\20\shortSIGN3.txt")) {
                uint N = 0;
                double dt = 0.0;

                string szöveg = null;
                int sor = 0;
                
                szöveg = sr.ReadLine();
                N = uint.Parse(szöveg.Split(separator: Separator)[0]);
                dt = double.Parse(szöveg.Split(separator: Separator)[1].Replace('.', ','));

                double[] tömb = new double[N];
                Complex[] dft = new Complex[N];
                double[] idft = new double[N];
                uint halfpoint = N / 2 + 1;
                //if (N % 2 == 0) {
                //    halfpoint = N / 2 + 1;
                //} else {
                //    halfpoint = N / 2;
                //}
                double[] samp = new double[halfpoint];

                while (((szöveg = sr.ReadLine()) != null) && (sor < N)) {
                    tömb[sor] = double.Parse(szöveg.Replace('.', ','));
                    sor++;
                }

                for (int k = 0; k < N; k++) {
                    Complex csum = new Complex(0, 0);
                    for (int l = 0; l < N; l++) {
                        Complex complex = new Complex(1, -1 * (2 * Math.PI / N * l * k), false);
                        csum += new Complex(tömb[l], 0) * complex;
                    }
                    dft[k] = csum / new Complex(N, 0);
                }

                for (int i = 0; i < N; i++) {
                    Complex csum = new Complex(0, 0);
                    for (int k = 0; k < N; k++) {
                        Complex complex = new Complex(1, 2 * Math.PI / N * k * i, false);
                        Complex szorzás = dft[k] * complex;
                        csum += szorzás;
                    }
                    idft[i] = csum.X;
                }

                for (int k = 0; k < halfpoint; k++) {
                    if (k == 0 || (k == N / 2 && N % 2 == 0)) {
                        samp[k] = dft[k].AbsVal;
                    } else {
                        samp[k] = 2 * dft[k].AbsVal;
                    }
                }

                Console.WriteLine("DFT:");
                for (int k = 0; k < N; k++) {
                    Console.WriteLine("F{0} = {1}", k, dft[k]);
                }

                Console.WriteLine("\nIDFT");
                for (int i = 0; i < N; i++) {
                    Console.WriteLine("f{0} = {1}", i, idft[i]);
                }

                Console.WriteLine("\nS-AMP");
                for (int k = 0; k < samp.Length; k++) {
                    Console.WriteLine("S-AMP: F{0} = {1}", k, samp[k]);
                }
            }
            Console.ReadLine();
        }
    }
}
