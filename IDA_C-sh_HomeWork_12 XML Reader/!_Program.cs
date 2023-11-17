// HomeWork template 1.4 // date: 17.10.2023

using Service;
using System;
using System.Linq.Expressions;
using System.Text;
using System.Xml;
using System.Xml.Linq;

/// QUESTIONS ///
/// 1. 

// HomeWork 12 : [{XML Reader}] --------------------------------

namespace IDA_C_sh_HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {

            MainMenu.MainMenu mainMenu = new MainMenu.MainMenu();
            do 
            {
                Console.Clear();
                mainMenu.Show_menu();
                if (mainMenu.User_Choice_Handle() == 0) break;
                Console.ReadKey();
            } while (true);
            // Console.ReadKey();
        }

        public static void Task_1(string work_name)
        /* Задание 
            Требуется написать C#-код, используя LINQ и XML, для выполнения следующих задач:
        Вывести все названия книг, отсортированные по названию в алфавитном порядке.
        Посчитать количество книг каждого жанра.
        Получить список авторов, у которых есть книги с годом издания до 1900
        Получить список авторов у которых не менее 2х книг в списке
        Посчитать количество книг в названиях которых больше одного слова и получить данные об этих книгах
        Получить имена авторов и книг, которые были написаны между 1940 и 2000 годами.*/
        {
            Console.WriteLine("\n***\t{0}\n\n", work_name);

            string filename_to_read = "books.xml";



            // Загрузка XML из файла.
            var xml_data = new XmlDocument();
            xml_data.Load(filename_to_read);

            // получим корневой элемент
            XmlElement? Library = xml_data.DocumentElement;
            
            foreach (XmlElement book in Library) // это узлы book
            {
                foreach (XmlElement book_inner_elements in book) // это элементы внутри book (title, author, year, genre)
                        Console.WriteLine($"{book_inner_elements.Name}:".PadLeft(10) +$"\t{book_inner_elements.InnerText}");
            }
       

    


        }


    }// class Program
}// namespace