using AutoWeb.Browsers;
using AutoWeb.Example.Pages.StarRepository;

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
