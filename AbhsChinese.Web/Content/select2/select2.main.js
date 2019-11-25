$(function () {
    let $select2Group = $('.select-group');
    let $selects = $select2Group.children();

    $selects.each(function (index, item) {
        let config = getSelect2Config(item);
        switch (index) {
            case 0:
                $(item).select2(config)
                    .on('select2:select', function () {
                        $selects[1].dataset.parentId = $(item).val();
                        resetSelect2($selects[1]);

                        $selects[2].dataset.parentId = -1;
                        resetSelect2($selects[2]);
                    });
                break;
            case 1:
                $(item).select2(config)
                    .on('select2:select', function () {
                        $selects[2].dataset.parentId = $(item).val();
                        resetSelect2($selects[2]);
                    });
                break;
            case 2:
                $(item).select2(config);
                break;
            default:
                break;
        }
    });

});

function resetSelect2(select2) {
    $(select2).val(null).trigger('change');

    let config = getSelect2Config(select2);
    $(select2).select2(config);
}

function getDataConfig(select2) {
    let dataConfig = function (params) {
        var query = {
            search: params.term,
            type: 'public',
            parentId: select2.dataset.parentId,
            region: select2.dataset.region
        }
        return query;
    };
    return dataConfig;
}

function getSelect2Config(item) {
    let config = {
        ajax: {
            url: item.dataset.url,
            data: function (params) {
                var query = {
                    search: params.term,
                    type: 'public',
                    parentId: item.dataset.parentId,
                    region: item.dataset.region
                }
                return query;
            },
            processResults: function (data) {
                let jsonData = JSON.parse(data);
                return {
                    results: jsonData
                };
            }
        },
        width: '200px'
    };

    return config;
}