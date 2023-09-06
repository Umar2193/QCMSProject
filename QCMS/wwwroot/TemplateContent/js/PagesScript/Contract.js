function LoadContractJQGid() {
    debugger

    jQuery("#jqGridtblContractGrid").jqGrid({
        url: '/Contract/ContractGrid/'
        ,
        postData: {
            CompanyID: function () {
                return _CompanyID;
            }
        },
        datatype: "json",
        colNames: [/*'Actions',*/ 'Contract Number','contractCompanyId', 'Contract Location', 'EnquiryID', 'ContractCustOrderNo', 'ContractName', 'ContractInvoiceType', 'EquipmentType'],
        colModel: [
            //{ name: "actions", label: "Actions", width: 55, align: "center", formatter: actionsFormatter, sortable: false },
            { name: 'contractNumber', index: 'contractNumber', width: 144, key: true },
            { name: 'contractCompanyId', index: 'contractCompanyId',hidden: true,  },
            { name: 'contractLocation', index: 'contractLocation', width: 200  },
            { name: 'contractQuotationNo', index: 'contractQuotationNo', width: 120, sortable: false   },
            { name: 'contractCustOrderNo', index: 'contractCustOrderNo', width: 144, sortable: false },
            { name: 'contractName', index: 'contractName', width: 144, sortable: false },
            { name: 'contractInvoiceType', index: 'contractInvoiceType', width: 144, sortable: false },
            { name: 'equipmentType', index: 'equipmentType', width: 160, sortable: false },
            
        ],

        onSelectRow: function (rowid, cellname, value, iRow, iCol) {
            //jQuery('#jqGridtblContractGrid').jqGrid('restoreRow', lastsel2);
            //_CompanyID = rowid
            if (_CompanyID != null && _CompanyID != "") {
                //LoadPeopleGrid();
                //LoadPromptGrid();
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
        pager: '#jqGridtblContractGridpager',
        sortname: 'contractNumber',
        viewrecords: true,
        sortorder: "asc",
        caption: "Contract",
       // editurl: "/Company/SaveCompanyDataFromGrid",
        ondblClickRow: function (id) {
            debugger
            var rowData = jQuery('#jqGridtblContractGrid').jqGrid("getRowData", id);

            ContractDetail(rowData.contractCompanyId, rowData.contractNumber)
        }
    });
    jQuery("#jqGridtblContractGrid").jqGrid('navGrid', '#jqGridtblContractGridpager', { edit: false, add: false, del: false });
    var $grid = $("#jqGridtblContractGrid"),
    newWidth = $grid.closest(".ui-jqgrid").parent().width();
    $grid.jqGrid("setGridWidth", '1219', true);
}
function LoadContractGrid(CompanyName) {

    //_vcmpname = $('#txtCompanyName_index').val();
    //jQuery('#jqGridtblContractGrid').jqGrid('restoreRow', lastsel2);
    $("#jqGridtblContractGrid").trigger("reloadGrid");
    $('#lblCOmpanyName_Cntr').html(CompanyName);
  

}
$(window).on("resize", function () {
    var $grid = $("#jqGridtblContractGrid"),
        newWidth = $grid.closest(".ui-jqgrid").parent().width();
    $grid.jqGrid("setGridWidth", newWidth, true);
});
function ContractDetail(CompanyID, ContractNo) {
    debugger
    $.ajax({
        url: "/Contract/ContractSearchDetail",
        data: { CompanyID: CompanyID, ContractNo: ContractNo },
        success: function (result) {
            $('#ExtraLargeModalPopup').modal('show');
            $('#ExtraLargeModalPopupTitle').html("Contract Detail");
            $('#ExtraLargeModalPopupBody').html(result);
        },
        error: function (result) {

        },
        beforeSend: function () {

        },
        complete: function (result) {

        }
    });
}
function GetContraclDetail(CompanyID, ContractNo,thiss) {

    debugger
    $('#ContractSearchtbl tr').removeClass("selectedrow");
    $(thiss).addClass("selectedrow");
    $.ajax({
        url: "/Contract/GetContraclDetail",
        data: { CompanyID: CompanyID, ContractNo: ContractNo },
        success: function (result) {

            $('#rightsectionContractDetail').html(result);
        },
        error: function (result) {

        },
        beforeSend: function () {

        },
        complete: function (result) {

        }
    });
}
function ClickEventForContractDetailPurchaseList(thiss) {
    $('#PurchaseorderSearchtbl tr').removeClass("selectedrow");
    $(thiss).addClass("selectedrow");
}
function PurchaseOrderDetail(PurchaseId, ContractNo, PurchaseCompanyId) {
    debugger
    $.ajax({    
        url: "/Contract/GetPurchaseOrderDetail",
        data: { PurchaseId: PurchaseId, ContractNo: ContractNo, CompanyID: PurchaseCompanyId },
        success: function (result) {
            $('#2ExtraLargeModalPopup').modal('show');
            $('#2ExtraLargeModalPopupTitle').html("Purchase Order Detail");
            $('#2ExtraLargeModalPopupBody').html(result);
        },
        error: function (result) {

        },
        beforeSend: function () {

        },
        complete: function (result) {

        }
    });
} 

function BasketSearchtblRowClick(thiss) {
    debugger
    $('#BasketSearchtbl tr').removeClass("selectedrow");
    $(thiss).addClass("selectedrow");
}
function ProductSearchtblRowClick(thiss) {
    $('#ProductSearchtbl tr').removeClass("selectedrow");
    $(thiss).addClass("selectedrow");
}   

function ProductSearchtblRowDoubleClick(thiss) {
    debugger
    ItemMovingBasket(thiss);
}
function MoveItemtoBasket() {
    debugger
    var thiss = $('#ProductSearchtbl tr.selectedrow');
    ItemMovingBasket(thiss);
}
function ItemMovingBasket(thiss) {
    var id = $(thiss).attr("data-id");
    var ProductName = $(thiss).attr("data-productname");
    var ProductPrice = $(thiss).attr("data-productprice");

    var lght = $('#BasketSearchtbl tr[data-id=' + id + ']');
    if (lght.length == 0) {
        var currentrow = "<tr ondblclick='LoadUpdateQuantity(this)' onclick='BasketSearchtblRowClick(this);' data-PurchaseItemId='-1' data-id='" + id + "' data-ProductName='" + ProductName + "' data-ProductPrice='" + ProductPrice + "' data-Quantity='1' data-TotalPrice='" + ProductPrice + "'><td>" + id + "</td><td>" + ProductName + "</td><td class='ProductPrice'>£" + ProductPrice + "</td><td class='Quantity'>1</td><td class='TotalPrice'>£" + ProductPrice + "</td></tr>";
        $('#BasketSearchtbl tbody').append(currentrow);
    }
    CalculateTotalBasketValue();
}
function RemoveItemfromBasket() {
    var thiss = $('#BasketSearchtbl tr.selectedrow');
    var id = $(thiss).attr("data-id");
    var isLock = $(thiss).attr("data-Lock");
    if (isLock != 1) {
        $('#BasketSearchtbl tr[data-id="' + id + '"]').remove();
    }

    CalculateTotalBasketValue();
}
function CalculateTotalBasketValue() {

    var _totalprice = 0.00;
    $('#BasketSearchtbl tr').each(function () {
        debugger
        //var ProductPrice = $(this).find(".ProductPrice").html();
        //var Quantity = $(this).find(".Quantity").html();
     
        var TotalPrice = $(this).attr("data-totalprice");;
        if (TotalPrice != null) {
            _totalprice = Number(_totalprice) + Number(TotalPrice);
            _totalprice = parseFloat(_totalprice).toFixed(2);
        }
    });
    $('#txtbaskettotal').val('£'+ _totalprice);
}

function LoadUpdateQuantity(thiss) {
    var thiss = $('#BasketSearchtbl tr.selectedrow');
    var PurchaseItemId = $(thiss).attr("data-purchaseitemid");
    var id = $(thiss).attr("data-id");
    var ProductName = $(thiss).attr("data-productname");
    var ProductPrice = $(thiss).attr("data-productprice");
    var Quantity = $(thiss).attr("data-quantity");
    $('#_prPurchaseProductID').val(id);
    $('#_prlblProductName').html(ProductName);
    $('#_prOrginalProductPrice').val(ProductPrice);
    $('#_prQuantity').val(Quantity);
    $('#PiceChangeSmallModalPopup').modal('show');

}
function SaveUpdatedQuantity() {
    debugger
    var _quantity = $('#_prQuantity').val();
    var _prNewProductPrice = $('#_prNewProductPrice').val()
    if (!(_quantity >= 0) ){
        toastr.error("Please enter valid quantity");
        return false;
    }
    if (_prNewProductPrice != null && _prNewProductPrice != '' && !(_prNewProductPrice >= 0)) {
        toastr.error("Please enter valid new price");
        return false;
    }
    var selectedrtowid = $('#_prPurchaseProductID').val();
    var selectedbasketrow = $('#BasketSearchtbl').find('tr[data-id="' + selectedrtowid + '"]');
    if (_quantity != null && _quantity != '' && _quantity > 0) {
        $(selectedbasketrow).attr("data-quantity", _quantity) 
    }
    if (_prNewProductPrice != null && _prNewProductPrice != '' && _prNewProductPrice > 0) {
        $(selectedbasketrow).attr("data-productprice", _prNewProductPrice) 
    }
    else {
        _prNewProductPrice = $('#_prOrginalProductPrice').val();
    }
    var totalprice = 0;
   
     totalprice = parseFloat(_prNewProductPrice * _quantity).toFixed(2);
    $(selectedbasketrow).attr("data-totalprice", totalprice);
    $('#BasketSearchtbl tr[data-id="' + selectedrtowid + '"] td.ProductPrice').html("£" + _prNewProductPrice);
    $('#BasketSearchtbl tr[data-id="' + selectedrtowid + '"] td.Quantity').html(_quantity);
    $('#BasketSearchtbl tr[data-id="' + selectedrtowid + '"] td.TotalPrice').html("£" + totalprice);
    OverallCalculateTotalBasketValue();
    toastr.success("updated successfully...");
    $('#PiceChangeSmallModalPopup').modal('hide');
   
}

function OverallCalculateTotalBasketValue() {
    debugger
    var SearchFieldsTable = $("#BasketSearchtbl tbody");

    var trows = SearchFieldsTable.children("tr");
    var totalprice = 0;
    $.each(trows, function (index, row) {
        debugger
        var _totalprice = $(row).attr("data-totalprice");
        totalprice = Number(totalprice) + Number(_totalprice);
       
    });
    totalprice = parseFloat(totalprice).toFixed(2);
    $('#txtbaskettotal').val('£' + totalprice);
}
function SavePurchaseOrderDetail() {
    debugger
    var SearchFieldsTable = $("#BasketSearchtbl tbody");

    var trows = SearchFieldsTable.children("tr");
    var TotalPOPrice = 0;
    var localBasket = [];
    $.each(trows, function (index, row) {
        debugger
        var $itm = $(row);
        localBasket.push({
            'PurchaseItemId': $itm.attr('data-purchaseitemid'),
            'PurchaseProductId': $itm.attr('data-id'),
            'ProductName': $itm.attr('data-productname'),
            'ProductPrice': $itm.attr('data-productprice'),
            'Quantity': $itm.attr('data-quantity'),
            'TotalPrice': $itm.attr('data-totalprice'),
        });
        TotalPOPrice = TotalPOPrice + Number($itm.attr('data-totalprice'));
        debugger
        var s = "";

    });
    TotalPOPrice = parseFloat(TotalPOPrice).toFixed(2);
    var purchaseorderDetail = new Object();
    purchaseorderDetail.PurchaseId = $('#frmPurchaseDetail #PurchaseId').val();
    purchaseorderDetail.PurchaseContractNumber = $('#frmPurchaseDetail #PurchaseContractNumber').val();
    purchaseorderDetail.PurchaseCompanyId = $('#frmPurchaseDetail #PurchaseCompanyId').val();
    purchaseorderDetail.PurchaseOrderDate = $('#frmPurchaseDetail #PurchaseOrderDate').val();
    purchaseorderDetail.PurchaseRequiredDate = $('#frmPurchaseDetail #PurchaseRequiredDate').val();
    purchaseorderDetail.PurchaseRequiredTime = $('#frmPurchaseDetail #PurchaseRequiredTime').val();
    purchaseorderDetail.PurchaseOrderNotes = $('#frmPurchaseDetail #PurchaseOrderNotes').val();
    purchaseorderDetail.PurchaseDeliveryAddress = $('#frmPurchaseDetail #PurchaseDeliveryAddress').val();
    purchaseorderDetail.TotalPurchase = TotalPOPrice;

    $.ajax({
        url: "/Contract/SavePurhcaseOrderDetail",
        type:"Post",
        data: { purchaseorderDetail: purchaseorderDetail, localBasket: localBasket },
        success: function (result) {
            if (result > 0) {
                toastr.success("Record saved successfully...!!!");
                $('#ContractSearchtbl tr.selectedrow').click();
                $('#2ExtraLargeModalPopup').modal('hide');
            }
            else {
                toastr.error("Some Error occur");
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