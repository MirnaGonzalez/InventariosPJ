using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventariosPJEH.CNegocios
{
    public class CComparatorResguardante : IEqualityComparer<CDatosResguardante>
    {
        public bool Equals(CDatosResguardante r1, CDatosResguardante r2 )
        {
            if (r1 == null && r2 == null)
                return true;
            else if (r1 == null || r2 == null)
                return false;
            else if (r1.intOIdEmpleado == r2.intOIdEmpleado)
                return true;
            else
                return false;
        }

        public int GetHashCode(CDatosResguardante r)
        {
            int hCode = r.intOIdEmpleado;
            return hCode.GetHashCode();
        }
    }
}