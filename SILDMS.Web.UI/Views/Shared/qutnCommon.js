window.attachQutnCommon = function ($scope, $http) {
    $scope.ActionLog = [];
    $scope.ItemVendorDetail = [];
    $scope.ItemVendorMsg = '';
    $scope.BackInfo = null;               // set when the latest action was Back (prep page banner)

    var ACTIONS = {
        Submit: 'Submitted', Resubmit: 'Re-submitted', Rec: 'Recommended',
        Aprv: 'Approved', Bck: 'Sent Back', Pst: 'Postponed'
    };
    var STAGES = { PREP: 'Preparation', RECM: 'Recommendation', APRV: 'Approval' };
    $scope.actionLabel = function (a) { return ACTIONS[a] || a; };
    $scope.stageLabel = function (s) { return STAGES[s] || s; };

    $scope.loadActionLog = function (clientReqID, openModal) {
        if (!clientReqID) return;
        $http.post('/QuotationToClient/GetActionLog', { ClientReqID: clientReqID })
            .success(function (r) {
                $scope.ActionLog = r.ActionLog || [];
                var last = $scope.ActionLog[0];
                $scope.BackInfo = (last && last.Action === 'Bck') ? last : null;
                if (openModal) $('#ActionLogModal').modal('show');
            });
    };

    $scope.showActionLog = function () {
        var id = ($scope.Client && $scope.Client.ClientReqID) || ($scope.MasterData && $scope.MasterData.ClientReqID);
        if (!id) { toastr.error('Select a quotation first.'); return; }
        $scope.loadActionLog(id, true);
    };

    $scope.detailVendorCS = function (row) {
        var reqID = row.ClientReqID || ($scope.Client && $scope.Client.ClientReqID) || ($scope.MasterData && $scope.MasterData.ClientReqID);
        $http.post('/QuotationToClient/GetItemVendorDetail', { ClientReqID: reqID, ServiceItemID: row.ServiceItemID })
            .success(function (r) {
                $scope.ItemVendorDetail = r.ItemVendorDetail || [];
                $scope.ItemVendorMsg = $scope.ItemVendorDetail.length ? '' : 'No vendor quotation for this item (QC item, own cost).';
                $('#detailVendorCSModal').modal('show');
            });
    };

    $scope.validateAction = function (op, reason) {
        if (!op) { toastr.error('Please select an action.'); return false; }
        if ((op === 'Bck' || op === 'Pst') && !(reason || '').trim()) {
            toastr.error('Reason is mandatory for Back / Postpone.'); return false;
        }
        return true;
    };
    $scope.openConfirmModal = function (action) {
        if (!$scope.validateAction($scope.MasterData.Operation, $scope.MasterData.ActionReason)) return;
        $scope.SaveQuotToClient(action);
    };

};