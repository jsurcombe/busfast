<template>
    <h1>BUS>>JOURNEY</h1>

    <div>
        <location placeholder="leaving from" @input="setFrom($event)"></location>
    </div>
    <div>
        <location placeholder="going to" @input="setTo($event)"></location>
    </div>
    <!--
    <div>
        <input id="when-now" type="radio" value="now" v-model="when" />
        <label for="when-now">now</label>

        <input id="when-later" type="radio" value="later" v-model="when" />
        <label for="when-later">later</label>
    </div>
    <div v-if="when == 'later'">
        <input type="text" v-model="time" @input="setTime($event.target.value)">
        <input type="text" v-model="day">
    </div>
    -->
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
    import Api, { ClusterItem, Journey } from '../api';

    export default class JourneyPage extends Vue {

        mounted() {
        }

        fromCluster: ClusterItem | null = null;
        toCluster: ClusterItem | null = null;

        get time(): string {
            if (this._time == null)
                this._time = this.timePart((new Date()).toISOString());

            return this._time;
        }

        set time(value: string) {
            this._time = value;
        }

        _time: string | null = null;
        day: string = 'today';
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

        getJourney() {
            if (!this.fromCluster || !this.toCluster)
                return;

            Api.getJourney(this.fromCluster.id, this.toCluster.id)
                .then(response => {
                    this.journey = response.data;
                });

        }

        timePart(dt: string): string {
            return dt.substr(11, 5);
        }
    }
</script>
