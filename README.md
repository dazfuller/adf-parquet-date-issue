# ADF Date issue

When using an Azure Data Factory copy activity to move data from Parquet files to Azure SQL DB, where the source type of the DataFrame is a java.sql.Date type the date arrives in the destination SQL table with 1969 years added. This seems to be because of the way Date and Timestamp values are treated.

This repo provides the following:

* DotNetCore application using Parquet.NET which reads the parquet file and outputs the Timestamp and Date values
* Screenshots in the /media directory of output from various solutions such as Python and Parquet Viewer on Windows.
