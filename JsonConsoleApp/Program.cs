//Установленная менеджером пакетов библиотека разбора json-строк
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
//Стандартная библиотека из .Net для Http-запросов к серверу
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JsonConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Запускаем свой статический метод, выполняющийся асинхронно
            postData();
            //В основном потоке выполнения ждем нажатия клавиши пользователем,
            //тем временем параллельный поток получает данные из сети и печатает их в консоль
            Console.ReadKey();
        }

        //Наш статический метод, выполняющийся асинхронно, получающий данные из сети
        static async void postData() {

            //Готовим строку - пустой объект json для отправки на сервер
            string emptyJson = "{}";
            //Создаем совеременную версию объекта для Http-запросов к серверу
            using (var client = new HttpClient())
            {
                //Делаем пост-запрос на сервер по адресу, взятому из веб-проекта
                var response = await client.PostAsync(
                    "http://localhost:49245/Default/DoAction?action=states",
                     new StringContent(emptyJson, Encoding.UTF8, "application/json"));
                //Ждем код результата
                response.EnsureSuccessStatusCode();
                //Стримом читаем строку из ответа
                string content = await response.Content.ReadAsStringAsync();
                //Разбираем строку 
                NoteBookResult result = JsonConvert.DeserializeObject<NoteBookResult>(content);
                //Коллекцию объектов "Состояние" читаем из свойства получившегося объекта,
                //затем перебираем циклом и печатаем содержимое в консоль
                foreach (var item in result.PagesData)
                {
                    Console.WriteLine($"id = {item.Id}; name = {item.Name}");
                }
            }
        }
    }
}
