(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .directive('dictionaryItem', function (MessageService) {
            return {
                replace: true,
                link: function (scope, element, attrs) {

                },
                scope: {
                    dictionary: '=ngModel',
                    editDictionaryFunction: '=',
                    removeDictionaryFunction: '=',
                    enableEditing: '='
                },
                restrict: 'AE',
                templateUrl: "/Templates/DirectivesTemplates/DictionaryItem.html"
            };
        });
})();