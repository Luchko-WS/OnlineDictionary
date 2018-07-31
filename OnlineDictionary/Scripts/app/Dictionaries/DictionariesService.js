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
            getDictionary: getDictionary,
            createDictionary: createDictionary,
            editDictionary: editDictionary,
            removeDictionary: removeDictionary
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

        function getDictionary(dictionaryId, skip, take) {
            return $http({
                method: 'GET',
                url: '/api/Dictionaries/Dictionary/' + dictionaryId + '/' + skip + '/' + take,
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
                method: 'PUT',
                url: '/api/Dictionaries/Edit/' + dictionaryId,
                data: dictionaryData
            });
        }

        function removeDictionary(dictionaryId) {
            return $http({
                method: 'DELETE',
                url: '/api/Dictionaries/Remove/' + dictionaryId
            });
        }
    }

})();