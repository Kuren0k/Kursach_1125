using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach_1125.VM
{
    internal class MainWindowMvvm : BaseVM
    {
        public CommandMvvm PageNavigation { get; set; }

        public CommandMvvm PageNavigationH { get; set; }
        public CommandMvvm PageNavigationF { get; set; }

        public MainWindowMvvm()
        {
            PageNavigation = new CommandMvvm(() =>
            {

            });

            PageNavigationH = new CommandMvvm(() =>
            {

            });
        }
    }
}
