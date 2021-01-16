$(document).ready(function () {
	$('#Invoice_CustomerId').trigger("change");
	calculateInvoice();
	$("#createOrUpdateInvoiceForm").validate();
});

$('#Invoice_CustomerId').on('change', function (event, params) {

	$.blockUI();
	var id = $("#Invoice_CustomerId :selected").val(); 
		$.ajax({
			type: "GET",
			url: $("#GetCustomerUrl").val(),
			data: { customerId: id },
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
	var isValid = $('#createOrUpdateInvoiceForm').valid();
	if (isValid) {
		$.confirm({
			title: 'Upozorenje!',
			content: 'Da li ste sigurni?',
			type: 'blue',
			buttons: {
				Da: {
					btnClass: 'btn btn-primary',
					action: function () {
						$.blockUI();
						var invoice = GetInvoiceData();
						$.post($("#createOrUpdateInvoiceUrl").val(), { invoice: invoice }, function (result) {
							alertify.notify(successMessage, 'success', 5);
							window.location.href = $("#invoicesUrl").val();
							$.unblockUI();
						});
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
	invoice.BaseTotal = $("#baseTotalHidden").val().replace(".", ",");
	invoice.Total = $("#totalHidden").val().replace(".", ",");
	invoice.PDV = $("#pdvHidden").val();
	invoice.Note = $("#note").val();
	invoice.IsPaid = $("#Invoice_IsPaid").is(":checked");
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

function CreateOrUpdateInvoiceItem(id, index, isAdd) {
	var invoiceItem = {};
	if (id == 0 && isAdd!="true") {
		invoiceItem = getInvoiceIntemFromRow(index);
	}
	$.blockUI();
	$.ajax({
		type: "GET",
		url: $("#createOrUpdateInvoiceItemUrl").val(),
		data: { invoiceItemId: id, index: index, isAdd: isAdd, name: invoiceItem.Name, itemId: invoiceItem.ItemId, quantity: invoiceItem.Quantity, price: invoiceItem.Price },
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
	var isValid = $("#CreateOrUpdateInvoiceItemDialogForm").valid();
	if (isValid) {
		var ind = 0;
		var date = new Date();
		var createdAt = date.toLocaleDateString();
		var pdvBase = $("#pdvValue").val();
		if (IsAdd) {
			ind = $(".invoiceItemRow").length + 1;
		}
		else {
			ind = index;
			createdAt = $("#invoiceItemCreatedAt").val();
		}
		var quantity = parseFloat($("#invoiceItemQuantity").val());
		var price = parseFloat($("#invoiceItemPrice").val());
		var totalWithoutPDV = quantity * price;
		var pdvValue = ((quantity * price) / 100) * pdvBase;
		var totalValue = totalWithoutPDV + pdvValue;


		var newRow = '<tr style="background-color: antiquewhite;" class="hover invoiceItemRow" id="ItemRow-' + ind + '" onclick="CreateOrUpdateInvoiceItem(' + id + ',' + ind + ',\'false\')">';
		newRow += '<td  class="itemIndex"><span class="itemIndexNum">' + ind + ' </span><input type="hidden" class="invoiceItemId" value="' + id + '"> <input type="hidden" class="invoiceItemCreatedAt" value="' + createdAt + '"></td>';
		newRow += '<td class="itemId">' + $("#invoiceItemItemId").val() + '</td>';
		newRow += '<td class="itemName">' + $("#invoiceItemName").val() + '</td>';
		newRow += '<td class="itemUnit">kom</td>';
		newRow += '<td class="itemQuantity">' + quantity + '</td>';
		newRow += '<td class="itemPrice">' + accounting.formatMoney(price) + '</td>';
		newRow += '<td class="">0</td>';
		newRow += '<td class="">' + accounting.formatMoney(price) + '</td>';
		newRow += '<td class="itemPdvBase">' + accounting.formatMoney(totalWithoutPDV) + '</td>';
		newRow += '<td class="itemPDV">' + pdvBase + '</td>';
		newRow += '<td class="itemPdvValue">' + accounting.formatMoney(pdvValue) + '</td>';
		newRow += '<td class="itemPdvTotal">' + accounting.formatMoney(totalValue) + '</td>';
		newRow += '<td class=""><button onclick="DeleteInvoiceItem(0,this,event)" class="btn btn-danger" type="button"><i class="fa fa-trash-alt"></i></button></td>';
		newRow += '</tr>'
		if (IsAdd) {
			$('#invoiceItemsTable > tbody:last-child').append(newRow);
		}
		else {
			var itemRow = '#ItemRow-' + index;
			$(itemRow).replaceWith(newRow);
		}

		$('.NoDataTr').remove();
		$('#CreateOrUpdateInvoiceItemDialog').modal('toggle');

		calculateInvoice();
		alertify.notify(successMessage, 'success', 5);
	}
	
	
}

function GetInvoiceItems() {
	var invoiceItems = [];
	$('#invoiceItemsTable tbody tr').each((index, tr) => {
		var invoiceItem = {};
		invoiceItem.ItemId = $(tr).find(".itemId").html();
		invoiceItem.Name = $(tr).find(".itemName").html();
		invoiceItem.Unit = $(tr).find(".itemUnit").html();
		invoiceItem.Quantity = $(tr).find(".itemQuantity").html();
		invoiceItem.Price = accounting.unformat($(tr).find(".itemPrice").html(), ",");
		invoiceItem.InvoiceId = $('#id').val();
		invoiceItem.PDVValue = $(tr).find(".itemPDV").html();

		invoiceItem.Id = $(tr).find(".invoiceItemId").val();
		invoiceItem.CreatedAt = $(tr).find(".invoiceItemCreatedAt").val();
		invoiceItems.push(invoiceItem);
	});
	return invoiceItems;
}

function getInvoiceIntemFromRow(rowId) {
	var invoiceItem = {};
	var itemRow = '#ItemRow-' + rowId
	invoiceItem.Name = $(itemRow).find(".itemName").html();
	invoiceItem.ItemId = $(itemRow).find(".itemId").html();
	invoiceItem.Unit = $(itemRow).find(".itemUnit").html();
	invoiceItem.Quantity = $(itemRow).find(".itemQuantity").html();
	invoiceItem.Price = $(itemRow).find(".itemPrice").html();
	return invoiceItem;
}

function calculateInvoice() {
	var pdvBase = 0;
	var pdvValue = 0;
	var totalValue = 0;
	$('#invoiceItemsTable tbody tr').each((index, tr) => {
		pdvBase += accounting.unformat($(tr).find(".itemPdvBase").html(), ",");
		pdvValue += accounting.unformat($(tr).find(".itemPdvValue").html(), ",");
		totalValue += accounting.unformat($(tr).find(".itemPdvTotal").html(), ",");
	});
	$(".tdTotalWithoutPDV").html(accounting.formatMoney(pdvBase));
	$(".tdPDV").html(accounting.formatMoney(pdvValue));
	$(".tdTotal").html(accounting.formatMoney(totalValue));

	$("#baseTotalHidden").val(pdvBase);
	$("#totalHidden").val(totalValue);
	$("#pdvHidden").val(pdvValue);
}

function DeleteInvoiceItem(invoiceItemId, e, evt) {
	evt.stopPropagation();
	$.confirm({
		title: 'Upozorenje!',
		content: 'Da li ste sigurni?',
		type: 'blue',
		buttons: {
			Da: {
				btnClass: 'btn btn-primary',
				action: function () {
					if (invoiceItemId == 0) {
						e.parentNode.parentNode.parentNode.removeChild(e.parentNode.parentNode);
						calculateInvoice();
						RecalculateIndexes();
					}
					else {
						DeleteInvoiceItemAjax(invoiceItemId, e);
					}
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

function DeleteInvoiceItemAjax(invoiceItemId,e) {
	$.blockUI();
	$.ajax({
		type: "GET",
		url: $("#DeleteInvoiceItemUrl").val(),
		data: { invoiceItemId: invoiceItemId },
		contentType: 'application/json',
		dataType: "json",
		success: function (response) {
			AlertSuccess(false);
			e.parentNode.parentNode.parentNode.removeChild(e.parentNode.parentNode);
			calculateInvoice();
			RecalculateIndexes();
		},
		error: function (response) {
			AlertError();
		},
		complete: function () {
			$.unblockUI();
		}
	});
}

function RecalculateIndexes() {
	$('#invoiceItemsTable tbody tr').each((index, tr) => {
		index++;
		$(tr).find(".itemIndexNum").html(index);
	});
}


$("#Invoice_IsPaid").change(function (event) {
	var checkbox = event.target;
	if (checkbox.checked) {
		$("#isPaidPlaceholder").html('<span style="font-size:16px; color:#26B99A">Plaćen</span>');
	} else {
		$("#isPaidPlaceholder").html('<span style="font-size:16px; color:red">Nije plaćen</span>');
	}
});


