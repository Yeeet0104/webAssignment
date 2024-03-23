using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webAssignment.Admin.Layout
{
    public interface IFilterable
    {
        void FilterListView( string searchTerm );
    }
}
