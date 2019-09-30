using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace XML_Parse
{
    class Program
    {

        static void Main(string[] args)
        {
            string[] files = Directory.GetFiles(@"D:\XML", "*.xml", SearchOption.AllDirectories); // Список всех файлов каталога расширения .xml

            foreach (string p in files)
                Parse(p);

            Console.WriteLine("The End");
            Console.ReadLine();
        }

        static void Parse(string file)
        {
            StreamReader reader = File.OpenText($"{file}"); // Открытие файла для чтения.
            string str = reader.ReadToEnd(); // Запись текста в строку.
            reader.Close();

            string[] arr = Regex.Matches(str, @"\>(\w+)\<").Cast<Match>().Select(m => m.Groups[1].Value).ToArray(); //Парсим строку по разделителю "> <", массив всех вхождений.

            for (int i = 0; i < arr.Length; i++)
            {
                string stroka = arr[i];
                if (stroka.Contains("ZDV")) // Проверка на наличие в строке ZDV.
                {
                    string entry = stroka.Remove(0, stroka.IndexOf("ZDV"));  // Удаление наименования участка объекта.
                    str = str.Replace(stroka, entry); // Замена старого наименования - на новое.
                }
            }

            using (System.IO.StreamWriter files = new System.IO.StreamWriter($"{file}"))
            {
                files.Write(str); // Запись изменений в файл.
            }

            reader.Close();
        }
    }
}


