function ShowToUser() {
    new SearchGrid({
        Title: "Employee",
        ColsName: "ID,EMP_NO,EMP_NAME",
        ColsTitle: "ID,No,Name",
        ColsHidden: "ID",
        DataUrl: "/HRMS/Employees/FindEmployee",
        DataMethod: "POST",
        DataParams: '',
        onSelect: function (selectedData) {
            if (selectedData.length > 0) {
                var sd = selectedData[0];
                $('#SB_MASTER_VM_TO_USER_ID').val(sd.ID);
                $('#SB_MASTER_VM_TO_USER_NAME').val(sd.EMP_NAME);
            }
            else {
                alertify.error('An error occurred, while getting to user');
            }
        }
    });
}

function ShowContact() {
    new SearchGrid({
        Title: "Customer",
        ColsName: "ID,CONTACT_CODE,CONTACT_GROUP,CONTACT_NAME,CONTACT_PERSON,CONTACT_NO,OFFICE_ADDRESS,BALANCE_AMOUNT",
        ColsTitle: "ID,Code,Group,Name,Person,Contact No,Address,Balance",
        ColsHidden: "ID",
        DataUrl: "/CRM/Contacts/ShowCustomer",
        DataMethod: "GET",
        DataParams: '',
        onSelect: function (selectedData) {
            if (selectedData.length > 0) {
                var sd = selectedData[0];
                $('#SB_MASTER_VM_CONTACT_ID').val(sd.ID);
                $('#SB_MASTER_VM_CONTACT_NAME').val(sd.CONTACT_NAME);
                $('#SB_MASTER_VM_CONTACT_BILL_TO_ID').val(sd.ID);
                $('#SB_MASTER_VM_CONTACT_BILL_TO_NAME').val(sd.CONTACT_NAME);
                $('#SB_MASTER_VM_CUSTOMER_REF_NO').val(sd.CONTACT_PERSON);
                //find address
                $('#SB_MASTER_VM_DELIVERY_ADDRESS_ID').val('');
                $('#SB_MASTER_VM_DELIVERY_ADDRESS').val('');

                $.ajax({
                    url: `/CRM/Contacts/FindCustomerDefaultAddress?customerId=${sd.ID}`,
                    type: 'GET',
                    success: function (response) {
                        $('#SB_MASTER_VM_DELIVERY_ADDRESS_ID').val(response.ID);
                        $('#SB_MASTER_VM_DELIVERY_ADDRESS').val(response.OFFICE_ADDRESS);
                        $('#SB_MASTER_VM_CUSTOMER_REF_NO').val(response.CONTACT_PERSON);
                    },
                    error: function (error) {
                        console.error('Error fetching contact address:', error);
                    }
                });
            }
            else {
                console.log(selectedData);
                alertify.error('No contact selected');
            }
        }
    });
}

function ShowContactBill() {
    var contactId = $('#SB_MASTER_VM_CONTACT_ID').val();
    if (contactId == "") {
        alertify.error('No contact selected');
        return;
    }
    new SearchGrid({
        Title: "Customer Bill",
        ColsName: "ID,CONTACT_CODE,CONTACT_GROUP,CONTACT_NAME,CONTACT_PERSON,CONTACT_NO,OFFICE_ADDRESS,BALANCE_AMOUNT",
        ColsTitle: "ID,Code,Group,Name,Person,Contact No,Address,Balance",
        ColsHidden: "ID",
        DataUrl: "/CRM/Contacts/FindCustomerBillTo",
        DataMethod: "POST",
        DataParams: { "contactId": contactId },
        onSelect: function (selectedData) {
            if (selectedData.length > 0) {
                var sd = selectedData[0];
                $('#SB_MASTER_VM_CONTACT_BILL_TO_ID').val(sd.ID);
                $('#SB_MASTER_VM_CONTACT_BILL_TO_NAME').val(sd.CONTACT_NAME);
            }
            else {
                alertify.error('No contact selected');
            }
        }
    });
}

function ShowAddress() {
    var contactId = $('#SB_MASTER_VM_CONTACT_ID').val();
    if (contactId == "") {
        alertify.error('No contact selected');
        return;
    }
    new SearchGrid({
        Title: "Customer Address",
        ColsName: "ID,CONTACT_PERSON,CONTACT_NO,OFFICE_ADDRESS,IS_DEFAULT",
        ColsTitle: "ID,Person,Contact No,Address,Default",
        ColsHidden: "ID",
        DataUrl: "/CRM/Contacts/FindCustomerAddress",
        DataMethod: "GET",
        DataParams: { "customerId": contactId },
        onSelect: function (selectedData) {
            if (selectedData.length > 0) {
                var sd = selectedData[0];
                $('#SB_MASTER_VM_DELIVERY_ADDRESS_ID').val(sd.ID);
                $('#SB_MASTER_VM_DELIVERY_ADDRESS').val(sd.OFFICE_ADDRESS);
            }
            else {
                alertify.error('No contact selected');
            }
        }
    });
}




var FromUserIJM = {
    'label': ['EMP_NO', 'EMP_NAME'],
    'value': 'EMP_NAME',
    'SB_MASTER_VM_TO_USER_ID': 'ID',
    'SB_MASTER_VM_TO_USER_NAME': 'EMP_NAME',
};
autocompletesearch('/HRMS/Employees/FindEmployee', 3, 'SB_MASTER_VM_TO_USER_NAME', FromUserIJM);

var ProductsIJM = {
    'label': ['PRODUCT_NAME', 'PRODUCT_CODE', 'BASE_PRICE'],
    'value': 'PRODUCT_NAME',
    'PRODUCT_ID': 'ID',
    'MAIN_PRODUCT_ID': 'ID',
    'PRODUCT_DESC': 'PRODUCT_DESC',
    'UNIT_ID': 'UNIT_CHILD_ID',
    'UNIT_NAME': 'UNIT_NAME',
    'PRODUCT_RATE': 'BASE_PRICE',
    'VAT_PCT': 'VAT_PCT',
    'PRODUCT_WEIGHT': 'WEIGHT_PER_UNIT',
};
autocompletesearch('/Inventory/Products/FindProductsForSalesBooking', 3, 'PRODUCT_NAME1', ProductsIJM);

var ColsName1;
var ColsTitle1;
var ColsHidden1;
var isgi = 'cf32e179-a0c6-437a-8f5a-27517586bb06';
function GetTableSetup() {
    $.ajax({
        url: "/Inventory/ItemMaster/GetTableSetup",
        dataType: "json",
        type: "POST",
        data: { item_sub_group_id: isgi },
        success: function (data) {
            ColsName1 = data.COLUMN_NAME;
            ColsTitle1 = data.COLUMN_TITLE;
            ColsHidden1 = data.COLUMN_HIDDEN;
        },
        error: function (xhr, status, error) {
            console.error("AJAX Error:", status, error);
        }
    });
}

$("#PRODUCT_NAME").blur(function () {
    GetTableSetup();
    if (ColsName1 == "undefined") {
        return;
    }
    let productName = $('#PRODUCT_NAME').val();
    if (productName) {
        new SearchGrid({
            Title: "Products",
            //ColsName: "ID,PRODUCT_NAME,PRODUCT_DESC,UNIT_CHILD_ID,UNIT_NAME,BASE_PRICE,VAT_PCT,WEIGHT_PER_UNIT," + ,
            //ColsTitle: "ID,Product,Description,UNIT_CHILD_ID,Unit,Base Price, VAT %, Weight per Unit",
            //ColsHidden: "ID,UNIT_CHILD_ID",
            ColsName: "ID,HS_CODE,LEAD_DAYS,MASTER_ITEM_CODE,MASTER_BAR_CODE,ITEM_NAME,WARRANTY_DAYS,EXPIRY_DAYS,IS_MAIN_ITEM,VAT_PCT,BASE_PRICE,ITEM_CODE,BAR_CODE,ITEM_DESC," + ColsName1,
            ColsTitle: "ID,HS Code,Lead Days,MASTER_ITEM_CODE,MASTER_BAR_CODE,Item Name,Warranty,Expiry,Main Item?,Vat%,Base Price,Code,Barcode,Item Detail," + ColsTitle1,
            ColsHidden: "ID,MASTER_ITEM_CODE,MASTER_BAR_CODE," + ColsHidden1,
            DataUrl: "/Inventory/ItemMaster/GetItemDetailsForSalesBookingBySubGroupIDByItemName",
            DataMethod: "POST",
            DataParams: { "productName": productName },
            onSelect: function (selectedData) {
                if (selectedData.length > 0) {
                    var sd = selectedData[0];
                    $('#PRODUCT_ID').val(sd.ID);
                    $('#PRODUCT_NAME').val(sd.PRODUCT_NAME);
                    $('#MAIN_PRODUCT_ID').val(sd.ID);
                    $('#PRODUCT_DESC').val(sd.PRODUCT_DESC);
                    $('#UNIT_ID').val(sd.UNIT_CHILD_ID);
                    $('#UNIT_NAME').val(sd.UNIT_NAME);
                    $('#PRODUCT_RATE').val(sd.BASE_PRICE);
                    $('#VAT_PCT').val(sd.VAT_PCT);
                    $('#PRODUCT_WEIGHT').val(sd.WEIGHT_PER_UNIT);
                }
                else {
                    alertify.error('No product selected');
                }
            }
        })
    }
});

$("#DELIVERY_ADDRESS").blur(function () {
    var contactId = $('#SB_MASTER_VM_CONTACT_ID').val();
    if (contactId == "") {
        alertify.error('No contact selected');
        return;
    }
    new SearchGrid({
        Title: "Customer Address",
        ColsName: "ID,CONTACT_PERSON,CONTACT_NO,OFFICE_ADDRESS,IS_DEFAULT",
        ColsTitle: "ID,Person,Contact No,Address,Default",
        ColsHidden: "ID",
        DataUrl: "/CRM/Contacts/FindCustomerAddress",
        DataMethod: "GET",
        DataParams: { "customerId": contactId },
        onSelect: function (selectedData) {
            if (selectedData.length > 0) {
                var sd = selectedData[0];
                $('#DELIVERY_ADDRESS_ID').val(sd.ID);
                $('#DELIVERY_ADDRESS').val(sd.OFFICE_ADDRESS);
            }
            else {
                alertify.error('No contact selected');
            }
        }
    });
});

//create a new row to data detail table
function AddNewRow(button) {
    var row = $(button).closest('tr');
    var isValid = true;
    row.find('input.need').each(function () {
        if ($(this).val().trim() === "") {
            isValid = false;
            return false; // break out of the each loop
        }
    });
    if (!isValid) {
        $.notify('Star marked input fields must be filled out!', 'error');
        return;
    }
    // Clone the row
    var clonedRow = row.clone();

    // Change the button in the last td to a delete button
    clonedRow.find('td:last-child').html('<button type="button" class="remove-row btn btn-xs btn-danger" data-id="0" title="Remove this line"><i class="fa fa-trash"></i></button>');

    // Append the cloned row to the tbody with class DATA_DETAIL
    $('tbody.DATA_DETAIL').append(clonedRow);

    row.find('input.clear').each(function () {
        $(this).val('');
    });
    //Table Summary
    GetTableSummary();
}
//delete a row from the mentioned table 'DATA_DETAIL'
$(document).on('click', 'button.remove-row', function (e) {
    e.preventDefault();
    var $self = $(this);
    if ($(this).attr('data-id') == "0") {
        $(this).parents('tr').css("background-color", "#D2042D").fadeOut(400, function () {
            $(this).remove();
            //calculate net of table
            GetTableSummary();
        });
    }
});

//table summary
function GetTableSummary() {
    var total_cost = 0;
    $("tbody.DATA_DETAIL > tr").each(function () {
        var rate = $(this).find('td:eq(3) input').val();
        var qty = $(this).find('td:eq(4) input').val();
        var item_cost_amount = parseFloat(rate) * parseFloat(qty);
        total_cost += item_cost_amount;
        $(this).find('td:eq(8) input').val(item_cost_amount);
    });
    $("#SB_MASTER_VM_TRN_AMOUNT").val(total_cost.toFixed(3));
    $("#SB_MASTER_VM_NET_AMOUNT").val(total_cost.toFixed(3));

    var paidAmt = $("#SB_MASTER_VM_PAID_AMOUNT").val();
    $("#SB_MASTER_VM_DUE_AMOUNT").val((total_cost - parseFloat(paidAmt)).toFixed(3));
    $("#btnSubmit").attr("disabled", false);
};

//submit form
function formSubmitView(bodyClass) {
    var datatbody = $('tbody.' + bodyClass);
    // Check if there are no rows
    if (datatbody.children().length == 0) {
        $.notify('At least one Product is required', 'warn');
        return;
    }
    $("tbody.DATA_ENTRY > tr").remove();
    $('#btnSubmit').addClass('disabled').children().addClass('fa-spin');

    // Remove previously added hidden inputs
    //$("#frm1 input[name^='PO_DETAIL']").remove();
    // Iterate over each row in the tbody
    datatbody.find('tr').each(function (index) {
        const $inputs = $(this).find('input,select');
        $inputs.each(function () {
            $("#frm1").prepend("<input type='hidden' name='" + $(this).attr('name').split('.')[0] + "[" + index + "]." + $(this).attr('name').split('.')[1] + "' value='" + $(this).val() + "'>");
        })
    });
    //perform form submit
}