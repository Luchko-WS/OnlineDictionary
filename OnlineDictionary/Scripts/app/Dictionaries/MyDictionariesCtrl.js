(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .controller('MyDictionariesCtrl', MyDictionariesCtrl);

    MyDictionariesCtrl.$inject = ['$uibModal', 'DictionariesService'];

    function MyDictionariesCtrl($uibModal, DictionariesService) {
        var vm = this;

        vm.createDictionary = createDictionary;

        init();

        function init() {
            DictionariesService.getMyDictionaries()
                .success(function (data) {
                    vm.myDictionaries = data;
                })
                .error(function (error) {
                    console.log(error);
                });
        }

        function createDictionary() {
            var modalInstance = $uibModal.open({
                templateUrl: '/Dictionaries/CreateDictionary',
                controller: 'CreateDictionaryCtrl',
                controllerAs: 'vm'
            });

            modalInstance.result.then(function (newDictionary) {
                vm.myDictionaries.push(newDictionary);
            });
        }
    }

})();