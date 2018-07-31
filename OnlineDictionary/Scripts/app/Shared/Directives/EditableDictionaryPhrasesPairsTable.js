(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .directive('editableDictionaryPhrasesPairsTable', function () {
            return {
                replace: true,
                link: function (scope, element, attrs) {
                    scope.editFormIsShowed = false;

                    if (scope.enableEditing) {
                        scope.phrasesPair = {};
                        scope.phrasesPair.firstPhrase = {};
                        scope.phrasesPair.secondPhrase = {};
                        scope.phrasesPair.firstPhrase.language = scope.dictionary.sourceLanguage;
                        scope.phrasesPair.secondPhrase.language = scope.dictionary.targetLanguage;
                        scope.phrasesPair.dictionaryId = scope.dictionary.id;

                        scope.createPhrasesPair = createPhrasesPair;
                        scope.showEditForm = showEditForm;
                        scope.cancel = cancel;

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

                        function showEditForm() {
                            scope.editFormIsShowed = true;
                        }

                        function cancel() {
                            scope.editFormIsShowed = false;
                        }
                    }
                },
                scope: {
                    dictionary: '=ngModel',
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