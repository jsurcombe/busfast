<template>
    <h1>BUS>>JOURNEY</h1>

    <div>
        <location placeholder="leaving from" @input="setFrom($event)"></location>
    </div>
    <div>
        <location placeholder="going to" @input="setTo($event)"></location>
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
    import Api, { ClusterItem, Journey } from '../api';

    export default class JourneyPage extends Vue {

        mounted() {
        }

        fromCluster: ClusterItem | null = null;
        toCluster: ClusterItem | null = null;

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
