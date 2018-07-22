(function () {
    'use strict';
    window.app = angular.module('OnlineDictionary',
        [
            'ui.bootstrap',
            'blockUI'
        ]);

    app.config(['blockUIConfig'], function (blockUIConfig) {
        blockUIConfig.templateUrl = '~/Templates/Shared/BlockUiTemplate.html';
    });
})();