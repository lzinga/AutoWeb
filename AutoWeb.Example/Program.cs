using AutoWeb.Example.Pages.StarRepository;
using System;

namespace AutoWeb.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            new PageCollection()
                    .AddPage<LoginPage>()
                    .AddPage<RepositoryPage>()
                    .Execute();
        }
    }
}
