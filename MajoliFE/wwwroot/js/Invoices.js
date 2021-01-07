function DeleteInvoice(invoiceId) {
	$.confirm({
		title: 'Upozorenje!',
		content: 'Da li ste sigurni?',
		type: 'blue',
		buttons: {
			Da: {
				btnClass: 'btn btn-primary',
				action: function() {
					DeleteInvoiceAjax(invoiceId);
				}
			},
			Ne: {
				btnClass: 'btn btn-warning',
				action: function () {
				}
			}
		}
	});
}

function DeleteInvoiceAjax(invoiceId) {
	$.blockUI();
	$.ajax({
		type: "GET",
		url: $("#DeleteInvoiceUrl").val(),
		data: { invoiceId: invoiceId },
		contentType: 'application/json',
		dataType: "json",
		success: function (response) {
			AlertSuccess(true);
		},
		error: function (response) {
			AlertError(); 
		},
		complete: function () {
			$.unblockUI();
		}
	});
}

function DownloadInvoice(invoiceId) {
	$.confirm({
		title: 'Upozorenje!',
		content: 'Da li ste sigurni?',
		type: 'blue',
		buttons: {
			Da: {
				btnClass: 'btn btn-primary',
				action: function () {
					DownloadInvoiceAjax(invoiceId);
				}
			},
			Ne: {
				btnClass: 'btn btn-warning',
				action: function () {
				}
			}
		}
	});
}

function DownloadInvoiceAjax(InvoiceId) {
	$.blockUI();
	$.ajax({
		type: "POST",
		dataType: "json",
		url: $("#DownloadInvoiceUrl").val(),
		data: { invoiceId: InvoiceId },
		success: function (response) {
			//var resp = JSON.parse(response);
			window.location = '/Home/Download?fileGuid=' + response.value.fileGuid
				+ '&filename=' + response.value.fileName;
		},
		error: function (response) {
			AlertError();
		},
		complete: function () {
			$.unblockUI();
		}
	});
}
