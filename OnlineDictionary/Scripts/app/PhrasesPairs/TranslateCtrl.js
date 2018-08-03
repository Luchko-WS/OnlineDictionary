(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .controller('TranslateCtrl', TranslateCtrl);

    TranslateCtrl.$inject = ['PhrasesPairsService', 'MessageService'];

    function TranslateCtrl(PhrasesPairsService, MessageService) {
        var vm = this;
        vm.phrase = {};
        vm.translateResults = [];

        vm.translate = translate;
        vm.clear = clear;

        init();

        function init() {
            vm.loaded = true;
        }

        function translate() {
            vm.loaded = false;
            PhrasesPairsService.translate(vm.phrase)
                .success(function (data) {
                    console.log(data);
                    vm.translateResults = data;
                    vm.loaded = true;
                })
                .error(function (error) {
                    errorHandler(error);
                    vm.loaded = true;
                });
        }

        function errorHandler(error) {
            console.error(error);
            MessageService.showMessage('commonErrorMessage', 'error');
        }

        function clear() {
            vm.phrase = {};
        }
    }
})();