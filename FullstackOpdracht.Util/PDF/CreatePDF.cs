using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Util.PDF.Interfaces;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace FullstackOpdracht.Util.PDF
{
    public class CreatePDF : ICreatePDF
    {
        private static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }


        public MemoryStream CreatePDFDocumentAsync(Ticket ticket, Domains.Entities.Match match, string logoPath, string username, string section)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(stream);
                PdfDocument pdf = new PdfDocument(writer);
                iText.Layout.Document document = new iText.Layout.Document(pdf);


                document.Add(new Paragraph("Ticket").SetFontSize(20));
                document.Add(new Paragraph("eigenaar: " + username));
                document.Add(new Paragraph("Stoelnummer: " + ticket.SeatId).SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA)).SetFontSize(16).SetFontColor(ColorConstants.BLUE));
                document.Add(new Paragraph("in vak: " + section).SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA)).SetFontSize(16).SetFontColor(ColorConstants.BLUE));
                document.Add(new Paragraph("Datum: " + match.MatchDate));
                document.Add(new Paragraph("Voor match: " + match.HomeTeam.Name + " vs " + match.AwayTeam.Name));

                //QR-Code
                // Binnen de GeneratePdf methode
                string qrContent = ticket.Id.ToString();
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(10);
                iText.Layout.Element.Image qrCodeImageElement = new
                iText.Layout.Element.Image(ImageDataFactory.Create(BitmapToBytes(qrCodeImage))).SetHorizontalAlignment(HorizontalAlignment.CENTER);
                document.Add(qrCodeImageElement);


                document.Close();
                return new MemoryStream(stream.ToArray());
            }
        }

        public MemoryStream CreatePDFDocumentMembership(Membership membership, string section, string teamName, string logoPath, string username)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(stream);
                PdfDocument pdf = new PdfDocument(writer);
                iText.Layout.Document document = new iText.Layout.Document(pdf);


                document.Add(new Paragraph("Abonnement").SetFontSize(20));
                document.Add(new Paragraph("eigenaar: " + username));
                document.Add(new Paragraph("Stoelnummer: " + membership.SeatId).SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA)).SetFontSize(16).SetFontColor(ColorConstants.BLUE));
                document.Add(new Paragraph("in vak: " + section).SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA)).SetFontSize(16).SetFontColor(ColorConstants.BLUE));
                document.Add(new Paragraph("Voor ploeg: " + teamName));

                //QR-Code
                // Binnen de GeneratePdf methode
                string qrContent = membership.Id.ToString();
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(10);
                iText.Layout.Element.Image qrCodeImageElement = new
                iText.Layout.Element.Image(ImageDataFactory.Create(BitmapToBytes(qrCodeImage))).SetHorizontalAlignment(HorizontalAlignment.CENTER);
                document.Add(qrCodeImageElement);


                document.Close();
                return new MemoryStream(stream.ToArray());
            }
        }

    }
}
