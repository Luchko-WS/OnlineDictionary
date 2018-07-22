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
            DictionariesService.getAllPublicDictionaries()
                .success(function (data) {
                    vm.publicDictionaries = data;
                })
                .error(function (error) {
                    console.log(error);
                });
        }
    }

})();