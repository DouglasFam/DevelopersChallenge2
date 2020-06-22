using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Xayah.App.ViewModels
{
    public class TransactionViewModel
    {
        [Key]
        public int Id { get; set; }

        public TransactionTypeViewModel TRNTYPE { get; set; }

        public DateTime DTPOSTED { get; set; }

        public string TRNAMT { get; set; }

        public string MEMO { get; set; }
    }
}
