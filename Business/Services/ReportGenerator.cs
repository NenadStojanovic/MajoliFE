using AutoMapper;
using MajoliFE.Business.Dtos;
using MajoliFE.Business.Interfaces;
using MajoliFE.Data.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MajoliFE.Business.Services
{
	public class ReportGenerator : IReportGenerator
	{
		private IInvoiceRepository _invoiceRepository;
		private ISettingsRepository _settingsRepository;
		private readonly IMapper _mapper;

		public ReportGenerator(IInvoiceRepository invoiceRepository, ISettingsRepository settingsRepository, IMapper mapper)
		{
			_invoiceRepository = invoiceRepository;
			_settingsRepository = settingsRepository;
			_mapper = mapper;
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
					result.InvoiceReportData = package.GetAsByteArray();
					dbinvoice.IsIssued = true;
					_invoiceRepository.Update(dbinvoice.Id, dbinvoice);
					_invoiceRepository.SaveChanges();
				}

				
			}
			return result;
		}
	}
}
