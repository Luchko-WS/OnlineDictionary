using OnlineDictionary.Models;
using OnlineDictionary.ViewModels;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;
using System.Data.Entity;
using OnlineDictionary.Common;
using OfficeOpenXml;
using System.Net.Http.Headers;
using System.IO;

namespace OnlineDictionary.API
{
    [RoutePrefix("api/Dictionaries")]
    public class DictionariesController : BaseApiController
    {
        [Route("GetAllPublicDictionaries")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<dynamic> GetAllPublicDictionaries([FromUri]DictionaryFilterViewModel filter)
        {
            var query = _dbContext.Dictionaries.Where(d => d.IsPublic);
            query = PrepareQueryByFilter(query, filter);
            var dictionaries = await query.OrderByDescending(d => d.CreationDate).ToListAsync();
            return Request.CreateResponse(HttpStatusCode.OK, dictionaries);
        }

        [Route("GetMyDictionaries")]
        [HttpGet]
        public async Task<dynamic> GetMyDictionaries([FromUri]DictionaryFilterViewModel filter)
        {
            var query = _dbContext.Dictionaries.Where(d => d.OwnerId == User.Identity.Name);
            query = PrepareQueryByFilter(query, filter);
            var dictionaries = await query.OrderByDescending(d => d.CreationDate).ToListAsync();
            return Request.CreateResponse(HttpStatusCode.OK, dictionaries);
        }

        private IQueryable<Dictionary> PrepareQueryByFilter(IQueryable<Dictionary> query, DictionaryFilterViewModel filter)
        {
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    query = query.Where(d => d.Name.ToLower().Contains(filter.Name.ToLower()));
                }
                if (!string.IsNullOrEmpty(filter.SourceLanguage))
                {
                    query = query.Where(d => d.SourceLanguage.ToLower().Contains(filter.SourceLanguage.ToLower()));
                }
                if (!string.IsNullOrEmpty(filter.TargetLanguage))
                {
                    query = query.Where(d => d.TargetLanguage.ToLower().Contains(filter.TargetLanguage.ToLower()));
                }
                if (!string.IsNullOrEmpty(filter.OwnerId))
                {
                    query = query.Where(d => d.OwnerId.ToLower().Contains(filter.OwnerId.ToLower()));
                }
            }
            return query;
        }

        [Route("Dictionary/{dictionaryId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> GetDictionary([FromUri]PhrasesPairsFilterViewModel filter, Guid dictionaryId)
        {
            var dictionary = await _dbContext.Dictionaries
                .Include(d => d.PhrasesPairs)
                .Include(d => d.PhrasesPairs.Select(p => p.FirstPhrase))
                .Include(d => d.PhrasesPairs.Select(p => p.SecondPhrase))
                .FirstOrDefaultAsync(d => d.Id == dictionaryId);

            if (dictionary == null) return Request.CreateResponse(HttpStatusCode.NotFound);
            if (!dictionary.IsPublic && dictionary.OwnerId != User.Identity.Name) return Request.CreateResponse(HttpStatusCode.Forbidden);

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.SourceLanguageValue))
                {
                    dictionary.PhrasesPairs = dictionary.PhrasesPairs
                                                            .Where(pp => pp.FirstPhrase.Text.ToLower().Contains(filter.SourceLanguageValue.ToLower()))
                                                            .ToList();
                }

                if (!string.IsNullOrEmpty(filter.TargetLanguageValue))
                {
                    dictionary.PhrasesPairs = dictionary.PhrasesPairs
                                                            .Where(pp => pp.SecondPhrase.Text.ToLower().Contains(filter.TargetLanguageValue.ToLower()))
                                                            .ToList();
                }
            }

            var res = Mapper.MapProperties<DictionaryViewModel>(dictionary);
            res.IsMyDictionary = dictionary.OwnerId == User.Identity.Name;
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        [Route("Create")]
        [HttpPost]
        public async Task<HttpResponseMessage> CreateDictionary(DictionaryViewModel vm)
        {
            Dictionary newDictionary = new Dictionary()
            {
                Name = vm.Name,
                Description = vm.Description,
                SourceLanguage = vm.SourceLanguage,
                TargetLanguage = vm.TargetLanguage,
                IsPublic = vm.IsPublic,
                CreationDate = DateTime.Now,
                LastChangeDate = DateTime.Now,
                OwnerId = User.Identity.Name
            };

            _dbContext.CreateDictionary(newDictionary);
            await _dbContext.SaveDbChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK, newDictionary);
        }

        [Route("Edit/{dictionaryId}")]
        [HttpPut]
        public async Task<HttpResponseMessage> EditDictionary([FromUri]Guid dictionaryId, DictionaryViewModel vm)
        {
            var dictionary = await _dbContext.Dictionaries.FirstOrDefaultAsync(d => d.Id == dictionaryId);
            if (dictionary != null)
            {
                dictionary.Name = vm.Name;
                dictionary.Description = vm.Description;
                dictionary.IsPublic = vm.IsPublic;
                dictionary.LastChangeDate = DateTime.Now;

                await _dbContext.SaveDbChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK, dictionary);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [Route("Remove/{id}")]
        [HttpDelete]
        public async Task<HttpResponseMessage> RemoveDictionary(Guid id)
        {
            var dictionaryToRemove = await _dbContext.Dictionaries.FirstOrDefaultAsync(d => d.Id == id);
            if (dictionaryToRemove != null)
            {
                var res = await _dbContext.RemoveDictionary(dictionaryToRemove);
                await _dbContext.SaveDbChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Download/{dictionaryId}/{format}")]
        public async Task<HttpResponseMessage> DownloadDictionary([FromUri]Guid dictionaryId, string format)
        {
            if (string.IsNullOrEmpty(format))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var dictionary = await _dbContext.Dictionaries
                                                .Include(d => d.PhrasesPairs)
                                                .Include(d => d.PhrasesPairs.Select(p => p.FirstPhrase))
                                                .Include(d => d.PhrasesPairs.Select(p => p.SecondPhrase))
                                                .FirstOrDefaultAsync(d => d.Id == dictionaryId);

            if (dictionary == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Dictionary not found");
            }

            if (!dictionary.IsPublic && dictionary.OwnerId != User.Identity.Name)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            switch (format.ToLower())
            {
                case "excel":
                    return GetExcelResponseForDictionary(dictionary);
                case "pdf":
                    return GetPdfResponseForDictionary(dictionary);
                default:
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Unknow format");
            }
        }

        private HttpResponseMessage GetExcelResponseForDictionary(Dictionary dictionary)
        {
            ExcelPackage excelDocument = new ExcelPackage();

            string firstWorkSheetName = string.Format("{0} — {1}", dictionary.SourceLanguage, dictionary.TargetLanguage);
            excelDocument.Workbook.Worksheets.Add(firstWorkSheetName);
            ExcelWorksheet workSheet1 = excelDocument.Workbook.Worksheets[firstWorkSheetName];
            int col = 2, row = 2;
            foreach (var phrasesPair in dictionary.PhrasesPairs.OrderBy(p => p.FirstPhrase.Text))
            {
                workSheet1.Cells[row, col].Value = phrasesPair.FirstPhrase.Text;
                workSheet1.Cells[row++, col + 1].Value = phrasesPair.SecondPhrase.Text;
            }

            string secondWorkSheetName = string.Format("{0} — {1}", dictionary.TargetLanguage, dictionary.SourceLanguage);
            excelDocument.Workbook.Worksheets.Add(secondWorkSheetName);
            ExcelWorksheet workSheet2 = excelDocument.Workbook.Worksheets[secondWorkSheetName];
            row = 2;
            foreach (var phrasesPair in dictionary.PhrasesPairs.OrderBy(p => p.SecondPhrase.Text))
            {
                workSheet2.Cells[row, col].Value = phrasesPair.SecondPhrase.Text;
                workSheet2.Cells[row++, col + 1].Value = phrasesPair.FirstPhrase.Text;
            }

            var memoryStream = new MemoryStream();
            excelDocument.Workbook.Properties.Author = dictionary.OwnerId;
            excelDocument.SaveAs(memoryStream);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(memoryStream.ToArray());
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            string filename = string.Format("{0}.xlsx", dictionary.Name);
            response.Content.Headers.ContentDisposition.FileName = Uri.EscapeUriString(filename);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
            return response;
        }

        private HttpResponseMessage GetPdfResponseForDictionary(Dictionary dictionary)
        {
            iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4);
            var memoryStream = new MemoryStream();
            iTextSharp.text.pdf.PdfWriter.GetInstance(document, memoryStream);

            document.Open();
            string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.TTF");
            iTextSharp.text.pdf.BaseFont baseFont = iTextSharp.text.pdf.BaseFont.CreateFont(
                ARIALUNI_TFF, iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 14, iTextSharp.text.Font.NORMAL);

            uint rowNumber = 1;
            foreach (var phrasesPair in dictionary.PhrasesPairs.OrderBy(p => p.FirstPhrase.Text))
            {
                document.Add(new iTextSharp.text.Paragraph(
                    string.Format("{0}) {1} - {2}", rowNumber++, phrasesPair.FirstPhrase.Text, phrasesPair.SecondPhrase.Text),
                    font)); 
            }
            document.NewPage();
            rowNumber = 1;
            foreach (var phrasesPair in dictionary.PhrasesPairs.OrderBy(p => p.SecondPhrase.Text))
            {
                document.Add(new iTextSharp.text.Paragraph(
                    string.Format("{0}) {1} - {2}", rowNumber++, phrasesPair.SecondPhrase.Text, phrasesPair.FirstPhrase.Text),
                    font));
            }
            document.AddAuthor(dictionary.OwnerId);
            document.Close();

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(memoryStream.ToArray());
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            string filename = string.Format("{0}.pdf", dictionary.Name);
            response.Content.Headers.ContentDisposition.FileName = Uri.EscapeUriString(filename);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            return response;
        }
    }
}
