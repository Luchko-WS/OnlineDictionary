(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .factory('DictionariesService', DictionariesService);

    DictionariesService.$inject = ['$http'];

    function DictionariesService($http) {
        var service = {
            createDictionary: createDictionary 
        };

        return service;

        function createDictionary(dictionary) {
            return $http({
                method: 'POST',
                url: '/api/Dictionaries/Create',
                data: dictionary
            });
        }
    }

})();