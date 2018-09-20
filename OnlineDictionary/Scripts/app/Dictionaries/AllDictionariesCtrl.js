(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .controller('AllDictionariesCtrl', AllDictionariesCtrl);

    AllDictionariesCtrl.$inject = ['$uibModal', 'DictionariesService', 'MessageService'];

    function AllDictionariesCtrl($uibModal, DictionariesService, MessageService) {
        var vm = this;
        vm.searchDictionaryByName = searchDictionaryByName;
        vm.extendedDictionarySearch = extendedDictionarySearch;
        vm.resetSearch = resetSearch;
        vm.downloadDictionary = downloadDictionary;
        init();

        function init() {
            getDictionaries();
        }

        function getDictionaries(filter) {
            vm.loaded = false;
            DictionariesService.getAllPublicDictionaries(filter)
                .success(function (data) {
                    vm.publicDictionaries = data;
                    vm.loaded = true;
                })
                .error(function (error) {
                    errorHandler(error);
                    vm.loaded = true;
                });
        }

        function searchDictionaryByName(name) {
            var filter = {
                name: name
            };
            getDictionaries(filter);
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

        function resetSearch() {
            vm.searchDictionaryName = null;
            getDictionaries();
        }

        function downloadDictionary(dictionary, format) {
            DictionariesService.downloadDictionary(dictionary.id, format)
                .success(DownloadFileService.makeLinkElement)
                .error(errorHandler);
        }

        function errorHandler(error) {
            console.error(error);
            MessageService.showMessage('commonErrorMessage', 'error');
        }
    }

})();