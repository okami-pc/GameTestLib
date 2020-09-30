using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TicTacToe.NewFolder {

    public class Class1 {

        [DbColAttr("LastName")]
        public string name { get; set; }

        [DbColAttr("FirstName")]
        public string fname { get; set; }

        [DbColAttr("Address")]
        public string street { get; set; }

    }

    public class DbColAttr : Attribute {
        public string ColumnName { get; set; }
        public DbColAttr(string colName) : base() {
            ColumnName = colName;
        }

        public static void SetValues<T>(T o, Dictionary<string, object> data) {
            Type oType = o.GetType();

            foreach(PropertyInfo pi in oType.GetProperties()) {
                DbColAttr? ca = pi.GetCustomAttribute<DbColAttr>();
                if (ca != null) {
                    pi.SetValue(o, data[ca?.ColumnName]);
                }
            }
        }
    }

}
