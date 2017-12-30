using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FROM_ANSI1252_to_1250
{
    class Program
    {
        private static List<char[]> replaceTab = new List<char[]> 
        {
            new char[] {'¹', 'ą'},
            new char[] {'¿', 'ż'},
            new char[] {'³', 'ł'},
            new char[] {'ê', 'ę'},
            new char[] {'œ', 'ś'},
            new char[] {'æ', 'ć'},
            new char[] {'Ÿ', 'ź'}
        };

        static void Main(string[] args)
        {
            string subPath = "";

            StringBuilder list = new StringBuilder(
                "Available commands list:\n" +
                "-------\n" +
                "  file {path_To_File_In_Current_Directory}\n" +
                "-------\n" +
                "example: file subtitles.srt\n"
            );

            if (args.Length == 2)
            {
                switch (args[0])
                {
                    case "file":
                        subPath = Environment.CurrentDirectory.ToString() + @"\" + args[1];
                        if(!File.Exists(subPath))
                        {
                            System.Console.WriteLine("Make sure file exist.");
                            Environment.Exit(1);
                        }
                        System.Console.WriteLine("Path commited: " + subPath);
                        break;
                    default:
                        System.Console.WriteLine("Do not recognised command.");
                        System.Console.WriteLine(list);
                        Environment.Exit(1);
                        break;
                }
            }
            else
            {
                System.Console.WriteLine(list);
                Environment.Exit(1);
            }

            using (FileStream subFileStream = new FileStream(subPath, FileMode.Open, FileAccess.ReadWrite))
            {
                byte[] bytes;

                using (StreamReader reader = new StreamReader(subFileStream))
                {
                    string txt = reader.ReadToEnd();

                    foreach(var _char in replaceTab)
                    {
                        txt = txt.Replace(_char[0], _char[1]);
                    }
                    
                    UTF8Encoding temp = new UTF8Encoding();
                    bytes = temp.GetBytes(txt);

                    subFileStream.SetLength(0);
                    subFileStream.Write(bytes, 0, bytes.Length);
                    System.Console.WriteLine("Replacing chars complete.");
                }
               
            }
        }
    }
}
