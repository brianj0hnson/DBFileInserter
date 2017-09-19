using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Specialized;
using Helper;

namespace Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string configPath;
            string csvPath;
            bool csvHeader;

            if (args.Length == 0)
            {
                Console.WriteLine("Not yet implemented.");
            }
            if (args.Length == 3)
            {
                configPath = args[0];
                csvPath = args[1];
                var csvheader = args[2];

                csvHeader = csvheader.ToLower().Equals("y");

                if (String.IsNullOrWhiteSpace(configPath) ||
                    !File.Exists(configPath))
                {
                    Console.WriteLine("The config file does not exist.");
                    return;
                }
                if (String.IsNullOrWhiteSpace(csvPath) ||
                    !File.Exists(csvPath))
                {
                    Console.WriteLine("The csv file does not exist.");
                    return;
                }
                if (!csvHeader && !csvheader.ToLower().Equals("n"))
                {
                    Console.WriteLine("Last argument has to be either Y or N.");
                    return;
                }




                try
                {
                    AppConfig.Change(configPath);

                    var cm = new ConfigModel();

                    var sqlConnection = EstablishConnection(Properties.Settings.Default.Server, Properties.Settings.Default.Database, Properties.Settings.Default.Username, Properties.Settings.Default.Password);
                    if (sqlConnection == null)
                        Console.WriteLine("Bad server credentials.");

                    cm.SQLConnection = sqlConnection;

                    cm.FilesPath = Properties.Settings.Default.FilesPath;

                    cm.FileTableSchema = Properties.Settings.Default.FileTableSchema;
                    cm.FileTableName = Properties.Settings.Default.FileTableName;
                    cm.FileTableColumns = new List<string>();
                    foreach (string str in Properties.Settings.Default.FileTableColumns)
                        cm.FileTableColumns.Add(str);

                    cm.DetailTableSchema = Properties.Settings.Default.DetailTableSchema;
                    cm.DetailTableName = Properties.Settings.Default.DetailTableName;
                    cm.DetailTableColumns = new List<string>();
                    foreach (string str in Properties.Settings.Default.DetailTableColumns)
                        cm.DetailTableColumns.Add(str);


                }
                catch (Exception exc)
                {
                    throw exc;
                }

            }
            else
            {
                Console.Write("Arguments:\n0: path to config file\n1: path to csv\n2: csvheader\t\t(Y/N)\n");
                return;
            }


            try
            {
                var affectedrows = -1;

                //affectedrows = Inserter.Insert(configPathInput, dataPathInput, cm, csvHeader);

                if (affectedrows > -1)
                    Console.WriteLine($"Total rows affected: {affectedrows}");

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public static List<ColumnModel> CollectColumns(string configPath, List<string> columns, bool csvheader)
        {
            if (csvheader)
            {

            }


            return null;
        }

        public static SqlConnection EstablishConnection(string server, string database, string username, string password)
        {
            var connectionString = $"Server={Properties.Settings.Default.Server};Database={Properties.Settings.Default.Database};User Id={Properties.Settings.Default.Username};Password={Properties.Settings.Default.Password};";
            var sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();
            }
            catch (Exception)
            {
                return null;
            }

            sqlConnection.Close();
            return sqlConnection;
        }
    }
}
