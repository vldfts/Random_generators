using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace As_Cript_WPF
{
    static class Model
    {
        static Random rand = new Random();
        public static StringBuilder Vstr(int n)
        {
            StringBuilder vidp = new StringBuilder();
            for (int i = 0; i < n / 32; i++)
            {
                vidp.Append(Convert.ToString(rand.Next(1, int.MaxValue), 2).PadLeft(32, '0'));
            }
            return vidp;

        }
        public static StringBuilder Lehmer(bool index, int n)
        {
            double m = Math.Pow(2, 32);
            double a = Math.Pow(2, 16) + 1;
            int c = 119;
            double x0 = rand.Next(1, int.MaxValue);
            StringBuilder vidp = new StringBuilder();
            if (!index)//High
            {
                for (int i = 0; i < n / 8; i++)
                {
                    x0 = (a * x0 + c) % m;
                    vidp.Append(Convert.ToString(Convert.ToUInt32(x0), 2).PadLeft(32, '0').Substring(0, 8));
                }
            }
            else//Low
            {
                for (int i = 0; i < n / 8; i++)
                {
                    x0 = (a * x0 + c) % m;
                    vidp.Append(Convert.ToString(Convert.ToUInt32(x0), 2).PadLeft(32, '0').Substring(24));
                }
            }
            return vidp;
        }
        public static StringBuilder L20(int n)
        {
            string x = Convert.ToString(rand.Next(1, 2 << 19), 2).PadLeft(20, '0');
            StringBuilder vidp = new StringBuilder(x);
            for (int i = 0; i < n - 20; i++)
            {
                vidp.Append(x[0] ^ x[11] ^ x[15] ^ x[17]);
                x = (x + (x[0] ^ x[11] ^ x[15] ^ x[17])).Substring(1, 20);
            }
            return vidp;
        }
        public static StringBuilder L89(int n)
        {
            byte[] mas = new byte[12];
            rand.NextBytes(mas);
            string x = "";
            for (int i = 0; i < 12; i++)
            {
                x += Convert.ToString(mas[i], 2);
            }
            x = x.PadLeft(89, '0').Substring(0, 89);
            StringBuilder vidp = new StringBuilder(x);
            for (int i = 0; i < n - 89; i++)
            {
                vidp.Append(x[0] ^ x[51]);
                x = (x + (x[0] ^ x[51])).Substring(1, 89);
            }
            return vidp;
        }
        public static StringBuilder Geffe(int n)
        {
            string L11 = Convert.ToString(rand.Next(1, 2 << 10), 2).PadLeft(11, '0').Substring(0, 11);
            string L9 = Convert.ToString(rand.Next(1, 2 << 8), 2).PadLeft(9, '0').Substring(0, 9);
            string L10 = Convert.ToString(rand.Next(1, 2 << 9), 2).PadLeft(10, '0').Substring(0, 10);
            StringBuilder z = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                if (L10[0] == '1')
                {
                    z.Append(L11[0]);
                }
                else { z.Append(L9[0]); }
                L11 = (L11 + (L11[0] ^ L11[2])).Substring(1, 11);
                L9 = (L9 + (L9[0] ^ L9[1] ^ L9[3] ^ L9[4])).Substring(1, 9);
                L10 = (L10 + (L10[0] ^ L10[3])).Substring(1, 10);
            }
            return z;
        }
        public static StringBuilder Wolfram(int n)
        {
            StringBuilder x = new StringBuilder();
            int r = rand.Next(1, int.MaxValue);
            for (int i = 0; i < n; i++)
            {
                x.Append(Math.Abs(r % 2));
                r = ((r << 1)) ^ (r | ((r >> 1)));
            }
            return x;
        }
        public static StringBuilder Biblio()
        {
            string path = "//texts//text.txt";
            StringBuilder data = new StringBuilder();
            byte[] bytes;
            using (StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + path))
            {
                bytes = Encoding.UTF8.GetBytes(sr.ReadToEnd());
            }
            for (int i = 0; i < bytes.Length; i++)
            {
                data.Append(bytes[i] + " ");
            }
            return data;
        }
        public static StringBuilder BM(bool index, int n)
        {
            string p = "CEA42B987C44FA642D80AD9F51F10457690DEF10C83D0BC1BCEE12FC3B6093E3";
            string a = "5B88C41246790891C095E2878880342E88C79974303BD0400B090FE38A688356";
            BigInteger numb = BigInteger.Abs(BigInteger.Parse(p, System.Globalization.NumberStyles.HexNumber)) - 1;
            BigInteger numb2 = BigInteger.Parse(a, System.Globalization.NumberStyles.HexNumber);
            BigInteger rivn = BigInteger.Divide(numb, 2);
            byte[] bytes = numb.ToByteArray();
            BigInteger T;
            while (true)
            {
                rand.NextBytes(bytes);
                T = BigInteger.Abs(new BigInteger(bytes));
                if (BigInteger.Compare(T, numb) != -1)
                {
                    break;
                }
            }
            StringBuilder vidpovid = new StringBuilder();
            if (index)
            {
                if (BigInteger.Compare(T, rivn) < 0)
                {
                    vidpovid.Append("1");
                }
                else
                {
                    vidpovid.Append("0");
                }
                for (int i = 0; i < n - 1; i++)
                {
                    T = BigInteger.ModPow(numb2, T, numb);
                    if (BigInteger.Compare(T, rivn) < 0)
                    {
                        vidpovid.Append("1");
                    }
                    else
                    {
                        vidpovid.Append("0");
                    }

                }
            }
            else
            {
                for (int i = 0; i < n / 8; i++)
                {
                    T = BigInteger.ModPow(numb2, T, numb);
                    for (int k = 0; k < 256; k++)
                    {
                        if (BigInteger.Compare(BigInteger.Divide(BigInteger.Multiply(k, numb), 256), T) < 0 && BigInteger.Compare(BigInteger.Divide(BigInteger.Multiply(k + 1, numb), 256), T) >= 0)
                        {
                            vidpovid.Append(k + " ");
                            break;
                        }
                    }
                }
            }
            return vidpovid;


        }
        public static StringBuilder BBS(bool index, int n)
        {
            string p = "D5BBB96D30086EC484EBA3D7F9CAEB07";
            string q = "425D2B9BFDB25B9CF6C416CC6E37B59C1F";
            BigInteger numb = BigInteger.Parse(p, System.Globalization.NumberStyles.HexNumber);
            BigInteger numb2 = BigInteger.Parse(q, System.Globalization.NumberStyles.HexNumber);
            BigInteger numb3 = BigInteger.Multiply(numb, numb2);
            byte[] bytes = numb3.ToByteArray();
            BigInteger r = rand.Next(2, int.MaxValue);
            StringBuilder vidpovid = new StringBuilder();
            if (index)
            {
                for (int i = 0; i < n; i++)
                {
                    r = BigInteger.ModPow(r, 2, numb3);
                    vidpovid.Append(r % 2);
                }
            }
            else
            {
                for (int i = 0; i < n / 8; i++)
                {
                    r = BigInteger.ModPow(r, 2, numb3);
                    vidpovid.Append(r % 256 + " ");
                }
            }
            return vidpovid;
        }
        public static string Peretvor_Bit_To_Byte(StringBuilder bitPoslidovn)
        {
            string bitPoslidovn2 = bitPoslidovn.ToString();
            string new_mas = "";
            for (int i = 0; i < bitPoslidovn2.Length - 7; i += 8)
            {
                new_mas += Convert.ToByte(bitPoslidovn2.Substring(i, 8), 2) + " ";
            }
            return new_mas;
        }
        public static int[] Statystyka_Vhodjen_Byte(StringBuilder bytePoslidovn)
        {
            string bytePoslidovn2 = bytePoslidovn.ToString();
            string[] a = bytePoslidovn2.Split(' ');
            int[] list_kilk_vhodj = new int[256];
            for (int i = 0; i < a.Length - 1; i++)
            {
                list_kilk_vhodj[int.Parse(a[i])] += 1;
            }
            return list_kilk_vhodj;
        }
        public static double[] Rivnomirnist(StringBuilder bytePoslidovn, double kvatl_norm_rozpodil)
        {
            int[] list_kilk_vhodj = Statystyka_Vhodjen_Byte(bytePoslidovn);
            double[] mas = new double[3];
            double Xi2 = 0;
            string bytePoslidovn2 = bytePoslidovn.ToString();
            int vh_mas = 0;
            for (int i = 0; i < list_kilk_vhodj.Length; i++)
            {
                vh_mas += list_kilk_vhodj[i];
            }
            double nj = (double)vh_mas / 256;
            for (int i = 0; i < 256; i++)
            {
                if (list_kilk_vhodj[i] != 0)
                {
                    Xi2 += Math.Pow(list_kilk_vhodj[i] - nj, 2) / Convert.ToDouble(nj);
                }
            }
            mas[0] = Xi2;
            mas[2] = Math.Sqrt(510) * kvatl_norm_rozpodil + 255;
            if (Xi2 <= mas[2]) { mas[1] = 1; }
            else { mas[1] = 0; }
            return mas;
        }
        public static double[] Nezalezhnist(StringBuilder bytePoslidovn, double kvatl_norm_rozpodil)
        {
            double[] mas = new double[3];
            double Xi2 = 0;
            string bytePoslidovn2 = bytePoslidovn.ToString();
            string[] a = bytePoslidovn2.Split(' ');
            int length = a.Length - 1;
            int[,] vij = new int[256, 256];
            int[] vi = new int[256];
            int[] aj = new int[256];
            for (int i = 0; i < a.Length - 2; i += 2)
            {
                vij[int.Parse(a[i]), int.Parse(a[i + 1])]++;
                vi[int.Parse(a[i])]++;
                aj[int.Parse(a[i + 1])]++;
            }
            for (int ij = 0; ij < 256; ij++)
            {
                for (int jk = 0; jk < 256; jk++)
                {
                    if (vij[ij, jk] != 0)
                    {
                        Xi2 += Math.Pow(vij[ij, jk], 2) / Convert.ToDouble(vi[ij] * aj[jk]);

                    }
                }
            }
            Xi2 = (Xi2 - 1) * length / 2;
            mas[0] = Xi2;
            mas[2] = Math.Sqrt(2) * 255 * kvatl_norm_rozpodil + 255 * 255;
            if (Xi2 <= mas[2]) { mas[1] = 1; }
            else { mas[1] = 0; }
            return mas;
        }
        public static double[] Odnoridnist(StringBuilder bytePoslidovn, double kvatl_norm_rozpodil)
        {
            int r = 10;
            double[] mas = new double[3];
            string bytePoslidovn2 = bytePoslidovn.ToString();
            string[] vh_mas = bytePoslidovn2.Split(' ');
            int[,] odn_mas = new int[r, 256];
            int length = vh_mas.Length - 1;
            int[] vi = new int[256];
            for (int k = 0; k < r; k++)
            {
                for (int j = 0; j < length / r; j++)
                {
                    odn_mas[k, int.Parse(vh_mas[k * (length / r) + j])]++;
                    vi[int.Parse(vh_mas[k * (length / r) + j])]++;
                }

            }
            double Xi2 = 0;
            double aj = length / r;
            for (int ij = 0; ij < r; ij++)
            {
                for (int jk = 0; jk < 256; jk++)
                {
                    if (odn_mas[ij, jk] != 0)
                    {
                        Xi2 += Math.Pow(odn_mas[ij, jk], 2) / Convert.ToDouble(vi[jk] * aj);
                    }
                }
            }
            Xi2 = (Xi2 - 1) * length;
            mas[0] = Xi2;
            mas[2] = Math.Sqrt(2 * 255 * (r - 1)) * kvatl_norm_rozpodil + 255 * (r - 1);
            if (Xi2 <= mas[2]) { mas[1] = 1; }
            else { mas[1] = 0; }
            return mas;
        }

    }
}

