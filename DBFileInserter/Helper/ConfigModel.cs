using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace Helper
{
    public class ConfigModel
    {
        public SqlConnection SQLConnection { get; set; }
        public string FilesPath { get; set; }
        public string FileTableSchema { get; set; }
        public string FileTableName { get; set; }
        public List<string> FileTableColumns { get; set; }
        public string DetailTableSchema { get; set; }
        public string DetailTableName { get; set; }
        public List<string> DetailTableColumns { get; set; }

        public ConfigModel()
        {

        }
        public ConfigModel(SqlConnection sqlConnection, string filesPath, string fileTableSchema, string fileTableName, List<string> fileTableColumns, string detailTableSchema, string detailTableName, List<string> detailTableColumns)
        {
            SQLConnection = sqlConnection;

            FilesPath = filesPath;

            FileTableSchema = fileTableSchema;
            FileTableName = fileTableName;
            FileTableColumns = fileTableColumns;

            DetailTableSchema = detailTableSchema;
            DetailTableName = detailTableName;
            DetailTableColumns = detailTableColumns;
        }

    }
    
   
}
