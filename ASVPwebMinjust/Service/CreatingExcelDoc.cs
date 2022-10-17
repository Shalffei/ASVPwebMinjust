using ASVPwebMinjust.Models.ModelsForPostRequest;
using ASVPwebMinjust.Models.ModelsForResponse;
using ClosedXML;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASVPwebMinjust.Service
{
    public class CreatingExcelDoc
    {
        public void CreateExcel(List<Result> results, string worksheetName, int listPosition, string path, string docName)
        {
            bool fileExist = File.Exists(path += docName += ".xlsx");
            if (fileExist == true)
            {
                using (var workbook = new XLWorkbook(path))
                {
                    Recording(results, workbook, worksheetName, listPosition);
                    workbook.Save();
                }
            }
            else
            {
                using (var workbook = new XLWorkbook())
                {
                    Recording(results, workbook, worksheetName, listPosition);
                    workbook.SaveAs(path);
                }
            }
            
        }
        private XLWorkbook Recording (List<Result> results, XLWorkbook workbook, string worksheetName, int listPosition)
        {
            var worksheet = workbook.Worksheets.Add(worksheetName, listPosition);
            var currentRow = 1;
            #region Header
            worksheet.Cell(currentRow, 1).Value = "№ Виконавчого провадження";
            worksheet.Cell(currentRow, 2).Value = "Орган ДВС / приватний виконавець";
            worksheet.Cell(currentRow, 3).Value = "ЭДРПОУ";
            worksheet.Cell(currentRow, 4).Value = "email";
            worksheet.Cell(currentRow, 5).Value = "тел";
            worksheet.Cell(currentRow, 6).Value = "Дата відкриття провадження";
            worksheet.Cell(currentRow, 7).Value = "Стан виконавчого провадження";
            worksheet.Cell(currentRow, 8).Value = "Реквізити органу ДВС/приватного виконавця";
            worksheet.Cell(currentRow, 9).Value = "Боржник";
            worksheet.Cell(currentRow, 10).Value = "ПІБ/Назва(для юр. осіб)";
            worksheet.Cell(currentRow, 11).Value = "Дата народження";
            worksheet.Cell(currentRow, 12).Value = "Стягувач";
            worksheet.Cell(currentRow, 13).Value = "ПІБ/Назва(для юр. осіб)";
            worksheet.Cell(currentRow, 14).Value = "ЄДРПОУ";
            #endregion
            #region Body
            foreach (var result in results)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = result.orderNum;
                worksheet.Cell(currentRow, 2).Value = result.depStr;
                worksheet.Cell(currentRow, 3).Value = result.depEdrpou;
                worksheet.Cell(currentRow, 4).Value = result.depEmail;
                worksheet.Cell(currentRow, 5).Value = result.depPhone;
                worksheet.Cell(currentRow, 6).Value = result.beginDate;
                worksheet.Cell(currentRow, 7).Value = result.mi_wfStateWithError;
                if (result.depAccounts.Count == 0 || result.depAccounts == null)
                {

                }
                else
                {
                    foreach (var account in result.depAccounts)
                    {
                        string enter = "\n";
                        string spase = " ";
                        worksheet.Cell(currentRow, 8).Value += account.bankName += spase += account.iban += enter;
                    }
                }
                foreach (var debitor in result.debtors)
                {
                    string enter = "\n";
                    string spase = " ";
                    if (result.debtors.Count > 1)
                    {

                    }
                    worksheet.Cell(currentRow, 9).Value = debitor.personSubTypeString;
                    worksheet.Cell(currentRow, 10).Value = debitor.lastName += spase += debitor.firstName += spase += debitor.middleName;
                    worksheet.Cell(currentRow, 11).Value = debitor.birthDate;
                }
                foreach (var creditor in result.creditors)
                {
                    if (result.creditors.Count > 1)
                    {

                    }
                    worksheet.Cell(currentRow, 12).Value = creditor.personSubTypeString;
                    worksheet.Cell(currentRow, 13).Value = creditor.name;
                    worksheet.Cell(currentRow, 14).Value = creditor.edrpou;
                }
                
            }
            #endregion
            worksheet.Columns().AdjustToContents();
            worksheet.Columns().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Top);
            worksheet.Columns().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
            return workbook;
        }
    }
}