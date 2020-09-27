using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Security.DataModels;

namespace Lorn.GridTradingStaff.DataModels
{
    public class SecurityProviderInfo:DataBase
    {
        private DataProvider provider;
        private string providerKey;
        private Guid securityId;
        private SecurityInfo security;

        public DataProvider Provider { get => provider; set { SetProperty<DataProvider>(ref provider, value); } }
        public string ProviderKey { get => providerKey; set { SetProperty<string>(ref providerKey, value); } }
        [Required]
        public Guid SecurityId { get => securityId; set { SetProperty<Guid>(ref securityId, value); } }
        [NotMapped]
        public SecurityInfo Security { get => security; set { SetProperty<SecurityInfo>(ref security, value); } }

        protected override void RefreshDataInternal(DataBase data)
        {
            base.RefreshDataInternal(data);
            var newData = data as SecurityProviderInfo;
            this.Provider = newData.Provider;
            this.ProviderKey = newData.ProviderKey;
            this.SecurityId = newData.SecurityId;
            this.Security = newData.Security;
        }
    }

    public enum DataProvider
    {
        TuShare,
        NetEase,
        EastMoney,
        Sina,
        LocalDatabase
    }
}
