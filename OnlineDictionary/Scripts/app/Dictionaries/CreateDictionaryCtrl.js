(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .controller('CreateDictionaryCtrl', CreateDictionaryCtrl);

    CreateDictionaryCtrl.$inject = ['$uibModal', 'DictionariesService'];

    function CreateDictionaryCtrl($uibModal, DictionariesService) {
        var vm = this;

        vm.dictionary = {};

        vm.createDictionary = createDictionary;

        init();

        function init() {
            console.log('create dictionary ctrl init');
        }

        function createDictionary() {
            DictionariesService.createDictionary(vm.dictionary)
                .success(function (data) {
                    console.log(data);
                })
                .error(function (error) {
                    console.log(error);
                });
        }
    }

})();