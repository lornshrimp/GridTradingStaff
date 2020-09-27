using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Security.DataModels;

namespace Lorn.GridTradingStaff.DataModels
{
    public class OnsitePrice:BasicPrice
    {
        private decimal openPrice;
        private decimal highPrice;
        private decimal lowPrice;

        [Required]
        public decimal OpenPrice { get => openPrice; set => SetProperty(ref openPrice,value); }
        [Required]
        public decimal HighPrice { get => highPrice; set => SetProperty(ref highPrice, value); }
        [Required]
        public decimal LowPrice { get => lowPrice; set => SetProperty(ref lowPrice, value); }
        protected override void RefreshDataInternal(DataBase data)
        {
            base.RefreshDataInternal(data);
            var newData = data as OnsitePrice;
            this.OpenPrice = newData.OpenPrice;
            this.HighPrice = newData.HighPrice;
            this.LowPrice = newData.LowPrice;
        }
    }
}
