using System;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using OfficeOpenXml;
using System.Collections.Generic;
using Hobbit_Farnell;
using System.Collections;
using static Hobbit_Farnell.PriceListModel;
using System.Net;
using Newtonsoft.Json;
using System.Xml;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Runtime.Remoting.Contexts;
using System.Drawing;
using OfficeOpenXml.Style;

namespace Hobbit_Farnell
{
    public class PriceListController
    {

        public void ParseAltiumExcel(string fileName, int count1, int count2, int count3, int count4, int numbDpsInOneSeries)
        {
            //Opening an existing Excel file
            FileInfo fi = new FileInfo(fileName);
            PriceListModels priceList = null;
            List<PriceListModels> listPriceList = new List<PriceListModels>();

            var partnumber = fi.ToString().Split('.');

            using (ExcelPackage excelPackage = new ExcelPackage(fi))
            {
                //Get a WorkSheet by index. Note that EPPlus indexes are base 1, not base 0!
                ExcelWorksheet firstWorksheet = excelPackage.Workbook.Worksheets[1];

                var allRows = firstWorksheet.Dimension.Rows;

                for (int row = 2; row < allRows; row++)
                {
                    priceList = new PriceListModels();
                    priceList.Comment = firstWorksheet.Cells[row, 1].Value.ToString();
                    priceList.Description = firstWorksheet.Cells[row, 2].Value.ToString();
                    priceList.Designator = firstWorksheet.Cells[row, 3].Value.ToString();
                    priceList.Manufacture = firstWorksheet.Cells[row, 4].Value.ToString();
                    priceList.Quantity = Convert.ToInt32(firstWorksheet.Cells[row, 5].Value);
                    priceList.SupplierId = firstWorksheet.Cells[row, 6].Value.ToString();
                    priceList.UniqueIdName = firstWorksheet.Cells[row, 8].Value.ToString();

                    listPriceList.Add(priceList);
                }

                this.GeneratePriceList(listPriceList, count1, count2, count3, count4, numbDpsInOneSeries, partnumber.FirstOrDefault());
            }
        }

        // TODO: MESS IS FINE :)
        public void GeneratePriceList(List<PriceListModels> priceList, int _count1, int _count2, int _count3, int _count4, int _numbDpsInOneSeries, string fileName)
        {
            List<GeneratedPriceListModel> generatedPriceListModelList = new List<GeneratedPriceListModel>();
            foreach (var r in priceList)
            {
                Prices prices = null;
                List<Prices> pricesList = new List<Prices>();
                GeneratedPriceListModel generatedPriceListModel = new GeneratedPriceListModel();
                var ApiFarnellUrl = "http://api.element14.com//catalog/products" +
                                    "?term=id:" + r.SupplierId + 
                                    "&storeInfo.id=sk.farnell.com" +
                                    "&resultsSettings.offset=0" +
                                    "&resultsSettings.numberOfResults=1" +
                                    "&resultsSettings.refinements.filters=" +
                                    "&resultsSettings.responseGroup=medium" +
                                    "&callInfo.omitXmlSchema=false&callInfo.callback=" +
                                    "&callInfo.responseDataFormat=xml" +
                                    "&callinfo.apiKey=gd8n8b2kxqw6jq5mutsbrvur";

                generatedPriceListModel.Comment = r.Comment;
                generatedPriceListModel.Description = r.Description;
                generatedPriceListModel.Designator = r.Designator;
                generatedPriceListModel.SupplierId = r.SupplierId;
                generatedPriceListModel.Manufacture = r.Manufacture;
                generatedPriceListModel.Quantity = r.Quantity;
                generatedPriceListModel.UniqueIdName = r.UniqueIdName;
                generatedPriceListModel.CreatedDate = DateTime.Now.ToShortDateString();

                using (var w = new WebClient())
                {
                    var xml_data = string.Empty;
                    XmlDocument xmlRead = new XmlDocument();
                    // attempt to download JSON data as a string
                    try
                    {
                        xml_data = w.DownloadString(ApiFarnellUrl);
                        xmlRead.LoadXml(xml_data);
                        foreach (XmlNode node in xmlRead.DocumentElement.ChildNodes)
                        {
                            foreach (XmlNode node2 in node)
                            {
                                if (node2 != null)
                                {
                                    if (node2.Name == "ns1:id")
                                    {
                                        generatedPriceListModel.Id = node2.InnerText;
                                    }
                                    else if (node2.Name == "ns1:datasheets")
                                    {
                                        foreach (XmlNode datasheet in node2)
                                        {
                                            if (datasheet.Name == "ns1:url")
                                            {
                                                generatedPriceListModel.DatasheetUrl = datasheet.InnerText;
                                            }
                                        }
                                    }
                                    else if (node2.Name == "ns1:prices")
                                    {
                                        prices = new Prices();
                                        foreach (XmlNode pricesNode in node2)
                                        {
                                            if (pricesNode.Name == "ns1:to")
                                            {
                                                prices.to = Convert.ToDecimal(pricesNode.InnerText);
                                            }
                                            else if (pricesNode.Name == "ns1:from")
                                            {
                                                prices.from = Convert.ToDecimal(pricesNode.InnerText);
                                            }
                                            else if (pricesNode.Name == "ns1:cost")
                                            {
                                                prices.cost = float.Parse(pricesNode.InnerText, CultureInfo.InvariantCulture.NumberFormat);
                                            }
                                        }
                                        pricesList.Add(prices);
                                    }
                                    else if (node2.Name == "ns1:translatedMinimumOrderQuality")
                                    {
                                        generatedPriceListModel.MinimumOrder = Convert.ToInt16(node2.InnerText);
                                    }
                                    else if (node2.Name == "ns1:stock")
                                    {
                                        foreach (XmlNode stock in node2)
                                        {
                                            if (stock.Name == "ns1:level")
                                            {
                                                generatedPriceListModel.InStock = Convert.ToInt64(stock.InnerText);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

                decimal quantityPerOneSeries = r.Quantity * _numbDpsInOneSeries;
                decimal quantityPerFirstseries = 0;
                decimal quantityPerSecondtseries = 0;
                decimal quantityPerThirdseries = 0;
                decimal quantityPerFourthseries = 0;

                foreach (var quant in pricesList)
                {
                    if (quantityPerOneSeries > generatedPriceListModel.MinimumOrder)
                    {
                        if ((quantityPerOneSeries + generatedPriceListModel.MinimumOrder) <= quant.to && ((quantityPerOneSeries + generatedPriceListModel.MinimumOrder) >= quant.from))
                        {
                            generatedPriceListModel.PerOneSeries = (float)quantityPerOneSeries * quant.cost;
                        }
                    }
                    else
                    {
                        quantityPerOneSeries = generatedPriceListModel.MinimumOrder;
                        generatedPriceListModel.info = "order set to " + generatedPriceListModel.MinimumOrder + " per one series / ";

                        if ((quantityPerOneSeries + generatedPriceListModel.MinimumOrder) <= quant.to && ((quantityPerOneSeries + generatedPriceListModel.MinimumOrder) >= quant.from))
                        {
                            generatedPriceListModel.PerOneSeries = (float)quantityPerOneSeries * quant.cost;
                        }
                    }
                }

                if (_count1 != 0)
                {
                    quantityPerFirstseries = _count1 * quantityPerOneSeries;

                    foreach (var quant in pricesList)
                    {
                        if (quantityPerFirstseries > generatedPriceListModel.MinimumOrder)
                        {
                            if ((quantityPerFirstseries + generatedPriceListModel.MinimumOrder) <= quant.to && ((quantityPerFirstseries + generatedPriceListModel.MinimumOrder) >= quant.from))
                            {
                                generatedPriceListModel.FirstChooseSeries = (float)quantityPerFirstseries * quant.cost;
                                break;
                            }
                        }
                        else
                        {
                            quantityPerFirstseries = generatedPriceListModel.MinimumOrder;
                            generatedPriceListModel.info = "order set to " + generatedPriceListModel.MinimumOrder + " per " + _count1 + " series / ";

                            if ((quantityPerFirstseries + generatedPriceListModel.MinimumOrder) <= quant.to && ((quantityPerFirstseries + generatedPriceListModel.MinimumOrder) >= quant.from))
                            {
                                generatedPriceListModel.FirstChooseSeries = (float)quantityPerFirstseries * quant.cost;
                                break;
                            }
                        }
                    }
                }

                if (_count2 != 0)
                {
                    quantityPerSecondtseries = _count2 * quantityPerOneSeries;

                    foreach (var quant in pricesList)
                    {
                        if (quantityPerSecondtseries > generatedPriceListModel.MinimumOrder)
                        {
                            if ((quantityPerSecondtseries+ + generatedPriceListModel.MinimumOrder) <= quant.to && ((quantityPerSecondtseries + generatedPriceListModel.MinimumOrder) >= quant.from))
                            {
                                generatedPriceListModel.SecondChooseSeries = (float)quantityPerSecondtseries * quant.cost;
                                break;
                            }
                        }
                        else
                        {
                            quantityPerSecondtseries = generatedPriceListModel.MinimumOrder;
                            generatedPriceListModel.info = "order set to " + generatedPriceListModel.MinimumOrder + " per " + _count2 + " series / ";

                            if ((quantityPerSecondtseries + generatedPriceListModel.MinimumOrder) <= quant.to && ((quantityPerSecondtseries + generatedPriceListModel.MinimumOrder) >= quant.from))
                            {
                                generatedPriceListModel.SecondChooseSeries = (float)quantityPerSecondtseries * quant.cost;
                                break;
                            }
                        }
                    }
                }

                if (_count3 != 0)
                {
                    quantityPerThirdseries = _count3 * quantityPerOneSeries;

                    foreach (var quant in pricesList)
                    {
                        if (quantityPerThirdseries > generatedPriceListModel.MinimumOrder)
                        {
                            if ((quantityPerThirdseries + generatedPriceListModel.MinimumOrder) <= quant.to && ((quantityPerThirdseries + generatedPriceListModel.MinimumOrder) >= quant.from))
                            {
                                generatedPriceListModel.ThirdChooseSeries = (float)quantityPerThirdseries * quant.cost;
                                break;
                            }
                        }
                        else
                        {
                            quantityPerThirdseries = generatedPriceListModel.MinimumOrder;
                            generatedPriceListModel.info = "order set to " + generatedPriceListModel.MinimumOrder + " per " + _count3 + " series / ";

                            if ((quantityPerThirdseries + generatedPriceListModel.MinimumOrder) <= quant.to && ((quantityPerThirdseries + generatedPriceListModel.MinimumOrder) >= quant.from))
                            {
                                generatedPriceListModel.ThirdChooseSeries = (float)quantityPerThirdseries * quant.cost;
                                break;
                            }
                        }
                    }
                }

                if (_count4 != 0)
                {
                    quantityPerFourthseries = _count4 * quantityPerOneSeries;

                    foreach (var quant in pricesList)
                    {
                        if (quantityPerFourthseries > generatedPriceListModel.MinimumOrder)
                        {
                            if ((quantityPerFourthseries + generatedPriceListModel.MinimumOrder) <= quant.to && ((quantityPerFourthseries + generatedPriceListModel.MinimumOrder) >= quant.from))
                            {
                                generatedPriceListModel.FourthChooseSeries = (float)quantityPerFourthseries * quant.cost;
                                break;
                            }
                        }
                        else
                        {
                            quantityPerFourthseries = generatedPriceListModel.MinimumOrder;
                            generatedPriceListModel.info = "order set to " + generatedPriceListModel.MinimumOrder + " per " + _count4 + " series";

                            if ((quantityPerFourthseries + generatedPriceListModel.MinimumOrder) <= quant.to && ((quantityPerFourthseries + generatedPriceListModel.MinimumOrder) >= quant.from))
                            {
                                generatedPriceListModel.FourthChooseSeries = (float)quantityPerFourthseries * quant.cost;
                                break;
                            }
                        }
                    }
                }

                generatedPriceListModelList.Add(generatedPriceListModel);
                System.Threading.Thread.Sleep(500);
            }

            CreateExcelPriceList(generatedPriceListModelList, fileName, _count1, _count2, _count3, _count4);
        }

        public void CreateExcelPriceList(List<GeneratedPriceListModel> ListPriceList, string filename, int firstCount, int secondCount, int thirdCount, int fourthCount)
        {
            var partName = Regex.Split(filename.ToString(), "/");
            // Set the file name and get the output directory
            var _fileName = filename + "_PriceList" + " " + DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + ".xlsx";

            // Create the file using the FileInfo object
            var file = new FileInfo(_fileName);

            // Create the package and make sure you wrap it in a using statement
            using (var package = new ExcelPackage(file))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Price list - " + DateTime.Now.ToShortDateString());
                
                // Add some formatting to the worksheet
                worksheet.TabColor = System.Drawing.Color.LightGray;
                worksheet.DefaultRowHeight = 12;
                worksheet.HeaderFooter.FirstFooter.LeftAlignedText = string.Format("Generated: {0}", DateTime.Now.ToShortDateString());

                // Start adding the header
                // First of all the first row
                worksheet.Cells[1, 1].Value = "Coment";
                worksheet.Cells[1, 2].Value = "Description";
                worksheet.Cells[1, 3].Value = "Designator";
                worksheet.Cells[1, 4].Value = "Manufacture";
                worksheet.Cells[1, 5].Value = "Quantity";
                worksheet.Cells[1, 6].Value = "SupplierId";
                worksheet.Cells[1, 7].Value = "Unique name Id";
                worksheet.Cells[1, 8].Value = "Datasheet url";
                worksheet.Cells[1, 9].Value = "Id";
                worksheet.Cells[1, 10].Value = "On stock";
                worksheet.Cells[1, 11].Value = "Minimum order";
                worksheet.Cells[1, 12].Value = "Price per one series";
                worksheet.Cells[1, 13].Value = "Price per " + firstCount + " series";
                worksheet.Cells[1, 14].Value = "Price per " + secondCount + " series";
                worksheet.Cells[1, 15].Value = "Price per " + thirdCount + " series";
                worksheet.Cells[1, 16].Value = "Price per " + fourthCount + " series";
                worksheet.Cells[1, 17].Value = "Created date";
                worksheet.Cells[1, 18].Value = "Info";

                //Ok now format the first row of the heade, but only the first two columns;
                using (var range = worksheet.Cells[1, 1, 1, 18])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gray);
                    range.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                    range.Style.ShrinkToFit = false;
                }

                int rowNumber = 2;
                // Loop through all the companies and add their vehicles
                foreach (var list in ListPriceList)
                {
                    worksheet.Cells[rowNumber, 1].Value = list.Comment;
                    worksheet.Cells[rowNumber, 2].Value = list.Description;
                    worksheet.Cells[rowNumber, 3].Value = list.Designator;
                    worksheet.Cells[rowNumber, 4].Value = list.Manufacture;
                    worksheet.Cells[rowNumber, 5].Value = list.Quantity;
                    worksheet.Cells[rowNumber, 6].Value = list.SupplierId;
                    worksheet.Cells[rowNumber, 7].Value = list.UniqueIdName;
                    worksheet.Cells[rowNumber, 8].Value = list.DatasheetUrl;
                    worksheet.Cells[rowNumber, 9].Value = list.Id;
                    worksheet.Cells[rowNumber, 10].Value = list.InStock;
                    worksheet.Cells[rowNumber, 11].Value = list.MinimumOrder;
                    worksheet.Cells[rowNumber, 12].Value = list.PerOneSeries + " €";
                    worksheet.Cells[rowNumber, 13].Value = list.FirstChooseSeries + " €";
                    worksheet.Cells[rowNumber, 14].Value = list.SecondChooseSeries + " €";
                    worksheet.Cells[rowNumber, 15].Value = list.ThirdChooseSeries + " €";
                    worksheet.Cells[rowNumber, 16].Value = list.FourthChooseSeries + " €";
                    worksheet.Cells[rowNumber, 17].Value = list.CreatedDate.ToString();
                    worksheet.Cells[rowNumber, 18].Value = list.info;
                    rowNumber++;
                }

                // Fit the columns according to its content
                worksheet.Column(1).AutoFit();
                worksheet.Column(2).AutoFit();
                worksheet.Column(3).AutoFit();
                worksheet.Column(4).AutoFit();
                worksheet.Column(5).AutoFit();
                worksheet.Column(6).AutoFit();
                worksheet.Column(7).AutoFit();
                worksheet.Column(8).AutoFit();
                worksheet.Column(9).AutoFit();
                worksheet.Column(10).AutoFit();
                worksheet.Column(11).AutoFit();
                worksheet.Column(12).AutoFit();
                worksheet.Column(13).AutoFit();
                worksheet.Column(14).AutoFit();
                worksheet.Column(15).AutoFit();
                worksheet.Column(16).AutoFit();
                worksheet.Column(17).AutoFit();
                worksheet.Column(18).AutoFit();

                package.Save();
            }
        }
    }
}
