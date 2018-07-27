angular.module('OnlineDictionary').directive('asyncPageWithLoader', function ($compile, $templateRequest) {
    return {
        transclude: true,
        replace: true,
        link: function (scope, element, attrs) {
            // scope.$watch('isLoaded', function (newValue, oldValue) {
            //    if (!newValue) {
            //        console.log(newValue);
            //    }
            //    else {
            //        console.log(newValue);
            //     }
            //     console.log(scope.isLoaded);
            //});
        },
        scope: {
            isLoaded: '=',
        },
        restrict: 'E',
        templateUrl: '/Templates/DirectivesTemplates/LoadingSpinnerTemplate.html'
    };
});