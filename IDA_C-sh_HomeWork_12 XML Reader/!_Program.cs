// HomeWork template 1.4 // date: 17.10.2023

using Service;
using System;
using System.Linq.Expressions;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using IDA_C_sh_HomeWork_12_XML_Reader;
using System.Collections.Generic;

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

            List<Book> book_list = new List<Book>();

            // Загрузка XML из файла.
            var xml_data = new XmlDocument();
            xml_data.Load(filename_to_read);

            // получим корневой элемент
            XmlElement? Library = xml_data.DocumentElement;
            
            foreach (XmlElement book in Library) // это узлы book
            {
                Book book_temp_obj = new Book();
                foreach (XmlElement book_inner_elements in book) // это элементы внутри book (title, author, year, genre)
                //Console.WriteLine($"{book_inner_elements.Name}:".PadLeft(10) +$"\t{book_inner_elements.InnerText}");
                {
                    switch (book_inner_elements.Name)
                    {
                        case "title": book_temp_obj.Title = book_inner_elements.InnerText; break;
                        case "author": book_temp_obj.Author = book_inner_elements.InnerText; break;
                        case "year": book_temp_obj.Year = Int32.TryParse(book_inner_elements.InnerText, out var year) ? year : 0; break;                  
                        case "genre": book_temp_obj.Genre = book_inner_elements.InnerText; break;
                        default: throw new Exception("something wrong");
                    }
                }
                book_list.Add(book_temp_obj);
            }


            //Вывести все названия книг, отсортированные по названию в алфавитном порядке.
            Console.WriteLine(new string('-', 35));
            Console.WriteLine("названия книг, отсортированные по названию в алфавитном порядке:\n");
            var ordered_by_name = from book in book_list
                                  orderby book.Title
                                  select book;

            foreach (var element in ordered_by_name)
                Console.WriteLine(element.Title);

            //Посчитать количество книг каждого жанра.
            Console.WriteLine(new string('-', 35));
            Console.WriteLine("количество книг каждого жанра:\n");           
            

            var genre_books_qua = from x in book_list
                    group x by x.Genre into g
                    let count = g.Count()
                    orderby count descending
                    select new { Name = g.Key, Count = count };

            foreach (var x in genre_books_qua)            
                Console.WriteLine("Genre: " + x.Name + " | books: " + x.Count);
            

            //Получить список авторов, у которых есть книги с годом издания до 1900
            Console.WriteLine(new string('-', 35));
            Console.WriteLine("список авторов, у которых есть книги с годом издания до 1900:\n");

            var authors_books_before_1900 = from b in book_list where b.Year < 1900 select b;
            
            foreach (var author in authors_books_before_1900)            
                Console.WriteLine(author.Author);

            // Получить список авторов у которых не менее 2х книг в списке
            Console.WriteLine(new string('-', 35));
            Console.WriteLine("список авторов у которых не менее 2х книг в списке:\n");

            var authors_with_more_than_2_books = from b in book_list group b by b.Author into res where res.Count() >= 2 select res;

            foreach (var author in authors_with_more_than_2_books)
                Console.WriteLine(author.First().Author);

            // Посчитать количество книг в названиях которых больше одного слова и получить данные об этих книгах
            Console.WriteLine(new string('-', 35));
            Console.WriteLine("количество книг в названиях которых больше одного слова и получить данные об этих книгах:\n");

            var count_books_big_title = from b in book_list where b.Title.Contains(" ") select b;

            Console.WriteLine(count_books_big_title.Count() + $"\t(общее кол-во: {book_list.Count()})");

            // Получить имена авторов и книг, которые были написаны между 1940 и 2000 годами.
            Console.WriteLine(new string('-', 35));
            Console.WriteLine("имена авторов и книг, которые были написаны между 1940 и 2000 годами:\n");

            var authors_and_books_between_1940_2000 = from b in book_list where (b.Year >= 1940 && b.Year <= 2000) select b;

            foreach (var book in authors_and_books_between_1940_2000)
                Console.WriteLine(book.Title + "\t" + book.Author);
        }


    }// class Program
}// namespace