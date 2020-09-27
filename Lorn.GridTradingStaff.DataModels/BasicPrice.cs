using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Security.DataModels;

namespace Lorn.GridTradingStaff.DataModels
{
    public class BasicPrice:DataBase
    {
        private DateTime priceTime;
        private decimal closePrice;
        private Guid stockId;
        private SecurityInfo stock;
        /// <summary>
        /// 价格时间
        /// </summary>
        [Key]
        public DateTime PriceTime { get => priceTime; set => SetProperty(ref priceTime,value); }
        /// <summary>
        /// 收盘价或累计净值
        /// </summary>
        [Required]
        public decimal ClosePrice { get => closePrice; set => SetProperty(ref closePrice,value); }
        /// <summary>
        /// 证券Id
        /// </summary>
        [Required]
        public Guid StockId { get => stockId; set => SetProperty(ref stockId,value); }
        /// <summary>
        /// 证券
        /// </summary>
        [ForeignKey("StockId")]
        public SecurityInfo Stock { get => stock; set { SetProperty(ref stock, value);this.StockId = value.Id; } }
        protected override void RefreshDataInternal(DataBase data)
        {
            base.RefreshDataInternal(data);
            var newData = data as BasicPrice;
            this.PriceTime = newData.PriceTime;
            this.ClosePrice = newData.ClosePrice;
            this.StockId = newData.StockId;
            this.Stock = newData.Stock;
        }
    }
}
