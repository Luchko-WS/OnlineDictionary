(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .controller('SearchDictionaryCtrl', SearchDictionaryCtrl);

    SearchDictionaryCtrl.$inject = ['$uibModalInstance', 'searchPar'];

    function SearchDictionaryCtrl($uibModalInstance, searchPar) {
        var vm = this;
        vm.filter = {};

        vm.cancel = function () { $uibModalInstance.dismiss('cancel'); }
        vm.searchDictionary = searchDictionary;

        init();

        function init() {
            vm.searchPar = searchPar;
        }

        function searchDictionary() {
           $uibModalInstance.close(vm.filter);
        }
    }

})();