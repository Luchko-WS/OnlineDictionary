(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .factory('PhrasesPairsService', PhrasesPairsService);

    PhrasesPairsService.$inject = ['$http'];

    function PhrasesPairsService($http) {
        var service = {
            createPhrasesPair: createPhrasesPair,
            removePhrasesPair: removePhrasesPair
        };

        return service;

        function createPhrasesPair(phrasesPair) {
            return $http({
                method: 'POST',
                url: '/api/PhrasesPairs/Create',
                data: phrasesPair
            });
        }

        function removePhrasesPair(phrasesPairId) {
            return $http({
                method: 'DELETE',
                url: '/api/PhrasesPairs/Remove/' + phrasesPairId
            });
        }
    }

})();