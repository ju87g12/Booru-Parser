using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booru_Pareser
{
    class Yande_re : Konachan
    {
        public Yande_re(string _tag) : base (_tag)
        {   }

        public override string Url
        {
            get
            {
                url = "http://yande.re/post?page=" + current_page + "&tags=" + Tag; // Yande.re полностью аналогичен Konachan за исключенем запроса
                return url;
            }
        }
    }
}
