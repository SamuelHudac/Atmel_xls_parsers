using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hobbit_Farnell
{
    public class GeneratedPriceListModel
    {
        public string Comment;
        public string Description;
        public string Designator;
        public string Manufacture;
        public int Quantity;
        public string SupplierId;
        public string UniqueIdName;
        public string DatasheetUrl;
        public string Id;
        public double InStock;
        public int MinimumOrder;
        public float PerOneSeries;
        public float FirstChooseSeries;
        public float SecondChooseSeries;
        public float ThirdChooseSeries;
        public float FourthChooseSeries;
        public string CreatedDate;
        public string info;
    }

    public class Prices
    {
        public decimal from;
        public decimal to;
        public float cost;
    }
}
