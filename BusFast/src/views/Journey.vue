<template>
    <h1>BUS>>JOURNEY</h1>

    <location placeholder="leaving from" @onchange="changedFrom(value)"></location>
    >>
    <location placeholder="going to" @onchange="changedTo(value)"></location>

</template>

<script lang="ts">
    import { Vue } from 'vue-class-component';
    import Api, { ClusterItem } from '@/api';
    
    export default class Journey extends Vue {

        fromStopQ: string = '';
        fromStop: ClusterItem | null = null;

        fromStops: ClusterItem[] | null = null;

        changedFromQ(q: string) {
            this.fromStopQ = q;
            this.getStops();
        }

        mounted() {
        }

        getStops() {
            if (this.fromStopQ) {
                const q = this.fromStopQ;
                Api.getStops(this.fromStopQ)
                    .then(response => {
                        if (q === this.fromStopQ)
                            this.fromStops = response.data;
                    });

            } else {
                this.fromStops = [];
            }
        }

        selectFromStop(stop: ClusterItem) {
            this.fromStop = stop;
            this.fromStopQ = stop.name;
            this.fromStops = null;
        }
    }
</script>
