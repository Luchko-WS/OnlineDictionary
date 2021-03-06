﻿(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .factory('PhrasesPairsService', PhrasesPairsService);

    PhrasesPairsService.$inject = ['$http'];

    function PhrasesPairsService($http) {
        var service = {
            createPhrasesPair: createPhrasesPair,
            editPhrasesPair: editPhrasesPair,
            removePhrasesPair: removePhrasesPair,
            translate: translate
        };

        return service;

        function createPhrasesPair(phrasesPair) {
            return $http({
                method: 'POST',
                url: '/api/PhrasesPairs/Create',
                data: phrasesPair
            });
        }

        function editPhrasesPair(phrasesPairId, phrasesPairData) {
            return $http({
                method: 'PUT',
                url: '/api/PhrasesPairs/Edit/' + phrasesPairId,
                data: phrasesPairData
            });
        }

        function removePhrasesPair(phrasesPairId) {
            return $http({
                method: 'DELETE',
                url: '/api/PhrasesPairs/Remove/' + phrasesPairId
            });
        }

        function translate(phrase) {
            return $http.get('/api/PhrasesPairs/Transalte', {
                params: {
                    Text: phrase.text,
                    SourceLanguage: phrase.sourceLanguage,
                    TargetLanguage: phrase.targetLanguage,
                    LookThroughTheOwnPhrases: phrase.lookThroughTheOwnPhrases
                }
            });
        }
    }

})();