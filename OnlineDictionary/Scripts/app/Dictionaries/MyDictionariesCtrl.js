(function () {
    'use strict';

    angular
        .module('OnlineDictionary')
        .controller('MyDictionariesCtrl', MyDictionariesCtrl);

    MyDictionariesCtrl.$inject = ['$uibModal'];

    function MyDictionariesCtrl($uibModal) {
        var vm = this;

        vm.createDictionary = createDictionary;

        init();

        function init() {
            console.log('my dictionaries init');
        }

        function createDictionary() {
            var modalInstance = $uibModal.open({
                templateUrl: '/Dictionaries/CreateDictionary',
                //controller: 'ChangeQueenLineCtrl',
                //controllerAs: 'vm',
                /*resolve: {
                    queenParams: {
                        currentLine: vm.queen.line,
                        currentAbbreviation: vm.queen.abbreviation
                    }
                }*/
            });

            modalInstance.result.then(function (updatedQueen) {
                alert('closed');
            });
        }
    }

})();