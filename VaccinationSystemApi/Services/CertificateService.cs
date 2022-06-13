using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Collections.Generic;
using System.IO;
using VaccinationSystemApi.Models;

namespace VaccinationSystemApi.Services
{
    public interface ICertificateGeneratorService
    {
        byte[] Generate(List<Appointment> appointments);
    }

    public class CertificateGeneratorService : ICertificateGeneratorService
    {
        public byte[] Generate(List<Appointment> appointments)
        {
            var patient = appointments[0].Patient_;

            var documentBytes = new MemoryStream();
            var pdfDocument = new PdfDocument(new PdfWriter(documentBytes));
            var document = new Document(pdfDocument);

            var header = "Vaccination Certificate";
            document.Add(new Paragraph(
                new Text(header)
                    .SetBold()
                    .SetFontSize(24)
                    .SetUnderline())
                .SetTextAlignment(TextAlignment.CENTER)
            );

            var baseInformation = new Paragraph();
            baseInformation.Add(new Text("First name:  ").SetBold());
            baseInformation.Add(new Text($"{patient.FirstName}\n"));
            baseInformation.Add(new Text("Last name:  ").SetBold());
            baseInformation.Add(new Text($"{patient.LastName}\n"));
            baseInformation.Add(new Text("PESEL:  ").SetBold());
            baseInformation.Add(new Text($"{patient.Pesel}\n"));
            document.Add(baseInformation);

            document.Add(new Paragraph());

            var vaccinesTable = new Table(2);
            vaccinesTable.AddCell(new Paragraph(
                new Text("Vaccine").SetBold()
            ));
            vaccinesTable.AddCell(new Paragraph(
                new Text("Date").SetBold()
            ));
            foreach (var visit in appointments)
            {
                vaccinesTable.AddCell(visit.Vaccine_.Name);
                vaccinesTable.AddCell(visit.TimeSlot_.From.ToString("dd-MM-yyyy"));
            }
            document.Add(
                vaccinesTable.SetWidth(new UnitValue(UnitValue.PERCENT, 100))
            );

            document.Close();
            return documentBytes.ToArray();
        }
    }
}