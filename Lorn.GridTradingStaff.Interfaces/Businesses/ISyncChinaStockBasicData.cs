using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using Security.DataModels;

namespace Lorn.GridTradingStaff.Interfaces.Businesses
{
    public interface ISyncChinaStockBasicData:IGetData<ChinaStock>
    {
    }
}
