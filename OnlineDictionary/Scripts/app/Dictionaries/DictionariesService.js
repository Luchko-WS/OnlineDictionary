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

        function getAllPublicDictionaries(filter) {
            if (filter != null) {
                return $http.get('/api/Dictionaries/GetAllPublicDictionaries', {
                    params: {
                        Name: filter.name,
                        SourceLanguage: filter.sourceLanguage,
                        TargetLanguage: filter.targetLanguage,
                        OwnerId: filter.ownerId
                    }
                });
            }
            else {
                return $http({
                    method: 'GET',
                    url: '/api/Dictionaries/GetAllPublicDictionaries',
                });
            }
        }

        function getMyDictionaries(filter) {
            if (filter != null) {
                return $http.get('/api/Dictionaries/GetMyDictionaries', {
                    params: {
                        Name: filter.name,
                        SourceLanguage: filter.sourceLanguage,
                        TargetLanguage: filter.targetLanguage,
                        OwnerId: filter.ownerId
                    }
                });
            }
            else {
                return $http({
                    method: 'GET',
                    url: '/api/Dictionaries/GetMyDictionaries',
                });
            }
        }

        function getDictionary(dictionaryId, phrasesPairsFilter) {
            if (phrasesPairsFilter != null) {
                return $http.get('/api/Dictionaries/Dictionary/' + dictionaryId, {
                    params: {
                        SourceLanguageValue: phrasesPairsFilter.sourceLanguageValue,
                        TargetLanguageValue: phrasesPairsFilter.targetLanguageValue
                    }
                });
            }
            else {
                return $http({
                    method: 'GET',
                    url: '/api/Dictionaries/Dictionary/' + dictionaryId
                });
            }
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