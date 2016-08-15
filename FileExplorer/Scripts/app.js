
// Define the `phonecatApp` module
var testApp = angular.module('testApp', []);

// Define the `PhoneListController` controller on the `phonecatApp` module
testApp.controller('TestController', function TestController($scope, $http, ApiCall) {
    ApiCall.GetApiCall("Home", "GetDirectory", null, (result) =>
    {
        $scope.directories = result;
        $scope.currentDir = result.Root;
        $scope.countSizes(result.Root);
    });

    $scope.getSubDirectories = function (dir)
    {
        ApiCall.GetApiCall("Home", "GetDirectories", { directory: dir }, (result) =>
        {
            $scope.directories = result;
            $scope.currentDir = result.Current;
            $scope.countSizes(result.Current);
        })
    };

    $scope.getParentDirectory = function (dir) {
        ApiCall.GetApiCall("Home", "GetParentDirectory", { directory: dir }, (result) => {
            $scope.directories = result;
            $scope.currentDir = result.Current;
            $scope.countSizes(result.Current);
        })
    };

    $scope.countSizes = function (dir) {
        ApiCall.GetApiCall("Home", "CountSizes", { directory: dir }, (result) => {
            $scope.Smallest = result.Smallest;
            $scope.Middle = result.Middle;
            $scope.Biggest = result.Biggest;
        })
    };

});

testApp.service("ApiCall", ['$http', function ($http) {

    this.GetApiCall = function (controllerName, methodName, param, callBack) {
        $http.get('/' + controllerName + '/' + methodName, {params : param}).success(function (data, status) {
            result = data;
            console.log(result);
            callBack(data);
        }).error(() => { alert("Error"); });  
    }
    

}]);