using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using PDFFormExtract.Models;

namespace PDFFormExtract.Services
{
    public class ITextExtractService : IExtractService
    {
        public ITextExtractService()
        {

        }

        public FormDefinition ExtractData(string filename, byte[] file)
        {
            PdfReader reader = new PdfReader(file);

            FormDefinition form = new FormDefinition();
            form.FileName = filename;
            List<FieldDefinition> foundFields = new List<FieldDefinition>();
            List<Page> pages = new List<Page>();
            // What array starts at 1?
            for (int x = 1; x <= reader.NumberOfPages; x++)
            {
                iTextSharp.text.Rectangle mediabox = reader.GetPageSize(x);
                iTextSharp.text.Rectangle cropbox = reader.GetCropBox(x);
                Console.WriteLine("Page is " + mediabox.Width + " by " + mediabox.Height);
                Console.WriteLine("Crop is " + cropbox.Width + " by " + cropbox.Height);
                Page page = new Page();
                page.Height = mediabox.Height;
                page.Width = mediabox.Width;
                page.Fields = new List<FieldDefinition>();
                pages.Add(page);

                form.Height = mediabox.Height;
                form.Width = mediabox.Width;
                form.Padding = 5;
            }
            form.Pages = pages;


            AcroFields fields = reader.AcroFields;

            var fieldsMap = fields.Fields;
            foreach (var field in fieldsMap)
            {
                FieldDefinition fieldDef = new FieldDefinition();


                //form.fieldMappings.Add(field.Key, field.Key);
                IList<AcroFields.FieldPosition> fieldPositions = fields.GetFieldPositions(field.Key);
                //Console.WriteLine(field.Key + ":" + fieldPositions.Count);
                string dimension = fieldPositions.First().position.Left + "," + fieldPositions.First().position.Top + "," +
                    fieldPositions.First().position.Width + "," + fieldPositions.First().position.Height;
                fieldDef.Dimension = dimension;
                fieldDef.Name = field.Key;
                fieldDef.Page = fieldPositions.First().page - 1; // again the 1 index
                try
                {
                    form.Pages[fieldDef.Page].Fields.Add(fieldDef);
                }catch (ArgumentOutOfRangeException ioe)
                {
                    Console.WriteLine(field.Key + ":" + fieldDef.Page);
                }
                String pageKey = "Page" + fieldPositions.First().page;
                //Dictionary<String, FieldDimension> fieldDimension = null;
                //if (form.InspectedFields.ContainsKey(pageKey))
                //{
                //    fieldDimension = form.InspectedFields[pageKey];
                //}
                //else
                //{
                //    fieldDimension = new Dictionary<string, FieldDimension>();
                //}
                //fieldDimension.Add(field.Key, new FieldDimension
                //{
                //    Name = field.Key,
                //    Page = fieldPositions.First().page,
                //    Dimension = dimension,
                //    Selected = false
                //});
                //form.InspectedFields[pageKey] = fieldDimension;
                //Console.WriteLine(field.Key + " => " + reader.AcroFields.GetField(field.Key));
                //Console.WriteLine("Field Left: " + fieldPositions.First().position.Left);
                //Console.WriteLine("Field Top: " + fieldPositions.First().position.Top);
                //Console.WriteLine("Field Width: " + fieldPositions.First().position.Width);
                //Console.WriteLine("Field Height: " + fieldPositions.First().position.Height);
            }
            
            reader.Close();
            return form;
        }
    }
}