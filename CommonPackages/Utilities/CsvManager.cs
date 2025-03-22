using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using log4net;

namespace CommonPackages.Utilities
{
    public class CsvManager
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CsvManager));

        public static void Write(DataTable dataTable, string filePath, CsvConfiguration config = null)
        {
            Logger.Info($"Writing data as CSV to {filePath}");

            config = config ?? new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, config))
            {
                var headings = dataTable.Columns;
                foreach (var heading in headings)
                {
                    csv.WriteField(heading);
                }

                csv.NextRecord();
                foreach (var row in dataTable.Rows)
                {
                    foreach (var heading in headings)
                    {
                        csv.WriteField(((DataRow)row)[heading.ToString()]);
                    }

                    csv.NextRecord();
                }
            }

            Logger.Info($"Finished writing data as CSV file");
        }

        public static void Write(List<Dictionary<string, string>> dict, string filePath, CsvConfiguration config = null)
        {
            Logger.Info($"Writing data as CSV to {filePath}");

            config = config ?? new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, config))
            {
                var headings = new List<string>(dict.First().Keys);
                foreach (var heading in headings)
                {
                    csv.WriteField(heading);
                }

                csv.NextRecord();
                foreach (var item in dict)
                {
                    foreach (var heading in headings)
                    {
                        csv.WriteField(item[heading] ?? "");
                    }

                    csv.NextRecord();
                }
            }

            Logger.Info("Finished writing data as CSV file");
        }

        public static List<T> Read<T>(string filePath, CsvConfiguration config = null)
        {
            Logger.Info($"Reading data from CSV file {filePath}");

            config = config ?? new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true,
                // set the header matching to case-insensitive
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };

            List<T> records;
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                records = csv.GetRecords<T>().ToList();
            }

            Logger.Info($"Finished read CSV data, got {records.Count} records");
            return records;
        }
    }
}