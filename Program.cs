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

            string x  = System.Console.ReadLine();
            Interpretator.Parser interpreter = new Interpretator.Parser(result);
            Interpretator.Expression node = interpreter.Parse();

            Console.WriteLine($"Ответ: {node.Accept(new Interpretator.ValueBuilder())}");
            Console.WriteLine("Hello World!");
        }
    }
}
