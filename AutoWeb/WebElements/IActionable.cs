using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb.WebElements
{
    public interface IActionable
    {
        IActionable PressEnter();
        IActionable Wait(int milliseconds);
        IActionable Submit();
    }
}
