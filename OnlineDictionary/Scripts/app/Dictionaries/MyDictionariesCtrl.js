(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .controller('MyDictionariesCtrl', MyDictionariesCtrl);

    MyDictionariesCtrl.$inject = ['$uibModal', 'DictionariesService', 'blockUI'];

    function MyDictionariesCtrl($uibModal, DictionariesService, blockUI) {
        var vm = this;

        vm.createDictionary = createDictionary;
        vm.editDictionary = editDictionary; 

        init();

        function init() {
            blockUI.start();
            DictionariesService.getMyDictionaries()
                .success(function (data) {
                    vm.myDictionaries = data;
                    blockUI.stop();
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

        function editDictionary(dictionary) {
            var modalInstance = $uibModal.open({
                templateUrl: '/Dictionaries/EditDictionary',
                controller: 'EditDictionaryCtrl',
                controllerAs: 'vm',
                resolve: {
                    dicitonaryPar: {
                        id: dictionary.id,
                        name: dictionary.name,
                        description: dictionary.description,
                        fromLanguage: dictionary.fromLanguage,
                        toLanguage: dictionary.toLanguage,
                        isPublic: dictionary.isPublic
                    }
                }
            });

            modalInstance.result.then(function (editedDictionary) {
                for (var i = 0; i < vm.myDictionaries.length; i++) {
                    if (vm.myDictionaries[i].id == editedDictionary.id) {
                        vm.myDictionaries[i].name = editedDictionary.name;
                        vm.myDictionaries[i].description = editedDictionary.description;
                        vm.myDictionaries[i].isPublic = editedDictionary.isPublic;
                        vm.myDictionaries[i].lastChangeDate = editedDictionary.lastChangeDate;
                        return;
                    }
                }
            });
        }
    }

})();