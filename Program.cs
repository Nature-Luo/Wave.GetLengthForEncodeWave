﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Wave.GetLengthForEncodeWave
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
                Console.WriteLine("Usage: exe encode_wave_folder");
            else
                RunGetLength(args[0]);
        }

        private static void RunGetLength(string wavf)
        {
            string outf = wavf + "_length.txt";
            string[] files = Directory.GetFiles(wavf, "*.wav", SearchOption.AllDirectories);

            int samplingrate = 8000;
            int bitrate = 16;
            int channel = 1;

            using (StreamWriter sw = new StreamWriter(outf, false, Encoding.UTF8)) {
                sw.WriteLine("文件\t时长（秒）");
                double total_len = 0;
                foreach (string file in files) {
                    FileInfo fi = new FileInfo(file);
                    long size = fi.Length - 44;

                    double leng_sec = 0;
                    leng_sec = Convert.ToDouble(size) * 8 / (bitrate * samplingrate * channel);
                    total_len += leng_sec;

                    sw.WriteLine(file+"\t"+leng_sec.ToString("0.000"));
                }
                sw.WriteLine("总计\t"+total_len.ToString("0.000"));
                Console.WriteLine(wavf+"\t"+total_len.ToString("0.00"));
                //Console.WriteLine("!!! Finished !!!");
            }
            
        }
    }
}
