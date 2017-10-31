import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'mapToIterable'
})
export class MapToIterable implements PipeTransform{
    transform(dict: any): Array<{ key: string, value: any }> {
        var a = [];
        for (var key in dict) {
            if (dict.hasOwnProperty(key)) {
                a.push({ key: key, value: dict[key] });
            }
        }
        return a;
    }
}