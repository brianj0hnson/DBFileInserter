using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class ColumnModel
    {
        public string Schema { get; set; }
        public string TableName { get; set; }
        public string AppConfigColumnName { get; set; }
        public string CSVColumnName { get; set; }
        public string SQLType { get; set; }
        public string Value { get; set; }

        public ColumnModel(string schema, string tableName, string appConfigColumnName, string csvColumnName, string sqlType, string value)
        {
            Schema = schema;
            TableName = tableName;
            AppConfigColumnName = appConfigColumnName;
            CSVColumnName = csvColumnName;
            SQLType = sqlType;
            Value = value;
        }
        override public string ToString()
        {
            return $"{Schema}, {TableName}, {AppConfigColumnName}, {CSVColumnName}, {SQLType}, {Value}";
        }

    }
}

