using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    abstract class File1
    {
        public string Name { get; set; }
        public string Extension { get; set; }  // тип
        public string Size { get; set; }       // размер

        public File1(string name, string extension, string size)
        {
            Name = name;
            Extension = extension;
            Size = size;
        }

        public virtual void Display()
        {
            Console.WriteLine($"\t{Name}\n\t\tExtension: {Extension}\n\t\tSize: {Size}");
        }
    }

    class Movie : File1
    {
        public string Info { get; set; }

        public Movie(string name, string extension, string size, string info)
    : base(name, extension, size)
        {
            Info = info;
        }

        public override void Display()
        {
            base.Display();
            string Resolution = Info.Substring(0, Info.LastIndexOf(';'));
            string Lenght = Info.Substring(Info.LastIndexOf(';') + 1);
            Console.WriteLine($"\t\tResolution: {Resolution}\n\t\tLenght: {Lenght}");
        }
    }

    class Image : File1
    {
        public string Resolution { get; set; }

        public Image(string name, string extension, string size, string res)
    : base(name, extension, size)
        {
            Resolution = res;
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine($"\t\tResolution: {Resolution}");
        }
    }

    class Text : File1
    {
        public string Content { get; set; }

        public Text(string name, string extension, string size, string con)
    : base(name, extension, size)
        {
            Content = con;
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine($"\t\tContent: {Content}");
        }
    }

    public static class SplitString
    {
        public static string Name(string sourceString)
        {
            return sourceString.Substring(sourceString.IndexOf(':') + 1,
                sourceString.IndexOf('(') - sourceString.IndexOf(':') - 1);
        }

        public static string Extension(string sourceString)
        {
            return sourceString.Substring(sourceString.LastIndexOf('.') + 1,
                sourceString.IndexOf('(') - sourceString.LastIndexOf('.') - 1);
        }

        public static string Size(string sourceString)
        {
            return sourceString.Substring(sourceString.IndexOf('(') + 1,
                sourceString.IndexOf(')') - sourceString.IndexOf('(') - 1);
        }

        public static string Info(string sourceString)
        {
            return sourceString.Substring(sourceString.IndexOf(';') + 1);

        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\student\source\repos\KATE\OOPcontrol\ConsoleApp1\ConsoleApp1\file.txt";
            FileInfo fileInf = new FileInfo(path);
            string[] allText = null;

            if (fileInf.Exists)
            {
                try
                {   // Чтение файла
                    // Чтение всех строк файла в массив строк
                    allText = File.ReadAllLines(path);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            ConsoleApp1.List<File1> list = new List<File1>(); // список всех объектов

            var sortedText = from stringText in allText
                             orderby stringText[0] descending
                             select stringText;


            foreach (string s in sortedText)
            {
                if (s[0] == 'T')
                {
                    Text text = new Text(SplitString.Name(s), SplitString.Extension(s),
                        SplitString.Size(s), SplitString.Info(s));
                    list.Add(text);
                }
                else if (s[0] == 'I')
                {
                    Image images = new Image(SplitString.Name(s), SplitString.Extension(s),
                        SplitString.Size(s), SplitString.Info(s));
                    list.Add(images);
                }
                else
                {
                    Movie movies = new Movie(SplitString.Name(s), SplitString.Extension(s),
                        SplitString.Size(s), SplitString.Info(s));
                    list.Add(movies);
                }
            }

            Console.WriteLine("Text files:\n");
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].GetType().ToString().Equals("ConsoleApp1.Text"))
                {
                    list[i].Display();
                }
            }

            Console.WriteLine("Movies:\n");
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].GetType().ToString().Equals("ConsoleApp1.Movie"))
                {
                    list[i].Display();
                }
            }

            Console.WriteLine("Images:\n");
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].GetType().ToString().Equals("ConsoleApp1.Image"))
                {
                    list[i].Display();
                }
            }
        }
    }
}
