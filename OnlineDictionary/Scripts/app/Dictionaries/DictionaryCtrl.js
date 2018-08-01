﻿(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .controller('DictionaryCtrl', DictionaryCtrl);

    DictionaryCtrl.$inject = ['DictionariesService', 'PhrasesPairsService'];

    function DictionaryCtrl(DictionariesService, PhrasesPairsService) {
        var vm = this;

        vm.init = init;
        vm.getDictionaryWithFilteredPhrasesPairs = getDictionaryWithFilteredPhrasesPairs;
        vm.createPhrasesPair = createPhrasesPair;
        vm.editPhrasesPair = editPhrasesPair;
        vm.deletePhrasePair = deletePhrasePair;

        function init(dictionaryId) {

            vm.loaded = false;

            if (!dictionaryId) {
                console.error('miss dictionary id');
                vm.loaded = true;
                return;
            }

            DictionariesService.getDictionary(dictionaryId)
                .success(function (data) {
                    vm.dictionary = data;
                    vm.loaded = true;
                })
                .error(function (error) {
                    console.error(error);
                    vm.loaded = true;
                });
        }

        function getDictionaryWithFilteredPhrasesPairs(dictionaryId, filter) {
            return DictionariesService.getDictionary(dictionaryId, filter);
        }

        function createPhrasesPair(phrasesPair) {
            return PhrasesPairsService.createPhrasesPair(phrasesPair);
        }

        function editPhrasesPair(id, data) {
            return PhrasesPairsService.editPhrasesPair(id, data);
        }

        function deletePhrasePair(phrasesPairId) {
            return PhrasesPairsService.removePhrasesPair(phrasesPairId);
        }
    }
})();