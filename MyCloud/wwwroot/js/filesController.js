﻿(function () {
    'use strict';

    angular
        .module('appPanel')
        .controller('filesController', filesController)
        .directive('ngDownloadPreview',
        [
            '$window', '$timeout', function ($window, $timeout) {
                return {
                    restrict: 'A',
                    template: '<canvas/>',
                    link: function (scope, element, attributes) {

                        var params = scope.$eval(attributes.ngDownloadPreview);

                        var canvas = element.find('canvas');

                        var img = new Image();

                        img.src = "data:image/png;base64,".concat(params.base64Code);

                        img.onload = function () {
                            canvas.attr({ width: 150, height: 100 });
                            var ctx = canvas[0].getContext('2d');
                            ctx.drawImage(this, 0, 0, 150, 100);
                        }

                        $timeout(function () {
                            canvas.on("click", function () {
                                $("#js-modal").css('display', 'block');
                                $("#js-img-modal")[0].src = img.src;
                            });
                        });
                    }
                }
            }
        ])
        .directive('selectPreview',
        [

            '$timeout', function ($timeout) {
            return {
                restrict: 'E',
                templateUrl: "/views/selectPreview.html",
                link: function (scope, element, attributes) {

                    $timeout(function () {

                        $(element).on('click',
                            function () {
                                $(this).children().toggleClass("circle-base-animation-checked");
                            });
                    });
                }
            }
        }]);

    function filesController($http, $scope, $stateParams, $state) {

        $scope.params = $stateParams; 

        var base64UserFiles = this;

        base64UserFiles.codes = [];
        base64UserFiles.folders = [];
        base64UserFiles.selectedIndexes = [];
        base64UserFiles.isGettingPreviews = true;
        $scope.selected = { value: 0 };
        
        $http({

            url: '/api/files/getJsonFiles',
            method: "GET",
            params: { folder: $scope.params.folder }

        }).then(function (response) {

            if (!response.data) {
                base64UserFiles.isGettingPreviews = false;
                return;
            }

            angular.forEach(response.data,
                function (key, value) {
                    base64UserFiles.codes.push(key);
                    base64UserFiles.isGettingPreviews = false;
                });
        });

        $scope.nextPicture = function (index) {
            if (index >= 0 && index < base64UserFiles.codes.length) {
                $("#js-img-modal")[0].src = "data:image/png;base64,".concat(base64UserFiles.codes[index]);
            } else {
                $scope.selected = { value: 0 };
            }
        } 

        $scope.deleteFile = function(index) {
            $http({

                url: '/api/files/deleteFile',
                method: "POST",
                data: {
                    base64Code: base64UserFiles.codes[index],
                    folder: $scope.params.folder
                }

            }).then(function () {

                $("#js-modal").css('display', 'none');
                $state.go($state.current, {}, { reload: true });
            });
        }

        $scope.deleteSelectedFiles = function (selectedFiles) {

            base64UserFiles.isGettingPreviews = true; 

            angular.forEach(selectedFiles, function (key) {

                $http({
                    url: '/api/files/deleteFile',
                    method: "POST",
                    data: {
                        base64Code: base64UserFiles.codes[key],
                        folder: $scope.params.folder
                    }

                });
            });

            base64UserFiles.isGettingPreviews = false;
            $state.go($state.current, {}, { reload: true });
        }

        $scope.moveSelectedFiles = function (selectedFiles, selectedFolder) {

            base64UserFiles.isGettingPreviews = true;
            var codesOfFilesToMove = [];

            angular.forEach(selectedFiles, function (key) {
                codesOfFilesToMove.push(base64UserFiles.codes[key]);
            });

            $http({
                url: '/api/files/moveSelectedFiles',
                method: "POST",
                data: {
                    codes: codesOfFilesToMove,
                    currentFolder: $scope.params.folder,
                    newFolder: selectedFolder
                }

            });

            base64UserFiles.isGettingPreviews = false;
            $state.go($state.current, {}, { reload: true });
        }


        $scope.fileSelected = function(index) {

            if (!base64UserFiles.selectedIndexes.includes(index))
            {
                base64UserFiles.selectedIndexes.push(index);
            } else {
                base64UserFiles.selectedIndexes = base64UserFiles.selectedIndexes.filter(function (item) {
                    return item !== index;
                });
            }
        } 

        $scope.clearSelections = function() {
            base64UserFiles.selectedIndexes = [];
            $('.circle-base').removeClass('circle-base-animation-checked');
        }

        $scope.folderRemovalClick = function () {

            $.confirm({
                title: 'Usunięcie folderu',
                content: '' +
                '<form action="" class="formName">' +
                '<div class="form-group">' +
                '<label>Podaj nazwę folderu aby potwierdzić usunięcie</label>' +
                '<input type="text" placeholder="Nazwa folderu..." class="name form-control" required />' +
                '</div>' +
                '</form>',
                buttons: {
                    formSubmit: {
                        text: 'Zatwierdź',
                        btnClass: 'btn-danger',
                        action: function () {
                            var name = this.$content.find('.name').val();
                            if (!name) {
                                $.alert('Nie wprowadzono nazwy folderu');
                                return false;
                            }

                            if (name === $scope.params.folder) {
                                $.confirm({
                                    title: 'Czy na pewno chcesz usunąć folder:',
                                    content: '<strong>' + name + ' ?' + '</strong>',
                                    buttons: {
                                        usuń: {
                                            btnClass: 'btn-danger',
                                            action: function () {

                                                $http({
                                                    url: '/api/folders/deleteFolder',
                                                    method: "POST",
                                                    data: { name: $scope.params.folder }

                                                }).then(function() {
                                                    $state.go('folders', {}, { reload: true });
                                                });
                                            }
                                        },
                                        anuluj: function () {
                                            return true;
                                        }
                                    }
                                });
                            } else {
                                $.alert('Wprowadzona nazwa nie zgadza się z nazwą aktualnie otwartego folderu.');
                            }
                        }
                    },
                    cofnij: function () {
                        return true;
                    }
                }
            });
        }

        $scope.shareFile = function(index) {
            $http({
                url: '/api/share/shareFile',
                method: "POST",
                data: {
                    base64Code: base64UserFiles.codes[index],
                    folder: $scope.params.folder
                }

            }).then(function (response) {
                $("#js-modal").css('display', 'block');

                $.confirm({
                    icon: 'fa fa-link',
                    title: 'Udostępnianie',
                    content: '' +
                    '<form class="formName">' +
                    '<div class="form-group">' +
                    '<label> Skopiuj ten link i wyślij osobie której chcesz udostępnić ten obraz </label>' +
                    '<textarea class="share-link-textarea">' + response.data + '</textarea>' +
                    '</div>' +
                    '</form>',
                    buttons: {
                        OK: function () {
                            return true;
                        }
                    }
                });
            });
        }

        $scope.shareFolder = function() {
            $http({
                url: '/api/share/shareFolder',
                method: "POST",
                data: {
                    name: $scope.params.folder
                }

            }).then(function (response) {
                $.confirm({
                    icon: 'fa fa-link',
                    title: 'Udostępnianie',
                    content: '' +
                    '<form class="formName">' +
                    '<div class="form-group">' +
                    '<label> Skopiuj ten link i wyślij osobie której chcesz udostępnić ten folder </label>' +
                    '<textarea class="share-link-textarea">' + response.data + '</textarea>' +
                    '</div>' +
                    '</form>',
                    buttons: {
                        OK: function () {
                            return true;
                        }
                    }
                });
            });
        }

        $scope.getUserFolders = function () {
            if (base64UserFiles.folders.length === 0)
            {
                $http({

                    url: '/api/folders/getUserFolders',
                    method: "GET"

                }).then(function (response) {

                    if (!response.data) {
                        base64UserFiles.isGettingPreviews = false;
                        return;
                    }

                    angular.forEach(response.data,
                        function (key, value) {
                            base64UserFiles.folders.push(key);
                            base64UserFiles.isGettingPreviews = false;
                        });
                });
            }
        }
    }
})();
