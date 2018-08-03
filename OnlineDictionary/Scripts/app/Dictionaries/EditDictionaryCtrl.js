(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .controller('EditDictionaryCtrl', EditDictionaryCtrl);

    EditDictionaryCtrl.$inject = ['$uibModalInstance', 'DictionariesService', 'dicitonaryPar', 'MessageService'];

    function EditDictionaryCtrl($uibModalInstance, DictionariesService, dicitonaryPar, MessageService) {
        var vm = this;

        vm.cancel = function () { $uibModalInstance.dismiss('cancel'); }

        vm.editDictionary = editDictionary;

        init();

        function init() {
            console.log('edit dictionary ctrl init');
            vm.dictionary = dicitonaryPar;
        }

        function editDictionary() {
            DictionariesService.editDictionary(vm.dictionary.id, vm.dictionary)
                .success(function (data) {
                    $uibModalInstance.close(data);
                })
                .error(errorHandling(error));
        }

        function errorHandling(error) {
            console.error(error);
            MessageService.showMessage('commonErrorMessage', 'error');
        }
    }

})();