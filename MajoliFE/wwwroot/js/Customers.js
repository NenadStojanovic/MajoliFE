function CreateOrUpdateCustomer(customerId) {
	$.blockUI(); 
	$.ajax({
		type: "GET",
		url: "CreateOrUpdateCustomerDialog",
		data: { customerId: customerId },
		contentType: 'application/json',
		dataType: "html",
		success: function (response) {
			$("#CreateOrUpdateCustomerDialogHolder").html(response);
			$("#CreateOrUpdateCustomerDialog").modal({ show: true });
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
