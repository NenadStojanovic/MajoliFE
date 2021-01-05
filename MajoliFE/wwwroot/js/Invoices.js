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