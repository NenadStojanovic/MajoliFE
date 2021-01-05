// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var successMessage = "Akcija je uspešno izvršena.";

$(document).ready(function () {
	if ($('#Message').length > 0) {
		var message = $('#Message').val();
		//alertify.set('notifier', 'position', 'top-right');
		//alertify.notify(message, 'success', 5);
		AlertInfo(message);
	}

	// Display placeholder="" text as tooltip for :focused, non-empty inputs
	BindPlaceholderTooltips(); 
	$('.chosen-select').prepend($('<option>', {
		value: 0,
		text: '	Molimo odaberite...'
	}));
	$(".chosen-select").chosen();

	$('.datepicker').datepicker({
		format: "dd.mm.yyyy",
		autoclose: true,
		todayBtn: true,
	});

});

function BindPlaceholderTooltips() {
	$('form input').blur(function () {
		var inputVal = $(this).val(),
			titleText = $(this).attr('placeholder');
		if (inputVal != '') {
			$(this).tooltip({
				title: titleText,
				trigger: 'focus',
				container: 'form'
			});
		}
	});
}
// unblock when ajax activity stops 
$.blockUI.defaults.message = '<span class="loaderSpan">Molimo sačekajte...</span>'//'<img class="loaderImg" src="loaderV3.gif" runat="server" />';
$.blockUI.defaults.overlayCSS.backgroundColor = "#000";
$.blockUI.defaults.overlayCSS.opacity = 0.5;
$(document).ajaxStop($.unblockUI); 

//accounting.js settings
accounting.settings.currency.symbol = "";
accounting.settings.currency.decimal = ",";
accounting.settings.currency.thousand = ".";

alertify.set('notifier', 'position', 'top-right');

//Custom Alerts
function AlertSuccess(shouldReload) {
	$.confirm({
		title: 'Obaveštenje',
		content: 'Akcija je uspešno izvršena.',
		type: 'green',
		buttons: {
			Ok: {
				action: function () {
					if (shouldReload) {
						location.reload();
					}
					
				}
			}
		}
	});
}

function AlertError() {
	$.confirm({
		title: 'Greška',
		content: 'Došlo je do greške. Pokušajte ponovo.',
		type: 'red',
		typeAnimated: true,
		buttons: {
			Ok: {
				action: function () {
				}
			}
		}
	});
}

function AlertInfo(message) {
	$.confirm({
		title: 'Obaveštenje',
		content: message,
		type: 'blue',
		buttons: {
			Ok: {
				action: function () {
				
				}
			}
		}
	});
}