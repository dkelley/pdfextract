using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDFFormExtract.Models;

namespace PDFFormExtract.Services
{
    public interface IExtractService
    {
        //FormDimension ExtractData(byte[] file);
        FormDefinition ExtractData(string filename, byte[] file);
    }
}
