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
                            .error(errorHandler);
                    }

                    function errorHandler(error) {
                        console.error(error);
                        MessageService.showMessage('commonErrorMessage', 'error');
                    }

                    if (scope.enableEditing) {
                        scope.newPhrasesPair = {};
                        scope.newPhrasesPair.firstPhrase = {};
                        scope.newPhrasesPair.secondPhrase = {};
                        scope.newPhrasesPair.firstPhrase.language = scope.dictionary.sourceLanguage;
                        scope.newPhrasesPair.secondPhrase.language = scope.dictionary.targetLanguage;
                        scope.newPhrasesPair.dictionaryId = scope.dictionary.id;

                        scope.createPhrasesPair = createPhrasesPair;
                        scope.editPhrasesPair = editPhrasesPair;
                        scope.removePhrasesPair = removePhrasesPair;
                        scope.toggleCreatingForm = toggleCreatingForm;
                        scope.toogleEditFormForItem = toogleEditFormForItem;
                        scope.checkValid = checkValid;
                       
                        function checkValid(phrasesPair) {
                            phrasesPair.invalid = !(phrasesPair.firstPhrase.text && phrasesPair.secondPhrase.text);
                        }

                        function createPhrasesPair(phrasesPair) {
                            scope.createPhrasesPairPromise(phrasesPair)
                                .success(function (data) {
                                    scope.dictionary.phrasesPairs.unshift(data);
                                    toggleCreatingForm();
                                })
                                .error(errorHandler);
                        }

                        function editPhrasesPair(phrasesPair) {
                            scope.editPhrasesPairPromise(phrasesPair.id, phrasesPair)
                                .success(function (data) {
                                    for (var i = 0; i < scope.dictionary.phrasesPairs.length; i++) {
                                        if (scope.dictionary.phrasesPairs[i].id == phrasesPair.id) {
                                            scope.dictionary.phrasesPairs[i].firstPhrase.text = data.firstPhrase.text;
                                            scope.dictionary.phrasesPairs[i].secondPhrase.text = data.secondPhrase.text;
                                            scope.dictionary.phrasesPairs[i].editMode = false;
                                            return;
                                        }
                                    }
                                })
                                .error(errorHandler);
                        }

                        function removePhrasesPair(phrasesPairId) {
                            MessageService.showMessageYesNo('removePairQuestion', 'removePair')
                                .then(function (result) {
                                    if (result === 'OK') {
                                        scope.removePhrasesPairPromise(phrasesPairId)
                                            .success(function (data) {
                                                for (var i = 0; i < scope.dictionary.phrasesPairs.length; i++) {
                                                    if (scope.dictionary.phrasesPairs[i].id === data.id) {
                                                        scope.dictionary.phrasesPairs.splice(i, 1);
                                                        break;
                                                    }
                                                }
                                            })
                                            .error(errorHandler);
                                    }
                                });
                        }

                        function toggleCreatingForm() {
                            scope.creatingFormIsShowed = !scope.creatingFormIsShowed;
                            if (scope.creatingFormIsShowed) {
                                checkValid(scope.newPhrasesPair);
                            }
                            else {
                                scope.newPhrasesPair.firstPhrase.text = null;
                                scope.newPhrasesPair.secondPhrase.text = null;
                            }
                        }

                        function toogleEditFormForItem(phrasesPair) {
                            phrasesPair.editMode = !phrasesPair.editMode;
                            if (phrasesPair.editMode) {
                                checkValid(phrasesPair);
                                phrasesPair.tmpSourceLangText = phrasesPair.firstPhrase.text;
                                phrasesPair.tmpTargetLangText = phrasesPair.secondPhrase.text;
                            }
                            else {
                                phrasesPair.firstPhrase.text = phrasesPair.tmpSourceLangText;
                                phrasesPair.secondPhrase.text = phrasesPair.tmpTargetLangText; 
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