using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Manager.Models
{
    public class SelectResultType
    {
        public SelectResultType() { }

        public int Id { get; set; }
        public string ResultType { get; set; }

        public SelectResultType(string resultType)
        {
            ResultType = resultType;
        }
    }
}
