using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Asm1
{
    class Program
    {
        static List<double> ConvertMatrix(List<int> rgb)
        {
            double r = rgb[0], g = rgb[1], b = rgb[2];

            double cg =  (g / 2) - (b / 4) - (r / 4);
            double y = (g / 2) + (b / 4) + (r / 4);
            double co = (r / 2) - (b / 2);
            return new List<double>() { y, co, cg };
        }

        static List<double> ConvertLeap(List<int> rgb)
        {
            double r = rgb[0], g = rgb[1], b = rgb[2];

            double co2 = r - b;
            double cob = (co2 / 2) + b;
            double cg2 = g - cob;
            double y = (cg2 / 2) + cob;

            return new List<double>() { y, (co2 / 2), (cg2 / 2) };
        }

        static void Timer()
        {
            const int upbound = 255, lowbound = 0, itercount = 1000000;

            Stopwatch sw1 = Stopwatch.StartNew();
            Stopwatch sw2 = Stopwatch.StartNew();

            Random rnd = new Random();

            for (int i = 0; i < itercount; ++i)
            {
                List<int> rgb = new List<int>();
                for (int j = 0; j < 3; ++j)
                {
                    rgb.Add(rnd.Next(lowbound, upbound));
                }

                sw1.Start();
                ConvertMatrix(rgb);
                sw1.Stop();

                sw2.Start();
                ConvertLeap(rgb);
                sw2.Stop();
            }
            Console.WriteLine($"Matrix: {sw1.ElapsedMilliseconds}ms, Leap: {sw2.ElapsedMilliseconds}ms");
            Console.ReadKey();
        }

        static void InputLoop()
        {
            while (true)
            {
                Console.WriteLine("Enter RGB (EX: '255,255,255'): ");
                var input = Console.ReadLine();
                var rgbstrs = input.Trim().Split(',');

                List<int> rgb = new List<int>();

                try
                {
                    if (rgbstrs.Length != 3)
                    {
                        throw new Exception();
                    }

                    foreach (var rgbstr in rgbstrs)
                    {
                        var val = int.Parse(rgbstr);
                        if (val < 0 || val > 255)
                        {
                            throw new Exception();
                        }
                        rgb.Add(val);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid format\n");
                    continue;
                }
                var ycocgM = ConvertMatrix(rgb);
                var ycocgL = ConvertLeap(rgb);
                Console.WriteLine($"RGB ({string.Join(", ", rgbstrs)}): YCoCg ({string.Join(", ", ycocgM)})");
                Console.WriteLine($"RGB ({string.Join(", ", rgbstrs)}): YCoCg ({string.Join(", ", ycocgL)})\n");
            }
        }

        static void Main(string[] args)
        {
            InputLoop();
        }
    }
}
