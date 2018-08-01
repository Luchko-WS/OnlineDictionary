(function () {
    'use strict';
    window.app = angular.module('OnlineDictionary',
        [
            'ui.bootstrap'
        ]);

    /*app.config(['$translateProvider', function ($translateProvider) {
        $translateProvider.useUrlLoader('/api/Lexicon');
        $translateProvider.useSanitizeValueStrategy('escapeParameters');
    }]);*/
})();