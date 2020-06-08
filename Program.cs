using System;
using System.IO;
using System.Collections.Generic;

namespace Interpretator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string result = NewInput();

            Console.WriteLine();
            {
                try
                {
                    Interpretator.Parser interpreter = new Interpretator.Parser(result);
                    Interpretator.Expression node = interpreter.Parse();

                    Console.WriteLine($"Ответ: {node.Accept(new Interpretator.BuilderValue())}");
                }
                catch (Interpretator.InvalidSyntaxException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        public static string NewInput()
        {
            Console.WriteLine("Входные данные:");
            int count = -1;
            StreamReader file = new StreamReader("Input.txt");
            string line;
            //ArrayList arrstr = new ArrayList();
            List<string> strArr = new List<string>();
            while ((line = file.ReadLine()) != null)
            {
                strArr.Add(line);
                Console.WriteLine(line);
                count++;
            }
            string result = strArr[count];
            strArr.RemoveAt(count);
            for (int i = 0; i < strArr.Count; i++)
            {
                string[] str = strArr[i].Split(" = ");
                result = result.Replace(str[0], str[1]);
            }

            return result;
        }
    }

}