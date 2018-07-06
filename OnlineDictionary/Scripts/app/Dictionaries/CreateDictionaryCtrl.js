(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .controller('CreateDictionaryCtrl', CreateDictionaryCtrl);

    CreateDictionaryCtrl.$inject = ['$uibModal', '$uibModalInstance', 'DictionariesService'];

    function CreateDictionaryCtrl($uibModal, $uibModalInstance, DictionariesService) {
        var vm = this;

        vm.dictionary = {};
        vm.cancel = function () { $uibModalInstance.dismiss('cancel'); }
        vm.createDictionary = createDictionary;

        init();

        function init() {
            console.log('create dictionary ctrl init');
        }

        function createDictionary() {
            DictionariesService.createDictionary(vm.dictionary)
                .success(function (data) {
                    $uibModalInstance.close(data);
                })
                .error(function (error) {
                    console.log(error);
                });
        }
    }

})();