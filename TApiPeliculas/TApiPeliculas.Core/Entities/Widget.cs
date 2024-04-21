using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TApiPeliculas.Core.Entities
{
    public class Widget : DynamicObject
    {
        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            result = binder.Name;
            return true;
        }
        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object? result)
        {
            if(indexes.Length == 1)
            {
                result = new string ('*',(int)indexes[0]);
            }
            return base.TryGetIndex(binder, indexes, out result);
        }
    }
}
