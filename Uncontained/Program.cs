using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Uncontained
{
    class Program
    {
        static void Main(string[] args)
        {
            var dic = new Dictionary<string, string>();

            if (args.Length < 4) {
                return;
            }

            var file1 = args[0];//入力ファイル1
            var file2 = args[1];//入力ファイル2
            var keyidx = args[2];//
            var validx = args[3];//

            var wkkey = keyidx.Split('=');
            var wkval = validx.Split('=');

            using (StreamReader sr = new StreamReader(file1, Encoding.GetEncoding("UTF-8")))
            {
                //file1をdicに格納
                while (sr.EndOfStream == false)
                {
                    var line = sr.ReadLine();
                    var arr = line.Split(',');
                    var key = arr[int.Parse(wkkey[0])];
                    var val = arr[int.Parse(wkval[0])];
                    if (dic.ContainsKey(key)) {
                        Console.WriteLine(key + "が重複ていします。file1を見直してください。");
                        return;
                    }
                    dic.Add(key,val);
                }
            }

            using (StreamReader sr = new StreamReader(file2, Encoding.GetEncoding("UTF-8")))
            {
                //file1のdicと比較
                while (sr.EndOfStream == false)
                {
                    var line = sr.ReadLine();
                    var arr = line.Split(',');
                    var key = arr[int.Parse(wkkey[1])];
                    var val = arr[int.Parse(wkval[1])];

                    //file1にないのは知らない
                    if (dic.ContainsKey(key))
                    {
                        var f1val = dic[key];
                        if (!f1val.Equals(val))
                        {
                            Console.WriteLine("key:" + key + "の値が異なっています。 file1:" + f1val + ", file2:" + line);
                        }
                    }
                }
            }
            Console.WriteLine("処理を完了しました。");
            Console.ReadLine();
        }
    }
}
//https://tnakamura.hatenablog.com/entry/2017/10/24/microsoft-extensions-commandlineutils