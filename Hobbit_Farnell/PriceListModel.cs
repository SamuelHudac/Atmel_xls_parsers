using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hobbit_Farnell.PriceListModel;

namespace Hobbit_Farnell
{
    public class PriceListModel: IEnumerable<PriceListModels>
    {
        public IEnumerator<PriceListModels> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public class PriceListModels
        {
            public string Comment;
            public string Description;
            public string Designator;
            public string Manufacture;
            public int Quantity;
            public string SupplierId;
            public string UniqueIdName;
        }
        
    }
}
