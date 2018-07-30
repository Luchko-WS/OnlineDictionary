(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .factory('PhrasesPairsService', PhrasesPairsService);

    PhrasesPairsService.$inject = ['$http'];

    function PhrasesPairsService($http) {
        var service = {
            createPhrasesPair: createPhrasesPair
        };

        return service;

        function createPhrasesPair(phrasesPair) {
            return $http({
                method: 'POST',
                url: '/api/PhrasesPairs/Create',
                data: phrasesPair
            });
        }
    }

})();