<template>
    <h1>BUS>>JOURNEY</h1>

    <div>
        <location placeholder="leaving from" @input="setFrom($event)"></location>
    </div>
    <div>
        <location placeholder="going to" @input="setTo($event)"></location>
    </div>

    <div>
        <input id="when-now" type="radio" value="now" :checked="when == 'now'" @input="when = 'now'; getJourney();" />
        <label for="when-now">now</label>

        <input id="when-later" type="radio" value="later" :checked="when == 'later'" @input="setWhenLater()" />
        <label for="when-later">later</label>
    </div>
    <div v-if="when == 'later' && nowInfo">
        <input size="10" type="text" inputmode="numeric" :value="time" @input="time = $event.target.value; getJourney();" >
        <select :value="day" @input="day = $event.target.value; getJourney()">
            <option v-for="(day, index) in days" :value="index" :key="index">{{ day }}</option>
        </select>
    </div>

    <div v-if="journey">
        <table class="journey">
            <tr v-for="(step, index) in journey.steps" :key="index">
                <th>{{ timePart(step.at) }}</th>
                <td>{{ step.description }}</td>
            </tr>
        </table>
    </div>

</template>

<script lang="ts">
    import { Vue } from 'vue-class-component';
    import Api, { ClusterItem, Journey, NowInfo } from '../api';

    export default class JourneyPage extends Vue {

        mounted() {
        }

        fromCluster: ClusterItem | null = null;
        toCluster: ClusterItem | null = null;
        nowInfo: NowInfo | null = null;

        days: string[] = [];

        private makeDays(): string[] {

            const res: string[] = [];

            res.push('today');
            res.push('tomorrow');

            var dayNames = ['sunday', 'monday', 'tuesday', 'wednesday', 'thursday', 'friday', 'saturday'];

            for (let i = 2; i < 7; i++) {
                res.push(dayNames[(this.nowInfo!.dayOfWeek + i) % 7]);
            }
            return res;
        }

        setWhenLater() {
            this.when = 'later';

            if (!this.nowInfo) {
                Api.getNowInfo().then(result => {
                    this.nowInfo = result.data;
                    this.days = this.makeDays();
                    this.time = this.timePart(result.data.nowPlusOneMinute);

                    this.getJourney();
                });
            } else
                this.getJourney();
        }

        time: string | null = null;
        day: number = 0;

        when: 'now' | 'later' = 'now';

        journey: Journey | null = null;

        setFrom(e: ClusterItem) {
            this.fromCluster = e;
            this.getJourney();
        }

        setTo(e: ClusterItem) {
            this.toCluster = e;
            this.getJourney();
        }

        valid(): boolean {
            if (!this.fromCluster)
                return false;

            if (!this.toCluster)
                return false;

            if (this.when == 'later') {
                if (!this.time || !/^(((|0|1)[0-9])|(2[0-3])):[0-5][0-9]$/.test(this.time)) {
                    return false;
                }
            }

            return true;
        }

        getJourney() {

            if (!this.valid())
                return;

            let at: string | null;

            if (this.when == 'now') {
                at = null; // the server will determine the time
            } else {
                at = this.day + "|" + this.time;
            }

            Api.getJourney(this.fromCluster!.id, this.toCluster!.id, at)
                .then(response => {
                    this.journey = response.data;
                });

        }

        timePart(dt: string): string {
            return dt.substr(11, 5);
        }
    }
</script>
