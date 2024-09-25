using FullstackOpdracht.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullstackOpdracht.Util.PDF.Interfaces
{
    public interface ICreatePDF
    {
        abstract MemoryStream CreatePDFDocumentAsync(Ticket tickets, Match match, string logoPath, string username, string section);

        abstract MemoryStream CreatePDFDocumentMembership(Membership membership, string section, string teamName, string logoPath, string username);
    }
}
