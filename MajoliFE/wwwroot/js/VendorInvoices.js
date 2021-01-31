function InitControls() {
	$(".chosen-select").chosen({ width: '100%' }).change(function () {
		var id = $(this).val();
		$("#VendorId").val(id);
	});
	$("#VendorIdChosen").val(vendorId).trigger("chosen:updated");
	$('.datepicker').datepicker({
		format: "dd.mm.yyyy",
		autoclose: true,
		todayBtn: true,
		language: "sr-latin"
	});

	//Hack for missing datepicker arrows
	$('.prev i').removeClass();
	$('.prev i').addClass("fa fa-chevron-left");

	$('.next i').removeClass();
	$('.next i').addClass("fa fa-chevron-right");
}


function CreateOrUpdateVendorInvoice(vendorInvoiceId) {
	$.blockUI();
	$.ajax({
		type: "GET",
		url: "CreateOrUpdateVendorInvoiceDialog",
		data: { vendorInvoiceId: vendorInvoiceId },
		contentType: 'application/json',
		dataType: "html",
		success: function (response) {
			$("#CreateOrUpdateVendorInvoiceDialogHolder").html(response);
			InitControls();
			$("#CreateOrUpdateVendorInvoiceDialog").modal({ show: true });
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

function DeleteVendorInvoice(vendorInvoiceId) {
	$.confirm({
		title: 'Upozorenje!',
		content: 'Da li ste sigurni?',
		type: 'blue',
		buttons: {
			Da: {
				btnClass: 'btn btn-primary',
				action: function () {
					DeleteVendorInvoiceAjax(vendorInvoiceId);
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

function DeleteVendorInvoiceAjax(vendorInvoiceId) {
	$.blockUI();
	$.ajax({
		type: "GET",
		url: $("#DeleteVendorInvoiceUrl").val(),
		data: { vendorInvoiceId: vendorInvoiceId },
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