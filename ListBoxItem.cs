using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiSafeAI
{
    public class ListBoxItem
    {
        public int Id {  get; set; }
        public DateTime ReportDate { get; set; }
        public string FilePath 
        {
            get;
            set; 
        }
        public override string ToString()
        {
            return $"{ReportDate:yyyy-MM-dd HH:mm:ss}";
        }
    }
}
