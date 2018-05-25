using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskBoard.Models.User;
using ExcelRef = Microsoft.Office.Interop.Excel;

namespace TaskBoard.ReportGenerators
{
    public static class Excel
    {
        private static ExcelRef.Application exApp;

        public static void Export(IEnumerable<UserModel> userModels)
        {
            exApp = new ExcelRef.Application();
            exApp.Visible = true;
            exApp.SheetsInNewWorkbook = 1;
            exApp.Workbooks.Add();

           ExcelRef.Worksheet exWrkSht = (ExcelRef.Worksheet) exApp.Workbooks[1].Worksheets.get_Item(1);

            List<UserModel> users = userModels.ToList();

            for (int x = 0; x < users.Count(); x++)
            {
                exWrkSht.Cells[x + 1, 1] = users[x].Login;
                exWrkSht.Cells[x + 1, 2] = users[x].Email;
                exWrkSht.Cells[x + 1, 3] = users[x].Name;
            }

            exApp.Workbooks[1].SaveAs("users.xlsx");
        }
    }
}
