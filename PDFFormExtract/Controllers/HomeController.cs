using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using PDFFormExtract.Services;
using PDFFormExtract.Models;
using Newtonsoft.Json;

namespace PDFFormExtract.Controllers
{
    public class HomeController : ApiController
    {

        [HttpGet, Route("home")]
        public string get()
        {
            Console.WriteLine("API Version home was called");
            return "API Version 1.0";
        }


        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        [HttpPost, Route("api/upload")]
        public async Task<IHttpActionResult> Upload()
        {
            Console.WriteLine("upload was called");
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            FormDefinition formDef = null;

            foreach (var file in provider.Contents)
            {
                var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                var buffer = await file.ReadAsByteArrayAsync();
                //Do whatever you want with filename and its binaray data.
                Console.WriteLine(filename);

                ITextExtractService extractor = new ITextExtractService();
                formDef = extractor.ExtractData(filename, buffer);
            }
            //var results = JsonConvert.SerializeObject(formDef);
            return Ok(formDef);
        }
    }
}
