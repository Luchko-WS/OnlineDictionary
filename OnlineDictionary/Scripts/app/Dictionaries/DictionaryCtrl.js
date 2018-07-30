﻿(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .controller('DictionaryCtrl', DictionaryCtrl);

    DictionaryCtrl.$inject = ['DictionariesService', 'PhrasesPairsService'];

    function DictionaryCtrl(DictionariesService, PhrasesPairsService) {
        var vm = this;
        vm.phrasesPair = {};
        vm.phrasesPair.firstPhrase = {};
        vm.phrasesPair.secondPhrase = {};

        vm.init = init;
        vm.createPhrasesPair = createPhrasesPair;

        function init(dictionaryId) {
            vm.loaded = false;

            if (!dictionaryId) {
                console.log('miss dictionary id');
                vm.loaded = true;
            }

            DictionariesService.getDictionary(dictionaryId, 0, 100)
                .success(function (dictionary) {
                    vm.dictionary = dictionary;
                    vm.phrasesPair.firstPhrase.language = dictionary.sourceLanguage;
                    vm.phrasesPair.secondPhrase.language = dictionary.targetLanguage;
                    vm.phrasesPair.dictionaryId = dictionary.id;
                    vm.loaded = true;
                })
                .error(function (error) {
                    console.error(error);
                    vm.loaded = true;
                });
        }
    }

    function createPhrasesPair() {
        PhrasesPairsService.createPhrasesPair(vm.phrasesPair)
            .success(function (data) {

            })
            .error(function (error) {

            });
    }

})();