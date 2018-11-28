import { TestBed } from '@angular/core/testing';
import { WeatherService } from './weather-service.service';
describe('WeatherService', function () {
    beforeEach(function () { return TestBed.configureTestingModule({}); });
    it('should be created', function () {
        var service = TestBed.get(WeatherService);
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=weather-service.service.spec.js.map