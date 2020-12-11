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

function CreateOrUpdateInvoiceItem(id,index, isAdd) {
	$.blockUI();
	$.ajax({
		type: "GET",
		url: $("#createOrUpdateInvoiceItemUrl").val(),
		data: { invoiceItemId: id, index: index, isAdd:isAdd },
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

function AddOrUpdateInvoiceItem(id, index, IsAdd) {
	var ind = 0;
	var date = new Date();
	var createdAt = date.toLocaleDateString();
	var pdvBase = $("#pdvValue").val();
	if (IsAdd) {
		ind = $(".invoiceItemRow").length+1;
	}
	else {
		ind = index;
		createdAt = $("#invoiceItemCreatedAt").val();
	}
	var quantity =parseFloat($("#invoiceItemQuantity").val());
	var price = parseFloat($("#invoiceItemPrice").val());
	var totalWithoutPDV = quantity * price;
	var pdvValue = ((quantity * price) / 100) * pdvBase;
	var totalValue = totalWithoutPDV + pdvValue;


	var newRow = '<tr class="hover invoiceItemRow" id="ItemRow-' + ind + '" onclick="CreateOrUpdateInvoiceItem(' + id + ',' + ind +',\'false\')">';
	newRow += '<td>' + ind + ' <input type="hidden" class="invoiceItemId" value="' + id + '"> <input type="hidden" class="invoiceItemCreatedAt" value="' + createdAt +'"></td>';
	newRow += '<td class="itemId">' + $("#invoiceItemItemId").val()+'</td>';
	newRow += '<td class="itemName">' + $("#invoiceItemName").val() +'</td>';
	newRow += '<td class="itemUnit">kom</td>';
	newRow += '<td class="itemQuantity">' + quantity.toFixed(2) +'</td>';
	newRow += '<td class="itemPrice">' + price.toFixed(2) +'</td>';
	newRow += '<td class="">0</td>';
	newRow += '<td class="">' + price.toFixed(2) + '</td>';
	newRow += '<td class="">' + totalWithoutPDV.toFixed(2) + '</td>';
	newRow += '<td class="itemPDV">' + pdvBase + '</td>';
	newRow += '<td class="">' + pdvValue.toFixed(2) + '</td>';
	newRow += '<td class="">' + totalValue.toFixed(2) + '</td>';
	newRow += '</tr>'
	if (IsAdd) {
		$('#invoiceItemsTable > tbody:last-child').append(newRow);
	}
	else {
		var itemRow = '#ItemRow-' + index;
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