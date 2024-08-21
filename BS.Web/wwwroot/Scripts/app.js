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
            };