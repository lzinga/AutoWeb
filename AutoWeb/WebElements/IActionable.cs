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
        IActionable WaitForElement(Where where, string value);

        IActionable WaitToBeInteractable() => WaitToBeInteractable(new TimeSpan(0, 0, 30));
        IActionable WaitToBeInteractable(TimeSpan timeout);
        IActionable Submit();
    }
}
