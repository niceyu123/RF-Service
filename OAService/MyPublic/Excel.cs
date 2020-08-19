using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace OAService.MyPublic
{
    public class Excel
    {
        /// <summary>
        /// 生成EXCEL文件(.xlsx)
        /// </summary>
        /// <param name="data">待保存的数据</param>
        /// <param name="header">表头，为空时则无表头</param>
        /// <returns></returns>
        public static XSSFWorkbook GenerateXLSX(IList<List<string>> data, IList<string> header = null, string author = null, string sheetName = "Sheet1")
        {
            XSSFWorkbook xlsx = new XSSFWorkbook();
            ISheet sheet = xlsx.CreateSheet(sheetName);

            Int32 rowID = 0;
            // 创建表头
            if (header != null && 0 < header.Count)
            {
                IRow row = sheet.CreateRow(rowID);
                for (Int32 col = 0; col < header.Count; ++col)
                {
                    row.CreateCell(col).SetCellValue(header[col]);
                }
            }

            //填充数据
            foreach (var rowData in data)
            {
                rowID += 1;
                IRow row = sheet.CreateRow(rowID);
                for (Int32 col = 0; col < rowData.Count; ++col)
                {
                    //row.CreateCell(col).SetCellValue(rowData[col]);
                    //row.CreateCell(col).SetCellType(CellType.Numeric);
                    //如果为数字，转换为double型
                    if (rowData[col] != null
                        && Regex.IsMatch(rowData[col], @"^[+-]?/d*[.]?/d*$")
                        && !Regex.IsMatch(rowData[col], @"^(((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$ ")
                    )
                    {
                        row.CreateCell(col).SetCellType(CellType.Numeric);
                        row.CreateCell(col).SetCellValue(double.Parse(rowData[col]));
                    }
                    else
                    {
                        row.CreateCell(col).SetCellValue(rowData[col]);
                    }

                }
            }
            #region 设置作者
            if (!string.IsNullOrEmpty(author))
            {
                rowID += 2;
                IRow rowAuthor = sheet.CreateRow(rowID);
                IRow rowDate = sheet.CreateRow(++rowID);
                rowAuthor.CreateCell(data[0].Count - 2).SetCellValue("Author:");
                rowAuthor.CreateCell(data[0].Count - 1).SetCellValue(author);
                rowDate.CreateCell(data[0].Count - 2).SetCellValue("Date");
                rowDate.CreateCell(data[0].Count - 1).SetCellValue(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            }
            #endregion
            return xlsx;
        }
    }
}