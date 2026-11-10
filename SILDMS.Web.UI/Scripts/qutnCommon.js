/* qutnCommon.js
 * Shared behaviour for: Quotation to Client (prep), Recommendation, Approval, All Approved (print).
 * Usage: first line of each AngularJS controller ->  attachQutnCommon($scope, $http);
 * Page-specific functions defined AFTER this call override the ones here.
 */
window.attachQutnCommon = function ($scope, $http) {

    /* ── shared state ─────────────────────────────────────────── */
    $scope.ActionLog = [];
    $scope.ItemVendorDetail = [];
    $scope.ItemVendorMsg = '';
    $scope.BackInfo = null;                    // latest action when it was "Back" (banner on prep page)

    $scope.ClientReq = { TermsID: "", FormName: "" };
    $scope.ClientReqTerm = { ClientReqTermsID: "", ClientReqID: "", TermsID: "", TermsCode: "", TermsName: "" };
    $scope.clientReqTermArray = [];
    $scope.TermsConditionsList = [];
    $scope.TermsConditionsDisplayedCollection = [];

    /* ── labels ───────────────────────────────────────────────── */
    var ACTIONS = {
        Submit: 'Submitted', Resubmit: 'Re-submitted', Rec: 'Recommended',
        Aprv: 'Approved', Bck: 'Sent Back', Pst: 'Postponed'
    };
    var STAGES = { PREP: 'Preparation', RECM: 'Recommendation', APRV: 'Approval' };
    $scope.actionLabel = function (a) { return ACTIONS[a] || a; };
    $scope.stageLabel = function (s) { return STAGES[s] || s; };

    /* ── action history (requirements 2 and 4) ────────────────── */
    $scope.loadActionLog = function (clientReqID, openModal) {
        if (!clientReqID) return;
        $http.post('/QuotationToClient/GetActionLog', { ClientReqID: clientReqID })
            .success(function (r) {
                $scope.ActionLog = r.ActionLog || [];
                var last = $scope.ActionLog[0];
                $scope.BackInfo = (last && last.Action === 'Bck') ? last : null;
                if (openModal) $('#ActionLogModal').modal('show');
            })
            .error(function () { toastr.error('Failed to load history.'); });
    };

    $scope.showActionLog = function () {
        var id = ($scope.Client && $scope.Client.ClientReqID) ||
            ($scope.MasterData && $scope.MasterData.ClientReqID);
        if (!id) { toastr.error('Select a quotation first.'); return; }
        $scope.loadActionLog(id, true);
    };

    /* ── vendor detail popup: works for CS, NCS and QC items (requirement 7) ── */
    $scope.detailVendorCS = function (row) {
        var reqID = row.ClientReqID ||
            ($scope.Client && $scope.Client.ClientReqID) ||
            ($scope.MasterData && $scope.MasterData.ClientReqID);
        $http.post('/QuotationToClient/GetItemVendorDetail', { ClientReqID: reqID, ServiceItemID: row.ServiceItemID })
            .success(function (r) {
                $scope.ItemVendorDetail = r.ItemVendorDetail || [];
                $scope.ItemVendorMsg = $scope.ItemVendorDetail.length ? '' :
                    'No vendor quotation for this item (QC item, own cost).';
                $('#detailVendorCSModal').modal('show');
            })
            .error(function () { toastr.error('Failed to load vendor details.'); });
    };

    /* ── Back / Postpone must carry a reason ──────────────────── */
    $scope.validateAction = function (op, reason) {
        if (!op) { toastr.error('Please select an action.'); return false; }
        if ((op === 'Bck' || op === 'Pst') && !(reason || '').trim()) {
            toastr.error('Reason is mandatory for Back / Postpone.');
            return false;
        }
        return true;
    };

    /* ── page helpers ─────────────────────────────────────────── */
    $scope.toggleRefreshTable = function () { window.location.reload(); };
    $scope.refreshPage = function () { setTimeout(function () { window.location.reload(); }, 2000); };

    /* ── uploaded documents (PDF) ─────────────────────────────── */
    $scope.getDocumentList = function () {
        var ids = ($scope.Client && $scope.Client.DocumentID) ||
            ($scope.ClientDetails && $scope.ClientDetails[0] && $scope.ClientDetails[0].DocumentID);
        if (!ids) return [];
        return ids.split(',').map(function (s) { return s.trim(); }).filter(Boolean);
    };

    $scope.ViewDocument = function (docID) {
        if (!docID) { toastr.error("No document selected."); return; }
        docID = docID.replace(/\.pdf$/i, '');      // the action appends the extension itself
        $scope.loading = true;

        var url = "/ClientRequisition/ViewDocument?" +
            "&serverUrl=" + encodeURIComponent("MCL/Client_Requisition") +
            "&DocID=" + encodeURIComponent(docID) +
            "&ext=" + encodeURIComponent("pdf");

        $http.get(url, { responseType: 'arraybuffer' })
            .then(function (r) {
                var blob = new Blob([r.data], { type: 'application/pdf' });
                $("#pdfViewer").attr("src", URL.createObjectURL(blob) + "#view=Fit");
                $('#ShowPDFModal').modal('show');
            })
            .catch(function (e) { toastr.error("Failed to load the PDF."); console.error(e); })
            .finally(function () { $scope.loading = false; });
    };

    /* ── Terms & Conditions helpers ───────────────────────────── */
    $scope.ShowTermsConditionsList = function () {
        $http.get("/ClientRequisition/GetTermsConditionsList")
            .success(function (r) {
                $scope.TermsConditionsList = r.termsConditionsList;
                $scope.TermsConditionsDisplayedCollection = [].concat($scope.TermsConditionsList);
                $scope.loading = false;
            }).error(function () { });
        $('#ClientReqTermsConditionsModal').modal('show');
    };

    $scope.toggleTermsConditionsSelect = function (row) {
        $scope.ClientReq.TermsID = row.TermsID;
        $scope.ClientReq.FormName = row.FormName;
        $http.post('/ClientRequisition/GetClientReqTermAgainstFormList', { TermsID: row.TermsID })
            .success(function (r) {
                $scope.TermsConditionsList = r.ClientReqTermList;
                $scope.TermsConditionsDisplayedCollection = [].concat($scope.TermsConditionsList);
                $scope.loading = false;
            }).error(function () { $scope.loading = false; });
        $('#ClientReqTermsConditionsModal').modal('hide');
    };

    $scope.AddReqTermModal = function () { $('#RequisitionTermModal').modal('show'); };

    $scope.AddReqTerm = function () {
        if (!$('#TermsCode').val()) { toastr.error("Terms Code is required."); return; }
        if (!$('#TermsName').val()) { toastr.error("Terms Name is required."); return; }
        var newTerm = { TermsCode: $('#TermsCode').val(), TermsName: $('#TermsName').val() };
        $scope.clientReqTermArray.push(newTerm);
        $scope.TermsConditionsDisplayedCollection = $scope.TermsConditionsDisplayedCollection.concat(newTerm);
        $scope.resetClientReqTerm();
        $("#btnAddReqTerm").html('Add');
        $('#RequisitionTermModal').modal('hide');
    };

    $scope.resetClientReqTerm = function () {
        $scope.ClientReqTerm = { ClientReqTermsID: "", ClientReqID: "", TermsID: "", TermsCode: "", TermsName: "" };
    };

    $scope.toggleReqTermEdit = function (tableRow, index) {
        $('#RequisitionTermModal').modal('show');
        $scope.resetClientReqTerm();
        $scope.ClientReqTerm = angular.copy(tableRow);
        $scope.TermsConditionsDisplayedCollection.splice(index, 1);
        $scope.clientReqTermArray.splice(index, 1);
        $("#btnAddReqTerm").html('Update');
    };

    $scope.toggleReqTermDelete = function (tableRow, index) {
        $scope.resetClientReqTerm();
        $scope.TermsConditionsDisplayedCollection.splice(index, 1);
        $scope.clientReqTermArray.splice(index, 1);
        toastr.success('Data deleted successfully.');
    };


    $scope.formatDate = function (dateStr) {
        if (!dateStr) return '';
        var d;
        if (typeof dateStr === 'string' && dateStr.startsWith('/Date(')) {
            d = new Date(parseInt(dateStr.match(/\d+/)[0], 10));
        } else {
            d = new Date(dateStr);
        }
        if (isNaN(d.getTime())) return '';
        var day = String(d.getDate()).padStart(2, '0');
        var month = String(d.getMonth() + 1).padStart(2, '0');
        var year = d.getFullYear();
        return day + '/' + month + '/' + year;
    };

};