using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaMeeting.Functions.Interfaces
{
    internal interface IShowData<T, K>
    {
        void ShowOneItem(T data);
        void ShowAllItems(K data);
        void ShowNamesIndexes(K data);
    }
}
