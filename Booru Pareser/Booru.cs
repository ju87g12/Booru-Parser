using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace Booru_Pareser
{
    class Booru // базовый класс
    {
        public int total_page = 1; // общее количество страниц
        public int current_page  = 1; // текущая страница
        string tag; // тег по которому и происходит парсинг
        public bool error; // ошибка запроса
        public string url; // форма запроса

        public Booru(string _tag) // конструктор
        {
            tag = _tag;
        }

        public string Tag // свойство тега, так как присваивание существует только в базовом классе, доступно только для чтения 
        {
            get { return tag; }
        }

        public virtual string Url // свойство запроса, так как в каждном классе запрос свой, свойство виртуальное
        {
            get
            {
                url = "" + current_page + "&tags=" + tag;
                return url; 
            }
            set { url = value; }
        }
        public virtual List<string> getAll(List<string> pics = null) // рекурсивный метод на получение всех ссылок, для большенства подходит, но для некотрых нужно переопределние, потому он виртуальный 
        {
            List<string> pic_list = new List<string>(); // создаем лист
            pic_list = getPics(); // парсим первую партию картинок
            current_page++; // переходим на следующую страницу
            while (total_page >= current_page) // цикл идет пока  не дойдет до границы
            {
                foreach (var i in getAll(pic_list)) // сама рекурсия, вызываем метод и получаем с него лист с ссылками
                {
                    pic_list.Add(i.Replace("https", "http")); // так как могут быть проблемы с защищеным соединением, то нужна замена https на http
                }
            }
            return pic_list;
        }

        public string getPage()
        {
            try // обработка ситуации с возвращением кода ошибки сервера, будто 404, 403, 503... 
            {
                return new WebClient().DownloadString(Url); // скачивание страницы
            }
            catch(System.Net.WebException ex)
            {
            }
            return "";
        }

        public virtual List<string> getPics() // виртуальный метод для получения листа с ссылками, так как почти у каждного он отличается, он не определен
        { return new List<string>(); }

       



    }
}
