using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUWP.Interfaces
{
    public interface INavigation
    {
        void NavigateTo(string page, Object param = null);
        void Return();

    }
}
