(function () {
    'use strict';

    angular.module('OnlineDictionary').factory('MessageService', MessageService);

    MessageService.$inject = ['$uibModal'];

    function MessageService($uibModal) {
        function showMessage(message, header) {
            var modalInstance = $uibModal.open({
                templateUrl: '/Templates/MessageServiceDialogWindows/OkDialogWindow.html',
                controller: ["$uibModalInstance", "modalParams", messageCtrl],
                controllerAs: "vm",
                resolve: {
                    modalParams: {
                        message: message,
                        header: header
                    }
                }
            });
            return modalInstance.result;
        }

        function showMessageYesNo(message, header) {
            var modalInstance = $uibModal.open({
                templateUrl: '/Templates/MessageServiceDialogWindows/YesNoDialogWindow.html',
                controller: ["$uibModalInstance", "modalParams", messageCtrl],
                controllerAs: "vm",
                resolve: {
                    modalParams: {
                        message: message,
                        header: header
                    }
                }
            });
            return modalInstance.result;
        }

        function showMessageCustom(message, header, buttons) {
            var modalInstance = $uibModal.open({
                templateUrl: '/Templates/MessageServiceDialogWindows/CustomDialogWindow.html',
                controller: ["$uibModalInstance", "modalParams", messageCtrl],
                controllerAs: "vm",
                resolve: {
                    modalParams: {
                        message: message,
                        header: header,
                        buttons: buttons
                    }
                }
            });
            return modalInstance.result;
        }

        function messageCtrl($uibModalInstance, modalParams) {
            var vm = this;
            vm.ok = ok;
            vm.cancel = cancel;
            vm.closeForResult = closeForResult;
            vm.modalParams = modalParams;

            function ok() {
                $uibModalInstance.close("OK");
            }

            function cancel() {
                $uibModalInstance.dismiss("Cancel");
            }

            function closeForResult(result) {
                $uibModalInstance.close(result);
            }
        }

        return {
            showMessage: showMessage,
            showMessageYesNo: showMessageYesNo,
            showMessageCustom: showMessageCustom,
        };
    }
})();