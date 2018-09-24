var app = angular.module("FareCalculator", []);

app.controller('FareController', ['$scope', '$http', '$filter', function ($scope, $http, $filter) {
    let timestamp = new Date();
    $scope.data = {
        date: timestamp,
        time: null,
        MinutesAboveSixMph: 0,
        DistanceUnitsBelowSixMph: 0
    }
    $scope.totalFare = 0;
    $scope.calc = function (data) {

        let formattedDistance = (data.DistanceUnitsBelowSixMph * 100) / 20;
        console.log(formattedDistance);
        $http.post('/Home/CalculateFare', {
            date: data.date,
            time: data.time,
            MinutesAboveSixMph: data.MinutesAboveSixMph,
            DistanceUnitsBelowSixMph: formattedDistance
        })
        .then(function successCallback(response) {
            $scope.totalFare = response.data.totalFare;
        }, function errorCallback(response) {
            console.log(response);
        });
    };

    $scope.initialTime = new Date();
}])

app.directive('multipleOfFifth', function () {
    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ctrl) {
            ctrl.$validators.multipleOfFifth = function (modelValue, viewValue) {
                if (ctrl.$isEmpty(modelValue)) {
                    return true;
                }
                if (Math.floor(viewValue * 100)%20 == 0) {
                    // it is valid
                    console.log('Distance is valid: ', viewValue)
                    return true;
                }

                console.log('Distance isnt valid: ', viewValue)

                // it is invalid
                return false;
            };
        }
    };
});

app.directive('notNegative', function () {
    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ctrl) {
            ctrl.$validators.notNegative = function (modelValue, viewValue) {
                if (ctrl.$isEmpty(modelValue)) {
                    return true;
                }
                if (viewValue < 0) {
                    // it is invalid
                    return false;
                }
                console.log("here2")
                // it is valid
                return true;
            };
        }
    };
});

app.directive('calculator', function () {
    return {
        templateUrl: 'Views/Home/Calculator.html',
    };
});