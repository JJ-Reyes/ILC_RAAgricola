using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Cabana.Campo.RAAgricola.Pre.Web
{
    public static class MisObjetos
    {
        public static void Convertir<T>(this DataColumn column, Func<object, T> conversion)
        {
            foreach (DataRow row in column.Table.Rows)
            {
                row[column] = conversion(row[column]);
            }
        }
    }
}