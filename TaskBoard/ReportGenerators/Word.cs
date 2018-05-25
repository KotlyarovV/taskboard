using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskBoard.Models;
using WordRef = Microsoft.Office.Interop.Word;

namespace TaskBoard.ReportGenerators
{
    public static class Word
    {
        private static WordRef.Application wdApp;

        public static void Export(IEnumerable<OrderModel> orderModels)
        {
            List<OrderModel> orders = orderModels.ToList();
            wdApp = new WordRef.Application();
            wdApp.Visible = true;
            wdApp.Documents.Add();
            WordRef.Document docum = wdApp.Documents.get_Item(1);
            var wdTable = docum.Tables.Add(docum.Range(0, 0), orders.Count + 2, 2);
            wdTable.Cell(1, 1).Range.Text = "Заголовок";
            wdTable.Cell(1, 2).Range.Text = "Описание";
            for (int i = 0; i < orders.Count; i++)
            {
                wdTable.Cell(i + 3, 1).Range.Text = orders[i].Header;
                wdTable.Cell(i + 3, 2).Range.Text = orders[i].Description;
            }
            docum.SaveAs("orders.docx");
        }
    }
}
