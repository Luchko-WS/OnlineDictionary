(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .controller('AllDictionariesCtrl', AllDictionariesCtrl);

    AllDictionariesCtrl.$inject = ['DictionariesService'];

    function AllDictionariesCtrl(DictionariesService) {
        var vm = this;

        init();

        function init() {
            vm.loaded = false;
            DictionariesService.getAllPublicDictionaries()
                .success(function (data) {
                    vm.publicDictionaries = data;
                    vm.loaded = true;
                })
                .error(function (error) {
                    console.log(error);
                    vm.loaded = true;
                });
        }
    }

})();