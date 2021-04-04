using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb.Common
{
    internal struct WhereClause
    {
        public Where Where { get; set; }

        public string Path { get; set; }


        internal WhereClause(Where where, string path)
        {
            this.Where = where;
            this.Path = path;
        }
    }
}
