<template>
    <h1 v-if="service">
        <vue-title :title="service.routeName + ': ' + service.description"></vue-title>
        Route {{ service.routeName }}: {{ service.description }}
    </h1>

    <div v-if="serviceStops">
        <ul>
            <li v-for="(ss, index) in serviceStops" v-bind:class="{ highlight: highlight(ss) }" :key="index">
                <router-link :to="{ name: 'Cluster', params: { id: ss.cluster.id }, query: { serviceId: $route.params.id, stopId: ss.stopId }}">{{ timePart(ss.time) }} {{ss.cluster.name}}</router-link>
            </li>
        </ul>
    </div>
</template>

<script lang="ts">
    import { Vue } from 'vue-class-component';
    import Api, { ServiceItem, ServiceStopItem } from '@/api';

    export default class ServiceViewPage extends Vue {

        service: ServiceItem | null = null;
        serviceStops: ServiceStopItem[] | null = null;

        highlight(serviceStop: ServiceStopItem) {
            return this.$route.query && this.$route.query.stopId === serviceStop.stopId.toString();
        }

        mounted() {
            const id = this.$route.params.id as string;

            Api.getService(id)
                .then(response => {
                    this.service = response.data;
                });

            Api.getServiceStops(id)
                .then(response => {
                    this.serviceStops = response.data;
                });
        }

        timePart(dt: string): string {
            return dt.substr(0, 5);
        }
    }
</script>