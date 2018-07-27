(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .controller('MyDictionariesCtrl', MyDictionariesCtrl);

    MyDictionariesCtrl.$inject = ['$uibModal', 'DictionariesService', 'MessageService'];

    function MyDictionariesCtrl($uibModal, DictionariesService, MessageService) {
        var vm = this;

        vm.createDictionary = createDictionary;
        vm.editDictionary = editDictionary; 
        vm.removeDictionary = removeDictionary;

        init();

        function init() {
            vm.loaded = false;
            DictionariesService.getMyDictionaries()
                .success(function (data) {
                    vm.myDictionaries = data;
                    vm.loaded = true;
                })
                .error(function (error) {
                    vm.loaded = true;
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

        function removeDictionary(dictionaryId) {
            MessageService.showMessageYesNo("Do you want to remove this dictionary?", "Remove dictionary")
                .then(function (result) {
                    if (result === "OK") {
                        //removing of dictionary is not implemented yet
                        console.log('remove dictionary');
                    }
                });
        }
    }

})();