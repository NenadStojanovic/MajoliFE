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
