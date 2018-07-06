(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .factory('DictionariesService', DictionariesService);

    DictionariesService.$inject = ['$http'];

    function DictionariesService($http) {
        var service = {
            getAllPublicDictionaries: getAllPublicDictionaries,
            getMyDictionaries: getMyDictionaries,
            createDictionary: createDictionary,
            editDictionary: editDictionary
        };

        return service;

        function getAllPublicDictionaries() {
            return $http({
                method: 'GET',
                url: '/api/Dictionaries/GetAllPublicDictionaries',
            });
        }

        function getMyDictionaries() {
            return $http({
                method: 'GET',
                url: '/api/Dictionaries/GetMyDictionaries',
            });
        }

        function createDictionary(dictionary) {
            return $http({
                method: 'POST',
                url: '/api/Dictionaries/Create',
                data: dictionary
            });
        }

        function editDictionary(dictionaryId, dictionaryData) {
            return $http({
                method: 'POST',
                url: '/api/Dictionaries/Edit/' + dictionaryId,
                data: dictionaryData
            });
        }
    }

})();