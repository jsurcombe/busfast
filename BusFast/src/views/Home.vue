<template>
    <div>
        <h1>BUS>>FAST</h1>
        <input v-model="stopQ" @input="changedQ($event.target.value)" placeholder="find a stop">
        <ul>
            <li v-for="stop in stops" :key="stop.id">
                <router-link :to="{ name: 'Cluster', params: { id: stop.id }}">{{ stop.name }}</router-link>
            </li>
        </ul>
    </div>
</template>

<script lang="ts">
    import { Vue } from 'vue-class-component';
    import Api, { ClusterItem } from '@/api';

    export default class Home extends Vue {

        stopQ: string = '';

        stops: ClusterItem[] | null = null;

        changedQ(q: string) {
            this.stopQ = q;
            this.$router.push(`/?q=${this.stopQ}`);
            this.getStops();
        }

        mounted() {
            this.stopQ = this.$route.query.q as string;
            this.getStops();
        }

        getStops() {
            if (this.stopQ) {
                Api.getStops(this.stopQ)
                    .then(response => {
                        this.stops = response.data;
                    });
            } else
                this.stops = [];
        }
    }
</script>
