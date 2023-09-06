
function LoadCompanyJQGid() {
    debugger

    jQuery("#jqGridtblCompanyGrid").jqGrid({
        url: '/Company/CompanyGrid/'
        ,
        postData: {
            CompanyID: function () {
                return $('#ddlComapnyID_index').val();
            },
            CompanyName: function () {
                return $('#txtCompanyName_index').val();
            },
            CompanyTown: function () {
                return $('#txtCompanyTown_index').val();
            },
            CompanyPostCode: function () {
                return $('#txtCompanyPostCode_index').val();
            },
            DatabaseType: function () {
                return $('#ddldatabaseID_index').val();
            }
        },
        datatype: "json",
        colNames: ['Actions', 'Company ID', 'Company Name', 'Company Town', 'Company PostCode', 'Cus', 'Sup', 'Sub', 'Enq'],
        colModel: [
            { name: "actions", label: "Actions", width: 55, align: "center", formatter: actionsFormatter, sortable: false },
            { name: 'companyId', index: 'companyId', width: 80, key: true },
            { name: 'companyName', index: 'companyName', width: 200, editable: true, editoptions: { size: "180", maxlength: "200" } },
            { name: 'companyTown', index: 'companyTown', width: 120, editable: true, editoptions: { size: "100", maxlength: "200" } },
            { name: 'companyPostCode', index: 'companyPostCode', width: 100, editable: true, editoptions: { size: "80", maxlength: "100" } },
            { name: 'companyIsCustomer', index: 'companyIsCustomer', width: 70, align: "center", template: "booleanCheckboxFa", editable: true, edittype: "checkbox", editoptions: { value: "true:false" }, sortable: false },
            { name: 'companyIsSupplier', index: 'companyIsSupplier', width: 70, align: "center", template: "booleanCheckboxFa", editable: true, edittype: "checkbox", editoptions: { value: "true:false" }, sortable: false },
            { name: 'companyIsSubcontractor', index: 'companyIsSubcontractor', width: 70, align: "center", template: "booleanCheckboxFa", editable: true, edittype: "checkbox", editoptions: { value: "true:false" }, sortable: false },
            { name: 'companyIsEnquirer', index: 'companyIsEnquirer', width: 70, align: "center", template: "booleanCheckboxFa", editable: true, edittype: "checkbox", editoptions: { value: "true:false" }, sortable: false }
        ],

        onSelectRow: function (rowid, cellname, value, iRow, iCol) {
            debugger
            jQuery('#jqGridtblCompanyGrid').jqGrid('restoreRow', lastsel2);
            _CompanyID = rowid
            var rowData = jQuery('#jqGridtblCompanyGrid').jqGrid("getRowData", rowid);

            if (_CompanyID != null && _CompanyID != "") {
                //LoadPeopleGrid();
                //LoadPromptGrid();
                LoadContractGrid(rowData.companyName);
            }
        },
        afterSaveCell: function (rowid, cellname, value, iRow, iCol) {
            // Perform actions after saving the cell successfully
            console.log("Cell saved successfully:", rowid, cellname, value);
            // You can also make additional AJAX requests or update other parts of the grid if needed
        },
        rowNum: 20,
        iconSet: "fontAwesome",
        rowList: [20, 40, 60],
        pager: '#jqGridtblCompanyGridpager',
        sortname: 'companyId',
        viewrecords: true,
        sortorder: "asc",
        caption: "Company",
        editurl: "/Company/SaveCompanyDataFromGrid",
        gridComplete: function () {
            var _Firstrow = jQuery("#jqGridtblCompanyGrid").getDataIDs()[0];

            jQuery("#jqGridtblCompanyGrid").jqGrid("setSelection", _Firstrow);

        },
        ondblClickRow: function (id) { CompanyDetail(id, "") }
    });
    jQuery("#jqGridtblCompanyGrid").jqGrid('navGrid', '#jqGridtblCompanyGridpager', { edit: false, add: false, del: false });
    var $grid = $("#jqGridtblCompanyGrid"),
        newWidth = $grid.closest(".ui-jqgrid").parent().width();

    $grid.jqGrid("setGridWidth", newWidth, true);

}
// Custom formatter function for the actions column
function actionsFormatter(cellvalue, options, rowObject) {
    var editIcon = '<i class="fas jqgridediticon" title="Edit" onclick="editRow(' + options.rowId + ')"></i>';
    var deleteIcon = '<i class="fas fa-trash-alt" title="Delete" onclick="deleteRow(' + options.rowId + ')"></i>';

    return editIcon; //+ " " + deleteIcon;
}
function companyIsCustomerFormatter(cellvalue, options, rowObject) {
    debugger
    var editIcon = '<i class="fas jqgriduncheckicon" title="un checked" ></i>';
    if (options.rowData.companyIsCustomer == true) {
        editIcon = '<i class="fas jqgridcheckicon" title="Checked"></i>';
    }

    return editIcon;
}
function customCheckboxUnformat(cellValue, options, cell) {
    // Check if the cell contains the checkbox
    var checkbox = $(cell).find('input[type=checkbox]');

    // Return true or false based on the checkbox state
    return checkbox.is(':checked');
}
function checksave(result) {
    if (result.responseText == "") { alert("Update is missing!"); return false; }
    return true;
}
// Example functions for edit and delete actions
function editRow(rowId) {
    debugger
    if (rowId && rowId.id !== lastsel2) {
        jQuery('#jqGridtblCompanyGrid').jqGrid('restoreRow', lastsel2);
        jQuery('#jqGridtblCompanyGrid').jqGrid('editRow', rowId.id, true);


        lastsel2 = rowId.id;
        //$('input[type=text]').addClass('form-control');
    }
    console.log("Edit row ID: " + rowId);
    // Perform edit operation
}

function deleteRow(rowId) {
    console.log("Delete row ID: " + rowId);
    // Perform delete operation
}
function LoadCompanyGrid(isdefaultload) {

    _vcmpname = $('#txtCompanyName_index').val();
    jQuery('#jqGridtblCompanyGrid').jqGrid('restoreRow', lastsel2);
    $("#jqGridtblCompanyGrid").trigger("reloadGrid");

    //$.ajax({
    //    url: "/Company/CompanyGrid",
    //    data: { CompanyID: $('#ddlComapnyID_index').val(), CompanyName: $('#txtCompanyName_index').val(), CompanyTown: $('#txtCompanyTown_index').val(), CompanyPostCode: $('#txtCompanyPostCode_index').val(), DatabaseType: $('#ddldatabaseID_index').val() },
    //    success: function (result) {

    //        $("#divtblCompanyGrid").html(result);
    //        if (result != null && result.search('No Record Found') > 0) {
    //            return;
    //        }
    //        if ($.fn.DataTable.isDataTable('#dataTables-Companytbl')) {
    //            $('#dataTables-Companytbl').DataTable().destroy();
    //        }

    //        var tblcomp = $('#dataTables-Companytbl').DataTable({
    //            "pageLength": 25,
    //            bLengthChange: false,
    //            "bDestroy": true,
    //            language: {
    //                search: "<i class='ti-search'></i>",
    //                searchPlaceholder: 'Quick Search',
    //                paginate: {
    //                    next: "<i class='ti-arrow-right'></i>",
    //                    previous: "<i class='ti-arrow-left'></i>"
    //                }
    //            },
    //            columnDefs: [{
    //                visible: false
    //            }],
    //            responsive: true,
    //            searching: false,
    //        });
    //        $('#dataTables-Companytbl tbody').on('click', 'tr', function () {
    //            debugger
    //            if ($(this).hasClass('editrow')) {
    //               return false;
    //            }
    //            if ($(this).hasClass('selectedrow')) {
    //                $(this).removeClass('selectedrow');
    //            } else {
    //                tblcomp.$('tr.selectedrow').removeClass('selectedrow');
    //                $(this).addClass('selectedrow');
    //            }

    //            _CompanyID = $('#dataTables-Companytbl tbody tr.selectedrow').attr('data-companyid')
    //            if (_CompanyID != null && _CompanyID != "") {
    //                LoadPeopleGrid();
    //                LoadPromptGrid();
    //            }
    //        });
    //        $("#dataTables-Companytbl").on('mousedown.edit', "i.fa.fa-edit", function (e) {
    //            debugger
    //            $(this).removeClass().addClass("fa fa-save");
    //            var $row = $(this).closest("tr").off("mousedown");
    //            $row.addClass("editrow");
    //            var $tds = $row.find("td").not(':first').not(':last');

    //            $.each($tds, function (i, el) {
    //                var txt = $(this).text();
    //                $(this).html("").append("<input class='form-control' type='text' value=\"" + txt.trim() + "\">");
    //            });

    //        });
    //        $("#dataTables-Companytbl").on('mousedown.save', "i.fa.fa-save", function (e) {
    //            debugger

    //            var $row = $(this).closest("tr");
    //            var _dtCompanyID= $row.attr('data-companyid');
    //            var $tds = $row.find("td").not(':first').not(':last');

    //            var _txtCompanyName = "";
    //            var _txtCompanyTown = "";
    //            var _txtPostalCode = "";
    //            var _txtCus = "False";
    //            var _txtSup = "False";
    //            var _txtSub = "False";
    //            var _txtEnq = "False";
    //            var valid = true;
    //            $.each($tds, function (i, el) {
    //                debugger
    //                var txt = $(this).find("input").val()

    //                if (i == 0) {
    //                    _txtCompanyName = txt;
    //                    if (_txtCompanyName == null || _txtCompanyName == "") {
    //                        toastr.error("Please enter company name");
    //                        valid = false;
    //                        return false;
    //                    }
    //                }
    //                else if (i == 1) {
    //                    _txtCompanyTown = txt;
    //                }
    //                else if (i == 2) {
    //                    _txtPostalCode = txt;
    //                }
    //                else if (i == 3) {
    //                    if (txt.toLowerCase() == 'y') {
    //                        _txtCus = "True";
    //                    }
    //                    else if (txt.toLowerCase() == 'n') {

    //                    }
    //                    else {
    //                        toastr.error("Please enter 'Y' or 'N' in Yes/No fields.");
    //                        valid = false;
    //                        return false;
    //                    }

    //                }
    //                else if (i == 4) {
    //                    if (txt.toLowerCase() == 'y') {
    //                        _txtSup = "True";
    //                    }
    //                    else if (txt.toLowerCase() == 'n') {

    //                    }
    //                    else {
    //                        toastr.error("Please enter 'Y' or 'N' in Yes/No fields.");
    //                        valid = false;
    //                        return false;
    //                    }

    //                }
    //                else if (i == 5) {
    //                    if (txt.toLowerCase() == 'y') {
    //                        _txtSub = "True";
    //                    }
    //                    else if (txt.toLowerCase() == 'n') {

    //                    }
    //                    else {
    //                        toastr.error("Please enter 'Y' or 'N' in Yes/No fields.");
    //                        valid = false;
    //                        return false;
    //                    }
    //                }
    //                else if (i == 6) {
    //                    if (txt.toLowerCase() == 'y') {
    //                        _txtEnq = "True";
    //                    }
    //                    else if (txt.toLowerCase() == 'n') {

    //                    }
    //                    else {
    //                        toastr.error("Please enter 'Y' or 'N' in Yes/No fields.");
    //                        valid = false;
    //                        return false;
    //                    }
    //                }

    //            });
    //            if (valid) {
    //                $(this).removeClass().addClass("fa fa-edit");
    //                $row.removeClass("editrow");
    //                $.each($tds, function (i, el) {
    //                    debugger
    //                    var txt = $(this).find("input").val();
    //                    $(this).html(txt);
    //                });

    //                $.ajax({
    //                    url: "/Company/SaveCompanyDataFromGrid",
    //                    type: 'POST',
    //                    data: {
    //                        CompanyId: _dtCompanyID, CompanyName: _txtCompanyName
    //                        , CompanyTown: _txtCompanyTown
    //                        , CompanyPostCode: _txtPostalCode
    //                        , CompanyIsCustomer: _txtCus, CompanyIsSupplier: _txtSup
    //                        , CompanyIsSubcontractor: _txtSub, CompanyIsEnquirer: _txtEnq
    //                    },
    //                    success: function (result) {
    //                        if (result == -2) {
    //                            //already exist
    //                            toastr.error('Company Name already Exists..!!!');
    //                            return;
    //                        }
    //                        else if (result > 0) {
    //                            //success
    //                            toastr.success('Record saved scuccessfully..!!!');

    //                        }
    //                        else {
    //                            //fail
    //                            toastr.error('Some Error Occur');
    //                        }


    //                    },
    //                    error: function (result) {

    //                    },
    //                    beforeSend: function () {

    //                    },
    //                    complete: function (result) {

    //                    }
    //                });
    //            }
    //        });

    //        //$('#button').click(function () {
    //        //    tblcomp.row('.selected').remove().draw(false);
    //        //});
    //        //editor = new $.fn.dataTable.Editor({
    //        //    ajax: "../php/staff.php",
    //        //    table: "#dataTables-Companytbl",
    //        //    fields: [{
    //        //        label: "CompanyName",
    //        //        name: "CompanyName"
    //        //    }
    //        //    ]
    //        //});
    //        if (isdefaultload) {
    //            $('#dataTables-Companytbl').find('tbody tr:first').click();
    //        }
    //    },
    //    error: function (result) {

    //    },
    //    beforeSend: function () {

    //    },
    //    complete: function (result) {
    //        $(".overlay").hide();
    //    }
    //});
}
function LoadPeopleGrid() {
    $.ajax({
        url: "/Company/PeopleGrid",
        data: { CompanyID: _CompanyID },
        success: function (result) {

            $("#divtblPeopleGrid").html(result);
            if (result != null && result.search('No Record Found') > 0) {
                return;
            }
            if ($.fn.DataTable.isDataTable('#dataTables-Peopletbl')) {
                $('#dataTables-Peopletbl').DataTable().destroy();
            }
            var tblcomp = $('#dataTables-Peopletbl').DataTable({
                "pageLength": 5,
                bLengthChange: false,
                "bDestroy": true,
                language: {
                    search: "<i class='ti-search'></i>",
                    searchPlaceholder: 'Quick Search',
                    paginate: {
                        next: "<i class='ti-arrow-right'></i>",
                        previous: "<i class='ti-arrow-left'></i>"
                    }
                },
                columnDefs: [{
                    visible: false
                }],
                responsive: true,
                searching: false,
            });
            $('#dataTables-Peopletbl tbody').on('click', 'tr', function () {
                debugger
                if ($(this).hasClass('selectedrow')) {
                    $(this).removeClass('selectedrow');
                } else {
                    tblcomp.$('tr.selectedrow').removeClass('selectedrow');
                    $(this).addClass('selectedrow');
                }

                //_CompanyID = $('#dataTables-Companytbl tbody tr.selected').attr('data-companyid')
            });

            //$('#button').click(function () {
            //    tblcomp.row('.selected').remove().draw(false);
            //});

        },
        error: function (result) {

        },
        beforeSend: function () {

        },
        complete: function (result) {

        }
    });
}
function LoadPromptGrid() {
    $.ajax({
        url: "/Company/PromptsCallGrid",
        data: { CompanyID: _CompanyID },
        success: function (result) {
            debugger

            $("#divtblCallPromptGrid").html(result);
            if (result != null && result.search('No Record Found') > 0) {
                return;
            }
            if ($.fn.DataTable.isDataTable('#dataTables-CallPrompttbl')) {
                $('#dataTables-CallPrompttbl').DataTable().destroy();
            }
            var tblcomp = $('#dataTables-CallPrompttbl').DataTable({
                "pageLength": 5,
                bLengthChange: false,
                "bDestroy": true,
                language: {
                    search: "<i class='ti-search'></i>",
                    searchPlaceholder: 'Quick Search',
                    paginate: {
                        next: "<i class='ti-arrow-right'></i>",
                        previous: "<i class='ti-arrow-left'></i>"
                    }
                },
                columnDefs: [{
                    visible: false
                }],
                responsive: true,
                searching: false,
            });
            $('#dataTables-CallPrompttbl tbody').on('click', 'tr', function () {
                debugger
                if ($(this).hasClass('selectedrow')) {
                    $(this).removeClass('selectedrow');
                } else {
                    tblcomp.$('tr.selectedrow').removeClass('selectedrow');
                    $(this).addClass('selectedrow');
                }

                //_CompanyID = $('#dataTables-Companytbl tbody tr.selected').attr('data-companyid')
            });

            //$('#button').click(function () {
            //    tblcomp.row('.selected').remove().draw(false);
            //});

        },
        error: function (result) {

        },
        beforeSend: function () {

        },
        complete: function (result) {

        }
    });
}
function CompanyDetail(CompanyID, CompanyName) {
    $.ajax({
        url: "/Company/AddNewCompany",
        data: { CompanyID: CompanyID },
        success: function (result) {
            $('#LargeModalPopup').modal('show');
            $('#LargeModalPopupTitle').html("Client Information for : " + CompanyName);
            $('#LargeModalPopupBody').html(result);
        },
        error: function (result) {

        },
        beforeSend: function () {

        },
        complete: function (result) {

        }
    });
}
function ResetCompanyFilters() {
    $('#ddlComapnyID_index').val('').trigger("chosen:updated");
    $('#txtCompanyName_index').val('');
    $('#txtCompanyTown_index').val('');
    $('#txtCompanyPostCode_index').val('');
    $('#ddldatabaseID_index').val('');
    LoadCompanyGrid();
}
$(window).on("resize", function () {
    var $grid = $("#jqGridtblCompanyGrid"),
        newWidth = $grid.closest(".ui-jqgrid").parent().width();
    $grid.jqGrid("setGridWidth", newWidth, true);
});



function AddCompanySite(CompanyID, CompanyName) {

    $.ajax({
        url: "/Company/AddCompanySite",
        data: { CompanyID: CompanyID, CompanyName: CompanyName },
        success: function (result) {
            $('#MediumModalPopup').modal('show');
            $('#MediumModalPopupTitle').html("Client Sites");
            $('#MediumModalPopupBody').html(result);
        },
        error: function (result) {

        },
        beforeSend: function () {

        },
        complete: function (result) {

        }
    });
}
function GetCompanySites(_SiteCompanyID) {
    $.ajax({
        url: "/Company/GetCompanySites",
        data: { CompanyID: _SiteCompanyID },
        success: function (result) {
            var sihtml = "";
            if (result != null && result.length > 0) {
                $.each(result, function (key, value) {
                    ///companyId
                    sihtml += '<tr><td>' + value.contractLocation
                        + '</td><td><i title="Delete" style="color:red" class="fa fa-trash-alt"></i></td></tr>'
                });
                $("#companysitestbltrtd").html(sihtml);

            }


        },
        error: function (result) {

        },
        beforeSend: function () {

        },
        complete: function (result) {

        }
    });
}
function SaveCompanyInformation() {
    $('#hdnCompanyIsCustomer').val($('#CompanyIsCustomer').is(":checked"));
    $('#hdnCompanyIsSubcontractor').val($('#CompanyIsSubcontractor').is(":checked"));
    $('#hdnCompanyIsEnquirer').val($('#CompanyIsEnquirer').is(":checked"));
    $('#hdnCompanyApproved').val($('#CompanyApproved').is(":checked"));
    $.ajax({
        url: "/Company/SaveCompanyInformation",
        data: $('#frmCompanySave').serialize(),
        type: "post",
        success: function (result) {
            if (result > 0) {

                toastr.success("Record saved successfully..!!!");
                $('#LargeModalPopup').modal('hide');
                LoadCompanyGrid();
            }
            else {
                toastr.error("Unable to save record..!!!");
            }

        },
        error: function (result) {

        },
        beforeSend: function () {

        },
        complete: function (result) {

        }
    });
} 