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
            console.log('my dictionaries ctrl init');
        }

        function createDictionary() {
            var modalInstance = $uibModal.open({
                templateUrl: '/Dictionaries/CreateDictionary',
                controller: 'CreateDictionaryCtrl',
                controllerAs: 'vm'
            });

            modalInstance.result.then(function (updatedQueen) {
                alert('closed');
            });
        }
    }

})();