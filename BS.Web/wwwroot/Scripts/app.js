//use of auto complete
//var CustomerInputJsonMapping = {
//    'label': ['CONTACT_GROUP', 'CONTACT_NAME', 'CONTACT_PERSON'],
//    'value': 'CONTACT_NAME',
//    'SB_MASTER_VM_CONTACT_ID': 'ID',
//    'SB_MASTER_VM_CUSTOMER_REF_NO': 'CONTACT_PERSON',
//    'SB_MASTER_VM_CONTACT_BILL_TO_ID': 'ID',
//    'SB_MASTER_VM_CONTACT_BILL_TO_NAME': 'CONTACT_NAME',
//};
//autocompletesearch('/CRM/Contacts/FindCustomer', 3, 'SB_MASTER_VM_CONTACT_NAME', CustomerInputJsonMapping);
//url, search min length, search input, input and json property array
function autocompletesearch(url, minLength, searchInputId, mapping) {
    $(`#${searchInputId}`).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: url,
                dataType: "json",
                type: "POST",
                data: { "search_term": request.term },
                success: function (data) {
                    var mappedData = $.map(data, function (item) {
                        var mappedItem = {};
                        Object.keys(mapping).forEach(function (key) {
                            if (key === 'label') {
                                mappedItem[key] = mapping[key].map(prop => item[prop]).join(' - ');
                            } else {
                                mappedItem[key] = item[mapping[key]];
                            }
                        });
                        return mappedItem;
                    });
                    response(mappedData);
                },
                error: function (xhr, status, error) {
                    console.error("AJAX Error:", status, error);
                }
            });
        },
        minLength: minLength,
        change: function (event, ui) {
            Object.keys(mapping).forEach(function (key) {
                var targetId = key;
                var sourceProperty = mapping[key];
                //$(`#${targetId}`).val(ui.item[mapping[targetId]]);
                $(`#${targetId}`).val(ui.item[targetId]);
            });
        }
    });
}

function SearchGrid(options) {
    var title = options.Title ? options.Title : '...';
    var colsName = options.ColsName.split(',');
    var colsTitle = options.ColsTitle.split(',');
    var colsHidden = options.ColsHidden ? options.ColsHidden.split(',') : [];
    var tableHeaders = colsTitle.map(function (name) {
        return `<th>${name}</th>`;
    }).join('');

    // Create modal HTML structure
    var modalHTML = `<div class="modal fade" id="dynamicModal" tabindex="-1" role="dialog" aria-labelledby="modalLabel" aria-hidden="true">
          <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="modalLabel">Search result of - ${title}</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                    <table id="searchDataTable" class="display nowrap table-sm" style="cursor:pointer; width:100%;">
                        <thead class="table-info">
                            <tr>
                                ${tableHeaders}
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
              </div>
              <div class="modal-footer justify-content-between">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                <button id="btnSelectClose" type="button" class="btn btn-info" data-dismiss="modal">Select and Close</button>
              </div>
            </div>
          </div>
        </div>
      `;
    // Append modal HTML to body
    document.body.insertAdjacentHTML('beforeend', modalHTML);

    // Show the modal
    $('#dynamicModal').modal('show');

    // Remove the modal from the DOM when it is closed
    $('#dynamicModal').on('hidden.bs.modal', function () {
        $(this).remove();
    });

    //Hide column if it's in the hidden list
    var columns = colsName.map(function (col) {
        return {
            data: col.trim(),
            visible: !colsHidden.includes(col.trim())
        };
    });
    // Add column definition for the extra column
    //columns.push({
    //    data: null,
    //    defaultContent: '<button class="btn btn-sm btn-danger">Click!</button>',
    //    orderable: false
    //});
    var table = $('#searchDataTable').DataTable({
        ajax: {
            url: options.DataUrl,
            dataSrc: '', // Adjust this if the JSON response has a nested data structure
            type: options.DataMethod,
            data: options.DataParams
        },
        columns: columns,
        paging: true,
        searching: true,
        info: true,
        autoWidth: false,
        pageLength: 5,
        select: {
            style: 'multi' // Enable multiple row selection
        }
    });
    // Handle row selection
    $('#searchDataTable tbody').on('click', 'tr', function () {
        $(this).toggleClass('selected bg-info'); // Toggle selection
    });

    // Handle button click inside the table
    //$('#searchDataTable tbody').on('click', 'button', function () {
    //    var data = table.row($(this).closest('tr')).data();
    //    alert(data.CONTACT_NAME + "'s phone is: " + data.CONTACT_PERSON); // Adjust this alert as needed
    //});
    // Handle select and close button click
    $('#btnSelectClose').click(function () {
        var selectedData = table.rows('.selected').data().toArray();
        if (selectedData.length > 0) {
            options.onSelect(selectedData);
        }
    });
}