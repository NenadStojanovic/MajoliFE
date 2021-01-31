function CreateOrUpdateVendor(vendorId) {
	$.blockUI();
	$.ajax({
		type: "GET",
		url: "CreateOrUpdateVendorDialog",
		data: { vendorId: vendorId },
		contentType: 'application/json',
		dataType: "html",
		success: function (response) {
			$("#CreateOrUpdateVendorDialogHolder").html(response);
			$("#CreateOrUpdateVendorDialog").modal({ show: true });
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

function DeleteVendor(vendorId) {
	$.confirm({
		title: 'Upozorenje!',
		content: 'Da li ste sigurni?',
		type: 'blue',
		buttons: {
			Da: {
				btnClass: 'btn btn-primary',
				action: function () {
					DeleteVendorAjax(vendorId);
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

function DeleteVendorAjax(vendorId) {
	$.blockUI();
	$.ajax({
		type: "GET",
		url: $("#DeleteVendorUrl").val(),
		data: { vendorId: vendorId },
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