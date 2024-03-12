using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.data
{
   public interface IDataAcc
    {
        Task<IEnumerable<T>> GetData<T, P>(string query, P parametetrs, string connectionid = "key");
        Task SaveData<P>(string query, P parametetrs, string connectionid = "key");
        Task<int> SaveDataAndGetId<P>(string query, P parameters, string connectionid = "key");
    }
}
