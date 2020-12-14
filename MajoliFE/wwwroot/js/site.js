// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var successMessage = "Akcija je uspešno izvršena.";

$(document).ready(function () {
	if ($('#Message').length > 0) {
		var message = $('#Message').val();
		alertify.set('notifier', 'position', 'top-right');
		alertify.notify(message, 'success', 5);
	}

	// Display placeholder="" text as tooltip for :focused, non-empty inputs
	BindPlaceholderTooltips(); 
	$('.chosen-select').prepend($('<option>', {
		value: 0,
		text: '	Molimo odaberite...'
	}));
	$(".chosen-select").chosen();

	$('.datepicker').datepicker({
		format: "dd-mm-yyyy",
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
$.blockUI.defaults.message = '<img class="loaderImg" src="~/loaderV3.gif" runat="server" />';
$.blockUI.defaults.overlayCSS.backgroundColor = "#000";
$.blockUI.defaults.overlayCSS.opacity = 0.5;
$(document).ajaxStop($.unblockUI); 

//accounting.js settings
accounting.settings.currency.symbol = "";

function formatCurrency(amount, decimalCount = 2, decimal = ".", thousands = ",") {
	try {
		decimalCount = Math.abs(decimalCount);
		decimalCount = isNaN(decimalCount) ? 2 : decimalCount;

		const negativeSign = amount < 0 ? "-" : "";

		let i = parseInt(amount = Math.abs(Number(amount) || 0).toFixed(decimalCount)).toString();
		let j = (i.length > 3) ? i.length % 3 : 0;

		return negativeSign + (j ? i.substr(0, j) + thousands : '') + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousands) + (decimalCount ? decimal + Math.abs(amount - i).toFixed(decimalCount).slice(2) : "");
	} catch (e) {
		console.log(e)
	}
};
