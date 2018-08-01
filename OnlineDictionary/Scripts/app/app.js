(function () {
    'use strict';
    window.app = angular.module('OnlineDictionary',
        [
            'ui.bootstrap',
            'pascalprecht.translate'
        ]);

    app.config(['$translateProvider', function ($translateProvider) {
        $translateProvider.useUrlLoader('/api/Languages');
        $translateProvider.useSanitizeValueStrategy('escapeParameters');
        $translateProvider.preferredLanguage('ru');
    }]);
})();