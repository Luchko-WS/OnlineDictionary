(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .controller('EditDictionaryCtrl', EditDictionaryCtrl);

    EditDictionaryCtrl.$inject = ['$uibModalInstance', 'DictionariesService', 'dicitonaryPar'];

    function EditDictionaryCtrl($uibModalInstance, DictionariesService, dicitonaryPar) {
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
                .error(function (error) {
                    console.log(error);
                });
        }
    }

})();