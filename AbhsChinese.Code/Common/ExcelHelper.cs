using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace AbhsChinese.Code.Common
{
    public class ExcelHelper
    {
        /// <summary>
        /// /注：分浏览器进行编码（IE必须编码，FireFox不能编码，Chrome可编码也可不编码）
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sheetName"></param>
        /// <param name="filePath"></param>
        public static void ExportByWeb(IList list, string sheetName, string filePath)
        {
            DataTable dt = new DataTable();
            dt = ToDataTable(list);
            HttpContext context = System.Web.HttpContext.Current;
            context.Response.ContentType = "application/vnd.ms-excel";
            context.Response.Charset = "";
            if (context.Request.UserAgent.ToLower().IndexOf("firefox", System.StringComparison.Ordinal) > 0)
            {
                context.Response.AppendHeader("Content-Disposition", "attachment;filename=" + filePath);
            }
            else
            {
                context.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(filePath, System.Text.Encoding.UTF8));
            }

            //  curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" +strFileName);
            context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            context.Response.BinaryWrite(ExportToExcel(dt, sheetName).GetBuffer());
            context.Response.End();
        }

        /// <summary>
        /// 由DataTable导出Excel
        /// </summary>
        /// <param name="dt">要导出数据的DataTable</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel工作表</returns>
        private static MemoryStream ExportToExcel(DataTable dt, string sheetName)
        {
            IWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            string[] sheetNames = sheetName.Split(',');
            for (int i = 0; i < sheetNames.Length; i++)
            {
                ISheet sheet = workbook.CreateSheet(sheetNames[i]);

                #region Style
                ICellStyle style1 = workbook.CreateCellStyle();//样式
                style1.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;//文字水平对齐方式
                style1.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//文字垂直对齐方式
                                                                                      //设置边框
                style1.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                style1.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                style1.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                style1.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                style1.WrapText = true;//自动换行

                ICellStyle style2 = workbook.CreateCellStyle();//样式
                IFont font1 = workbook.CreateFont();//字体
                font1.FontName = "楷体";
                font1.Boldweight = (short)FontBoldWeight.Normal;//字体加粗样式
                style2.SetFont(font1);//样式里的字体设置具体的字体样式
                                      //设置背景色
                style2.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;//文字水平对齐方式
                style2.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//文字垂直对齐方式
                #endregion

                ICellStyle dateStyle = workbook.CreateCellStyle();//样式
                dateStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;//文字水平对齐方式
                dateStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//文字垂直对齐方式
                                                                                         //设置数据显示格式
                IDataFormat dataFormatCustom = workbook.CreateDataFormat();
                dateStyle.DataFormat = dataFormatCustom.GetFormat("yyyy-MM-dd HH:mm:ss");

                #region 列头
                IRow headerRow = sheet.CreateRow(0);
                ICellStyle headStyle = workbook.CreateCellStyle() as HSSFCellStyle;
                IFont font = workbook.CreateFont() as HSSFFont;
                font.FontHeightInPoints = 10;
                font.Boldweight = 700;
                headStyle.SetFont(font);

                //取得列宽
                int[] arrColWidth = new int[dt.Columns.Count];
                foreach (DataColumn item in dt.Columns)
                {
                    arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
                }

                // 处理列头
                foreach (DataColumn column in dt.Columns)
                {
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                    headerRow.GetCell(column.Ordinal).CellStyle = headStyle;
                    //设置列宽
                    sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);

                }
                #endregion

                #region 填充值
                int rowIndex = 1;
                foreach (DataRow row in dt.Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in dt.Columns)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }
                   
                    rowIndex++;
                    
                }
                #endregion
            }
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            workbook = null;
            return ms;
        }

        /// <summary>  
        /// 将集合类转换成DataTable  
        /// </summary>  
        /// <param name="list">集合</param>  
        /// <returns></returns>  
        private static DataTable ToDataTable(IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();

                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }
        
    }
}