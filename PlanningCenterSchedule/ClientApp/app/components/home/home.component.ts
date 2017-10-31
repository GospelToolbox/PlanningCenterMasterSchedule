import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

import randomcolor from 'randomcolor';
import tinycolor from 'tinycolor2';
import moment from 'moment';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent {
    schedule: any;
    filterName: string;
    loadingSchedule: boolean;

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {

        this.loadSchedule();
    }
    
    public loadSchedule() {
        this.http.get(this.baseUrl + 'api/Schedule/GetSchedule').subscribe(result => {
            this.schedule = result.json();
            console.log('Schedule, ', this.schedule);
        }, error => console.error(error));
    }

    public refreshSchedule() {
        this.loadingSchedule = true;

        this.http.post(this.baseUrl + 'api/Schedule/RefreshSchedule', null).subscribe(() => this.loadSchedule(), error => console.error(error), () => this.loadingSchedule = false);
    }

    public getStatusClass(person: any) {
        if (person.status === 'C' || person.status === 'Confirmed') {
            return 'label-success';
        } else if (person.status === 'D' || person.status === 'Declined') {
            return 'label-danger';
        } else {
            return 'label-default';
        }
    }

    /**
     * Converts an input date (yyyy-MM-dd) to pretty print
     * @param dateStr
     */
    public getDisplayDate(dateStr: string) {
        let date = moment(dateStr);
        return date.format('MMM D');
    }

    public getCellStyle(serviceTypeId: string, teamId?: string, position?: string) {
        return {
            //'background-color': this.getBackgroundColor(serviceTypeId, teamId, position)
        };
    }

    public getBackgroundColor(serviceTypeId: string, teamId?: string, position?: string) {
        let color = randomcolor({
            seed: this.schedule.serviceTypes[serviceTypeId],
            hue: 'blue'
        });

        if (teamId != null) {
            let colorMod = tinycolor(color);
            colorMod.lighten(10 + this.random(this.hash(teamId)) * 20);
            color = colorMod.toHexString();
        }

        if (position != null) {
            console.log(position);
            let colorMod = tinycolor(color);
            colorMod.lighten(this.random(this.hash(position)) * 20);
            color = colorMod.toHexString();
        }

        return color;
    }

    private random(seed: any) {
        var x = Math.sin(seed++) * 10000;
        return x - Math.floor(x);
    }

    private hash(str: string) {
        var hash = 0, i, chr;
        if (str.length === 0) return hash;
        for (i = 0; i < str.length; i++) {
            chr = str.charCodeAt(i);
            hash = ((hash << 5) - hash) + chr;
            hash |= 0; // Convert to 32bit integer
        }
        return hash;
    }

    public getServiceTypes() {
        return Object.keys(this.schedule.serviceTypes).filter(key => {
            return this.schedule.serviceTypeTeams[key] != null
                && this.schedule.serviceTypeTeams[key].length > 0;
        }).map(key => {
            return {
                key,
                value: this.schedule.serviceTypes[key]
            };
        });
    }

    public getServiceTypeRowSpan(serviceTypeId: string) {
        let rowSpan = 0;
        for (let teamId of this.schedule.serviceTypeTeams[serviceTypeId]) {
            rowSpan += this.getTeamRowSpan(teamId);

        }
        return rowSpan;
    }

    public getTeamRowSpan(teamId: string) {
        if (this.schedule.teamPositions[teamId] == null) {
            return 1;
        }

        let rowSpan = 0;
        for (let position of this.schedule.teamPositions[teamId]) {
            rowSpan++;
        }
        return rowSpan;
    }

    public getPeople(serviceTypeId: string, teamId: string, position: string, date: string) {
        if (this.schedule.records[serviceTypeId] == null
            || this.schedule.records[serviceTypeId][teamId] == null
            || this.schedule.records[serviceTypeId][teamId][position] == null) {
            return [];
        }

        let people: string[] = this.schedule.records[serviceTypeId][teamId][position][date] || [];

        return people.filter(name => this.filterName == null || name.toLowerCase().indexOf(this.filterName.toLowerCase()) > -1);
    }
}
