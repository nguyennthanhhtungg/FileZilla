
using CsvHelper;
using FileZilla.Models;
using FileZilla.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZilla.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }


        public async Task<string> ExportExpiredProductListToCsvFileData()
        {
            List<Product> productList = await _productRepository.GetExpiredProductList();


            using (MemoryStream memoryStream = new MemoryStream())
            using (StreamWriter streamWriter = new StreamWriter(memoryStream))
            using (CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteField("STT");
                csvWriter.WriteField("ProductId");
                csvWriter.WriteField("Product Name");
                csvWriter.WriteField("Short Description");
                csvWriter.WriteField("Product Code");
                csvWriter.WriteField("Expiry Date");
                csvWriter.WriteField("Manufacturing Date");
                csvWriter.WriteField("Price");
                csvWriter.WriteField("Discount");
                csvWriter.WriteField("Weight");
                csvWriter.NextRecord();

                for (int i = 0; i < productList.Count; i++)
                {
                    csvWriter.WriteField(i + 1);
                    csvWriter.WriteField(productList[i].ProductId);
                    csvWriter.WriteField(productList[i].ProductName);
                    csvWriter.WriteField(productList[i].ShortDescription);
                    csvWriter.WriteField(productList[i].ProductCode);
                    csvWriter.WriteField(productList[i].ExpiryDate);
                    csvWriter.WriteField(productList[i].ManufacturingDate);
                    csvWriter.WriteField(productList[i].Price);
                    csvWriter.WriteField(productList[i].Discount);
                    csvWriter.WriteField(productList[i].Weight);
                    csvWriter.NextRecord();
                }


                streamWriter.Flush();
                string result = Encoding.UTF8.GetString(memoryStream.ToArray());

                return result;
            }
        }
    }
}
