$('#Invoice_CustomerId').on('change', function (event, params) {

		$.blockUI();
		$.ajax({
			type: "GET",
			url: $("#GetCustomerUrl").val(),
			data: { customerId: params.selected },
			contentType: 'application/json',
			dataType: "json",
			success: function (response) {
				$('#customerName').val(response.name);
				$('#partnerId').val(response.partnerId);
				var address = '';
				if (response.street != undefined) {
					address = address + response.street;
				}
				if (response.city != undefined) {
					address = address + ' ' + response.city;
				}
				if (response.zip != undefined) {
					address = address + ' ' + response.zip;
				}
				$('#customerAddress').text(address);

				$('#customerPib').val(response.pib);
				$('#customerMb').val(response.mb);
			},
			error: function (response) {
				alertify.error('Došlo je do greške.');
			},
			complete: function () {
				BindPlaceholderTooltips();
				$.unblockUI();
			}
		});
	
});


$('#createOrUpdateInvoiceForm').submit(function (event) {
	event.preventDefault();
	//alert("submti");
	$.blockUI();
	var invoice = GetInvoiceData();
	$.post($("#createOrUpdateInvoiceUrl").val(), { invoice: invoice }, function (result) {
		alertify.notify(successMessage, 'success', 5);
		window.location.href = $("#invoicesUrl").val();
		$.unblockUI();
	});
	//$.blockUI();
	//$.ajax({
	//	type: "POST",
	//	url: $("#createOrUpdateInvoiceUrl").val(),
	//	data: { invoice: invoice },
	//	contentType: 'application/json',
	//	dataType: "json",
	//	success: function (response) {
	//		alertify.notify(message, 'success', 5);
	//		location.href = $("#invoiceUrl").val()
	//	},
	//	error: function (response) {
	//		alertify.error('Došlo je do greške.');
	//	},
	//	complete: function () {
	//		BindPlaceholderTooltips();
	//		$.unblockUI();
	//	}
	//});
	
});

function GetInvoiceData() {
	var invoice = {};
	invoice.Id = $('#id').val();
	invoice.InvoiceNumber = $('#invoiceNumber').val();
	invoice.DateIssued = $('#dateIssued').val();
	invoice.DateOfService = $('#dateOfService').val();
	invoice.Place = $('#place').val();
	invoice.CurrencyDate = $('#currencyDate').val();
	invoice.CurrencyDateNumOfDays = $('#numOfdays').val();
	invoice.CustomerId = $("#Invoice_CustomerId").val();
	invoice.BaseTotal = $("#baseTotalHidden").val();
	invoice.Total = $("#totalHidden").val();
	invoice.PDV = $("#pdvHidden").val();
	invoice.Note = $("#note").text();
	invoice.IsPaid = $("#isPaidHidden").val();
	invoice.IsIssued = $("#isIssuedHidden").val();
	invoice.CustomerName = $("#customerName").val();
	invoice.PartnerId = $("#partnerId").val();
	invoice.CustomerAddress = $("#customerAddress").text();
	invoice.CustomerPIB = $("#customerPib").val();
	invoice.CustomerMB = $("#customerMb").val();
	invoice.CreatedAt = $("#createdAt").val();
	invoice.InvoiceItems = GetInvoiceItems();
	return invoice;
}

function CreateOrUpdateInvoiceItem(id) {
	$.blockUI();
	$.ajax({
		type: "GET",
		url: $("#createOrUpdateInvoiceItemUrl").val(),
		data: { invoiceItemId: id },
		contentType: 'application/json',
		dataType: "html",
		success: function (response) {
			$("#CreateOrUpdateInvoiceItemDialogHolder").html(response);
			$("#CreateOrUpdateInvoiceItemDialog").modal({ show: true });
		},
		error: function (response) {
			alertify.error('Došlo je do greške.');
		},
		complete: function () {
			BindPlaceholderTooltips();
			$.unblockUI();
		}
	});
}

function AddOrUpdateInvoiceItem(id) {
	var newRow = '<tr class="hover" id="2" onclick="CreateOrUpdateInvoiceItem(2)">';
	newRow += '<td>0 <input type="hidden" class="invoiceItemId" value="2"> <input type="hidden" class="invoiceItemCreatedAt" value="28-Nov-20 16:38:35"></td>';
	newRow += '<td class="itemId">2656259</td>';
	newRow += '<td class="itemName">Test Spoljna guma 315/70</td>';
	newRow += '<td class="itemUnit">kom</td>';
	newRow += '<td class="itemQuantity">4</td>';
	newRow += '<td class="itemPrice">24700.00</td>';
	newRow += '<td class="">0</td>';
	newRow += '<td class="">24700.00</td>';
	newRow += '<td class="">98800.00</td>';
	newRow += '<td class="itemPDV">20</td>';
	newRow += '<td class="">19760.00</td>';
	newRow += '<td class="">118560.00</td>';
	newRow += '</tr>'
	if (id == 0) {
		$('#invoiceItemsTable > tbody:last-child').append(newRow);
	}
	else {
		var itemRow = '#ItemRow-' + id;
		$(itemRow).replaceWith(newRow);
	}
	$('invoiceItemsTable > .NoDataTr').remove();
	$('#CreateOrUpdateInvoiceItemDialog').modal('toggle');
	alertify.notify(successMessage, 'success', 5);
	//Recalculate invoice
}

function GetInvoiceItems() {
	var invoiceItems = [];
	$('#invoiceItemsTable tbody tr').each((index, tr) => {
		var invoiceItem = {};
		invoiceItem.ItemId = $(tr).find(".itemId").html();
		invoiceItem.Name = $(tr).find(".itemName").html();
		invoiceItem.Unit = $(tr).find(".itemUnit").html();
		invoiceItem.Quantity = $(tr).find(".itemQuantity").html();
		invoiceItem.Price = $(tr).find(".itemPrice").html();
		invoiceItem.InvoiceId = $('#id').val();
		invoiceItem.PDVValue = $(tr).find(".itemPDV").html();

		invoiceItem.Id = $(tr).find(".invoiceItemId").val();
		invoiceItem.CreatedAt = $(tr).find(".invoiceItemCreatedAt").val();
		invoiceItems.push(invoiceItem);
	});
	return invoiceItems;
}