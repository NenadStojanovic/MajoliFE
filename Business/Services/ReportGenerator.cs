using AutoMapper;
using MajoliFE.Business.Dtos;
using MajoliFE.Business.Interfaces;
using MajoliFE.Data.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace MajoliFE.Business.Services
{
	public class ReportGenerator : IReportGenerator
	{
		private IInvoiceRepository _invoiceRepository;
		private IVendorInvoiceRepository _vendorInvoiceRepository;
		private ISettingsRepository _settingsRepository;
		private readonly IMapper _mapper;

		public ReportGenerator(IInvoiceRepository invoiceRepository, ISettingsRepository settingsRepository, IMapper mapper, IVendorInvoiceRepository vendorInvoiceRepository)
		{
			_invoiceRepository = invoiceRepository;
			_settingsRepository = settingsRepository;
			_mapper = mapper;
			_vendorInvoiceRepository = vendorInvoiceRepository;
		}
		public Report GenerateVendorInvoicesReport(DateTime dateFrom, DateTime dateTo)
		{
			var result = new Report();
			var invoices = _vendorInvoiceRepository.GetVendorInvocesFromRange(dateFrom, dateTo).OrderByDescending(x => x.Date);
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			using (var package = new ExcelPackage(new FileInfo("Templates/vendor_invoices_majoli_template.xlsx")))
			{
				if (invoices != null && invoices.Count() > 0)
				{
					ExcelWorksheet ws = package.Workbook.Worksheets[0];
					int A = 1;
					int B = 2;
					int C = 3;
					int D = 4;
					int E = 5;
					int F = 6;
					int G = 7;
					int H = 8;
					int I = 9;
					int J = 10;
					var counter = 0;
					int startRow = 3; //where invoice rows starts
									  //title
					ws.Cells[1, A].Value = "Ulazni računi za period: " + dateFrom.ToShortDateString() + " - " + dateTo.ToShortDateString();
					//invoice rows
					foreach (var dbinvoice in invoices)
					{
						var invoice = _mapper.Map<VendorInvoiceDto>(dbinvoice);
						if (counter != 0)
						{
							ws.InsertRow(startRow + 1, 1);
							ws.Cells[startRow, A, startRow, J].Copy(ws.Cells[startRow + 1, A, startRow + 1, J]);
							startRow++;
						}
						ws.Cells[startRow, A].Value = counter + 1;
						ws.Cells[startRow, B].Value = invoice.Id;
						ws.Cells[startRow, C].Value = invoice.Date.ToShortDateString();
						ws.Cells[startRow, D].Value = invoice.Vendor?.Name;
						ws.Cells[startRow, E].Value = invoice.Total.ToString("N2");
						ws.Cells[startRow, F].Value = invoice.TotalPaid.ToString("N2");
						ws.Cells[startRow, G].Value = invoice.Model;
						ws.Cells[startRow, H].Value = invoice.ReferenceNumber;
						ws.Cells[startRow, I].Value = invoice.Vendor?.AccountNumber;
						ws.Cells[startRow, J].Value = invoice.Note;

						counter++;
					}
					//statistics part at the bottom
					var invoiceStatistics = GetVendorInvoiceStatistics(dateFrom, dateTo);
					if (invoiceStatistics != null)
					{
						int startRowForStatistics = startRow + 3;
						ws.Cells[startRowForStatistics, A].Value = invoiceStatistics.TotalNumOfInvoices;
						ws.Cells[startRowForStatistics, C].Value = invoiceStatistics.TotalAmount.ToString("N2");
						ws.Cells[startRowForStatistics, D].Value = invoiceStatistics.TotalPaidAmount.ToString("N2") + @"/" + invoiceStatistics.TotalAmount.ToString("N2");

					}
				}
				result.ReportData = package.GetAsByteArray();

			}
			return result;
		}
		public Report GenerateInvoicesReport(DateTime dateFrom, DateTime dateTo)
		{
			var result = new Report();
			var invoices = _invoiceRepository.GetInvocesFromRange(dateFrom, dateTo).OrderByDescending(x=>x.DateIssued);
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			using (var package = new ExcelPackage(new FileInfo("Templates/invoices_majoli_template.xlsx")))
			{
				if (invoices != null && invoices.Count() > 0)
				{
					ExcelWorksheet ws = package.Workbook.Worksheets[0];
					int A = 1;
					int B = 2;
					int C = 3;
					int D = 4;
					int E = 5;
					int F = 6;
					int G = 7;
					int H = 8;
					int I = 9;
					int J = 10;
					int K = 11;
					int L = 12;
					var counter = 0;
					int startRow = 3; //where invoice rows starts
					//title
					ws.Cells[1, A].Value = "Računi za period: " + dateFrom.ToShortDateString() + " - " + dateTo.ToShortDateString();
					//invoice rows
					foreach (var dbinvoice in invoices)
					{
						var invoice = _mapper.Map<InvoiceDto>(dbinvoice);
						if (counter != 0)
						{
							ws.InsertRow(startRow + 1, 1);
							ws.Cells[startRow, A, startRow, L].Copy(ws.Cells[startRow + 1, A, startRow + 1, L]);
							startRow++;
						}
						ws.Cells[startRow, A].Value = counter + 1;
						ws.Cells[startRow, B].Value = invoice.Id;
						ws.Cells[startRow, C].Value = invoice.InvoiceNumber;
						ws.Cells[startRow, D].Value = invoice.DateIssued.ToShortDateString();
						ws.Cells[startRow, E].Value = invoice.CurrencyDate.ToShortDateString();
						ws.Cells[startRow, F].Value = invoice.Customer?.Name;
						ws.Cells[startRow, G].Value = invoice.BaseTotal.ToString("N2");
						ws.Cells[startRow, H].Value = invoice.PDV.ToString("N2");
						ws.Cells[startRow, I].Value = invoice.Total.ToString("N2");
						ws.Cells[startRow, J].Value = invoice.TotalPaid.ToString("N2");
						ws.Cells[startRow, K].Value = invoice.IsIssued ? "Da" : "NE";
						ws.Cells[startRow, L].Value = invoice.IsPaid ? "Da" : "NE";

						counter++;
					}
					//statistics part at the bottom
					var invoiceStatistics = GetInvoiceStatistics(dateFrom, dateTo);
					if(invoiceStatistics != null)
					{
						int startRowForStatistics = startRow + 3;
						ws.Cells[startRowForStatistics, A].Value = invoiceStatistics.TotalNumOfInvoices;
						ws.Cells[startRowForStatistics, C].Value = invoiceStatistics.TotalNumOfIssuedInvoices +@"/"+ invoiceStatistics.TotalNumOfInvoices;
						ws.Cells[startRowForStatistics, D].Value = invoiceStatistics.TotalNumOfPaidInvoices + @"/" + invoiceStatistics.TotalNumOfInvoices;
						ws.Cells[startRowForStatistics, E].Value = invoiceStatistics.TotalBaseAmount.ToString("N2");
						ws.Cells[startRowForStatistics, F].Value = invoiceStatistics.TotalPDVAmount.ToString("N2");
						ws.Cells[startRowForStatistics, G].Value = invoiceStatistics.TotalAmount.ToString("N2");
						ws.Cells[startRowForStatistics, H].Value = invoiceStatistics.TotalPaidAmount.ToString("N2") + @"/" + invoiceStatistics.TotalAmount.ToString("N2");
					}
				}
				result.ReportData = package.GetAsByteArray();
			
			}
			return result;
		}
		public InvoiceReport GenerateInvoice(int invoiceId)
		{
			var result = new InvoiceReport();
			var dbinvoice = _invoiceRepository.GetById(invoiceId);
			var invoice = _mapper.Map<InvoiceDto>(dbinvoice);
			var settings = _settingsRepository.GetActiveSettings();
			result.CustomerName = invoice.CustomerName;
			if(invoice!=null && settings != null)
			{
				ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
				using (var package = new ExcelPackage(new FileInfo("Templates/invoice_majoli_template.xlsx")))
				{
					ExcelWorksheet ws = package.Workbook.Worksheets[0];
					//left side
					int D = 4;
					int G = 7;
					ws.Cells[9, D].Value = invoice.InvoiceNumber; //D9
					ws.Cells[11, D].Value = invoice.DateIssued.ToShortDateString(); //D11
					ws.Cells[12, D].Value = invoice.DateOfService.ToShortDateString(); //D12
					ws.Cells[13, D].Value = invoice.Place; //D13
					ws.Cells[14, D].Value = invoice.CurrencyDate.ToShortDateString(); //D14
					ws.Cells[14, G].Value = invoice.CurrencyDateNumOfDays; //G14

					//right side - customer info
					int H = 8;
					int L = 12;
					ws.Cells[9, H].Value = "Partner ID: "+invoice.PartnerId; //H9
					var address = invoice.CustomerName + " " + invoice.CustomerAddress;
					ws.Cells[10, H].Value = address; //H10
					ws.Cells[14, H].Value = "PIB:" + invoice.CustomerPIB; //H14
					ws.Cells[14, L].Value = "MB:" + invoice.CustomerMB; //L14

					//summary left side
					int E = 5;
					ws.Cells[21, D].Value = invoice.BaseTotal.ToString("N2"); //D21
					ws.Cells[21, E].Value = invoice.PDV.ToString("N2"); //E21

					//summary right side
					ws.Cells[19, L].Value = invoice.BaseTotal.ToString("N2"); //L19
					ws.Cells[21, L].Value = invoice.BaseTotal.ToString("N2"); //L21
					ws.Cells[22, L].Value = invoice.BaseTotal.ToString("N2"); //L22
					ws.Cells[23, L].Value = invoice.PDV.ToString("N2"); //L23
					ws.Cells[24, L].Value = invoice.Total.ToString("N2"); //L24

					//Bank info
					ws.Cells[28, H].Value = settings.BankName; //H28
					ws.Cells[28, L].Value = settings.BankAccount; //L28

					//Note
					int A = 1;
					if(invoice.Note==null)
					{
						ws.Cells[30, A].Value = "Nema napomene"; //A30
					}
					else
					{
						ws.Cells[30, A].Value = invoice.Note; //A30
					}

					//invoice items
					int N = 14;
					int B = 2;
					int C = 3;
					int F = 6;
					int I = 9;
					int J = 10;
					int K = 11;
					int M = 13;
					if(invoice.InvoiceItems!=null && invoice.InvoiceItems.Count>0)
					{
						var counter = 0;
						int startRow = 18;
						foreach (var invoiceItem in invoice.InvoiceItems)
						{
							if(counter!=0)
							{
								ws.InsertRow(startRow+1, 1);
								ws.Cells[startRow, A, startRow, N].Copy(ws.Cells[startRow + 1, A, startRow + 1, N]);
								startRow++;
							}

							ws.Cells[startRow, A].Value = counter + 1;
							ws.Cells[startRow, B].Value = invoiceItem.ItemId;
							ws.Cells[startRow, C].Value = invoiceItem.Name;
							ws.Cells[startRow, F].Value = invoiceItem.Unit;
							ws.Cells[startRow, G].Value = invoiceItem.Quantity;
							ws.Cells[startRow, H].Value = invoiceItem.Price.ToString("N2");
							ws.Cells[startRow, I].Value = 0.ToString("N2");
							ws.Cells[startRow, J].Value = invoiceItem.Price.ToString("N2");
							ws.Cells[startRow, K].Value = invoiceItem.TotalWithoutPDV.ToString("N2");
							ws.Cells[startRow, L].Value = settings.PDV.ToString("N2");
							ws.Cells[startRow, M].Value = invoiceItem.GetPdvValue(settings.PDV).ToString("N2");
							ws.Cells[startRow, N].Value = invoiceItem.GetTotalValue(settings.PDV).ToString("N2");

							counter++;
						}
					
					}
					result.ReportData = package.GetAsByteArray();
					dbinvoice.IsIssued = true;
					_invoiceRepository.Update(dbinvoice.Id, dbinvoice);
					_invoiceRepository.SaveChanges();
				}

				
			}
			return result;
		}

		public InvoiceStatistics GetInvoiceStatistics(DateTime? dateFrom = null, DateTime? dateTo = null)
		{
			if (dateFrom == null)
			{
				dateFrom = DateTime.MinValue;
			}
			if (dateTo == null)
			{
				dateTo = DateTime.MaxValue;
			}
			InvoiceStatistics model = new InvoiceStatistics();
			var invoces = _invoiceRepository.GetInvocesFromRange((DateTime)dateFrom, (DateTime)dateTo);
			if (invoces != null && invoces.Count > 0)
			{
				model.TotalNumOfInvoices = invoces.Count;
				model.TotalNumOfIssuedInvoices = invoces.Where(x => x.IsIssued == true).ToList()?.Count ?? 0;
				model.TotalNumOfPaidInvoices = invoces.Where(x => x.IsPaid == true).ToList()?.Count ?? 0;
				model.TotalAmount = invoces.Sum(x => x.Total);
				model.TotalPDVAmount = invoces.Sum(x => x.PDV);
				model.TotalBaseAmount = invoces.Sum(x => x.BaseTotal);
				model.TotalPaidAmount = invoces.Sum(x => x.TotalPaid);
			}
			return model;
		}

		public VendorInvoiceStatistics GetVendorInvoiceStatistics(DateTime? dateFrom = null, DateTime? dateTo = null)
		{
			if (dateFrom == null)
			{
				dateFrom = DateTime.MinValue;
			}
			if (dateTo == null)
			{
				dateTo = DateTime.MaxValue;
			}
			VendorInvoiceStatistics model = new VendorInvoiceStatistics();
			var invoces = _vendorInvoiceRepository.GetVendorInvocesFromRange((DateTime)dateFrom, (DateTime)dateTo);
			if (invoces != null && invoces.Count > 0)
			{
				model.TotalNumOfInvoices = invoces.Count;
				model.TotalAmount = invoces.Sum(x => x.Total);
				model.TotalPaidAmount = invoces.Sum(x => x.TotalPaid);
			}
			return model;
		}
	}
}
