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

function DeleteCustomer(customerId) {
	$.confirm({
		title: 'Upozorenje!',
		content: 'Da li ste sigurni?',
		type: 'blue',
		buttons: {
			Da: {
				btnClass: 'btn btn-primary',
				action: function() {
					DeleteCustomerAjax(customerId);
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

function DeleteCustomerAjax(customerId) {
	$.blockUI();
	$.ajax({
		type: "GET",
		url: $("#DeleteCustomerUrl").val(),
		data: { customerId: customerId },
		contentType: 'application/json',
		dataType: "json",
		success: function (response) {
			if (response) {
				AlertSuccess(true);
			}
			else {
				$.confirm({
					title: 'Obaveštenje',
					content: 'Klijent je povezan sa bar jednim računom. Brisanje nije moguće.',
					type: 'orange',
					typeAnimated: true,
					buttons: {
						Ok: {
							action: function () {
							}
						}
					}
				});
			}
		
		},
		error: function (response) {
			AlertError(); 
		},
		complete: function () {
			$.unblockUI();
		}
	});
}