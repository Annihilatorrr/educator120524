using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTMLQuestPDF.Extensions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Educator.ExercisesGenerators
{
    public class PdfDocumentCreator:IDocumentCreator
    {
        public byte[] Save(string fileName, string content)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Content().Column(col =>
                    {
                        col.Item().HTML(handler =>
                        {
                            handler.SetHtml(content);
                        });
                    });
                });
            }).GeneratePdf($"output/{fileName}");

            return File.ReadAllBytes($"output/{fileName}");
        }
    }
}
