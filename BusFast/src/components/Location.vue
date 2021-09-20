<template>
    <div class="autocomplete">
        <input type="text" v-model="stopQ" @input="changedQ($event.target.value)" :placeholder="placeholder">
        <ul class="suggestions">
            <li @click="selectStop(stop)" v-for="stop in stops" :key="stop.id">{{ stop.name }}</li>
        </ul>
    </div>
</template>

<script lang="ts">
    import { Options, Vue } from 'vue-class-component';
    import Api, { ClusterItem } from '@/api';

    @Options({
        props: {
            placeholder: String
        }
    })
    // Define the component in class-style
    export default class Location extends Vue {

        placeholder!: string

        stopQ: string = '';
        stop: ClusterItem | null = null;

        stops: ClusterItem[] | null = null;

        changedQ(q: string) {
            this.stopQ = q;
            this.getStops();
        }

        mounted() {
        }

        getStops() {
            if (this.stopQ) {
                const q = this.stopQ;
                Api.getStops(this.stopQ)
                    .then(response => {
                        if (q === this.stopQ)
                            this.stops = response.data;
                    });

            } else {
                this.stops = [];
            }
        }

        selectStop(stop: ClusterItem) {

            this.$emit('input', stop);
            this.stop = stop;
            this.stopQ = stop.name;
            this.stops = null;
        }
    }
</script>