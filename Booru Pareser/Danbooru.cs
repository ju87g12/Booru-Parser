using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booru_Pareser
{
    class Danbooru : Booru
    {
        public Danbooru(string _tag) : base(_tag)
        {
            string html_0 = @"<li class='numbered-page'>"; // html теги, записаны в переменные для удобности и читаемости
            string html_1 = "/posts?page=";
            string html_2 = "class='current-page'";
            string page_num = "";
            string page = getPage();
            if (page == null || page.Contains(html_2) == false) // если случилось исключение, или тега нет, то ошибка, использование html_2 нужно так как есть теги с 1 страницей
            {
                error = true;
            }
            else
            {
                while (page.Contains(html_0)) // если страница содержит нужный html код
                {
                    page_num = page.Substring(page.IndexOf(html_0) + html_0.Count()); // обрезаем к тому месту где ноходится нужный код
                    page = page_num;

                }
                if (page_num.Contains(html_1))
                {
                    page_num = page_num.Substring(page_num.IndexOf(html_1) + html_1.Count()); // 
                    total_page = Convert.ToInt32(page_num.Substring(0, page_num.IndexOf("&amp"))); // хоть в привычных браузерах в этом месте и не содержится этого кода, этот код содержится в скачиной версии, что творится в систмах где нет IE11 или есть EDGE не могу на данный момент проверить 
                }
                else total_page = 1;
            }
        }

        public override string Url
        {
            get
            {
                url = "https://danbooru.donmai.us/posts?page=" + current_page + "&tags=" + Tag; // переопределение запроса 
                return url;
            }
        }



        public override List<string> getPics() // переопределение метода на получение листа ссылок
        {
            List<string> pic_list = new List<string>();
            string html_0 = "data-file-url=" + '"'; // html код
            string _site_code = getPage();
            while (_site_code.Contains(html_0)) // пока страница содержит html код
            {
                string buffer = null;
                int index = 0;
                buffer = _site_code.Substring(html_0.Count() + _site_code.IndexOf(html_0)); // убираем все до ссылки на файл
                index = buffer.IndexOf('"');
                _site_code = buffer.Substring(index);
                pic_list.Add(buffer.Substring(0, buffer.IndexOf('"')).Replace("https", "http")); // так как могут быть проблемы с защищеным соединением, то нужна замена https на http
            }
            return pic_list;
        }

    }
}
