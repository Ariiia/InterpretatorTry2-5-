using System;
using System.IO;
using System.Collections.Generic;

namespace Interpretator
{
    class Program
    {
        static void Main(string[] args)
        {//entrance file
            int count = -1;
            List<string> strArr = new List<string>();
            string line;
           
             strArr.Add("c = 10");
            strArr.Add("sdfkjsdbjb = 910");
            strArr.Add("sdfkjsdbjb/c");
             
             count=3;
           
            string result = strArr[count];
            strArr.RemoveAt(count);
            System.Console.WriteLine(result);
            strArr.RemoveAt(count);
            for (int i = 0; i < strArr.Count; i++)
            {
                string[] str = strArr[i].Split(" = ");
                result = result.Replace(str[0], str[1]);
            }
            string x  = System.Console.ReadLine();
            Interpretator.Parser interpreter = new Interpretator.Parser(result);
            Interpretator.Expression node = interpreter.Parse();
            using (StreamWriter sw = new StreamWriter("result.txt", false, System.Text.Encoding.Default))
            {
                sw.WriteLine(node.Accept(new Interpretator.ValueBuilder()));
            }
            Console.WriteLine($"Ответ: {node.Accept(new Interpretator.ValueBuilder())}");
            Console.WriteLine("Hello World!");
        }
    }
}
