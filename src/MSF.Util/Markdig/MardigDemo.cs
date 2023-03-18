using Markdig;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Util.Markdig
{
    public static class MarkdigDemo
    {
        public static void StringToHtml()
        {
            var markdown = @"# Hello, World!
This is a sample of *Markdown* text.

- This is a bullet point
- Another bullet point

1. This is a numbered list
2. Another item in the list

> Citação: blabalbalbalbablal

`Code: Markdig.Markdown.ToHtml(string)`

Link: You can even include [Medium](https://medium.com/@abnoanmuniz/10-hidden-gems-for-net-developers-afefc140d41b)";


            var html = Markdown.ToHtml(markdown);
            Console.WriteLine(html);

            var text = Markdown.ToPlainText(markdown);
            Console.WriteLine(text);
        }
    }
}
