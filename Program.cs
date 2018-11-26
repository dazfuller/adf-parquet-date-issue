using System;
using System.IO;
using System.Linq;
using Parquet;
using Parquet.Attributes;

namespace sodemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var fs = File.OpenRead("data\\demo.parquet"))
            {
                using (var reader = new ParquetReader(fs))
                {
                    var dataFields = reader.Schema.GetDataFields();
                    
                    Console.WriteLine($"Column [{dataFields[1].Name}]: {dataFields[1].DataType}");

                    for (var i = 0; i < reader.RowGroupCount; i++)
                    {
                        using (var groupReader = reader.OpenRowGroupReader(i))
                        {
                            var columns = dataFields.Select(groupReader.ReadColumn).ToArray();

                            var tsColData = columns[1].Data;
                            var dtColData = columns[2].Data;

                            for (var j = 0; j < tsColData.Length; j++)
                            {
                                var tsValue = (DateTimeOffset)tsColData.GetValue(j);
                                var dtValue = (DateTimeOffset)dtColData.GetValue(j);
                                Console.WriteLine($"Timestamp: {tsValue.ToString("o")}\tDate: {dtValue.ToString("o")}");
                            }
                        }
                    }
                }
            }
        }
    }
}
