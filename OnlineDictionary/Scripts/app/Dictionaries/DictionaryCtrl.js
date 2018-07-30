(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .controller('DictionaryCtrl', DictionaryCtrl);

    DictionaryCtrl.$inject = ['DictionariesService'];

    function DictionaryCtrl(DictionariesService) {
        var vm = this;

        vm.init = init;

        function init(dictionaryId) {
            vm.loaded = false;

            if (!dictionaryId) {
                console.log('miss dictionary id');
                vm.loaded = true;
            }

            vm.dictionary = DictionariesService.getDictionary(dictionaryId, 0, 100)
                .success(function (dictionary) {
                    vm.dictionary = dictionary;
                    vm.loaded = true;
                })
                .error(function (error) {
                    console.error(error);
                    vm.loaded = true;
                });
        }
    }

})();