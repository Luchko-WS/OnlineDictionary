(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .directive('editableDictionaryPhrasesPairsTable', function (MessageService) {
            return {
                replace: true,
                link: function (scope, element, attrs) {

                    scope.filterValues = {};
                    scope.toggleFilter = toggleFilter;
                    scope.applyFilter = applyFilter;

                    function toggleFilter() {
                        scope.filterIsShowed = !scope.filterIsShowed;

                        if (!scope.filterIsShowed) {
                            scope.filterValues.sourceLanguageValue = null; 
                            scope.filterValues.targetLanguageValue = null;
                            applyFilter();
                        }
                    }

                    function applyFilter(filter) {
                        scope.filterPairsPromise(scope.dictionary.id, filter)
                            .success(function (data) {
                                scope.dictionary = data;
                            })
                            .error(function (error) {
                                console.error(error);
                            });
                    }

                    if (scope.enableEditing) {
                        scope.phrasesPair = {};
                        scope.phrasesPair.firstPhrase = {};
                        scope.phrasesPair.secondPhrase = {};
                        scope.phrasesPair.firstPhrase.language = scope.dictionary.sourceLanguage;
                        scope.phrasesPair.secondPhrase.language = scope.dictionary.targetLanguage;
                        scope.phrasesPair.dictionaryId = scope.dictionary.id;

                        scope.createPhrasesPair = createPhrasesPair;
                        scope.editPhrasesPair = editPhrasesPair;
                        scope.removePhrasesPair = removePhrasesPair;
                        scope.toggleCreatingForm = toggleCreatingForm;
                        scope.toogleEditFormForItem = toogleEditFormForItem;

                        function createPhrasesPair(phrasesPair) {
                            scope.createPhrasesPairPromise(phrasesPair)
                                .success(function (data) {
                                    scope.dictionary.phrasesPairs.unshift(data);
                                    scope.phrasesPair.firstPhrase.text = null;
                                    scope.phrasesPair.secondPhrase.text = null;
                                })
                                .error(function (error) {
                                    console.error(error);
                                });
                        }

                        function editPhrasesPair(phrasesPair) {
                            phrasesPair.firstPhrase.text = phrasesPair.tmpSourceLangText;
                            phrasesPair.secondPhrase.text = phrasesPair.tmpTargetLangText;
                            scope.editPhrasesPairPromise(phrasesPair.id, phrasesPair)
                                .success(function (data) {
                                    for (var i = 0; i < scope.dictionary.phrasesPairs.length; i++) {
                                        if (scope.dictionary.phrasesPairs[i].id == phrasesPair.id) {
                                            scope.dictionary.phrasesPairs[i].firstPhrase.text = data.firstPhrase.text;
                                            scope.dictionary.phrasesPairs[i].secondPhrase.text = data.secondPhrase.text;
                                            toogleEditFormForItem(scope.dictionary.phrasesPairs[i]);
                                            return;
                                        }
                                    }
                                })
                                .error(function (error) {
                                    console.error(error);
                                })
                        }

                        function removePhrasesPair(phrasesPairId) {
                            MessageService.showMessageYesNo("Do you want to remove this pair?", "Remove pair")
                                .then(function (result) {
                                    if (result === "OK") {
                                        scope.removePhrasesPairPromise(phrasesPairId)
                                            .success(function (data) {
                                                for (var i = 0; i < scope.dictionary.phrasesPairs.length; i++) {
                                                    if (scope.dictionary.phrasesPairs[i].id === data.id) {
                                                        scope.dictionary.phrasesPairs.splice(i, 1);
                                                        break;
                                                    }
                                                }
                                            })
                                            .error(function (error) {
                                                console.error(error);
                                            });
                                    }
                                });
                        }

                        function toggleCreatingForm () {
                            scope.creatingFormIsShowed = !scope.creatingFormIsShowed;
                        }

                        function toogleEditFormForItem(phrasesPair) {
                            phrasesPair.editMode = !phrasesPair.editMode;
                            if (phrasesPair.editMode) {
                                phrasesPair.tmpSourceLangText = phrasesPair.firstPhrase.text;
                                phrasesPair.tmpTargetLangText = phrasesPair.secondPhrase.text;
                            }
                        }
                    }
                },
                scope: {
                    dictionary: '=ngModel',
                    filterPairsPromise: '=',
                    createPhrasesPairPromise: '=',
                    editPhrasesPairPromise: '=',
                    removePhrasesPairPromise: '=',
                    enableEditing: '='
                },
                restrict: 'AE',
                templateUrl: "/Templates/DirectivesTemplates/EditableDictionaryPhrasesPairsTable.html"
            };
        });
})();