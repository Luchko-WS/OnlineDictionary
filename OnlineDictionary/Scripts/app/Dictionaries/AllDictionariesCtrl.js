(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .controller('AllDictionariesCtrl', AllDictionariesCtrl);

    AllDictionariesCtrl.$inject = ['$uibModal', 'DictionariesService'];

    function AllDictionariesCtrl($uibModal, DictionariesService) {
        var vm = this;
        vm.searchDictionaryByName = searchDictionaryByName;
        vm.extendedDictionarySearch = extendedDictionarySearch;
        vm.downloadDictionary = downloadDictionary;
        init();

        function init() {
            getDictionaries();
        }

        function searchDictionaryByName(name) {
            var filter = {
                name: name
            };
            getDictionaries(filter);
        }

        function getDictionaries(filter) {
            vm.loaded = false;
            DictionariesService.getAllPublicDictionaries(filter)
                .success(function (data) {
                    vm.publicDictionaries = data;
                    vm.loaded = true;
                })
                .error(function (error) {
                    console.error(error);
                    vm.loaded = true;
                });
        }

        function extendedDictionarySearch() {
            var modalInstance = $uibModal.open({
                templateUrl: '/Dictionaries/SearchDictionary',
                controller: 'SearchDictionaryCtrl',
                controllerAs: 'vm',
                resolve: {
                    searchPar: {
                        hideOwnerId: false,
                    }
                }
            });

            modalInstance.result.then(function (filter) {
                getDictionaries(filter);
            });
        }

        function downloadDictionary(dictionary) {
            DictionariesService.downloadDictionary(dictionary.id)
                .success(DownloadFileService.makeLinkElement)
                .error(function (error) {
                    console.error(error);
                });
        }
    }

})();