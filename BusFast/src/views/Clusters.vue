<template>
    <div>
        <h1>BUS>>STOPS</h1>

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

    export default class ClusterList extends Vue {
        stops: ClusterItem[] | null = null;

        mounted() {
            Api.getStops(null)
                .then(response => {
                    this.stops = response.data;
                    document.dispatchEvent(new Event("prerender-page-ready"));
                });
        }
    }
</script>
