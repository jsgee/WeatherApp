var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
var AppComponent = /** @class */ (function () {
    //public currentJogging: any;
    function AppComponent(weatherService) {
        var _this = this;
        this.weatherService = weatherService;
        this.weatherService.get().subscribe(function (data) { return _this.weatherData = data; });
    }
    var _a;
    AppComponent = __decorate([
        Component({
            selector: 'app-root',
            templateUrl: './app.component.html',
            styleUrls: ['./app.component.css']
        }),
        __metadata("design:paramtypes", [typeof (_a = typeof WeaherService !== "undefined" && WeaherService) === "function" ? _a : Object])
    ], AppComponent);
    return AppComponent;
}());
export { AppComponent };
title = 'Welcome to Angular';
subtitle = '.NET Core + Angular CLI v7 + Bootstrap & FontAwesome + Swagger Template';
//# sourceMappingURL=app.component.js.map