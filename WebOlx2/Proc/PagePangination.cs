﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebOlx2.Proc
{
    public static class PagePangination
    {
        public static string GetPangination(int CurPage, int MaxPage, string Query, string Page1Url = "")
        {
            StringBuilder PangLi = new StringBuilder();

            const int MaxCel = 8;                          //всего показано страниц
            const int JumpCel = 6;                          //кол-во страниц для перехода в крайнии положения
            const int IndentCel = (int)(MaxCel / 2.0) - 1;    //отступ с лева и справа

            //string Query = @"/page/";

            //string Page1Url = "";
            //if(Query.Contains("&page")) Page1Url = "1";

            if (MaxPage == 1)
            {
                PangLi.AppendLine(@"<li class=""PangCurPage""><span>1</span></li>");
                return PangLi.ToString();
            }

            if (MaxPage <= 10)
            {
                if (CurPage == 1)
                {
                    PangLi.AppendLine(@"<li class=""PangCurPage""><span>1</span></li>");
                    for (int i = 2; i <= MaxPage; i++)
                        PangLi.AppendLine($@"<li><a href=""{Query}{i}"">{i}</a></li>");
                }

                if (CurPage > 1 && CurPage != MaxPage)
                {
                    //создаем ссылки до текущай страницы
                    PangLi.AppendLine($@"<li><a href=""{Query}{Page1Url}"">1</a></li>");
                    for (int i = 2; i < CurPage; i++)
                        PangLi.AppendLine($@"<li><a href=""{Query}{i}"">{i}</a></li>");

                    //текущаая страница
                    PangLi.AppendLine($@"<li class=""PangCurPage""><span>{CurPage}</span></li>");

                    //добавляем остаток
                    for (int i = CurPage + 1; i <= MaxPage; i++)
                        PangLi.AppendLine($@"<li><a href=""{Query}{i}"">{i}</a></li>");
                }
                if (CurPage == MaxPage)
                {
                    //создаем ссылки до текущай страницы
                    PangLi.AppendLine($@"<li><a href=""{Query}{Page1Url}"">1</a></li>");
                    for (int i = 2; i < MaxPage; i++)
                        PangLi.AppendLine($@"<li><a href=""{Query}{i}"">{i}</a></li>");

                    //текущаая страница
                    PangLi.AppendLine($@"<li class=""PangCurPage""><span>{CurPage}</span></li>");
                }
                return PangLi.ToString();
            }

            //самое начало
            if (CurPage == 1)
            {
                PangLi.AppendLine(@"<li><span>&lt;&lt; Назад</span></li>");
                PangLi.AppendLine(@"<li class=""PangCurPage""><span>1</span></li>");

                for (int i = 2; i <= MaxCel; i++)
                    PangLi.AppendLine($@"<li><a href=""{Query}{i}"">{i}</a></li>");


                PangLi.AppendLine(@"<li><span>...</span></li>");
                PangLi.AppendLine($@"<li><a href=""{Query}{MaxPage}"">{MaxPage}</a></li>");
                PangLi.AppendLine($@"<li><a href=""{Query}{CurPage + 1}"">Вперед &gt;&gt; </a></li>");

                return PangLi.ToString();
            }

            //все еще в начале
            if (CurPage > 1 && CurPage < JumpCel)
            {
                //создаем кнопка назад
                PangLi.AppendLine($@"<li><a href=""{Query}{CurPage - 1}"">&lt;&lt; Назад</a></li>");

                //создаем ссылки до текущай страницы
                PangLi.AppendLine($@"<li><a href=""{Query}{Page1Url}"">1</a></li>");
                for (int i = 2; i < CurPage; i++)
                    PangLi.AppendLine($@"<li><a href=""{Query}{i}"">{i}</a></li>");

                //текущаая страница
                PangLi.AppendLine($@"<li class=""PangCurPage""><span>{CurPage}</span></li>");

                //добавляем остаток
                for (int i = CurPage + 1; i <= MaxCel; i++)
                    PangLi.AppendLine($@"<li><a href=""{Query}{i}"">{i}</a></li>");

                //+ кнопка Вперед
                PangLi.AppendLine(@"<li><span>...</span></li>");
                PangLi.AppendLine($@"<li><a href=""{Query}{MaxPage}"">{MaxPage}</a></li>");
                PangLi.AppendLine($@"<li><a href=""{Query}{CurPage + 1}"">Вперед &gt;&gt; </a></li>");

                return PangLi.ToString();
            }

            //серидина
            if (CurPage >= JumpCel && CurPage < (MaxPage - JumpCel))
            {
                //создаем кнопку назад
                PangLi.AppendLine($@"<li><a href=""{Query}{CurPage - 1}"">&lt;&lt; Назад</a></li>");
                PangLi.AppendLine($@"<li><a href=""{Query}{Page1Url}"">1</a></li>");
                PangLi.AppendLine(@"<li><span>...</span></li>");

                //отступаем в лево от текущей страницы
                for (int i = CurPage - IndentCel; i < CurPage; i++)
                    PangLi.AppendLine($@"<li><a href=""{Query}{i}"">{i}</a></li>");

                //текущаая страница
                PangLi.AppendLine($@"<li class=""PangCurPage""><span>{CurPage}</span></li>");

                //отступаем в право от текущей страницы
                for (int i = CurPage + 1; i <= CurPage + IndentCel; i++)
                    PangLi.AppendLine($@"<li><a href=""{Query}{i}"">{i}</a></li>");

                //+ кнопка Вперед
                PangLi.AppendLine(@"<li><span>...</span></li>");
                PangLi.AppendLine($@"<li><a href=""{Query}{MaxPage}"">{MaxPage}</a></li>");
                PangLi.AppendLine($@"<li><a href=""{Query}{CurPage + 1}"">Вперед &gt;&gt; </a></li>");

                return PangLi.ToString();
            }

            //почти в конце
            if (CurPage >= (MaxPage - JumpCel) && CurPage != MaxPage)
            {
                //создаем кнопку назад
                PangLi.AppendLine($@"<li><a href=""{Query}{CurPage - 1}"">&lt;&lt; Назад</a></li>");
                PangLi.AppendLine($@"<li><a href=""{Query}{Page1Url}"">1</a></li>");
                PangLi.AppendLine(@"<li><span>...</span></li>");


                //создаем ссылки до текущай страницы
                for (int i = MaxPage - MaxCel; i < CurPage; i++)
                    PangLi.AppendLine($@"<li><a href=""{Query}{i}"">{i}</a></li>");

                //текущаая страница
                PangLi.AppendLine($@"<li class=""PangCurPage""><span>{CurPage}</span></li>");

                //добавляем остаток
                for (int i = CurPage + 1; i <= MaxPage; i++)
                    PangLi.AppendLine($@"<li><a href=""{Query}{i}"">{i}</a></li>");

                //+ кнопка Вперед
                PangLi.AppendLine($@"<li><a href=""{Query}{CurPage + 1}"">Вперед &gt;&gt; </a></li>");

                return PangLi.ToString();
            }

            //в самом конце
            if (CurPage == MaxPage)
            {
                //создаем кнопку назад
                PangLi.AppendLine($@"<li><a href=""{Query}{CurPage - 1}"">&lt;&lt; Назад</a></li>");
                PangLi.AppendLine($@"<li><a href=""{Query}{Page1Url}"">1</a></li>");
                PangLi.AppendLine(@"<li><span>...</span></li>");


                //создаем ссылки до текущай страницы
                for (int i = MaxPage - MaxCel; i < CurPage; i++)
                    PangLi.AppendLine($@"<li><a href=""{Query}{i}"">{i}</a></li>");

                //текущаая страница
                PangLi.AppendLine($@"<li class=""PangCurPage""><span>{CurPage}</span></li>");

                //+ кнопка Вперед
                PangLi.AppendLine($@"<li><span>Вперед &gt;&gt; </span></li>");

                return PangLi.ToString();
            }
            return PangLi.ToString();
        }



    }
}